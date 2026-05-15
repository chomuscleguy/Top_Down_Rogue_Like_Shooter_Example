using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Database/Map")]
public class MapThemeDatabase : ScriptableObject
{
    public List<MapThemeData> map;

    public MapThemeData GetByID(int id)
    {
        return map.Find(c => c.id == id);
    }
}