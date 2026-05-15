using System;

[Serializable]
public struct CombatStats
{
    public float damage;
    public float damageMultiplier;

    public float attackInterval;
    public float cooldownReduction;

    public float critChance;
    public float critDamage;

    public static CombatStats operator +(CombatStats a, CombatStats b)
    {
        return new CombatStats
        {
            damage = a.damage + b.damage,
            damageMultiplier = a.damageMultiplier + b.damageMultiplier,

            attackInterval = a.attackInterval + b.attackInterval,
            cooldownReduction = a.cooldownReduction + b.cooldownReduction,

            critChance = a.critChance + b.critChance,
            critDamage = a.critDamage + b.critDamage
        };
    }
}

