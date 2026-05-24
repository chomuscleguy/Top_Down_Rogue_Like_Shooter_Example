using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Behaviour/BounceOnce")]
public class BounceBehaviour : ProjectileBehaviour
{
    private class State
    {
        public float timer;
        public bool bounced;
    }

    private readonly Dictionary<Projectile, State> states = new();

    public float bounceTime = 1.5f;
    public float randomness = 0.3f;

    public override void OnSpawn(Projectile p)
    {
        states[p] = new State();
    }

    public override void OnUpdate(Projectile p, float dt)
    {
        if (!states.TryGetValue(p, out var s))
            return;

        s.timer += dt;

        if (!s.bounced && s.timer >= p.ProjectileStat.duration / 3)
        {
            Vector2 v = p.Velocity;

            Vector2 newVelocity = (-v + Random.insideUnitCircle * randomness * v.magnitude);

            p.SetVelocity(newVelocity);

            s.bounced = true;
        }
    }

    public override void OnExpire(Projectile p)
    {
        states.Remove(p);
    }
}