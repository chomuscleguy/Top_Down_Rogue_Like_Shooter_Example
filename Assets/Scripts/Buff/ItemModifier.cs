//public class ItemModifier : IStatModifier
//{
//    private StatType type;
//    private float add;
//    private float mul;

//    public ItemModifier(StatType type, float add, float mul)
//    {
//        this.type = type;
//        this.add = add;
//        this.mul = mul;
//    }

//    public void Apply(ref CharacterStats s)
//    {
//        switch (type)
//        {
//            case StatType.CritChance:
//                s.critChance = StatMath.Apply(s.critChance, add, mul);
//                break;
//        }
//    }
//}