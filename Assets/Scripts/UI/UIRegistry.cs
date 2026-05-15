using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    CharacterSelectPopup,
    MapSelectPopup,
    UpgradePopup,
    LevelUpPopup,
    GameOverPopup,
    PausePopup,
}

public enum HUDType
{
    Lobby,
    Game
}

[System.Serializable]
public class UIEntry
{
    public UIType type;
    public BasePopup prefab;
}

[System.Serializable]
public class HUDEntry
{
    public HUDType type;
    public GameObject prefab;
}

[CreateAssetMenu(menuName = "UI/UIRegistry")]
public class UIRegistry : ScriptableObject
{
    [Header("Popup")]
    [SerializeField] private List<UIEntry> popupEntries;

    [Header("HUD")]
    [SerializeField] private List<HUDEntry> hudEntries;

    private Dictionary<UIType, BasePopup> popupDict;
    private Dictionary<HUDType, GameObject> hudDict;

    public void Init()
    {
        popupDict = new Dictionary<UIType, BasePopup>();
        hudDict = new Dictionary<HUDType, GameObject>();

        foreach (var e in popupEntries)
            popupDict[e.type] = e.prefab;

        foreach (var e in hudEntries)
            hudDict[e.type] = e.prefab;
    }

    public BasePopup GetPopup(UIType type)
    {
        return popupDict[type];
    }

    public GameObject GetHUD(HUDType type)
    {
        return hudDict[type];
    }
}