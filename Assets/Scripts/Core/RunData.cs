using System;
using System.Collections.Generic;
using UnityEngine;

public class RunData : ITickable, IStatQuery, IHealthProvider, IExperienceProvider
{
    public CharacterData Character { get; private set; }

    public PlayerProgress Progress { get; private set; }

    public ItemContainer Items { get; private set; }

    public CharacterStats RawStats { get; private set; }

    public CharacterStats FinalStats { get; private set; }

    public int Level { get; private set; } = 1;

    public int CurrentXP { get; private set; }

    public int XPToNextLevel { get; private set; } = 10;

    public int KillCount { get; private set; }

    public int Gold { get; private set; }

    public float PlayTime { get; private set; }

    public float CurrentHP => throw new NotImplementedException();

    public float MaxHP => throw new NotImplementedException();

    private List<UpgradeData> upgrades;

    public event Action<CharacterStats> OnStatsChanged;

    public event Action<ItemData> OnItemAdded;

    public event Action<int> OnGoldChanged;

    public event Action<int> OnKillChanged;

    public event Action<int> OnLevelChanged;

    public event Action OnLevelUp;

    public event Action<int, int> OnExpChanged;

    public event Action<float> OnTimeChanged;

    public event Action OnDeath;
    public event Action<float, float> OnHealthChanged;

    public CombatStats GetCombat() => FinalStats.combat;
    public SurvivalStats GetSurvival() => FinalStats.survival;
    public MovementStats GetMovement() => FinalStats.movement;
    public ProjectileStats GetProjectile() => FinalStats.projectile;
    public UtilityStats GetUtility() => FinalStats.utility;

    public void Init(CharacterData character, PlayerProgress progress, List<UpgradeData> upgradeDatabase)
    {
        Character = character;

        Progress = progress;

        upgrades = upgradeDatabase;

        Items = new ItemContainer();

        AddItem(character.baseWeapon);

        RecalculateStats();

        NotifyAll();
    }

    public void Tick(float dt)
    {
        PlayTime += dt;

        OnTimeChanged?.Invoke(PlayTime);
    }

    public void AddKill()
    {
        KillCount++;

        OnKillChanged?.Invoke(KillCount);
    }

    public void AddGold(int amount)
    {
        Gold += amount;

        OnGoldChanged?.Invoke(Gold);
    }

    public void AddXP(int amount)
    {
        CurrentXP += amount;

        while (CurrentXP >= XPToNextLevel)
        {
            LevelUp();
        }

        OnExpChanged?.Invoke(CurrentXP, XPToNextLevel);
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }

    private void LevelUp()
    {
        CurrentXP -= XPToNextLevel;

        Level++;

        XPToNextLevel = Mathf.RoundToInt(XPToNextLevel * 1.5f);

        OnLevelUp?.Invoke();

        OnLevelChanged?.Invoke(Level);
    }

    public void AddItem(ItemData item)
    {
        Items.AddItem(item);

        RecalculateStats();

        OnItemAdded?.Invoke(item);
    }

    public void RecalculateStats()
    {
        CharacterStats result = Character.stats;

        ApplyUpgradeStats(ref result);

        ApplyItemStats(ref result);

        RawStats = result;

        FinalStats = StatCalculator.Calculate(result);

        OnStatsChanged?.Invoke(FinalStats);
    }

    private void ApplyUpgradeStats(ref CharacterStats result)
    {
        foreach (UpgradeData upgrade in upgrades)
        {
            int level = Progress.GetLevel(upgrade);

            if (level <= 0)
                continue;

            for (int i = 1; i <= level; i++)
            {
                result += upgrade.GetStats(i);
            }
        }
    }

    private void ApplyItemStats(ref CharacterStats result)
    {
        foreach (var (item, level) in Items.GetAllItems())
        {
            if (level <= 0)
                continue;

            result += item.GetStats(level);
        }
    }

    private void NotifyAll()
    {
        OnLevelChanged?.Invoke(Level);

        OnExpChanged?.Invoke(CurrentXP, XPToNextLevel);

        OnGoldChanged?.Invoke(Gold);

        OnKillChanged?.Invoke(KillCount);

        OnTimeChanged?.Invoke(PlayTime);

        OnStatsChanged?.Invoke(FinalStats);
    }
}