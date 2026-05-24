using System.Collections.Generic;
using UnityEngine;

public class SpatialGrid
{
    private readonly float cellSize;

    private readonly Dictionary<Vector2Int, List<Enemy>> grid = new();

    private readonly Dictionary<Enemy, Vector2Int> enemyCells = new();

    public SpatialGrid(float cellSize)
    {
        this.cellSize = cellSize;
    }

    public void Add(Enemy enemy)
    {
        Vector2Int cell = WorldToCell(enemy.transform.position);

        enemyCells[enemy] = cell;

        if (!grid.TryGetValue(cell, out var list))
        {
            list = new List<Enemy>();

            grid[cell] = list;
        }

        list.Add(enemy);
    }

    public void Remove(Enemy enemy)
    {
        if (!enemyCells.TryGetValue(enemy, out var cell))
            return;

        if (grid.TryGetValue(cell, out var list))
        {
            list.Remove(enemy);

            if (list.Count == 0)
            {
                grid.Remove(cell);
            }
        }

        enemyCells.Remove(enemy);
    }

    public void Update(Enemy enemy)
    {
        Vector2Int newCell = WorldToCell(enemy.transform.position);

        if (!enemyCells.TryGetValue(enemy, out var oldCell))
            return;

        if (newCell == oldCell)
            return;

        Remove(enemy);

        enemyCells[enemy] = newCell;

        if (!grid.TryGetValue(newCell, out var list))
        {
            list = new List<Enemy>();

            grid[newCell] = list;
        }

        list.Add(enemy);
    }

    public List<Enemy> GetNearby(Vector2 position)
    {
        List<Enemy> result = new();

        Vector2Int center = WorldToCell(position);

        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector2Int cell = new Vector2Int(center.x + x, center.y + y);

                if (grid.TryGetValue(cell, out var list))
                {
                    result.AddRange(list);
                }
            }
        }

        return result;
    }

    private Vector2Int WorldToCell(Vector2 pos)
    {
        return new Vector2Int(Mathf.FloorToInt(pos.x / cellSize), Mathf.FloorToInt(pos.y / cellSize));
    }
}