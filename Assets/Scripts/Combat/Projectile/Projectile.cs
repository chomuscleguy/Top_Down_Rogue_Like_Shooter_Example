using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, ITickable
{
    public enum HitMode
    {
        Collision,
        TargetOnly
    }

    [Header("Behaviour")]
    [SerializeField]
    private List<ProjectileBehaviour> behaviours = new();

    private Vector2 velocity;

    private CombatStats combat;
    private ProjectileStats projectile;

    private ObjectPool<Projectile> pool;

    private float lifeTimer;
    private bool isExpired;

    private int pierceLeft;

    [Header("Hit Settings")]
    [SerializeField]
    private HitMode hitMode = HitMode.Collision;

    [SerializeField]
    private float targetHitDistance = 0.2f;

    public Transform Owner { get; private set; }
    public Transform Target { get; set; }

    public Vector2 Velocity => velocity;

    public CombatStats Combat => combat;
    public ProjectileStats ProjectileStat => projectile;

    public ObjectPool<Projectile> Pool => pool;

    public WeaponRuntime Source { get; private set; }

    public void Init(Vector2 dir, CombatStats combat, ProjectileStats projectile, ObjectPool<Projectile> pool, Transform owner, WeaponRuntime source)
    {
        this.velocity = dir.normalized * projectile.projectileSpeed;
        this.combat = combat;
        this.projectile = projectile;
        this.pool = pool;
        this.Owner = owner;
        this.Source = source;

        pierceLeft = projectile.pierce;

        lifeTimer = 0f;
        isExpired = false;

        gameObject.SetActive(true);

        Core.Instance.Tick.Register(this);

        for (int i = 0; i < behaviours.Count; i++)
        {
            behaviours[i].OnSpawn(this);
        }
    }

    public void Tick(float dt)
    {
        if (isExpired)
        {
            Expire();
            return;
        }

        for (int i = 0; i < behaviours.Count; i++)
        {
            behaviours[i].OnUpdate(this, dt);
        }

        Move(dt);

        if (hitMode == HitMode.TargetOnly)
        {
            UpdateTargetHit();
        }

        UpdateLife(dt);
    }

    private void Move(float dt)
    {
        transform.position += (Vector3)(velocity * dt);
    }

    private void UpdateLife(float dt)
    {
        lifeTimer += dt;

        if (lifeTimer >= projectile.duration)
        {
            RequestExpire();
        }
    }

    private void UpdateTargetHit()
    {
        if (Target == null)
        {
            RequestExpire();
            return;
        }

        float sqrDist = (Target.position - transform.position).sqrMagnitude;

        if (sqrDist <= targetHitDistance * targetHitDistance)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        if (!Target.TryGetComponent(out IDamageable damageable))
        {
            RequestExpire();
            return;
        }

        damageable.TakeDamage(combat.damage, Source);

        if (Target.TryGetComponent(out IKnockbackable knockbackable))
        {
            knockbackable.ApplyKnockback(velocity.normalized, combat.knockbackForce);
        }

        for (int i = 0; i < behaviours.Count; i++)
        {
            behaviours[i].OnHit(this, damageable);
        }

        RequestExpire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hitMode != HitMode.Collision)
            return;

        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        damageable.TakeDamage(combat.damage, Source);

        if (other.TryGetComponent(out IKnockbackable knockbackable))
        {
            knockbackable.ApplyKnockback(velocity.normalized, combat.knockbackForce);
        }

        for (int i = 0; i < behaviours.Count; i++)
        {
            behaviours[i].OnHit(this, damageable);
        }

        HandlePierce();
    }

    private void HandlePierce()
    {
        if (projectile.pierce == -1)
            return;

        pierceLeft--;

        if (pierceLeft <= 0)
        {
            RequestExpire();
        }
    }

    public void SetVelocity(Vector2 v)
    {
        velocity = v;
    }

    public void SetHitMode(HitMode mode)
    {
        hitMode = mode;
    }

    public void RequestExpire()
    {
        isExpired = true;
    }

    private void Expire()
    {
        Core.Instance.Tick.Unregister(this);

        for (int i = 0; i < behaviours.Count; i++)
        {
            behaviours[i].OnExpire(this);
        }

        pool.Return(this);
    }

    private void OnDestroy()
    {
        Core.Instance.Tick.Unregister(this);
    }
}