using UnityEngine;

public class PlayerMovement : MonoBehaviour, ITickable
{
    [Header("Collision")]
    [SerializeField]
    private LayerMask obstacleLayer;

    [SerializeField]
    private float collisionRadius;

    private float moveSpeed;

    private Vector2 moveInput;

    public void Init(float radius)
    {
        collisionRadius = radius;
    }

    public void ApplyStats(MovementStats stats)
    {
        moveSpeed = stats.moveSpeed * (1f + stats.speedMultiplier);
    }

    public void SetInput(Vector2 input)
    {
        moveInput = input;
    }

    public void Tick(float dt)
    {
        Move(dt);
    }

    private void Move(float dt)
    {
        if (moveInput.sqrMagnitude <= 0.001f)
            return;

        Vector2 direction = moveInput.normalized;

        Vector2 currentPos = transform.position;

        float moveAmount = moveSpeed * dt;

        Vector2 nextX = currentPos + Vector2.right * direction.x * moveAmount;

        bool blockedX = Physics2D.OverlapCircle(nextX, collisionRadius, obstacleLayer);

        if (!blockedX)
        {
            currentPos.x = nextX.x;
        }

        Vector2 nextY = currentPos + Vector2.up * direction.y * moveAmount;

        bool blockedY = Physics2D.OverlapCircle(nextY, collisionRadius, obstacleLayer);

        if (!blockedY)
        {
            currentPos.y = nextY.y;
        }

        transform.position = currentPos;
    }
}