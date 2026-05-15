using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour, IManager
{
    private string path;

    public void Init()
    {
        path = Application.persistentDataPath + "/save.json";

    }

    public SaveData Load()
    {
        if (!File.Exists(path))
        {
            SaveData newData = CreateDefault();
            newData.progress.OnAfterLoad();
            return newData;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        data = Validate(data);

        data.progress.OnAfterLoad();

        return data;
    }

    public void Save(SaveData data)
    {
        data.progress.OnBeforeSave();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    private SaveData CreateDefault()
    {
        return new SaveData
        {
            gold = 0,
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