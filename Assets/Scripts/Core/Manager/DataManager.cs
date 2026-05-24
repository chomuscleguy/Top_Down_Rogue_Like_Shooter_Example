using UnityEngine;

public class DataManager : MonoBehaviour, IManager
{
    public CharacterDatabase Characters { get; private set; }
    public MapThemeDatabase Maps { get; private set; }
    public UpgradeDatabase Upgrades { get; private set; }
    public ItemDatabase Items { get; private set; }

    public void Init()
    {
        Characters = Load<CharacterDatabase>("CharacterDatabase");
        Maps = Load<MapThemeDatabase>("MapThemeDatabase");
        Upgrades = Load<UpgradeDatabase>("UpgradeDatabase");
        Items = Load<ItemDatabase>("ItemDatabase");
    }

    private T Load<T>(string path) where T : ScriptableObject
    {
        T db = Resources.Load<T>($"Database/{path}");

        if (db == null)
        {
            Debug.LogError($"Database Load Failed: {path}");
            return null;
        }

        if (db is IBaseDatabase initable)
        {
            initable.Init();
        }

        return db;
    }
}