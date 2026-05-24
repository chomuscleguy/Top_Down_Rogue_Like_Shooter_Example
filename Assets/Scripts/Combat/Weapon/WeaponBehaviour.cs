using UnityEngine;

public abstract class WeaponBehaviour : ScriptableObject
{
    public abstract void Execute(WeaponContext ctx, CombatStats combat, ProjectileStats projectile);
}
