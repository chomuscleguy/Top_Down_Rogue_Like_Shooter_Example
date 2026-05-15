using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, ITickable
{
    [Header("Behaviour")]
    [SerializeField]
    private List<ProjectileBehaviour> behaviours = new();

    private Vector2 direction;
    private WeaponStats stats;
    private ObjectPool<Projectile> pool;

    private float lifeTimer;
    private const float maxLife = 5f;

    private bool isExpired;

    private int remainPierce;

    public Transform Target { get; set; }

    public void Init(Vector2 dir, WeaponStats stats, ObjectPool<Projectile> pool)
    {
        direction = dir;
        this.stats = stats;
        this.pool = pool;

        remainPierce = stats.pierce;

        lifeTimer = 0f;
        isExpired = false;

        Core.Instance.Tick.Register(this);

        foreach (var b in behaviours)
            b.OnSpawn(this);
    }

    public void Tick(float dt)
    {
        if (isExpired)
        {
            Expire();
            return;
        }

        Move(dt);
        UpdateLife(dt);
    }

    private void Move(float dt)
    {
        transform.position += (Vector3)(direction * stats.projectileSpeed * dt);
    }

    private void UpdateLife(float dt)
    {
        lifeTimer += dt;

        if (lifeTimer >= maxLife)
            Expire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        bool critical = Random.value < stats.critChance;

        HitContext hit = Core.Instance.Combat.CreateHit(gameObject, other.gameObject, stats.damage, direction, critical);

        Core.Instance.Combat.ProcessHit(hit);

        HandlePierce();
    }

    private void HandlePierce()
    {
        remainPierce--;

        if (remainPierce < 0)
            RequestExpire();
    }

    public void RequestExpire()
    {
        isExpired = true;
    }

    private void Expire()
    {
        Core.Instance.Tick.Unregister(this);

        foreach (var b in behaviours)
            b.OnExpire(this);

        pool.Return(this);
    }

    public WeaponStats GetStats() => stats;
    public void SetDirection(Vector2 dir) => direction = dir;
    public int GetRemainPierce() => remainPierce;
    public void ConsumePierce() => remainPierce--;
}