using UnityEngine;

public abstract class ItemData : BaseData
{
    [Header("Item")]
    public Sprite icon;

    public abstract CharacterStats GetStats(int level);
}