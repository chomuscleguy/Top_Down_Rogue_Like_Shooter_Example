using System.Collections.Generic;
using UnityEngine;

public class MapChunk : MonoBehaviour
{
    [Header("Renderer")]
    [SerializeField]
    private SpriteRenderer ground;

    [Header("Root")]
    [SerializeField]
    private Transform decorationRoot;
    [SerializeField]
    private Transform obstacleRoot;
    [SerializeField]
    private float centerSafeRadius = 3f;

    private readonly List<GameObject> spawned = new();

    private readonly List<Vector2> usedPositions = new();

    public Vector2Int Coord { get; private set; }

    private MapThemeData currentTheme;

    public void SetCoord(Vector2Int coord, int chunkSize)
    {
        Coord = coord;

        transform.position = new Vector3(coord.x * chunkSize, coord.y * chunkSize);
    }

    public void SetTheme(MapThemeData theme, int chunkSize)
    {
        if (theme == currentTheme)
            return;

        currentTheme = theme;

        ApplyTheme(chunkSize);
    }

    private void ApplyTheme(int chunkSize)
    {
        if (currentTheme == null)
            return;

        ApplyGround(chunkSize);

        ClearObjects();

        Generate(currentTheme.decorations, decorationRoot, chunkSize);

        Generate(currentTheme.obstacles, obstacleRoot, chunkSize);
    }

    private void ApplyGround(int chunkSize)
    {
        if (ground == null)
            return;

        ground.sprite = currentTheme.groundSprite;

        ground.drawMode = SpriteDrawMode.Tiled;

        ground.size = new Vector2(chunkSize, chunkSize);
    }

    private void Generate(DecorationData[] list, Transform parent, int chunkSize)
    {
        if (list == null)
            return;

        foreach (DecorationData deco in list)
        {
            if (deco.prefab == null)
                continue;

            if (Random.value > deco.spawnChance)
                continue;

            int count = Random.Range(deco.minCount, deco.maxCount + 1);

            for (int i = 0; i < count; i++)
            {
                for (int attempt = 0; attempt < 10; attempt++)
                {
                    Vector2 randomPos = new Vector2(Random.Range(-chunkSize * 0.5f, chunkSize * 0.5f), Random.Range(-chunkSize * 0.5f, chunkSize * 0.5f));

                    if (!CanPlace(randomPos, GetRadius(deco.prefab)))
                    {
                        continue;
                    }

                    GameObject obj = Instantiate(deco.prefab, parent);

                    obj.transform.localPosition = randomPos;

                    obj.transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                    float scale = Random.Range(0.8f, 1.2f);

                    obj.transform.localScale = Vector3.one * scale;

                    spawned.Add(obj);

                    usedPositions.Add(randomPos);

                    break;
                }
            }
        }
    }

    private bool CanPlace(Vector2 pos, float radius)
    {
        foreach (Vector2 used in usedPositions)
        {
            float dist = Vector2.Distance(pos, used);

            if (dist < radius)
                return false;
        }

        if (Coord == Vector2Int.zero)
        {
            float centerDist = pos.magnitude;

            if (centerDist < centerSafeRadius)
                return false;
        }

        return true;
    }

    private void ClearObjects()
    {
        foreach (GameObject obj in spawned)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        spawned.Clear();

        usedPositions.Clear();
    }

    private float GetRadius(GameObject prefab)
    {
        Collider2D col = prefab.GetComponent<Collider2D>();

        if (col == null)
            return 0.5f;

        switch (col)
        {
            case CircleCollider2D circle:
                return circle.radius * prefab.transform.localScale.x;

            case BoxCollider2D box:
                return Mathf.Max(box.size.x, box.size.y) * 0.5f;

            case CapsuleCollider2D capsule:
                return Mathf.Max(capsule.size.x, capsule.size.y) * 0.5f;
        }

        return 0.5f;
    }

    private void OnDisable()
    {
        ClearObjects();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, Vector3.one * 20f);
    }
#endif
}