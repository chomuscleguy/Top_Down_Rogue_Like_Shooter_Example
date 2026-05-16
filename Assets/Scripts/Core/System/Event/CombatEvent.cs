using UnityEngine;

public enum CombatEventType
{
    Damage,
    Death,
    Heal
}

public struct CombatEvent
{
    public CombatEventType type;
    public GameObject source;
    public float value;
}