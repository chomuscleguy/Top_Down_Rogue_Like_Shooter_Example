using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerProgress
{
    [Serializable]
    public struct UpgradeLevel
    {
        public UpgradeData data;
        public int level;
    }

    public event Action OnChanged;

    [SerializeField]
    private List<UpgradeLevel> serializedLevels = new();

    private Dictionary<UpgradeData, int> runtimeLevels = new();


    // =========================
    // 로드
    // =========================
    public void OnAfterLoad()
    {
        runtimeLevels.Clear();

        foreach (var s in serializedLevels)
        {
            if (s.data != null)
                runtimeLevels[s.data] = s.level;
        }
    }

    // =========================
    // 저장
    // =========================
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


    public int GetLevel(UpgradeData data)
    {
        return runtimeLevels.TryGetValue(data, out var lv) ? lv : 0;
    }

    public void AddLevel(UpgradeData data)
    {
        if (!runtimeLevels.ContainsKey(data))
            runtimeLevels[data] = 0;

        runtimeLevels[data]++;
        OnChanged?.Invoke();
    }

    public int CalculateTotalSpent(List<UpgradeData> upgrades)
    {
        int total = 0;

        foreach (var upgrade in upgrades)
        {
            int level = GetLevel(upgrade);

            for (int i = 0; i < level; i++)
            {
                total += upgrade.levels[i].cost; // 🔥 변경
            }
        }

        return total;
    }

    // =========================
    // 초기화
    // =========================
    public void ResetAll()
    {
        runtimeLevels.Clear();
        serializedLevels.Clear();

        OnChanged?.Invoke();
    }
}