using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Return")]
public class ReturnBehaviour : ProjectileBehaviour
{
    [SerializeField]
    private float turnDistance = 10f;

    [SerializeField]
    private float randomAngle = 30f;

    public override void OnUpdate(Projectile p, float dt)
    {
        if (p.HasTurned)
            return;

        float sqrDistance =
            ((Vector2)p.transform.position - p.SpawnPosition)
            .sqrMagnitude;

        if (sqrDistance < turnDistance * turnDistance)
            return;

        p.HasTurned = true;

        Vector2 reverseDir = -p.Direction;

        float angle =
            Random.Range(-randomAngle, randomAngle);

        Vector2 newDir =
            Quaternion.Euler(0, 0, angle)
            * reverseDir;

        p.SetDirection(newDir.normalized);
    }
}