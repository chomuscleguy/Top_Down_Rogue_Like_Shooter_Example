public static class WeaponStatCalculator
{
    public static WeaponStats Calculate(WeaponLevelData levelData, IStatQuery player)
    {
        WeaponStats result = levelData.stats;

        var combat = player.GetCombat();
        var projectile = player.GetProjectile();

        result.damage += combat.damage * combat.damageMultiplier;
        result.attackInterval *= (1f - combat.cooldownReduction);
        result.critChance += combat.critChance;
        result.critDamage += combat.critDamage;

        result.projectileCount += projectile.projectileCount;
        result.projectileSize += projectile.projectileSize;

        return result;
    }
}