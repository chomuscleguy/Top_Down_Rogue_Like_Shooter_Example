using UnityEngine;

[System.Serializable]
public class DecorationData
{
    public GameObject prefab;

    [Range(0f, 1f)]
    public float spawnChance = 0.3f;

    public int minCount = 3;
    public int maxCount = 8;
}