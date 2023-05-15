using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
using System.IO;

public class SaveManager : MonoBehaviour
{
    static public SaveManager Instance;

    public static event Action<SaveData> OnSaveLoaded;
    public static event Action<SaveData> OnSaveRequest;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Load();
    }

    public void Load()
    {
        string filepath = Application.persistentDataPath + "/save.json";

        SaveData saveData = new SaveData();

        if (System.IO.File.Exists(filepath))
        {
            string saverjson = System.IO.File.ReadAllText(filepath);
            saveData = JsonUtility.FromJson<SaveData>(saverjson);
        }

        OnSaveLoaded?.Invoke(saveData);
    }

    public void Save()
    {
        SaveData saveData = new SaveData();
        OnSaveRequest?.Invoke(saveData);
        string save = JsonUtility.ToJson(saveData, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/save.json", save);
    }

    void OnApplicationQuit()
    {
        Save();
    }


#if UNITY_EDITOR
    [MenuItem("Edit/Clean Save")]
    static void CleanSave()
    {
        PlayerPrefs.DeleteAll();
        if (File.Exists(Application.persistentDataPath + "/save.json"))
        {
            File.Delete(Application.persistentDataPath + "/save.json");
        }
    }

    [MenuItem("Edit/Open Save Folder")]
    static void OpenSaveFolder()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
#endif
}

[System.Serializable]
public class SaveData
{
    //Save Data
}