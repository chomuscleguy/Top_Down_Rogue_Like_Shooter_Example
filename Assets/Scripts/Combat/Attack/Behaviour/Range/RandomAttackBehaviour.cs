using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Behaviour/RandomAttack")]
public class RandomAttackBehaviour : ProjectileWeaponBehaviour
{
    public bool allowDuplicateTarget = true;

    public override void Execute(WeaponContext ctx, CombatStats combat, ProjectileStats projectile)
    {
        HashSet<Transform> usedTargets = new();

        for (int i = 0; i < projectile.projectileCount; i++)
        {
            Transform target = GetRandomTarget(ctx, usedTargets);

            if (target == null)
                break;

            if (!allowDuplicateTarget)
            {
                usedTargets.Add(target);
            }

            Projectile p = ctx.pool.Get();

            p.transform.position = target.position;

            p.Target = target;

            p.Init(Vector2.zero, combat, projectile, ctx.pool, ctx.owner, ctx.source);
        }
    }

    private Transform GetRandomTarget(WeaponContext ctx, HashSet<Transform> usedTargets)
    {
        List<Transform> candidates = ctx.scanner.Targets;

        if (candidates.Count == 0)
            return null;

        if (allowDuplicateTarget)
        {
            return candidates[Random.Range(0, candidates.Count)];
        }

        List<Transform> available = new();

        for (int i = 0; i < candidates.Count; i++)
        {
            Transform target = candidates[i];

            if (target == null)
                continue;

            if (usedTargets.Contains(target))
                continue;

            available.Add(target);
        }

        if (available.Count == 0)
            return null;

        return available[Random.Range(0, available.Count)];
    }
}