using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Buff")]
public class BuffData : ItemData
{
    public override ItemType Type => ItemType.Buff;
    public string BuffId;
    public string BuffName;

    public List<BuffLevelData> levels;

    public int MaxLevel => levels.Count;

    public override CharacterStats GetStats(int level)
    {
        CharacterStats result = default;

        for (int i = 0; i < level; i++)
            result += levels[i].stats;

        return result;
    }
}