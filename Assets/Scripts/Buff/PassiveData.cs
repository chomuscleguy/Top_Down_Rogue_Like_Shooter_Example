using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Passive")]
public class PassiveData : ItemData
{
    [Header("Levels")]
    public List<PassiveLevelData> levels = new();

    public int MaxLevel => levels.Count;

    public PassiveLevelData GetLevelData(int level)
    {
        if (level <= 0 || level > levels.Count)
            return null;

        return levels[level - 1];
    }

    public override CharacterStats GetStats(int level)
    {
        PassiveLevelData levelData = GetLevelData(level);

        if (levelData == null)
            return default;

        return levelData.stats;
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        if (ID < 1000 || ID >= 2000)
        {
            Debug.LogError($"{name}: Passive ID는 1000~1999 범위를 사용해야 합니다.", this);
        }
    }
#endif
}