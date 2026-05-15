using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Chain")]
public class ChainBehaviour : ProjectileBehaviour
{
    public int chainCount = 3;
    public float radius = 5f;

    public override void OnHit(Projectile p, Collider2D target)
    {
        Transform origin = target.transform;

        for (int i = 0; i < chainCount; i++)
        {
            var next = FindNext(origin.position);
            if (next == null) break;

            var dmg = next.GetComponent<IDamageable>();
            if (dmg != null)
                dmg.TakeDamage(p.GetStats().damage * 0.7f);
        }
    }

    private Collider2D FindNext(Vector2 pos)
    {
        var hits = Physics2D.OverlapCircleAll(pos, radius);

        foreach (var h in hits)
        {
            if (h.GetComponent<IDamageable>() != null)
                return h;
        }

        return null;
    }
}

