using System;

[Serializable]
public struct ProjectileStats
{
    public int projectileCount;
    public float projectileSize;
    public static ProjectileStats operator +(ProjectileStats a, ProjectileStats b)
    {
        return new ProjectileStats
        {
            projectileCount = a.projectileCount + b.projectileCount,
            projectileSize = a.projectileSize + b.projectileSize
        };
    }
}
