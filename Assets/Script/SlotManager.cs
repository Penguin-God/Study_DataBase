using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public string currentTabName = "Character";
    public List<ItemData> showItemList = new List<ItemData>();

    [SerializeField] GameObject[] slots;
    private void Start()
    {
        TabClick(currentTabName);
    }

    public void TabClick(string tabName)
    {
        currentTabName = tabName;
        // 일치하는 값뿐만 아니라 람다식을 사용해서 조건에 맞는 값들도 매서드를 통해 가져올 수 있음
        showItemList = DataBaseManager.instance.MyItemList.FindAll(itemData => itemData.type == tabName);

        for(int i = 0; i < showItemList.Count; i++)
        {
            slots[i].SetActive(true);
            slots[i].GetComponentInChildren<Text>().text = showItemList[i].name;
        }

        SetTabSprite();
    }

    [SerializeField] Image[] tabImgs;
    [SerializeField] Sprite[] selectConditionSprites;
    void SetTabSprite()
    {
        int currentTabNumber = -1;
        switch (currentTabName)
        {
            case "Character": currentTabNumber = 0; break;
            case "Balloon": currentTabNumber = 1; break;
        }

        for (int i = 0; i < tabImgs.Length; i++)
        {
            tabImgs[i].sprite = (i == currentTabNumber) ? selectConditionSprites[1] : selectConditionSprites[0];
        }
    }
}
