using System.Threading.Tasks;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private LoadingUI loadingUI;
    [SerializeField]
    private SceneLoader sceneLoader;

    private async void Start()
    {
        await InitializeAsync();

        sceneLoader.Init();

        Core.Instance.Game.ChangeState(GameState.Lobby);
    }

    private async Task InitializeAsync()
    {
        loadingUI.SetText("Initializing Managers...");

        await Task.Yield();

        Core.Instance.Init();

        loadingUI.SetProgress(0.2f);

        loadingUI.SetText("Loading Save Data...");

        await Task.Yield();

        Core.Instance.Game.Init();

        loadingUI.SetProgress(0.5f);

        loadingUI.SetText("Loading Databases...");

        await Task.Yield();

        Core.Instance.Data.Init();

        loadingUI.SetProgress(0.8f);

        loadingUI.SetText("Loading others...");

        await Task.Yield();

        Core.Instance.UI.Init();

        loadingUI.SetProgress(0.9f);

        loadingUI.SetText("Preparing Game...");

        await Task.Delay(300);

        loadingUI.SetProgress(1f);
    }
}