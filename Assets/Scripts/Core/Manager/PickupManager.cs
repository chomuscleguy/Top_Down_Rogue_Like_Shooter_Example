using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour, IManager
{
    [SerializeField] private Player player;

    private readonly List<Pickup> pickups = new();

    public void Init()
    {
    }

    public void SetPlayer(Player target)
    {
        player = target;
    }

    public void Register(Pickup pickup)
    {
        if (pickup == null)
            return;

        if (!pickups.Contains(pickup))
        {
            pickups.Add(pickup);
        }
    }

    public void Unregister(Pickup pickup)
    {
        pickups.Remove(pickup);
    }

    private void Update()
    {
        if (player == null)
            return;

        float dt = Time.deltaTime;

        Vector3 playerPos = player.transform.position;

        float magnetRange = player.Run.FinalStats.utility.pickupRadius;

        float magnetSqr = magnetRange * magnetRange;

        for (int i = pickups.Count - 1; i >= 0; i--)
        {
            Pickup pickup = pickups[i];

            if (pickup == null)
            {
                pickups.RemoveAt(i);
                continue;
            }

            Vector3 dir = playerPos - pickup.transform.position;

            float sqrDist = dir.sqrMagnitude;

            if (!pickup.IsFollowing && sqrDist <= magnetSqr)
            {
                pickup.Activate();
            }

            if (!pickup.IsFollowing)
                continue;

            if (sqrDist <= 0.04f)
            {
                Collect(pickup);
                continue;
            }

            float dist = Mathf.Sqrt(sqrDist);

            if (dist > 0.0001f)
            {
                dir /= dist;
            }

            float speed = pickup.Speed + sqrDist * 5f;

            pickup.transform.position += dir * speed * dt;
        }
    }

    private void Collect(Pickup pickup)
    {
        pickup.Collect(player);

        Unregister(pickup);
    }
}