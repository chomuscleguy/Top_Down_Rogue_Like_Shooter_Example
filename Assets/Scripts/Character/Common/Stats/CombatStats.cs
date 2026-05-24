using System;

[Serializable]
public struct CombatStats
{
    public float damage;

    public float cooldownMultiplier;

    public float critChance;
    public float critDamageMultiplier;

    public float knockbackForce;

    public float range;

    public static CombatStats operator +(CombatStats a, CombatStats b)
    {
        return new CombatStats
        {
            damage = a.damage + b.damage,

            cooldownMultiplier = a.cooldownMultiplier * b.cooldownMultiplier,

            critChance = a.critChance + b.critChance,
            critDamageMultiplier = a.critDamageMultiplier + b.critDamageMultiplier,

            knockbackForce = a.knockbackForce + b.knockbackForce,

            range = a.range + b.range,
        };
    }
}

