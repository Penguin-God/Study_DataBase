using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public string currentTabName = "Character";
    public List<ItemData> currentItemList = new List<ItemData>();

    [SerializeField] GameObject[] slots = null;
    private void Start()
    {
        TabClick(currentTabName);
    }

    public void TabClick(string tabName) // SetTab과 비슷함
    {
        currentTabName = tabName;
        // 일치하는 값뿐만 아니라 람다식을 사용해서 조건에 맞는 값들도 매서드를 통해 가져올 수 있음
        // 여기서 가져온 리스트는 같은 주소값을 참조함 (Return한 리스트의 값을 바꾸면 원본 리스트의 값도 바뀌고 반대도 됨)
        currentItemList = DataBaseManager.instance.MyItemList.FindAll(itemData => itemData.type == tabName);

        UpdateSlot();
        SetTabSprite();
    }



    [SerializeField] Image[] tabImgs = null;
    [SerializeField] Sprite[] selectConditionSprites = null;
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
        // 참조값이 같아 current_Item의 값 수정이 currentItemList -> MyItemList로 전달됨 (new 쓰면 참조값이 달라져서 작동 안됨)
        ItemData current_Item = currentItemList[number];
        ItemData using_Item = currentItemList.Find(data => data.isUsing);

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

        UpdateSlot();
    }

    [SerializeField] Sprite[] arr_ItemSprite = null;
    [SerializeField] GameObject[] arr_UsingImage = null;
    [SerializeField] Image[] arr_ItemImage = null;
    public void UpdateSlot()
    {
        for (int i = 0; i < currentItemList.Count; i++)
        {
            slots[i].SetActive(true);
            arr_UsingImage[i].SetActive(currentItemList[i].isUsing);
            slots[i].GetComponentInChildren<Text>().text = currentItemList[i].name;
            // MyItemList에서 인덱스를 이미지에 해당하는 인덱스를 찾아 MyItemList과 크기가 같은 스프라이트 배열에 대입
            arr_ItemImage[i].sprite = arr_ItemSprite[DataBaseManager.instance.MyItemList.FindIndex(data => data.name == currentItemList[i].name)];
        }
    }

    [Space] [Space]
    [SerializeField] RectTransform canvasRect = null;
    [SerializeField] GameObject explainPanel = null;
    [SerializeField] Vector2 distance_PanelBetweenMouse = Vector2.zero;
    Coroutine Co_PointerEnter = null;
    public void PointerEnter(int number) => Co_PointerEnter = StartCoroutine(PointerEnterDelay(number));

    IEnumerator PointerEnterDelay(int number)
    {
        yield return new WaitForSeconds(0.1f);
        SetPanel(number);
        explainPanel.SetActive(true);

        RectTransform panelRect = explainPanel.GetComponent<RectTransform>();
        while (true)
        {
            // 마우스 포지션에 사용되는 좌표계( (0,0)부터 해상도 크기까지 값을 가지는 사각형)를 Canvas좌표계(가운데가 (0,0)인 사각형 좌표계)로 바꾸는 작업
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, Camera.main, out Vector2 panelPos);
            panelRect.anchoredPosition = panelPos + distance_PanelBetweenMouse;
            yield return null;
        }
    }


    [SerializeField] Text txt_ItemName = null;
    [SerializeField] Text txt_ItemCount= null;
    [SerializeField] Text txt_ItemExplain = null;
    [SerializeField] Image img_Panel = null;
    void SetPanel(int slotNum)
    {
        txt_ItemName.text = currentItemList[slotNum].name;
        txt_ItemCount.text = currentItemList[slotNum].number + "개";
        txt_ItemExplain.text = currentItemList[slotNum].explain;
        img_Panel.sprite = arr_ItemSprite[DataBaseManager.instance.MyItemList.FindIndex(data => data.name == currentItemList[slotNum].name)];

    }

    public void PointerEXit(int number)
    {
        StopCoroutine(Co_PointerEnter);
        explainPanel.SetActive(false);
    }

}