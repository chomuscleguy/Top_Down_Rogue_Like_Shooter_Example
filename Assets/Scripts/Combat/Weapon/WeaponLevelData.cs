using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponLevelData
{
    public CombatStats combat;
    public ProjectileStats projectile;
    public float attackInterval;
    

    [TextArea]
    public string decription;
}