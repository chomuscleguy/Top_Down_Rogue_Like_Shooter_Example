using UnityEngine;

[CreateAssetMenu(menuName = "Map/Theme")]
public class MapThemeData : ScriptableObject
{
    public int id;
    public Sprite thumnail;
    public Sprite groundSprite;
    public string stageNumber;
    public string desc;
    public DecorationData[] decorations;
    public DecorationData[] obstacles;
}