using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Behaviour/Orbit")]
public class OrbitBehaviour : ProjectileBehaviour
{
    private class State
    {
        public Transform center;
        public float radius;
        public float speed;
        public float angle;
    }

    private readonly Dictionary<Projectile, State> states = new();

    public override void OnSpawn(Projectile p)
    {
        states[p] = new State();
    }

    public override void OnUpdate(Projectile p, float dt)
    {
        var s = states[p];

        s.angle += s.speed * dt;

        Vector2 offset = new Vector2(
            Mathf.Cos(s.angle),
            Mathf.Sin(s.angle)
        ) * s.radius;

        p.transform.position = s.center.position + (Vector3)offset;
    }

    public override void OnExpire(Projectile p)
    {
        states.Remove(p);
    }
}