using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Behaviour/Projectile")]
public class ProjectileWeaponBehaviour : WeaponBehaviour
{
    public override void Execute(WeaponContext ctx, CombatStats combat, ProjectileStats projectile)
    {
        Transform target = ctx.scanner.GetClosestTarget(combat.range);

        if (target == null)
            return;

        Vector2 dir = (target.position - ctx.owner.position).normalized;

        int count = Mathf.Max(1, projectile.projectileCount);

        if (projectile.spreadAngle > 0f)
        {
            FireSpread(ctx, combat, projectile, dir, target, count);
        }
        else
        {
            Fire(ctx, combat, projectile, dir, target);
        }
    }

    private void Fire(WeaponContext ctx, CombatStats combat, ProjectileStats projectile, Vector2 dir, Transform target)
    {
        Projectile p = ctx.pool.Get();

        p.transform.position = ctx.owner.position;

        p.Target = target;

        p.Init(dir, combat, projectile, ctx.pool, ctx.owner, ctx.source);
    }

    private void FireSpread(WeaponContext ctx, CombatStats combat, ProjectileStats projectile, Vector2 baseDir, Transform target, int count)
    {
        float totalAngle = projectile.spreadAngle;

        float step = count > 1 ? totalAngle / (count - 1) : 0f;

        for (int i = 0; i < count; i++)
        {
            float offset = -totalAngle * 0.5f + step * i;

            Vector2 dir = Quaternion.Euler(0, 0, offset) * baseDir;

            Fire(ctx, combat, projectile, dir, target);
        }
    }
}