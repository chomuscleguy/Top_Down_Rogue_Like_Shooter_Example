using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Pierce")]
public class PierceBehaviour : ProjectileBehaviour
{
    public override void OnHit(Projectile p, Collider2D target)
    {
        p.ConsumePierce();

        if (p.GetRemainPierce() < 0)
            p.RequestExpire();
    }
}