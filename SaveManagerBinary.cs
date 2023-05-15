using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManagerBinary : MonoBehaviour
{
    public static SaveManagerBinary Instance;

    public static event Action<SaveDataBinary> OnSaveLoaded;
    public static event Action<SaveDataBinary> OnSaveRequest;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        string filepath = Application.persistentDataPath + "/save.data";

        FileStream dataStream = new FileStream(filepath, FileMode.Create);

        SaveDataBinary saveData = new SaveDataBinary();

        OnSaveRequest?.Invoke(saveData);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, saveData);

        dataStream.Close();
    }

    public void Load()
    {
        string filepath = Application.persistentDataPath + "/save.data";
        SaveDataBinary saveData = new SaveDataBinary();

        if (File.Exists(filepath))
        {
            // File exists 
            FileStream dataStream = new FileStream(filepath, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            saveData = converter.Deserialize(dataStream) as SaveDataBinary;
            dataStream.Close();
        }
        else
        {
            Debug.LogWarning("Save file not found in " + filepath);
            saveData = new SaveDataBinary();
        }
        OnSaveLoaded?.Invoke(saveData);
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public static void DeleteSave()
    {
        if (File.Exists(Application.persistentDataPath + "/save.data"))
        {
            File.Delete(Application.persistentDataPath + "/save.data");
        }
    }
}

[System.Serializable]
public class SaveDataBinary
{
    //Save Data
}
