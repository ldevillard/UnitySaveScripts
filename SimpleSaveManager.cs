using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSaveManager : MonoBehaviour
{
    static public SimpleSaveManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void SaveGold()
    {
        PlayerPrefs.SetInt("Gold", CurrencyManager.Instance.AmountOfGold);
    }

    public int GetSavedGold()
    {
        if (PlayerPrefs.HasKey("Gold"))
            return PlayerPrefs.GetInt("Gold");
        else
            return 0;
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("Gold");
        CurrencyManager.Instance.SetGold(0);
        // PlayerPrefs.DeleteAll();
    }
}
