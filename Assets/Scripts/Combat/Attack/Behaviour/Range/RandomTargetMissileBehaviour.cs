using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Behaviour/RandomTarget")]
public class RandomTargetBehaviour : ProjectileWeaponBehaviour
{
    public override void Execute(WeaponContext ctx, CombatStats combat, ProjectileStats projectile)
    {
        Transform target = ctx.scanner.GetRandomTarget(combat.range);

        if (target == null)
            return;

        Vector2 dir = (target.position - ctx.owner.position).normalized;

        Projectile p = ctx.pool.Get();

        p.transform.position = ctx.owner.position;

        p.Init(dir, combat, projectile, ctx.pool, ctx.owner, ctx.source);

        p.Target = target;
    }
}