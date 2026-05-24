using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Behaviour/Gravity")]
public class GravityBehaviour : ProjectileBehaviour
{
    [SerializeField] private float gravity = 9.8f;

    public override void OnUpdate(Projectile p, float dt)
    {
        p.SetVelocity(p.Velocity + Vector2.down * gravity * dt);
    }
}