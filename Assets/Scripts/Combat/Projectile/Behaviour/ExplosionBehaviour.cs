using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Explosion")]
public class ExplosionBehaviour : ProjectileBehaviour
{
    [SerializeField]
    private float radius = 2f;

    [SerializeField]
    private float damageMultiplier = 0.5f;

    public override void OnExpire(Projectile p)
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                p.transform.position,
                radius);

        float damage =
            p.Stats.damage * damageMultiplier;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<IDamageable>(
                out var damageable))
            {
                continue;
            }

            damageable.TakeDamage(damage);
        }
    }
}