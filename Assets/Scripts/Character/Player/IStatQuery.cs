public interface IStatQuery
{
    CombatStats GetCombat();
    SurvivalStats GetSurvival();
    MovementStats GetMovement();
    ProjectileStats GetProjectile();
    UtilityStats GetUtility();
}