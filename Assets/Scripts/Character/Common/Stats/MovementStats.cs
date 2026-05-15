using System;

[Serializable]
public struct MovementStats
{
    public float moveSpeed;
    public float speedMultiplier;

    public static MovementStats operator +(MovementStats a, MovementStats b)
    {
        return new MovementStats
        {
            moveSpeed = a.moveSpeed + b.moveSpeed,
            speedMultiplier = a.speedMultiplier + b.speedMultiplier
        };
    }
}
