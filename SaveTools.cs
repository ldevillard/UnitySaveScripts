using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;

public class SaveTools : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Edit/Clean Save")]
    static void CleanSave()
    {
        PlayerPrefs.DeleteAll();
        if (File.Exists(Application.persistentDataPath + "/save.json"))
        {
            File.Delete(Application.persistentDataPath + "/save.json");
        }

        if (File.Exists(Application.persistentDataPath + "/save.data"))
        {
            File.Delete(Application.persistentDataPath + "/save.data");
        }
    }

    [MenuItem("Edit/Open Save Folder")]
    static void OpenSaveFolder()
    {
        //This will open the folder in the editor (mac)
        EditorUtility.RevealInFinder(Application.persistentDataPath);

        //Function will open the folder in explorer (windows)
        //Don't forget to include "using System.Diagnostics;"
        //Process.Start("explorer.exe", "/select," + Application.persistentDataPath.Replace('/', '\\'));
    }

#endif

}
