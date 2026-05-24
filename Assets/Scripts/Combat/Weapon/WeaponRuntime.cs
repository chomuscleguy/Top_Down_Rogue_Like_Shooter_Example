using UnityEngine;

public class WeaponRuntime
{
    private readonly WeaponData data;
    private readonly Transform owner;
    private readonly TargetScanner scanner;

    private readonly WeaponContext context;

    private int level;

    private float cooldownTimer;

    private CombatStats cachedCombat;
    private ProjectileStats cachedProjectile;
    private float cachedAttackInterval;

    private readonly ObjectPool<Projectile> pool;

    public WeaponData Data => data;
    public int Level => level;
    public WeaponRuntimeStats Stats { get; } = new WeaponRuntimeStats();

    public WeaponRuntime(WeaponData data, int level, Transform owner, TargetScanner scanner, IWeaponStatProvider statProvider)
    {
        this.data = data;
        this.level = level;
        this.owner = owner;
        this.scanner = scanner;

        pool = new ObjectPool<Projectile>();

        pool.Init(data.projectilePrefab, 20, Core.Instance.ProjectileRoot.transform);

        context = new WeaponContext(pool, scanner, data, owner, statProvider);

        Stats.acquiredTime = Core.Instance.Game.Run.PlayTime;
        context.source = this;
    }


    public void RebuildStats(IWeaponStatProvider provider)
    {
        WeaponLevelData levelData = data.GetLevelData(level);

        if (levelData == null)
            return;

        cachedCombat = levelData.combat + provider.GetCombatStats();
        cachedProjectile = levelData.projectile + provider.GetProjectileStats();

        cachedAttackInterval = levelData.attackInterval * levelData.combat.cooldownMultiplier;
    }

    public void Tick(float dt)
    {
        cooldownTimer += dt;

        if (cooldownTimer < cachedAttackInterval)
            return;

        cooldownTimer = 0f;

        data.behaviour.Execute(context, cachedCombat, cachedProjectile);
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }
}
