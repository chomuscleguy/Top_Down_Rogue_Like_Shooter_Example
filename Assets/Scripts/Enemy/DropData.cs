using System;
using UnityEngine;

public enum DropType
{
    XP,
    Gold,
}

[Serializable]
public struct DropData
{
    public DropType type;

    [Range(0f, 1f)]
    public float chance;

    public int amount;
}