using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour, IManager
{
    [Header("Root")]
    [SerializeField] private Transform popupRoot;
    [SerializeField] private Transform hudRoot;
    [SerializeField] private Transform worldRoot;

    [Header("Registry")]
    [SerializeField] private UIRegistry registry;

    private UIFactory factory;

    private Stack<BasePopup> popupStack = new();

    public void Init()
    {
        factory = new UIFactory(registry);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ClearUI();
    }

    public T ShowPopup<T>(UIType type, System.Action<T> onInit = null) where T : BasePopup
    {
        var popup = factory.CreatePopup<T>(type, popupRoot);

        onInit?.Invoke(popup);

        popupStack.Push(popup);

        popup.Open();

        UpdateTimeScale();

        return popup;
    }

    public void ShowHUD(HUDType type)
    {
        ClearUI();

        var hud = factory.CreateHUD<BaseHUD>(type, hudRoot);

        hud.Bind();
    }

    public void CloseTopPopup()
    {
        if (popupStack.Count == 0)
            return;

        var popup = popupStack.Pop();
        popup.Close();

        Destroy(popup.gameObject);

        UpdateTimeScale();
    }

    private void ClearUI()
    {
        while (popupStack.Count > 0)
        {
            var popup = popupStack.Pop();
            Destroy(popup.gameObject);
        }

        foreach (Transform child in hudRoot)
        {
            Destroy(child.gameObject);
        }

        UpdateTimeScale();
    }

    public Transform GetHUDRoot() => hudRoot;
    public Transform GetWorldRoot() => worldRoot;

    private void UpdateTimeScale()
    {
        Time.timeScale = popupStack.Count > 0 ? 0f : 1f;
    }
}