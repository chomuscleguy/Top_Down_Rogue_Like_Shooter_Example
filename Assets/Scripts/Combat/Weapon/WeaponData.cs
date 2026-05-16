using System.Collections.Generic;
using UnityEngine;
public enum WeaponCategory
{
    Melee,
    Ranged,
    Magic,
    Summon
}

public abstract class WeaponData : ItemData
{
    public override ItemType Type => ItemType.Weapon;

    [Header("Weapon")]
    public Weapon weaponPrefab;

    [Header("Category")]
    public WeaponCategory category;

    [Header("Levels")]
    public List<WeaponLevelData> levels = new();

    public int MaxLevel => levels.Count;

    public override CharacterStats GetStats(int level)
    {
        return default;
    }

    public WeaponLevelData GetLevelData(int level)
    {
        if (level <= 0 || level > levels.Count)
            return null;

        return levels[level - 1];
    }
}