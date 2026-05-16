using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private HUDType pendingHUD;

    public void Init()
    {
        Core.Instance.Game.OnStateChanged += HandleState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        Core.Instance.Game.OnStateChanged -= HandleState;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void HandleState(GameState state)
    {
        switch (state)
        {
            case GameState.Lobby:
                pendingHUD = HUDType.Lobby;
                SceneManager.LoadScene("LobbyScene");
                break;

            case GameState.Playing:
                pendingHUD = HUDType.Game;
                SceneManager.LoadScene("GameScene");

                break;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Core.Instance.UI.ShowHUD(pendingHUD);
    }
}