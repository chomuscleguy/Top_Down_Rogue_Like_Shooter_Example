using UnityEngine;

public static class StatCalculator
{
    public static CharacterStats Calculate(CharacterStats raw)
    {
        CharacterStats final = raw;

        final.combat.cooldownMultiplier = raw.combat.cooldownMultiplier * raw.combat.cooldownMultiplier;

        final.movement.moveSpeed = raw.movement.moveSpeed * (1f + raw.movement.speedMultiplier);

        final.combat.critChance = Mathf.Clamp(raw.combat.critChance, 0f, 1f);

        final.survival.armor = raw.survival.armor / (1f + raw.survival.armor * 0.1f);

        return final;
    }
}