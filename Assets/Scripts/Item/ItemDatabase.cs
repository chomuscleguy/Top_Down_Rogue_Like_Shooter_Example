using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Database/Item")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> items;
}