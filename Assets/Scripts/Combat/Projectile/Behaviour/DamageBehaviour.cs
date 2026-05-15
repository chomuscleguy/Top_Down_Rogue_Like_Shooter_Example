using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Projectile/Damage")]
public class DamageBehaviour : ProjectileBehaviour
{
    public override void OnHit(Projectile p, Collider2D target)
    {
        var damageable = target.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        var stats = p.GetStats();

        float damage = stats.damage;

        if (UnityEngine.Random.value < stats.critChance)
            damage *= stats.critDamage;

        damageable.TakeDamage(damage);
    }
}