using UnityEngine;

public enum EnemyType
{
    Normal,
    Elite,
    Boss
}

[CreateAssetMenu(fileName = "Data", menuName = "SO/Enemy")]
public class EnemyData : BaseData
{
    [Header("Type")]
    public EnemyType type;

    [Header("Stats")]
    public CharacterStats stats;

    [Header("Drops")]
    public DropData[] drops;

    [Header("Prefab")]
    public Enemy prefab;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        if (ID < 6000 || ID >= 7000)
        {
            Debug.LogError($"{name}: Enemy ID는 6000~6999 범위를 사용해야 합니다.", this);
        }
    }
#endif
}