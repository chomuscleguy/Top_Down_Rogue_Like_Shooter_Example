using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/Character")]
public class CharacterData : ScriptableObject
{
    public int id;
    public Sprite icon;
    public string characterName;
    public string description;
    public CharacterStats stats;
    public WeaponData baseWeapon;
}