using System;

[Serializable]
public struct CharacterStats
{
    public SurvivalStats survival;
    public MovementStats movement;
    public CombatStats combat;
    public ProjectileStats projectile;
    public UtilityStats utility;

    public static CharacterStats operator +(CharacterStats a, CharacterStats b)
    {
        return new CharacterStats
        {
            survival = a.survival + b.survival,
            movement = a.movement + b.movement,
            combat = a.combat + b.combat,
            projectile = a.projectile + b.projectile,
            utility = a.utility + b.utility
        };
    }
}
