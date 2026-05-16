using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetScanner : MonoBehaviour, ITickable
{
    [Header("Scan Settings")]
    [SerializeField] private float scanRange = 10f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float scanInterval = 0.1f;

    [Header("Buffer Settings")]
    [SerializeField] private int maxResults = 50;

    private ContactFilter2D contactFilter;
    private Collider2D[] scanResults;
    private List<Transform> targets = new List<Transform>();
    private float timer;

    public List<Transform> Targets => targets;

    public void Init()
    {
        scanResults = new Collider2D[maxResults];

        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(targetLayer);
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = true;

        Core.Instance.Tick.Register(this);
    }

    public void Tick(float deltaTime)
    {
        timer += deltaTime;

        if (timer >= scanInterval)
        {
            timer = 0;
            PerformScan();
        }
    }

    private void PerformScan()
    {
        targets.Clear();

        int count = Physics2D.OverlapCircle(transform.position, scanRange, contactFilter, scanResults);

        for (int i = 0; i < count; i++)
        {
            if (scanResults[i] != null)
                targets.Add(scanResults[i].transform);
        }
    }

    public Transform GetClosestTarget()
    {
        if (targets.Count == 0)
            return null;

        Transform closest = null;
        float minDistance = float.MaxValue;
        Vector2 currentPos = transform.position;

        foreach (var target in targets)
        {
            if (target == null) continue;
            float distance = Vector2.Distance(currentPos, target.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = target;
            }
        }
        return closest;
    }

    public Transform GetRandomTarget()
    {
        if (targets.Count == 0)
            return null;

        int randomIndex = Random.Range(0, targets.Count);
        return targets[randomIndex];
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);

        Gizmos.DrawWireSphere(transform.position, scanRange);

        if (targets != null && targets.Count > 0)
        {
            Gizmos.color = Color.red;
            foreach (var target in targets)
            {
                if (target != null)
                    Gizmos.DrawLine(transform.position, target.position);
            }
        }
    }
#endif
}