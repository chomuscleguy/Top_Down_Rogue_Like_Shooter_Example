using UnityEngine;

public class DefaultDamageApplier : MonoBehaviour, IDamageApplier
{
    public void Apply(HitContext hit)
    {
        if (hit.target == null)
            return;

        if (hit.target.TryGetComponent(out IDamageable dmg))
            dmg.TakeDamage(hit.finalDamage);

        if (hit.target.TryGetComponent(out IKnockbackable kb))
            kb.ApplyKnockback(hit.hitDirection, 3f);
    }
}