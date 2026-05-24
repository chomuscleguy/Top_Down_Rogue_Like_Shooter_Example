using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "SO/Upgrade")]
public class UpgradeData : BaseData
{
    [Header("Visual")]
    public Sprite icon;

    [Header("Levels")]
    public List<UpgradeLevelData> levels = new();

    public int MaxLevel => levels.Count;

    public UpgradeLevelData GetLevel(int level)
    {
        if (level <= 0 || level > levels.Count)
            return default;

        return levels[level - 1];
    }

    public CharacterStats GetStats(int level)
    {
        UpgradeLevelData data = GetLevel(level);

        return data.stats;
    }

    public int GetCost(int level)
    {
        UpgradeLevelData data = GetLevel(level);

        return data.cost;
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        if (ID < 5000 || ID >= 6000)
        {
            Debug.LogError($"{name}: Upgrade ID는 5000~5999 범위를 사용해야 합니다.", this);
        }
    }
#endif
}