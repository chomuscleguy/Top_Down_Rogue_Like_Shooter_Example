using UnityEngine;

public class EnemyMovement : MonoBehaviour, ITickable
{
    public Transform target;

    private float speed;

    private Vector2 knockbackVelocity;

    public void Init(float speed, GameObject target)
    {
        this.speed = speed;
        this.target = target.transform;

        Core.Instance.Tick.Register(this);
    }

    public void Tick(float dt)
    {
        if (target == null)
            return;

        Vector2 dir = ((Vector2)target.position - (Vector2)transform.position).normalized;

        Vector2 move = dir * speed + knockbackVelocity;

        transform.position += (Vector3)(move * dt);

        knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, 10f * dt);
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        knockbackVelocity = direction.normalized * force;
    }

    private void OnDisable()
    {
        Core.Instance?.Tick.Unregister(this);
    }
}