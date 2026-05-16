using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Knockback")]
public class KnockbackBehaviour : ProjectileBehaviour
{
    public override void OnHit(
        Projectile projectile,
        Collider2D target)
    {
        if (!target.TryGetComponent<Enemy>(
            out var enemy))
        {
            return;
        }

        Vector2 dir =
            (target.transform.position
            - projectile.transform.position)
            .normalized;

        float force =
            projectile.Stats.knockbackForce;

        enemy.ApplyKnockback(dir, force);
    }
}