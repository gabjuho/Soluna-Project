using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public int slotNum;
    public Item item;                   //슬롯에 저장될 아이템의 정보
    public Image itemIcon;              //아이템의 아이콘
    public float clickTime = 0f;
    Inventory inven;

    public ObjectEventTriger E_Trigger;

    public GearLozic gear;


    private void Start()
    {
        E_Trigger = GameObject.Find("Player").GetComponentInChildren<ObjectEventTriger>();
        gear = GameObject.Find("Gear_Plane").GetComponent<GearLozic>();
        inven = Inventory.instance;
    }
    public void UpdateSlotUI()                    //인벤토리에 탬먹을시 해당 아이콘 출력
    {                                             //
        itemIcon.sprite = item.itemImage;         //아이템이미지를 아이콘으로 복사
        itemIcon.gameObject.SetActive(true);      //아이콘 활성화
    }
    public void RemoveSlot()                             //슬롯 제거
    {
        item = null;                                     //아이템을 비우고
        itemIcon.gameObject.SetActive(false);            //아이콘을 비활성화
    }

    ItemType OnButtonDoubleClick()                              //더블클릭 ->해당아이템 사용
    {
        Debug.Log("더블클릭");
        return item.itemType;

    }

    void OnButtonClick()                                   //클릭 ->해당아이템 들기
    {
        Debug.Log("클릭");
        clickTime = Time.time;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (inven.items.Count > slotNum)
        {
            
            if (Mathf.Abs(Time.time - clickTime) < 0.3f)    //더블클릭 ->해당아이템 사용
            {
                if (OnButtonDoubleClick() != ItemType.ErrorType)
                {
                    Debug.Log("Del");
                    switch (item.itemType)
                    {
                        case ItemType.Object_Gear:
                            if (gear.on_triger)
                            {
                                gear.gearObj.SetActive(true);
                                item.SendItem();                        //아이템 정보 넘기기 리턴형 Item
                                RemoveSlot();
                                Inventory.instance.RemoveItem(slotNum);
                                clickTime = -1;
                            }
                            //톱니바퀴 넣은뒤 할행동
                            
                            break;
                    }
                    
                    
                }
                #region by Triglavian
                /*
                //Debug.Log($"{OnButtonDoubleClick() != ItemType.ErrorType}, {E_Trigger.IsValidPuzzle()}, { E_Trigger.IsSameType(item.itemType)}");
                if (OnButtonDoubleClick() != ItemType.ErrorType)// && E_Trigger.IsValidPuzzle() && E_Trigger.IsSameType(item.itemType)
                {
                    //E_Trigger.ActiveKeyObject();
                    //item.SendItem();                        //아이템 정보 넘기기 리턴형 Item
                    RemoveSlot();
                    Inventory.instance.RemoveItem(slotNum);
                    //E_Trigger.currentKeyObject = null;
                    clickTime = -1;
                }
                */
                #endregion
            }
            else                                            //클릭 ->해당아이템 들기
            {
                OnButtonClick();
            }
        }

    }

}
