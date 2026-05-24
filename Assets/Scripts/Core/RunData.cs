using System;
using System.Collections.Generic;
using UnityEngine;

public class RunData : ITickable, IStatQuery, IExperienceProvider
{
    public CharacterData Character { get; private set; }
    public PlayerProgress Progress { get; private set; }
    public ItemContainer Items { get; private set; }

    public CharacterStats MetaStats { get; private set; }
    public CharacterStats FinalStats { get; private set; }

    public int Level { get; private set; } = 1;
    public int CurrentXP { get; private set; }
    public int XPToNextLevel { get; private set; } = 10;

    public int KillCount { get; private set; }
    public int Gold { get; private set; }
    public float PlayTime { get; private set; }

    private WeaponSystem weaponSystem;

    private IReadOnlyList<UpgradeData> upgrades;
    public IReadOnlyList<WeaponRuntime> Weapons=> weaponSystem.Runtimes;

    public event Action<CharacterStats> OnStatsChanged;
    public event Action OnInventoryChanged;
    public event Action<int> OnGoldChanged;
    public event Action<int> OnKillChanged;
    public event Action<int> OnLevelChanged;
    public event Action OnLevelUp;
    public event Action<int, int> OnExpChanged;
    public event Action<float> OnTimeChanged;

    public CombatStats GetCombat() => FinalStats.combat;
    public SurvivalStats GetSurvival() => FinalStats.survival;
    public MovementStats GetMovement() => FinalStats.movement;
    public ProjectileStats GetProjectile() => FinalStats.projectile;
    public UtilityStats GetUtility() => FinalStats.utility;

    public void Init(CharacterData character, PlayerProgress progress, IReadOnlyList<UpgradeData> upgradeDatabase)
    {
        Character = character;
        Progress = progress;
        upgrades = upgradeDatabase;

        CharacterStats baseStats = character.stats;
        ApplyUpgradeStats(ref baseStats);
        MetaStats = baseStats;

        Items = new ItemContainer();

        AddItem(character.baseWeapon);

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
            LevelUp();

        OnExpChanged?.Invoke(CurrentXP, XPToNextLevel);
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
        OnInventoryChanged?.Invoke();
    }

    public void RecalculateStats()
    {
        CharacterStats runtimeStats = MetaStats;

        ApplyPassiveStats(ref runtimeStats);

        FinalStats = StatCalculator.Calculate(runtimeStats);

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

    private void ApplyPassiveStats(ref CharacterStats result)
    {
        foreach (var (item, level) in Items.GetAllItems())
        {
            if (level <= 0)
                continue;

            if (item is not PassiveData passive)
                continue;

            result += passive.GetStats(level);
        }
    }

    public void BindWeaponSystem(WeaponSystem weaponSystem)
    {
        this.weaponSystem = weaponSystem;
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