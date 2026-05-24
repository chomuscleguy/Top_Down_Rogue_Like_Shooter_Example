using System;

[Serializable]
public struct ProjectileStats
{
    public int projectileCount;
    public float projectileSize;

    public float projectileSpeed;

    public float spreadAngle;
    public int pierce;

    public float duration;

    public static ProjectileStats operator +(ProjectileStats a, ProjectileStats b)
    {
        return new ProjectileStats
        {
            projectileCount = a.projectileCount + b.projectileCount,
            projectileSize = a.projectileSize + b.projectileSize,
            projectileSpeed = a.projectileSpeed + b.projectileSpeed,

            spreadAngle = a.spreadAngle + b.spreadAngle,
            pierce = a.pierce + b.pierce,

            duration = a.duration + b.duration,
        };
    }
}
