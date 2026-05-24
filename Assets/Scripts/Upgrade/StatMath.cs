public static class StatMath
{
    public static float Apply(float baseValue, float add, float mul)
    {
        return (baseValue + add) * mul;
    }

    public static float ApplyInverse(float baseValue, float add, float mul)
    {
        return (baseValue - add) * (1f / mul);
    }
}