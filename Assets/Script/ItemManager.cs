using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] InputField input_ItemName = null;
    [SerializeField] InputField input_ItemCount = null;
    [SerializeField] SlotManager slotManager = null;

    string addItemCountText
    {
        get
        {
            return (input_ItemCount.text == "") ? "1" : input_ItemCount.text;
        }
    }

    void UpdateItem()
    {
        SortList();
        slotManager.UpdateSlot();
        DataBaseManager.instance.SaveFile();
    }

    public void GetItemClcik() 
    {
        ItemData item = DataBaseManager.instance.MyItemList.Find(data => data.name == input_ItemName.text);
        if (item != null) // MyItemList에 없는 경우 MyItemList에 Add해야하기 때문에 조건문 사용
        {
            int newNumber = int.Parse(item.number) + int.Parse(addItemCountText);
            item.number = newNumber.ToString();
        }
        else if(DataBaseManager.instance.itemDataList.Find(data => data.name == input_ItemName.text) != null)
        {
            item = DataBaseManager.instance.itemDataList.Find(data => data.name == input_ItemName.text);
            DataBaseManager.instance.MyItemList.Add(item); 
            item.number = addItemCountText;
        }
        UpdateItem();
    }

    public void RemoveItemClcik()
    {
        ItemData item = DataBaseManager.instance.MyItemList.Find(data => data.name == input_ItemName.text);
        if (item != null)
        {
            int newNumber = int.Parse(item.number) - int.Parse(addItemCountText);
            if (newNumber > 0) item.number = newNumber.ToString();
            else DataBaseManager.instance.MyItemList.Remove(item);

            UpdateItem();
        }
    }

    public void ResetItemClick()
    {
        ItemData defaultItem = DataBaseManager.instance.itemDataList.Find(data => data.name == "Pig");
        DataBaseManager.instance.MyItemList = new List<ItemData>() { defaultItem };
        slotManager.UpdateSlot();
        DataBaseManager.instance.SaveFile();
    }

    void SortList()
    {
        // 좀 찾아봤는데 그냥 공식처럼 사용하는듯 C#에 대해서 공부해야지 알 수 있을 듯
        DataBaseManager.instance.MyItemList.Sort((p1, p2) => p1.index.CompareTo(p2.index));
    }
}
