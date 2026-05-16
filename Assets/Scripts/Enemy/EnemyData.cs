using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Base Stats")]
    public CharacterStats stats;

    [Header("Reward")]
    public int xp;
    public int gold;

    [Header("Prefab")]
    public Enemy prefab;
}