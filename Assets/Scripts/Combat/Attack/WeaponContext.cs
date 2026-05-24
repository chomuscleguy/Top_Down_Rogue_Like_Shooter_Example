using UnityEngine;

public class WeaponContext
{
    public ObjectPool<Projectile> pool;
    public TargetScanner scanner;
    public WeaponData weaponData;
    public Transform owner;
    public IWeaponStatProvider stats;
    public WeaponRuntime source;

    public WeaponContext(ObjectPool<Projectile> pool, TargetScanner scanner, WeaponData weaponData, Transform owner, IWeaponStatProvider stats)
    {
        this.pool = pool;
        this.scanner = scanner;
        this.weaponData = weaponData;
        this.owner = owner;
        this.stats = stats;
    }
}