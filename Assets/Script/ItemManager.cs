using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] InputField input_ItemName = null;
    [SerializeField] InputField input_ItemCount = null;
    [SerializeField] SlotManager slotManager = null;

    public void GetItemClcik() 
    {
        ItemData item = DataBaseManager.instance.MyItemList.Find(data => data.name == input_ItemName.text);
        if(item != null)
        {
            int newNumber = int.Parse(item.number) + int.Parse(input_ItemCount.text);
            item.number = newNumber.ToString();
        }
        else if(DataBaseManager.instance.itemDataList.Find(data => data.name == input_ItemName.text) != null)
        {
            item = DataBaseManager.instance.itemDataList.Find(data => data.name == input_ItemName.text);
            DataBaseManager.instance.MyItemList.Add(item);
            item.number = input_ItemCount.text;
        }

        slotManager.UpdateSlot();
        DataBaseManager.instance.SaveFile();
    }

    public void RemoveItemClcik()
    {
        ItemData item = DataBaseManager.instance.MyItemList.Find(data => data.name == input_ItemName.text);
        if (item != null)
        {
            int newNumber = int.Parse(item.number) - int.Parse(input_ItemCount.text);
            if (newNumber > 0) item.number = newNumber.ToString();
            else DataBaseManager.instance.MyItemList.Remove(item);

            slotManager.UpdateSlot();
            DataBaseManager.instance.SaveFile();
        }
    }
}
