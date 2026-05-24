using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Behaviour/Melee")]
public class MeleeBehaviour : WeaponBehaviour
{
    private static readonly Collider2D[] buffer = new Collider2D[32];

    public override void Execute(WeaponContext ctx, CombatStats combat, ProjectileStats projectile)
    {
        Vector2 pos = ctx.owner.position;
        float range = projectile.projectileSize;

        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, range);

        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D col = hits[i];
            if (col == null) continue;

            if (!col.TryGetComponent(out Enemy enemy))
                continue;

            enemy.Health.TakeDamage(combat.damage, ctx.source);

            Vector2 dir = (enemy.transform.position - ctx.owner.position).normalized;
            enemy.ApplyKnockback(dir, combat.knockbackForce);
        }
    }
}