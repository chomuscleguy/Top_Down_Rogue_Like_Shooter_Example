using System.Threading.Tasks;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LoadingUI loadingUI;
    [SerializeField] private SceneLoader sceneLoader;

    private async void Start()
    {
        await InitializeAsync();

        sceneLoader.Init();

        Core.Instance.Game.ChangeState(GameState.Lobby);
    }

    private async Task InitializeAsync()
    {
        loadingUI.SetText("Initializing...");

        await Task.Yield();

        loadingUI.SetProgress(0.3f);

        Core.Instance.Init();

        loadingUI.SetText("Loading Systems...");

        await Task.Yield();

        loadingUI.SetProgress(0.8f);

        await Task.Delay(200);

        loadingUI.SetProgress(1f);
    }
}