using System;

[Serializable]
public struct UtilityStats
{
    public float pickupRadius;
    public float luck;
    public float expGain;
    public float goldGain;

    public static UtilityStats operator +(UtilityStats a, UtilityStats b)
    {
        return new UtilityStats
        {
            pickupRadius = a.pickupRadius + b.pickupRadius,
            luck = a.luck + b.luck,
            expGain = a.expGain + b.expGain,
            goldGain = a.goldGain + b.goldGain
        };
    }
}
