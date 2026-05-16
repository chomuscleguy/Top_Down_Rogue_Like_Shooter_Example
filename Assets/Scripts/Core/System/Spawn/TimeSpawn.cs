using System;
using UnityEngine;

[Serializable]
public class TimedSpawn
{
    [Header("Time")]
    public float startTime;

    [Header("Enemy Table")]
    public SpawnTable table;

    [Header("Spawn")]
    public float spawnInterval = 1f;

    public int spawnCount = 1;

    public int maxEnemyCount = 30;
}