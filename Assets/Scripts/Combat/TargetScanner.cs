using UnityEngine;

public class TargetScanner : MonoBehaviour, IScanable
{
    public float range;
    public LayerMask targetLayer;

    public Transform Scanning()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);

        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest;
    }
}
