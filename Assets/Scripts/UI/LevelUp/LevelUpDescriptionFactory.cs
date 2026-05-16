public static class LevelUpDescriptionFactory
{
    public static string Create(ItemData data, int level)
    {
        int nextLevel = level + 1;

        if (data is WeaponData weapon)
        {
            if (level >= weapon.MaxLevel)
                return "MAX LEVEL";

            var stat = weapon.levels[nextLevel - 1].stats;

            return
                $"Weapon Lv.{nextLevel}\n" +
                $"ATK: {stat.damage}\n" +
                $"Count: {stat.projectileCount}\n" +
                $"CD: {stat.attackInterval:0.##}";
        }

        if (data is BuffData buff)
        {
            if (level >= buff.MaxLevel)
                return "MAX LEVEL";

            var stat = buff.levels[nextLevel - 1].stats;

            return
                $"Buff Lv.{nextLevel}\n" +
                $"Damage +{stat.combat.damage}\n" +
                $"MoveSpeed +{stat.movement.moveSpeed}\n" +
                $"Crit +{stat.combat.critChance * 100f:0.#}%";
        }

        return "";
    }
}