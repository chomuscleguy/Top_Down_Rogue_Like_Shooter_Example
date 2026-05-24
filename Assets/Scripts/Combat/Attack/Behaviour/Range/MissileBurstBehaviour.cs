using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Behaviour/MissileBurst")]
public class MissileBurstBehaviour : ProjectileWeaponBehaviour
{
    public override void Execute(WeaponContext ctx, CombatStats combat, ProjectileStats projectile)
    {
        Transform target = ctx.scanner.GetClosestTarget(combat.range);
        if (target == null) return;

        Vector2 baseDir = (target.position - ctx.owner.position).normalized;

        int count = Mathf.Max(1, projectile.projectileCount);

        for (int i = 0; i < count; i++)
        {
            Projectile p = ctx.pool.Get();

            Vector2 offset = Random.insideUnitCircle * 0.15f;

            p.transform.position = (Vector2)ctx.owner.position + offset;

            p.Target = target;

            p.Init(baseDir, combat, projectile, ctx.pool, ctx.owner, ctx.source);
        }
    }
}