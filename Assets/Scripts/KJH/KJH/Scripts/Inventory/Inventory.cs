using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChageItem();
    public OnChageItem onChageItem;

    public List<Item> items = new List<Item>();                        //아이템을 저장시킬 리스트

    private int slotCnt;                                               //슬롯 갯수
    public int SlotCnt
    {
        get => slotCnt;
        set{
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }
    void Start()
    {
        slotCnt = 10;
        onSlotCountChange.Invoke(slotCnt);
    }
    public bool AddItem(Item _item)                                    //아이템 추가
    {
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            if (onChageItem != null)
            {
                onChageItem.Invoke();
            }

            return true;
        }
        return false;
    }
    public void RemoveItem(int index)
    {
        if(items.Count > 0)
        {
            Debug.Log("Del");
            items.RemoveAt(index);
            onSlotCountChange.Invoke(slotCnt);
        }
        
    }

}
