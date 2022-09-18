using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;                                               //
    public GameObject inventoryPanel;                              //
    public bool isInventory;                              //인벤토리 활성화/비활성화 관리하는 변수

    public Slot[] slots;                                           //슬롯 배열
    public Transform slotHolder;

    private void Start()
    {
        isInventory = true;
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange = SlotChange;                  //슬롯 갯수 인게임에서 변화필요하면 사용
        inven.onChageItem += RedrawSlotUI;                        //슬롯 그리기
        inventoryPanel.SetActive(isInventory);                    //슬롯 비활성화
        for (int i = 0; i < slots.Length; i++)                    //모든 슬롯 이미지 비우기
        {
            slots[i].RemoveSlot();
        }
    }

    private void FixedUpdate()
    {
        RedrawSlotUI();
    }

    private void SlotChange(int val)                                          
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;
            if (i < inven.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    /*void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))                           //탭키 눌르면 인벤토리 열고 닫기
        {
            isInventory = !isInventory;
            inventoryPanel.SetActive(isInventory);
        }
        
    }*/

    void RedrawSlotUI()                                              //인벤토리 UI 그리기
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();                                   //모든 슬롯 지우기
        }
        for(int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];                          //인벤의 아이템을 가져와서 슬롯에 저장
            slots[i].UpdateSlotUI();                                 //그아이템의 스프라이트를 슬롯 이미지에 저장
        }
    }

}
