using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    private Transform player;

    [SerializeField]
    private MapChunk chunkPrefab;
    [SerializeField]
    private MapThemeData defaultTheme;

    [Header("Chunk")]
    [SerializeField]
    private int chunkSize = 20;

    [SerializeField]
    private int range = 1;

    private readonly List<MapChunk> chunks = new();

    private Vector2Int currentPlayerChunk;

    private int gridSize;

    public void Init(Transform player)
    {
        this.player = player;

        gridSize = range * 2 + 1;
        currentPlayerChunk = GetChunkCoord(player.position);

        CreateInitialChunks();
    }


    private void Update()
    {
        Vector2Int playerChunk = GetChunkCoord(player.position);

        if (playerChunk == currentPlayerChunk)
            return;

        currentPlayerChunk = playerChunk;

        UpdateChunks();
    }

    private void CreateInitialChunks()
    {
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                Vector2Int coord = currentPlayerChunk + new Vector2Int(x, y);

                MapChunk chunk = Instantiate(chunkPrefab, transform);

                chunk.SetCoord(coord, chunkSize);
                chunk.SetTheme(defaultTheme, chunkSize);
                chunks.Add(chunk);
            }
        }
    }

    private void UpdateChunks()
    {
        foreach (MapChunk chunk in chunks)
        {
            Vector2Int coord = chunk.Coord;

            int dx = coord.x - currentPlayerChunk.x;

            int dy = coord.y - currentPlayerChunk.y;

            Vector2Int newCoord = coord;

            if (dx > range)
                newCoord.x -= gridSize;

            else if (dx < -range)
                newCoord.x += gridSize;

            if (dy > range)
                newCoord.y -= gridSize;

            else if (dy < -range)
                newCoord.y += gridSize;

            if (newCoord != coord)
            {
                chunk.SetCoord(newCoord, chunkSize);
            }
        }
    }

    private Vector2Int GetChunkCoord(Vector3 pos)
    {
        return new Vector2Int(Mathf.FloorToInt(pos.x / chunkSize), Mathf.FloorToInt(pos.y / chunkSize));
    }
}