using UnityEngine;

public class DataManager : MonoBehaviour, IManager
{
    public CharacterDatabase Characters { get; private set; }
    public MapThemeDatabase Map { get; private set; }
    public UpgradeDatabase Upgrades { get; private set; }
    public ItemDatabase Items { get; private set; }

    public void Init()
    {
        Characters = Resources.Load<CharacterDatabase>("Database/CharacterDatabase");
        Upgrades = Resources.Load<UpgradeDatabase>("Database/UpgradeDatabase");
        Items = Resources.Load<ItemDatabase>("Database/ItemDatabase");
        Map = Resources.Load<MapThemeDatabase>("Database/MapThemeDatabase");
    }
}
