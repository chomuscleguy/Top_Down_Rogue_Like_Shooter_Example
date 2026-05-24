using UnityEngine;

public class EnemyRelocator : MonoBehaviour
{
    public float maxDist = 15f;
    public float radius = 6f;

    public void Check(Enemy e, Transform player, Vector2 dir)
    {
        float d = Vector2.Distance(e.transform.position, player.position);

        if (d < maxDist) return;

        e.transform.position =
            (Vector2)player.position +
            dir * Random.Range(4f, 8f) +
            Random.insideUnitCircle * radius;
    }
}