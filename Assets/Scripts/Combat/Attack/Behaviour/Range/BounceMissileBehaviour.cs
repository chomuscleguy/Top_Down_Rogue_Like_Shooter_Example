using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Behaviour/BounceMissile")]
public class BounceMissileBehaviour : ProjectileWeaponBehaviour
{
    public override void Execute(WeaponContext ctx, CombatStats combat, ProjectileStats projectile)
    {
        Vector3 spawnPos = ctx.owner.position;

        for (int i = 0; i < projectile.projectileCount; i++)
        {
            Vector2 dir = Random.insideUnitCircle;

            if (dir.sqrMagnitude < 0.001f)
                dir = Vector2.right;

            dir.Normalize();

            Projectile p = ctx.pool.Get();

            p.transform.position = spawnPos;

            p.Init(dir, combat, projectile, ctx.pool, ctx.owner, ctx.source);
        }
    }
}