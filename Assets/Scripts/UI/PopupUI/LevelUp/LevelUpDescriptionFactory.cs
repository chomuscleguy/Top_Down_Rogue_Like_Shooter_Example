public static class LevelUpDescriptionFactory
{
    public static string Create(ItemData data, int level)
    {
        int nextLevel = level + 1;

        switch (data)
        {
            case WeaponData weapon:
                return CreateWeaponDescription(weapon, nextLevel);

            case PassiveData passive:
                return CreatePassiveDescription(passive, nextLevel);
        }

        return string.Empty;
    }

    private static string CreateWeaponDescription(WeaponData weapon, int level)
    {
        if (level > weapon.MaxLevel)
            return "MAX LEVEL";

        WeaponLevelData levelData = weapon.GetLevelData(level);

        if (levelData == null)
            return "INVALID DATA";

        var stat = levelData;

        return
            $"Weapon Lv.{level}\n" +
            $"ATK: {stat.combat.damage}\n" +
            $"Count: {stat.projectile.projectileCount}\n" +
            $"CD: {stat.attackInterval:0.##}";
    }

    private static string CreatePassiveDescription(PassiveData passive, int level)
    {
        if (level > passive.MaxLevel)
            return "MAX LEVEL";

        PassiveLevelData levelData = passive.levels[level - 1];

        var stat = levelData.stats;

        return
            $"Passive Lv.{level}\n" +
            $"Damage +{stat.combat.damage}\n" +
            $"MoveSpeed +{stat.movement.moveSpeed}\n" +
            $"Crit +{stat.combat.critChance * 100f:0.#}%";
    }
}