using System;
using UnityEngine;

public enum GameState
{
    Boot,
    Lobby,
    Playing,
    GameOver,
}

public class GameManager : MonoBehaviour, IManager
{
    public GameState CurrentState { get; private set; }

    public event Action<GameState> OnStateChanged;

    public SaveData SaveData { get; private set; }

    public PlayerProgress Progress => SaveData.progress;

    public RunData Run { get; private set; }

    public int SelectedCharacterID;
    public int SelectedMapID;

    public void Init()
    {
        SaveData = Core.Instance.Save.Load();

        SaveData.progress.OnAfterLoad();
    }

    public void StartRun()
    {
        CharacterData character = Core.Instance.Data.Characters.Get(SelectedCharacterID);

        MapThemeData map = Core.Instance.Data.Maps.Get(SelectedMapID);

        Run = new RunData();

        Run.Init(character, Progress, Core.Instance.Data.Upgrades.GetAll());

        ChangeState(GameState.Playing);
    }

    public void EndRun()
    {
        Run = null;

        ChangeState(GameState.GameOver);
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        OnStateChanged?.Invoke(newState);
    }
}