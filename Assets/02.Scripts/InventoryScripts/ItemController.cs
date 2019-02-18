using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class Stats
{
    public string stat;
    public int value;
}

[Serializable]
public class ItemData
{
    public string name;
    public string sort;
    public string description;
    //public List<Stats> stats;
    public Stats[] stats;
}

public class ItemController : MonoBehaviour {

    public Stats CreateStats(string _stat, int _value)
    {
        Stats stats = new Stats();
        stats.stat = _stat;
        stats.value = _value;
        return stats;
    }

    private void Start()
    {
        //ItemData[] itemData = new ItemData [2];
        ItemData itemData = new ItemData();

        List<Stats> statList = new List<Stats>();
        statList.Add(CreateStats("Power", 3));
        statList.Add(CreateStats("Speed", 1));


        itemData.name = "AA";
        itemData.sort = "Equip";
        itemData.description = "This is Helmet";
        itemData.stats = statList.ToArray();

        string toJson = JsonUtility.ToJson(itemData);

        File.WriteAllText(Application.dataPath + "/Saves/data.json", toJson);

        Debug.Log(toJson);
    }


}
