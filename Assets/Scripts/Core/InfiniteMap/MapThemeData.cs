using UnityEngine;

[CreateAssetMenu(menuName = "Map/Theme")]
public class MapThemeData : BaseData
{
    [Header("Visual")]
    public Sprite thumbnail;
    public Sprite groundSprite;

    [Header("Info")]
    public string stageNumber;

    [Header("Objects")]
    public DecorationData[] decorations;
    public DecorationData[] obstacles;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        if (ID < 4000 || ID >= 5000)
        {
            Debug.LogError($"{name}: Map ID는 4000~4999 범위를 사용해야 합니다.", this);
        }
    }
#endif
}