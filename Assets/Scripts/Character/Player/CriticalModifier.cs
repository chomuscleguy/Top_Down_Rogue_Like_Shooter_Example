public class CriticalModifier : IDamageModifier
{
    public void Modify(ref HitContext hit)
    {
        if (!hit.isCritical)
            return;

        float critDamage = hit.attackerStats.GetCombatStats().critDamage;

        hit.finalDamage *= critDamage;
    }
}