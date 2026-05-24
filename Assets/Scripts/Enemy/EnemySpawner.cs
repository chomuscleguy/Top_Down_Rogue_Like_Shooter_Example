using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private List<TimedSpawn> timedSpawns;

    [SerializeField] private float frontDistance = 8f;
    [SerializeField] private float sideDistance = 6f;
    [SerializeField] private float backDistance = 4f;

    [SerializeField] private float sideSpread = 3f;

    [Header("Reference")]
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemyPrefab;

    private ObjectPool<Enemy> pool;

    private float timer;
    private float gameTime;

    private int currentEnemyCount;

    private Vector2 lastPlayerPos;
    private Vector2 playerDir = Vector2.up;

    public void Init(Transform target)
    {
        player = target;

        pool = new ObjectPool<Enemy>();
        pool.Init(enemyPrefab, 300, transform);

        lastPlayerPos = player.position;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        UpdatePlayerDirection();

        var current = GetCurrentSpawn(gameTime);
        if (current == null) return;

        timer += Time.deltaTime;

        if (timer >= current.spawnInterval)
        {
            timer = 0f;

            for (int i = 0; i < current.spawnCount; i++)
                SpawnEnemy(current);
        }
    }

    private void UpdatePlayerDirection()
    {
        Vector2 pos = player.position;

        Vector2 v = pos - lastPlayerPos;

        if (v.sqrMagnitude > 0.0001f)
            playerDir = v.normalized;

        lastPlayerPos = pos;
    }

    private Vector2 GetSpawnPosition()
    {
        float r = Random.value;
        Vector2 basePos = player.position;

        if (r < 0.6f)
        {
            return basePos + playerDir * frontDistance + Random.insideUnitCircle * 2f;
        }

        if (r < 0.85f)
        {
            Vector2 side = new Vector2(-playerDir.y, playerDir.x);
            float sign = Random.value < 0.5f ? -1f : 1f;

            return basePos + side * sign * sideDistance + Random.insideUnitCircle * sideSpread;
        }

        return basePos - playerDir * backDistance + Random.insideUnitCircle * 1.5f;
    }


    private void SpawnEnemy(TimedSpawn data)
    {
        if (currentEnemyCount >= data.maxEnemyCount)
            return;

        EnemyData enemyData = data.table.GetRandomEnemy(gameTime);
        if (enemyData == null) return;

        Enemy enemy = pool.Get();

        enemy.transform.position = GetSpawnPosition();

        enemy.Init(pool, enemyData, this, player.gameObject);

        Core.Instance.Grid.Register(enemy);

        currentEnemyCount++;
    }

    public bool IsBehindTooDense()
    {
        return Core.Instance.Grid.IsBehindTooDense(player.position, playerDir);
    }

    public void OnEnemyDeath()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
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
}