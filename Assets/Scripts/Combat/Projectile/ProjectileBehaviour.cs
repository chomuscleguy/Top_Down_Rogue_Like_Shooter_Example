using UnityEngine;

public abstract class ProjectileBehaviour : ScriptableObject
{
    public virtual void OnSpawn(Projectile p) { }

    public virtual void OnUpdate(Projectile p, float dt) { }

    public virtual void OnHit(Projectile p, IDamageable target) { }

    public virtual void OnExpire(Projectile p) { }
}