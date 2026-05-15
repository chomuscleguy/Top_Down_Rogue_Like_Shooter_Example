using UnityEngine;

public class Enemy : MonoBehaviour, IKnockbackable, ICombatStatProvider, IMovementStatProvider, ISurvivalStatProvider, ITickable
{
    private EnemyData data;

    private ObjectPool<Enemy> pool;
    private EnemySpawner spawner;
    private EnemyMovement movement;
    private HitFlash hitFlash;

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
        movement = GetComponent<EnemyMovement>();
        hitFlash = GetComponent<HitFlash>();
    }

    public void Init(ObjectPool<Enemy> ownerPool, EnemyData enemyData, EnemySpawner ownerSpawner, GameObject target)
    {
        pool = ownerPool;
        data = enemyData;
        spawner = ownerSpawner;

        movement.Init(data.stats.movement.moveSpeed, target);

        ApplyStats();
        BindEvents();

        Health.ResetHealth();

        Core.Instance.Tick.Register(this);
    }

    public void Tick(float dt)
    {
        
    }

    private void ApplyStats()
    {
        Health.SetMaxHP(data.stats.survival.maxHP);
    }

    private void BindEvents()
    {
        Health.OnDeath -= HandleDeath;
        Health.OnDamageTaken -= HandleHit;

        Health.OnDeath += HandleDeath;
        Health.OnDamageTaken += HandleHit;
    }

    private void UnbindEvents()
    {
        Health.OnDeath -= HandleDeath;
        Health.OnDamageTaken -= HandleHit;
    }

    private void HandleDeath(Health health)
    {
        Core.Instance.Drop.SpawnExp(data.xp, transform.position);

        spawner.OnEnemyDeath();

        Core.Instance.Tick.Unregister(this);

        pool.Return(this);
    }

    private void HandleHit(float damage)
    {
        hitFlash?.Play();
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        movement.ApplyKnockback(direction, force);
    }

    private void OnDisable()
    {
        UnbindEvents();
        Core.Instance?.Tick.Unregister(this);
    }

    public CombatStats GetCombatStats() => data.stats.combat;

    public MovementStats GetMovementStats() => data.stats.movement;

    public SurvivalStats GetSurvivalStats() => data.stats.survival;
}