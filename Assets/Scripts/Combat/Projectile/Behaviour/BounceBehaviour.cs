using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Bounce")]
public class BounceBehaviour : ProjectileBehaviour
{
    public override void OnHit(Projectile p, Collider2D target)
    {
        p.ConsumePierce();

        if (p.GetRemainPierce() < 0)
        {
            p.RequestExpire();
            return;
        }

        p.SetDirection(Random.insideUnitCircle.normalized);
    }
}