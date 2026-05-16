using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Damage")]
public class DamageBehaviour : ProjectileBehaviour
{
    public override void OnHit(Projectile p, Collider2D target)
    {
        var damageable = target.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        WeaponStats stats = p.Stats;

        float damage = stats.damage;

        bool critical = Random.value < stats.critChance;

        if (critical)
            damage *= stats.critDamage;

        damageable.TakeDamage(damage);
    }
}