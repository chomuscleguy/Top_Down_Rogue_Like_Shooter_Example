using System.Collections.Generic;
using UnityEngine;

public class EnemyGridManager : MonoBehaviour,IManager
{
    [Header("Grid")]
    public float cellSize = 0.5f;
    public int maxPerCell = 6;

    [Header("Relocate")]
    public float maxDistance = 6f;
    public float relocateRadius = 12f;

    [Header("Behind Density")]
    [SerializeField] private int maxEnemiesBehind = 20;

    private Transform player;

    private readonly Dictionary<Vector2Int, List<Enemy>> grid = new();
    private readonly Dictionary<Enemy, Vector2Int> map = new();

    public void SetPlayer(Transform p) => player = p;

    public void Init()
    {
        
    }

    public void Register(Enemy e)
    {
        var c = GetCell(e.transform.position);

        if (!grid.TryGetValue(c, out var list))
        {
            list = new List<Enemy>();
            grid[c] = list;
        }

        list.Add(e);
        map[e] = c;
    }

    public void Unregister(Enemy e)
    {
        if (!map.TryGetValue(e, out var c)) return;

        if (grid.TryGetValue(c, out var list))
            list.Remove(e);

        map.Remove(e);
    }

    public void UpdateEnemy(Enemy e)
    {
        if (!map.TryGetValue(e, out var old)) return;

        var n = GetCell(e.transform.position);

        if (old == n) return;

        if (grid.TryGetValue(old, out var oldList))
            oldList.Remove(e);

        if (!grid.TryGetValue(n, out var newList))
        {
            newList = new List<Enemy>();
            grid[n] = newList;
        }

        newList.Add(e);
        map[e] = n;
    }

    public Vector2 Separation(Vector2 pos, Enemy self)
    {
        Vector2Int c = GetCell(pos);
        Vector2 f = Vector2.zero;

        float r = GetRadius(self);
        float sqr = r * r;

        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                var cell = new Vector2Int(c.x + x, c.y + y);

                if (!grid.TryGetValue(cell, out var list))
                    continue;

                foreach (var e in list)
                {
                    if (e == self) continue;

                    Vector2 d = pos - (Vector2)e.transform.position;
                    float s = d.sqrMagnitude;

                    if (s < 0.0001f || s > sqr) continue;

                    f += d.normalized / s;
                }
            }

        return f;
    }

    private Vector2 GetRelocatePosition()
    {
        Vector2 basePos = player.position;
        Vector2 offset = Random.insideUnitCircle.normalized * relocateRadius;
        return basePos + offset;
    }

    public void RelocateIfTooFar(Enemy e)
    {
        if (player == null) return;

        Vector2 to = (Vector2)e.transform.position - (Vector2)player.position;

        if (to.sqrMagnitude < maxDistance * maxDistance)
            return;

        e.transform.position = GetRelocatePosition();
        UpdateEnemy(e);
    }

    public bool IsBehindTooDense(Vector2 playerPos, Vector2 forward)
    {
        int count = 0;

        Vector2Int center = GetCell(playerPos);

        for (int x = -2; x <= 2; x++)
            for (int y = -2; y <= 2; y++)
            {
                var cell = new Vector2Int(center.x + x, center.y + y);

                if (!grid.TryGetValue(cell, out var list))
                    continue;

                foreach (var e in list)
                {
                    Vector2 to = (Vector2)e.transform.position - playerPos;

                    if (to.sqrMagnitude > 100f)
                        continue;

                    if (Vector2.Dot(to.normalized, forward) < -0.3f)
                        count++;

                    if (count >= maxEnemiesBehind)
                        return true;
                }
            }

        return false;
    }

    private Vector2Int GetCell(Vector2 p)
    {
        return new Vector2Int(
            Mathf.FloorToInt(p.x / cellSize),
            Mathf.FloorToInt(p.y / cellSize)
        );
    }

    float GetRadius(Enemy e)
    {
        var col = e.GetComponent<Collider2D>();
        if (col == null) return 0.5f;

        return col.bounds.extents.magnitude;
    }

    
}