using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public partial class PlayerProgress
{
    public event Action OnChanged;
    public event Action<int> OnGoldChanged;

    [Header("Currency")]
    [SerializeField]
    private int gold;

    [Header("Upgrade")]
    [SerializeField]
    private List<UpgradeLevel> serializedLevels = new();

    private readonly Dictionary<UpgradeData, int> runtimeLevels = new();

    public int Gold => gold;

    public void OnAfterLoad()
    {
        runtimeLevels.Clear();

        foreach (var s in serializedLevels)
        {
            if (s.data == null)
                continue;

            runtimeLevels[s.data] = s.level;
        }
    }

    public void OnBeforeSave()
    {
        serializedLevels.Clear();

        foreach (var kv in runtimeLevels)
        {
            if (kv.Key == null)
                continue;

            serializedLevels.Add(new UpgradeLevel
            {
                data = kv.Key,
                level = kv.Value
            });
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;

        OnGoldChanged?.Invoke(gold);
        OnChanged?.Invoke();
    }

    public bool SpendGold(int amount)
    {
        if (gold < amount)
            return false;

        gold -= amount;

        OnGoldChanged?.Invoke(gold);
        OnChanged?.Invoke();

        return true;
    }

    public int GetLevel(UpgradeData data)
    {
        return runtimeLevels.TryGetValue(data, out var lv) ? lv : 0;
    }

    public bool AddLevel(UpgradeData data)
    {
        if (data == null)
            return false;

        if (!runtimeLevels.ContainsKey(data))
            runtimeLevels[data] = 0;

        int currentLevel = runtimeLevels[data];

        if (currentLevel >= data.levels.Count)
            return false;

        runtimeLevels[data]++;

        OnChanged?.Invoke();

        return true;
    }

    public int CalculateTotalSpent(IReadOnlyList<UpgradeData> upgrades)
    {
        int total = 0;

        foreach (var upgrade in upgrades)
        {
            int level = GetLevel(upgrade);

            for (int i = 0; i < level; i++)
            {
                total += upgrade.levels[i].cost;
            }
        }

        return total;
    }

    public void ResetAll()
    {
        runtimeLevels.Clear();
        serializedLevels.Clear();

        OnGoldChanged?.Invoke(gold);
        OnChanged?.Invoke();
    }

    public IEnumerable<(UpgradeData upgrade, int level)> GetAllUpgrades()
    {
        foreach (var pair in runtimeLevels)
        {
            yield return (pair.Key, pair.Value);
        }
    }
}