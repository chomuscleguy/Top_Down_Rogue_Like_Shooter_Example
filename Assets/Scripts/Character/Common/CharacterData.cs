using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/Character")]
public class CharacterData : BaseData
{
    public Sprite icon;
    public string characterName;
    public CharacterStats stats;
    public WeaponData baseWeapon;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        if (ID < 0 || ID >= 1000)
        {
            Debug.LogError($"{name}: Character ID는 0~999 범위를 사용해야 합니다.", this);
        }
    }
#endif
}