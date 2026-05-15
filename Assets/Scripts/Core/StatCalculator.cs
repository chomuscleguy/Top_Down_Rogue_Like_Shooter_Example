using UnityEngine;

public static class StatCalculator
{
    public static CharacterStats Calculate(CharacterStats raw)
    {
        CharacterStats final = raw;

        final.combat.attackInterval = raw.combat.attackInterval * (1f - raw.combat.cooldownReduction);

        final.movement.moveSpeed = raw.movement.moveSpeed * (1f + raw.movement.speedMultiplier);

        final.combat.critChance = Mathf.Clamp(raw.combat.critChance, 0f, 1f);

        final.combat.attackInterval = Mathf.Max(0.05f, final.combat.attackInterval);

        final.survival.armor = raw.survival.armor / (1f + raw.survival.armor * 0.1f);

        return final;
    }
}