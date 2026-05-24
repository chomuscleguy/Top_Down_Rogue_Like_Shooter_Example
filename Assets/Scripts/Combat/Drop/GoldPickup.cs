public class GoldPickup : Pickup
{
    public override void Collect(Player player)
    {
        Core.Instance.Game.Run.AddGold(Value);

        Deactivate();
    }
}