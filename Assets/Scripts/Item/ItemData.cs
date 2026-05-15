using UnityEngine;

public enum ItemType
{
    Buff,
    Weapon,
}

public abstract class ItemData : ScriptableObject
{
    public abstract ItemType Type { get; }

    public string itemName;
    public int id;
    public Sprite icon;

    public abstract CharacterStats GetStats(int level);
}