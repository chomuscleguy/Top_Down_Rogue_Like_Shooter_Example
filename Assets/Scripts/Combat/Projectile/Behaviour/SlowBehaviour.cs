using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Slow")]
public class SlowBehaviour : ProjectileBehaviour
{
    public float slowAmount = 0.3f;
    public float duration = 2f;

    public override void OnHit(Projectile p, Collider2D target)
    {
        var slow = target.GetComponent<IMovable>();
        if (slow != null)
        {
            slow.ApplySlow(slowAmount, duration);
        }
    }
}