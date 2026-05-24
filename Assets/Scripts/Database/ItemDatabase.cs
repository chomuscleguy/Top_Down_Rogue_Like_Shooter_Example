using UnityEngine;

[CreateAssetMenu(menuName = "Database/Item")]
public class ItemDatabase : BaseDatabase<ItemData>
{
    public WeaponData GetWeapon(int id)
    {
        return Get(id) as WeaponData;
    }

    public PassiveData GetPassive(int id)
    {
        return Get(id) as PassiveData;
    }
}