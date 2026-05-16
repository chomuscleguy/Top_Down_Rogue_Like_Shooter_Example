using UnityEngine;

public class WeaponContext
{
    public ObjectPool<Projectile> pool;

    public TargetScanner scanner;

    public WeaponData weaponData;

    public Transform owner;

    public ICombatStatProvider combatStats;

    public IProjectileStatProvider projectileStats;

    public WeaponContext(ObjectPool<Projectile> pool, TargetScanner scanner, WeaponData weaponData,
        Transform owner = null, ICombatStatProvider combatStats = null, IProjectileStatProvider projectileStats = null)
    {
        this.pool = pool;
        this.scanner = scanner;
        this.weaponData = weaponData;

        this.owner = owner;

        this.combatStats = combatStats;

        this.projectileStats = projectileStats;
    }
}