using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    Scene scene;

    public int slotNum;
    public Item item;                   //슬롯에 저장될 아이템의 정보
    public Image itemIcon;              //아이템의 아이콘
    public float clickTime = 0f;
    Inventory inven;

    public ObjectEventTriger E_Trigger;

    #region 1F
    GearLozic gear;
    ColorButtonLozic color_lozic;
    SteamLozic steam;
    SundialLozic sundial;
    #endregion
    #region 2F
    Book_Puzzle RightBookUsingPoint;
    Book_Puzzle LeftBookUsingPoint;
    Book_Puzzle MiddleBookUsingPoint;
    #endregion
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        E_Trigger = GameObject.Find("Player").GetComponentInChildren<ObjectEventTriger>();
        #region 1F
        if (scene.name.Equals("1F Test"))                           //1층에서 사용
        {
            gear = GameObject.Find("Gear_Plane").GetComponent<GearLozic>();
            color_lozic = GameObject.Find("Lozic").GetComponent<ColorButtonLozic>();
            steam = GameObject.Find("Steam_Plane").GetComponent<SteamLozic>();
            sundial = GameObject.Find("Sundial_Object").GetComponent<SundialLozic>();
        }
        #endregion

        #region 2F
        if(scene.name.Equals("2F"))
        {
            RightBookUsingPoint = GameObject.Find("RightBookUsingPoint").GetComponent<Book_Puzzle>();
            LeftBookUsingPoint = GameObject.Find("LeftBookUsingPoint").GetComponent<Book_Puzzle>();
            MiddleBookUsingPoint = GameObject.Find("MiddleBookUsingPoint").GetComponent<Book_Puzzle>();
        }
        #endregion

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
                    #region 1F
                    if (scene.name.Equals("1F Test"))                   //1F Test씬에서만 작동
                    {
                        switch (item.itemType)
                        {
                            case ItemType.Object_Gear:
                                if (gear.on_triger)
                                {
                                    if (gear.lozicClear == false)
                                    {
                                        //실행시킬내용
                                        //--------------------------
                                        gear.GearSetActive(true);
                                        gear.on_gear = true;
                                        //--------------------------
                                        item.SendItem();                        //아이템 정보 넘기기 리턴형 Item
                                        RemoveSlot();
                                        Inventory.instance.RemoveItem(slotNum);
                                        clickTime = -1;
                                    }

                                }
                                break;
                            case ItemType.Object_Fuel:
                                if (steam.on_triger)
                                {
                                    if (color_lozic.lozicClear && steam.lozicClear == false)
                                    {
                                        //실행시킬내용
                                        //--------------------------
                                        steam.on_starFuel = true;
                                        Debug.Log("별연료 사용");
                                        //--------------------------
                                        item.SendItem();                        //아이템 정보 넘기기 리턴형 Item
                                        RemoveSlot();
                                        Inventory.instance.RemoveItem(slotNum);
                                        clickTime = -1;
                                    }

                                }
                                break;
                            case ItemType.Object_ToolBox:
                                if (steam.on_triger)
                                {
                                    if (color_lozic.lozicClear && steam.lozicClear == false)
                                    {
                                        //실행시킬내용
                                        //--------------------------
                                        steam.on_toolBox = true;
                                        Debug.Log("공구박스 사용");
                                        //--------------------------
                                        item.SendItem();                        //아이템 정보 넘기기 리턴형 Item
                                        RemoveSlot();
                                        Inventory.instance.RemoveItem(slotNum);
                                        clickTime = -1;
                                    }

                                }
                                break;
                            case ItemType.Object_StarPiece:
                                if (sundial.on_triger)
                                {
                                    if (steam.lozicClear && sundial.lozicClear == false)
                                    {
                                        //실행시킬내용
                                        //--------------------------
                                        sundial.on_starPiece = true;
                                        Debug.Log("해시계 사용");
                                        //--------------------------
                                        item.SendItem();                        //아이템 정보 넘기기 리턴형 Item
                                        RemoveSlot();
                                        Inventory.instance.RemoveItem(slotNum);
                                        clickTime = -1;
                                    }

                                }
                                break;
                        }
                    }
                    #endregion
                    #region 2F
                    if(scene.name.Equals("2F"))
                    {
                        switch(item.itemType)
                        {
                            case ItemType.Object_Book1:
                                if(ChangeTimeButton.isDay && MiddleBookUsingPoint.on_trigger)
                                {
                                    MiddleBookUsingPoint.isClear = true;
                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("마법책 사용");
                                    RemoveSlot();
                                    Inventory.instance.RemoveItem(slotNum);
                                }
                                break;
                            case ItemType.Object_Book2:
                                if (ChangeTimeButton.isDay && RightBookUsingPoint.on_trigger)
                                {
                                    RightBookUsingPoint.isClear = true;
                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("시계책 사용");
                                    RemoveSlot();
                                    Inventory.instance.RemoveItem(slotNum);
                                }
                                break;
                            case ItemType.Object_Book3:
                                if (ChangeTimeButton.isDay && LeftBookUsingPoint.on_trigger)
                                {
                                    LeftBookUsingPoint.isClear = true;
                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("톱니책 사용");
                                    RemoveSlot();
                                    Inventory.instance.RemoveItem(slotNum);
                                }
                                break;
                        }
                    }
                    #endregion
                }
            }
            else                                            //클릭 ->해당아이템 들기
            {
                OnButtonClick();
            }
        }

    }

}
