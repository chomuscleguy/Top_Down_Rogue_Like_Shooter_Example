using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Behaviour/Chain")]
public class ChainBehaviour : ProjectileBehaviour
{
    private class State
    {
        public int chainCount;
        public HashSet<Enemy> hitEnemies = new();
    }

    private readonly Dictionary<Projectile, State> states = new();

    public int maxChain = 5;
    public float chainRange = 5f;
    public float damageDecay = 0.8f;
    public LayerMask enemyLayer;

    public override void OnSpawn(Projectile p)
    {
        if (!states.ContainsKey(p))
        {
            states[p] = new State();
        }
    }

    public override void OnHit(Projectile p, IDamageable target)
    {
        if (!states.TryGetValue(p, out var state))
            return;

        Enemy enemy = target as Enemy;

        if (enemy == null)
            return;

        state.hitEnemies.Add(enemy);

        if (state.chainCount >= maxChain)
            return;

        Enemy next = FindNextTarget(enemy.transform.position, state.hitEnemies);

        if (next == null)
            return;

        Vector2 dir = (next.transform.position - enemy.transform.position).normalized;

        Projectile clone = p.Pool.Get();

        clone.transform.position = enemy.transform.position;

        clone.Target = next.transform;

        clone.SetHitMode(Projectile.HitMode.TargetOnly);

        CombatStats nextCombat = p.Combat;

        nextCombat.damage *= damageDecay;

        clone.Init(dir, nextCombat, p.ProjectileStat, p.Pool, p.Owner, p.Source);

        states[clone] = new State
        {
            chainCount = state.chainCount + 1,
            hitEnemies = new HashSet<Enemy>(state.hitEnemies)
        };
    }

    public override void OnExpire(Projectile p)
    {
        states.Remove(p);
    }

    private Enemy FindNextTarget(Vector3 origin, HashSet<Enemy> excluded)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, chainRange, enemyLayer);

        Enemy nearest = null;
        float nearestDist = float.MaxValue;

        foreach (var h in hits)
        {
            Enemy e = h.GetComponent<Enemy>();

            if (e == null)
                continue;

            if (excluded.Contains(e))
                continue;

            float dist = (e.transform.position - origin).sqrMagnitude;

            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = e;
            }
        }

        return nearest;
    }
}