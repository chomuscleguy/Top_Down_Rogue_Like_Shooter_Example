using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Melee Weapon")]
public class MeleeWeaponData : WeaponData
{
    public GameObject effectPrefab;

    public override CharacterStats GetStats(int level)
    {
        throw new System.NotImplementedException();
    }
}