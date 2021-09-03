﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ItemData
{
    // 생산자 : 이름이 클래스와 같은 매서드
    public ItemData(string _type, string _name, string _explain, string _number, bool _isUsing)
    {
        type = _type; name = _name; explain = _explain; number = _number; isUsing = _isUsing;
    }

    public string type, name, explain, number;
    public bool isUsing;
}

[System.Serializable]
public class Serialization<T1, T2>
{
    public Serialization(List<T1> p_One, List<T2> p_Two)
    {
        one = p_One;
        two = p_Two;
    }
    public List<T1> one;
    public List<T2> two;

}


public class DataBaseManager : MonoBehaviour
{
    [SerializeField] TextAsset itemDataBase;
    public List<ItemData> itemDataList, MyItemList = new List<ItemData>();

    [ContextMenu("Set Slot List")]
    void SetSlotData()
    {
        string[] line = itemDataBase.text.Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            itemDataList.Add(new ItemData(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }
    }

    private void Start()
    {
        SaveFile();
        LoadFile();
    }

    public List<string> names = new List<string>();
    public List<ItemData> datas = new List<ItemData>();
    public Dictionary<string, ItemData> dataDic = new Dictionary<string, ItemData>();

    void SaveFile()
    {
        names.Add("하!");
        names.Add("마!");
        datas.Add(itemDataList[0]);
        datas.Add(itemDataList[1]);

        //string jsonData = JsonUtility.ToJson(new Serialization<string, ItemData>(dataDic));

        string jsonData = JsonUtility.ToJson(new Serialization<string, ItemData>(names, datas), true);
        //for (int i = 0; i < itemDataList.Count; i++)
        //{
        //    jsonData +=  i + "name : " + JsonUtility.ToJson(itemDataList[i], true);
        //}
        string path = Path.Combine(Application.dataPath, "Resources", "Data", "Name.txt");
        File.WriteAllText(path, jsonData);

    }

    public List<string> dicList;

    void LoadFile()
    {
        string path = Path.Combine(Application.dataPath, "Resources", "Data", "Name.txt");
        string jsonData = File.ReadAllText(path);
        dicList = JsonUtility.FromJson<Serialization<string, ItemData>>(jsonData).one;
        MyItemList = JsonUtility.FromJson <Serialization<string, ItemData>> (jsonData).two;

        for(int i = 0; i < dicList.Count; i++)
        {
            dataDic.Add(dicList[i], MyItemList[i]);
        }

        foreach(string key in dataDic.Keys)
        {
            Debug.Log(key);
            Debug.Log(dataDic[key]);
        }
    }
}
