using System;
using UnityEngine;

[Serializable]
public struct WeaponStats
{
    [Header("Damage")]
    public float damage;
    public float damageMultiplier;

    [Header("Attack")]
    public float attackInterval;
    public float cooldownReduction;

    [Header("Critical")]
    public float critChance;
    public float critDamage;

    [Header("Projectile")]
    public int projectileCount;
    public float projectileSpeed;
    public float projectileSize;
    public int pierce;
    public float spreadAngle;
    public float knockbackForce;
    public float range;

    public float FinalDamage => damage * damageMultiplier;

    public float FinalInterval => attackInterval * (1f - cooldownReduction);

    public static WeaponStats operator +(WeaponStats a, WeaponStats b)
    {
        return new WeaponStats
        {
            damage = a.damage + b.damage,
            damageMultiplier = a.damageMultiplier + b.damageMultiplier,

            attackInterval = a.attackInterval + b.attackInterval,
            cooldownReduction = a.cooldownReduction + b.cooldownReduction,

            critChance = a.critChance + b.critChance,
            critDamage = a.critDamage + b.critDamage,

            projectileCount = a.projectileCount + b.projectileCount,
            projectileSpeed = a.projectileSpeed + b.projectileSpeed,
            projectileSize = a.projectileSize + b.projectileSize,

            pierce = a.pierce + b.pierce,
            spreadAngle = a.spreadAngle + b.spreadAngle,
            knockbackForce = a.knockbackForce + b.knockbackForce,
            range = a.range + b.range,
        };
    }
}