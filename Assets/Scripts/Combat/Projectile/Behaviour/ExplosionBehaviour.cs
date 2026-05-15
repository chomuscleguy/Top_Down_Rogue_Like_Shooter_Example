using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Explosion")]
public class ExplosionBehaviour : ProjectileBehaviour
{
    public float radius = 2f;
    public float damageMultiplier = 0.5f;

    public override void OnExpire(Projectile p)
    {
        var stats = p.GetStats();

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            p.transform.position,
            radius
        );

        foreach (var h in hits)
        {
            var dmg = h.GetComponent<IDamageable>();
            if (dmg == null) continue;

            float damage = stats.damage * damageMultiplier;
            dmg.TakeDamage(damage);
        }
    }
}

