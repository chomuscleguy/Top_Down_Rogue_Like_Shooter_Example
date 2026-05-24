using UnityEngine;

public class DropManager : MonoBehaviour, IManager
{
    [Header("XP")]
    [SerializeField] private ExpPickup smallExp;

    [SerializeField] private ExpPickup mediumExp;

    [SerializeField] private ExpPickup largeExp;

    [Header("Gold")]
    [SerializeField] private GoldPickup smallGold;

    [SerializeField] private GoldPickup mediumGold;

    [SerializeField] private GoldPickup largeGold;

    public void Init()
    {
    }

    public void SpawnDrops(EnemyData data, Vector3 position)
    {
        foreach (DropData drop in data.drops)
        {
            if (Random.value > drop.chance)
                continue;

            switch (drop.type)
            {
                case DropType.XP:
                    SpawnPickup(drop.amount, position, smallExp, mediumExp, largeExp);
                    break;

                case DropType.Gold:
                    SpawnPickup(drop.amount, position, smallGold, mediumGold, largeGold);
                    break;
            }
        }
    }

    private void SpawnPickup<T>(int amount, Vector3 position, T small, T medium, T large) where T : Pickup
    {
        while (amount > 0)
        {
            T prefab;

            int value;

            if (amount >= 20)
            {
                prefab = large;
                value = 20;
            }
            else if (amount >= 5)
            {
                prefab = medium;
                value = 5;
            }
            else
            {
                prefab = small;
                value = 1;
            }

            amount -= value;

            T pickup = Instantiate(prefab, position, Quaternion.identity);

            pickup.Init(value);

            Core.Instance.Pickup.Register(pickup);
        }
    }
}