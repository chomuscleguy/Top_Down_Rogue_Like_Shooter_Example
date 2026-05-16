using UnityEngine;

public struct HitContext
{
    public GameObject attacker;
    public GameObject target;

    public float baseDamage;
    public float finalDamage;

    public ICombatStatProvider attackerStats;
    public ISurvivalStatProvider targetStats;

    public bool isCritical;

    public Vector2 hitDirection;
}