using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed = 3f;

    [Header("Weights")]
    [SerializeField] private float seekWeight = 1f;
    [SerializeField] private float separationWeight = 2f;

    [Header("Smoothing")]
    [SerializeField] private float smooth = 10f;

    private Transform target;
    private Enemy enemy;

    private Vector2 velocity;
    private Vector2 knockback;

    public void Init(float speed, Transform t)
    {
        this.speed = speed;
        target = t;
        enemy = GetComponent<Enemy>();

        Core.Instance.Grid.Register(enemy);
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 pos = transform.position;


        Vector2 seek = ((Vector2)target.position - pos).normalized;

        Vector2 sep = Core.Instance.Grid.Separation(pos, enemy);


        Vector2 dir =   seek * seekWeight +  sep * separationWeight;

        dir = Vector2.ClampMagnitude(dir, 1f);


        Vector2 desired = dir * speed + knockback;

        velocity = Vector2.Lerp(velocity, desired, smooth * Time.fixedDeltaTime);

        transform.position += (Vector3)(velocity * Time.fixedDeltaTime);


        Core.Instance.Grid.UpdateEnemy(enemy);
        Core.Instance.Grid.RelocateIfTooFar(enemy);


        knockback = Vector2.Lerp(knockback, Vector2.zero, 8f * Time.fixedDeltaTime);
    }

    public void ApplyKnockback(Vector2 dir, float force)
    {
        knockback += dir.normalized * force;
    }
}