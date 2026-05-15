using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private readonly List<IDamageModifier> modifiers = new();

    private IDamageApplier damageApplier;

    private void Awake()
    {
        modifiers.AddRange(GetComponents<IDamageModifier>());

        damageApplier = GetComponent<IDamageApplier>();
    }
    public HitContext CreateHit(GameObject attacker, GameObject target, float baseDamage, Vector2 direction, bool isCritical)
    {
        return new HitContext
        {
            attacker = attacker,
            target = target,
            baseDamage = baseDamage,
            finalDamage = baseDamage,
            hitDirection = direction,
            isCritical = isCritical
        };
    }

    public void ProcessHit(HitContext hit)
    {
        hit.finalDamage = hit.baseDamage;

        ApplyModifiers(ref hit);

        ApplyResult(hit);
    }

    private void ApplyModifiers(ref HitContext hit)
    {
        for (int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].Modify(ref hit);
        }
    }

    private void ApplyResult(HitContext hit)
    {
        if (damageApplier != null)
        {
            damageApplier.Apply(hit);
            return;
        }

        ApplyDefault(hit);
    }

    private void ApplyDefault(HitContext hit)
    {
        if (hit.target == null)
            return;

        if (hit.target.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(hit.finalDamage);
        }

        if (hit.target.TryGetComponent(out IKnockbackable knockback))
        {
            knockback.ApplyKnockback(hit.hitDirection, 3f);
        }
    }
}