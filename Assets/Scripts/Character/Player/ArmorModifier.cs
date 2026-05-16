public class ArmorModifier : IDamageModifier
{
    public void Modify(ref HitContext hit)
    {
        float armor = hit.targetStats.GetSurvivalStats().armor;

        float reduction = 100f / (100f + armor);

        hit.finalDamage *= reduction;
    }
}