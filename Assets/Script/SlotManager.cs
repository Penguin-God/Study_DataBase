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

    public void TabClick(string tabName) // SetTab과 비슷함
    {
        currentTabName = tabName;
        // 일치하는 값뿐만 아니라 람다식을 사용해서 조건에 맞는 값들도 매서드를 통해 가져올 수 있음
        // 여기서 가져온 리스트는 같은 주소값을 참조함 (Return한 리스트의 값을 바꾸면 원본 리스트의 값도 바뀌고 반대도 됨)
        showItemList = DataBaseManager.instance.MyItemList.FindAll(itemData => itemData.type == tabName);

        for(int i = 0; i < showItemList.Count; i++)
        {
            slots[i].SetActive(true);
            slots[i].GetComponentInChildren<Text>().text = showItemList[i].name + "/" + showItemList[i].isUsing;
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

    public void ClickSlot(int number)
    {
        // 참조값이 같아 current_Item의 값 수정이 showItemList -> MyItemList로 전달됨 (new 쓰면 참조값이 달라져서 작동 안됨)
        ItemData current_Item = showItemList[number];
        ItemData using_Item = showItemList.Find(data => data.isUsing);

        if(currentTabName == "Character")
        {
            current_Item.isUsing = true;
            if(using_Item != null) using_Item.isUsing = false;
        }
        else
        {
            // 이 경우는 current_Item과 using_Item이 비슷한 ItemData일 수 있기 때문에 서순이 중요함 (current_Item을 false로 했다가 다시 !false로 해버림)
            current_Item.isUsing = !current_Item.isUsing;
            if (using_Item != null) using_Item.isUsing = false;
        }

        TabClick(currentTabName); // 변경사항 적용
    }
}
