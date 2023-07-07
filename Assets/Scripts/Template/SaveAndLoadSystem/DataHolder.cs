using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour, IDataPersistence
{
    public GameData currentData;


    public void LoadData(GameData data)
    {
        currentData = data;
    }

    public void SaveData(GameData data)
    {

    }
}
