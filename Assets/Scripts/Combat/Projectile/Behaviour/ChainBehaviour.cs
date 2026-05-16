using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Chain")]
public class ChainBehaviour : ProjectileBehaviour
{
    [SerializeField]
    private int chainCount = 3;

    [SerializeField]
    private float radius = 5f;

    public override void OnHit(Projectile p, Collider2D target)
    {
        HashSet<Collider2D> hitTargets = new();

        Collider2D current = target;

        hitTargets.Add(current);

        for (int i = 0; i < chainCount; i++)
        {
            Collider2D next =
                FindNext(current.transform.position, hitTargets);

            if (next == null)
                break;

            hitTargets.Add(next);

            if (next.TryGetComponent<IDamageable>(
                out var damageable))
            {
                damageable.TakeDamage(
                    p.Stats.damage * 0.7f);
            }

            current = next;
        }
    }

    private Collider2D FindNext(
        Vector2 pos,
        HashSet<Collider2D> excluded)
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(pos, radius);

        float closestDistance = float.MaxValue;

        Collider2D closest = null;

        foreach (var hit in hits)
        {
            if (excluded.Contains(hit))
                continue;

            if (!hit.TryGetComponent<IDamageable>(
                out _))
            {
                continue;
            }

            float sqrDistance =
                ((Vector2)hit.transform.position - pos)
                .sqrMagnitude;

            if (sqrDistance < closestDistance)
            {
                closestDistance = sqrDistance;
                closest = hit;
            }
        }

        return closest;
    }
}