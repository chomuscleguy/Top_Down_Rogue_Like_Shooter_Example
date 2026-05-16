using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnTable", menuName = "SO/SpawnTable")]
public class SpawnTable : ScriptableObject
{
    public List<SpawnEntry> enemies;

    public EnemyData GetRandomEnemy(float gameTime)
    {
        List<SpawnEntry> validEnemies = new();

        float totalWeight = 0f;

        foreach (var e in enemies)
        {
            if (gameTime < e.minTime)
                continue;

            if (gameTime > e.maxTime)
                continue;

            validEnemies.Add(e);

            totalWeight += e.weight;
        }

        if (validEnemies.Count == 0)
            return null;

        float rand = Random.value * totalWeight;

        foreach (var e in validEnemies)
        {
            rand -= e.weight;

            if (rand <= 0f)
                return e.enemyData;
        }

        return validEnemies[0].enemyData;
    }
}