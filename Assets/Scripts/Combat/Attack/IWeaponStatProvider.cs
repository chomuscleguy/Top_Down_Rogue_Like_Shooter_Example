public interface IWeaponStatProvider
{
    CombatStats GetCombatStats();
    ProjectileStats GetProjectileStats();
    UtilityStats GetUtilityStats();
}