using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Projectile,
    Melee,
    Orbit
}

public abstract class WeaponData : ItemData
{
    [Header("Type")]
    public WeaponType type;

    [Header("Projectile")]
    public Projectile projectilePrefab;

    [Header("Behaviour")]
    public WeaponBehaviour behaviour;

    [Header("Levels")]
    public List<WeaponLevelData> levels = new();

    public int MaxLevel => levels.Count;

    public WeaponLevelData GetLevelData(int level)
    {
        if (level <= 0 || level > levels.Count)
            return null;

        return levels[level - 1];
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        if (ID < 2000 || ID >= 3000)
        {
            Debug.LogError($"{name}: Weapon ID는 2000~2999 범위");
        }
    }
#endif
}