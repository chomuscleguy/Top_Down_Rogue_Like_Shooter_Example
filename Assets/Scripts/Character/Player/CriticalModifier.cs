public class CriticalModifier : IDamageModifier
{
    public void Modify(ref HitContext hit)
    {
        if (!hit.isCritical)
            return;

        float critDamage = 1 + hit.attackerStats.GetCombatStats().critDamageMultiplier;

        hit.finalDamage *= critDamage;
    }
}