using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField]
    private List<TimedSpawn> timedSpawns;

    [SerializeField]
    private float spawnRadius = 8f;

    [Header("Reference")]
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Enemy enemyPrefab;

    private ObjectPool<Enemy> pool;

    private float timer;

    private float gameTime;

    private int currentEnemyCount;

    public void Init(Transform target)
    {
        this.player = target;

        pool = new ObjectPool<Enemy>();
        pool.Init(enemyPrefab, 300, transform);
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        TimedSpawn currentSpawn = GetCurrentSpawn(gameTime);

        if (currentSpawn == null)
            return;

        timer += Time.deltaTime;

        if (timer >= currentSpawn.spawnInterval)
        {
            timer = 0f;

            for (int i = 0; i < currentSpawn.spawnCount; i++)
            {
                SpawnEnemy(currentSpawn);
            }
        }
    }

    private void SpawnEnemy(TimedSpawn spawnData)
    {
        if (currentEnemyCount >= spawnData.maxEnemyCount)
            return;

        Vector2 dir = Random.insideUnitCircle.normalized;

        if (dir == Vector2.zero)
            dir = Vector2.up;

        Vector2 spawnPos = (Vector2)player.position + dir * spawnRadius;

        EnemyData data = spawnData.table.GetRandomEnemy(gameTime);

        if (data == null)
            return;

        Enemy enemy = pool.Get();

        enemy.transform.position = spawnPos;

        enemy.Init(pool, data, this, player.gameObject);

        currentEnemyCount++;
    }

    private TimedSpawn GetCurrentSpawn(float time)
    {
        if (timedSpawns == null || timedSpawns.Count == 0)
            return null;

        TimedSpawn current = timedSpawns[0];

        foreach (var t in timedSpawns)
        {
            if (time >= t.startTime)
                current = t;
            else
                break;
        }

        return current;
    }

    public void OnEnemyDeath()
    {
        currentEnemyCount--;

        if (currentEnemyCount < 0)
            currentEnemyCount = 0;
    }
}