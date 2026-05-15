using System;
using UnityEngine;

public enum GameState
{
    Boot,
    Lobby,
    Playing,
    GameOver,
}

public class GameManager : MonoBehaviour,IManager
{
    public GameState CurrentState { get; private set; }

    public event Action<GameState> OnStateChanged;

    public event Action<int> OnGoldChanged;

    public SaveData SaveData { get; private set; }

    public RunData Run { get; private set; }

    public int SelectedCharacterID;
    public int SelectedMapID;

    public int Gold => SaveData.gold;

    public void Init()
    {
        SaveData = Core.Instance.Save.Load();
        OnGoldChanged?.Invoke(SaveData.gold);
    }

    public void StartRun()
    {
        CharacterData character = Core.Instance.Data.Characters.GetByID(SelectedCharacterID);
        MapThemeData map = Core.Instance.Data.Map.GetByID(SelectedMapID);

        Run = new RunData();

        Run.Init(character, SaveData.progress, Core.Instance.Data.Upgrades.upgrades);

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

    public void AddGold(int amount)
    {
        SaveData.gold += amount;

        OnGoldChanged?.Invoke(SaveData.gold);
    }

    public bool SpendGold(int amount)
    {
        if (SaveData.gold < amount)
            return false;

        SaveData.gold -= amount;

        OnGoldChanged?.Invoke(SaveData.gold);

        return true;
    }
}