using UnityEngine;

public class UIFactory
{
    private UIRegistry registry;

    public UIFactory(UIRegistry registry)
    {
        this.registry = registry;
        registry.Init();
    }

    public T CreatePopup<T>(UIType type, Transform parent) where T : BasePopup
    {
        var prefab = registry.GetPopup(type);

        var instance = GameObject.Instantiate(prefab, parent);

        return instance as T;
    }

    public BasePopup CreatePopup(UIType type, Transform parent)
    {
        var prefab = registry.GetPopup(type);

        return GameObject.Instantiate(prefab, parent);
    }

    public GameObject CreateHUD(HUDType type, Transform parent)
    {
        var prefab = registry.GetHUD(type);
        return GameObject.Instantiate(prefab, parent);
    }
}