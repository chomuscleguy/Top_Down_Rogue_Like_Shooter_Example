using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Behaviour/AirDrop")]
public class AirDropWeaponBehaviour : ProjectileWeaponBehaviour
{
    public float spread = 2f;
    public float upwardForce = 1200f;

    public override void Execute(WeaponContext ctx, CombatStats combat, ProjectileStats projectile)
    {
        int count = projectile.projectileCount;

        for (int i = 0; i < count; i++)
        {
            Projectile p = ctx.pool.Get();

            p.transform.position = ctx.owner.position;

            Vector2 random = Random.insideUnitCircle * spread;

            Vector2 initialVelocity = Vector2.up * upwardForce + random;

            p.Init(initialVelocity, combat, projectile, ctx.pool, ctx.owner, ctx.source);
        }
    }
}