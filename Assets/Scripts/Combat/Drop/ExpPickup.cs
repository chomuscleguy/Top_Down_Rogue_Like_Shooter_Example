using UnityEngine;

public class ExpPickup : Pickup
{
    public override void Collect(Player player)
    {
        player.Experience.AddXP(Value);

        Deactivate();
    }
}
