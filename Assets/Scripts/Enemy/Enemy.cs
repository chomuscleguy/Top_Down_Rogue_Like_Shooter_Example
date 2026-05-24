using UnityEngine;

public class Enemy : MonoBehaviour, IKnockbackable, ICombatStatProvider, IMovementStatProvider, ISurvivalStatProvider, ITickable
{
    private EnemyData data;
    private ObjectPool<Enemy> pool;
    private EnemySpawner spawner;

    private EnemyAttack attack;
    private EnemyMovement movement;
    private HitFlash hitFlash;

    public Health Health { get; private set; }

    private bool isRegistered;

    private void Awake()
    {
        Health = GetComponent<Health>();
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttack>();
        hitFlash = GetComponent<HitFlash>();
    }

    public void Init(ObjectPool<Enemy> ownerPool, EnemyData enemyData, EnemySpawner ownerSpawner, GameObject target)
    {
        pool = ownerPool;
        data = enemyData;
        spawner = ownerSpawner;

        movement.Init(data.stats.movement.moveSpeed, target.transform);
        attack.Init(enemyData);

        ApplyStats();
        BindEvents();

        Health.ResetHealth();

        RegisterToGrid();

        Core.Instance.Tick.Register(this);
    }

    public void Tick(float dt)
    {
        Core.Instance.Grid?.UpdateEnemy(this);
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

    private void RegisterToGrid()
    {
        if (isRegistered) return;

        Core.Instance.Grid?.Register(this);
        isRegistered = true;
    }

    private void UnregisterFromGrid()
    {
        if (!isRegistered) return;

        Core.Instance.Grid?.Unregister(this);
        isRegistered = false;
    }

    private void HandleDeath(Health health)
    {
        Core.Instance.Game.Run.AddKill();

        Core.Instance.Drop.SpawnDrops(data, transform.position);

        spawner.OnEnemyDeath();

        Core.Instance.Tick.Unregister(this);

        UnregisterFromGrid();

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

        Core.Instance.Tick.Unregister(this);

        UnregisterFromGrid();
    }

    public CombatStats GetCombatStats() => data.stats.combat;
    public MovementStats GetMovementStats() => data.stats.movement;
    public SurvivalStats GetSurvivalStats() => data.stats.survival;
}