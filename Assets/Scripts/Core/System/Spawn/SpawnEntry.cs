using UnityEngine;

[System.Serializable]
public class SpawnEntry
{
    [Header("Enemy")]
    public EnemyData enemyData;

    [Min(0)]
    public float weight = 1f;

    [Header("Time Condition")]
    public float minTime = 0f;

    public float maxTime = 99999f;
}