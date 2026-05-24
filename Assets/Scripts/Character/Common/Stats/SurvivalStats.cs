using System;

[Serializable]
public struct SurvivalStats
{
    public float maxHP;
    public float hpRegen;
    public float armor;
    public float dodgeChance;

    public static SurvivalStats operator +(SurvivalStats a, SurvivalStats b)
    {
        return new SurvivalStats
        {
            maxHP = a.maxHP + b.maxHP,
            hpRegen = a.hpRegen + b.hpRegen,
            armor = a.armor + b.armor,
            dodgeChance = a.dodgeChance + b.dodgeChance
        };
    }
}
