using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, ITickable
{
    [Header("Behaviour")]
    [SerializeField]
    private List<ProjectileBehaviour> behaviours = new();

    private Vector2 spawnPosition;
    private Vector2 direction;

    private WeaponStats stats;
    private ObjectPool<Projectile> pool;

    private float lifeTimer;
    private const float maxLife = 5f;

    private bool isExpired;
    private bool hasTurned;

    private int remainPierce;

    public Transform Target { get; set; }

    // =========================
    // Properties
    // =========================

    public Vector2 Direction => direction;

    public Vector2 SpawnPosition => spawnPosition;

    public float Speed => stats.projectileSpeed;

    public WeaponStats Stats => stats;

    public bool HasTurned
    {
        get => hasTurned;
        set => hasTurned = value;
    }

    // =========================
    // Init
    // =========================

    public void Init(
        Vector2 dir,
        WeaponStats stats,
        ObjectPool<Projectile> pool)
    {
        spawnPosition = transform.position;

        direction = dir.normalized;

        this.stats = stats;
        this.pool = pool;

        remainPierce = stats.pierce;

        lifeTimer = 0f;

        isExpired = false;
        hasTurned = false;

        Core.Instance.Tick.Register(this);

        foreach (var b in behaviours)
            b.OnSpawn(this);
    }

    // =========================
    // Tick
    // =========================

    public void Tick(float dt)
    {
        if (isExpired)
        {
            Expire();
            return;
        }

        foreach (var b in behaviours)
            b.OnUpdate(this, dt);

        Move(dt);

        UpdateLife(dt);
    }

    // =========================
    // Move
    // =========================

    private void Move(float dt)
    {
        transform.position +=
            (Vector3)(direction * stats.projectileSpeed * dt);
    }

    // =========================
    // Life
    // =========================

    private void UpdateLife(float dt)
    {
        lifeTimer += dt;

        if (lifeTimer >= maxLife)
            RequestExpire();
    }

    // =========================
    // Collision
    // =========================

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        foreach (var b in behaviours)
            b.OnHit(this, other);

        HandlePierce();
    }

    private void HandlePierce()
    {
        // -1 = infinite pierce
        if (remainPierce < 0)
            return;

        remainPierce--;

        if (remainPierce < 0)
            RequestExpire();
    }

    // =========================
    // Public
    // =========================

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public void RequestExpire()
    {
        isExpired = true;
    }

    // =========================
    // Expire
    // =========================

    private void Expire()
    {
        Core.Instance.Tick.Unregister(this);

        foreach (var b in behaviours)
            b.OnExpire(this);

        pool.Return(this);
    }

    private void OnDisable()
    {
        if (Core.Instance != null)
            Core.Instance.Tick.Unregister(this);
    }
}