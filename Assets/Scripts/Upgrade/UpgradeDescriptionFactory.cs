using System.Collections.Generic;

public static class UpgradeDescriptionFactory
{
    public static string Create(UpgradeData data, int currentLevel)
    {
        if (currentLevel >= data.MaxLevel)
            return $"{data.displayName} (MAX)";

        var levelData = data.GetLevel(currentLevel + 1);

        if (levelData.cost == 0 && levelData.stats.Equals(default(CharacterStats)))
            return "";

        var stats = levelData.stats;

        List<string> lines = new();

        if (stats.combat.damage != 0)
            lines.Add($"Damage +{stats.combat.damage}");

        if (stats.movement.moveSpeed != 0)
            lines.Add($"MoveSpeed +{stats.movement.moveSpeed}");

        if (stats.combat.cooldownMultiplier != 0)
            lines.Add($"Cooldown -{stats.combat.cooldownMultiplier * 100f}%");

        if (stats.combat.critChance != 0)
            lines.Add($"Crit +{stats.combat.critChance * 100f}%");

        if (stats.projectile.projectileCount != 0)
            lines.Add($"Projectile +{stats.projectile.projectileCount}");

        return $"{data.displayName}\n{string.Join("\n", lines)}";
    }
}