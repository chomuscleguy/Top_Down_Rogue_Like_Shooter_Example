using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        Core.Instance.Game.StartRun();
    }
}