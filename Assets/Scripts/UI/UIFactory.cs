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

        return instance.GetComponent<T>();
    }

    public T CreateHUD<T>(HUDType type, Transform parent) where T : BaseHUD
    {
        var prefab = registry.GetHUD(type);

        var instance = GameObject.Instantiate(prefab, parent);

        return instance.GetComponent<T>();
    }
}