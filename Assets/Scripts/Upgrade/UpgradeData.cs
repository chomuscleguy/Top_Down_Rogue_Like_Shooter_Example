using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "SO/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public Sprite icon;
    public string displayName;

    public List<UpgradeLevelData> levels;

    public int MaxLevel => levels.Count;

    public UpgradeLevelData GetLevel(int level)
    {
        if (level <= 0 || level > levels.Count)
            return default;

        return levels[level - 1];
    }
    public CharacterStats GetStats(int level)
    {
        if (level <= 0 || level > levels.Count)
            return default;

        return levels[level - 1].stats;
    }

    public int GetCost(int level)
    {
        if (level < 0 || level >= levels.Count)
            return 0;

        return levels[level].cost;
    }
}
