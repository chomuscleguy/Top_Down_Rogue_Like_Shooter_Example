using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour, IManager
{
    private string Path => Application.persistentDataPath + "/save.json";

    public void Init()
    {
    }

    public SaveData Load()
    {
        if (!File.Exists(Path))
        {
            SaveData newData = CreateDefault();
            newData.progress.OnAfterLoad();
            Debug.Log(Path);
            return newData;
        }

        string json = File.ReadAllText(Path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        data = Validate(data);

        data.progress.OnAfterLoad();

        return data;
    }

    public void Save(SaveData data)
    {
        data.progress.OnBeforeSave();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Path, json);
    }

    private SaveData CreateDefault()
    {
        return new SaveData
        {
            progress = new PlayerProgress(),
            unlocks = new UnlockData()
        };
    }

    private SaveData Validate(SaveData data)
    {
        if (data == null)
            return CreateDefault();

        if (data.progress == null)
            data.progress = new PlayerProgress();

        if (data.unlocks == null)
            data.unlocks = new UnlockData();

        if (data.unlocks.unlockedCharacterIDs == null)
            data.unlocks.unlockedCharacterIDs = new List<int>();

        return data;
    }
}