using UnityEngine;

public enum TargetingMode
{
    Closest,
    Random,
}

[CreateAssetMenu(menuName = "Weapon/Projectile Weapon")]
public class ProjectileWeaponData : WeaponData
{
    public override CharacterStats GetStats(int level)
    {
        return default;
    }
}
