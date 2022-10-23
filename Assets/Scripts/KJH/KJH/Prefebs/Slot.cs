using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    Scene scene;

    public int slotNum;
    public Item item;                   //슬롯에 저장될 아이템의 정보
    public Image itemIcon;              //아이템의 아이콘
    public float clickTime = 0f;
    Inventory inven;
    [SerializeField]
    TextMeshProUGUI item_name;

    public ObjectEventTriger E_Trigger;

    #region 1F
    GearLozic gear;
    ColorButtonLozic color_lozic;
    SteamLozic steam;
    SundialLozic sundial;

    public GameObject[] planets;
    string[] planets_name;

    public SoundManager sound;
    [SerializeField]
    HintManager hint;

    [SerializeField]
    Sprite[] backImg;

    bool onClick;

    TextID_Controll iD_Controll;
    #endregion
    #region 2F
    Book_Puzzle RightBookUsingPoint;
    Book_Puzzle LeftBookUsingPoint;
    Book_Puzzle MiddleBookUsingPoint;
    HintStateManager hintStateManager;
    InventoryUI inventoryUI;
    public GameObject hintArrow;
    #endregion
    #region 3F
    Puzzle3FManager puzzle3FManager;
    #endregion
    private void Awake()
    {
        onClick = false;
        scene = SceneManager.GetActiveScene();
        #region 1F
        if (scene.name.Equals("1F"))
        {
            planets = new GameObject[6];
            planets_name = new string[6];
            planets_name[0] = "Sun_Obj";
            planets_name[1] = "Mercury_Obj";
            planets_name[2] = "Venus_Obj";
            planets_name[3] = "Earth_Obj";
            planets_name[4] = "Mars_Obj";
            planets_name[5] = "Jupiter_Obj";
        }
        #endregion
    }

    private void Start()
    {
        E_Trigger = GameObject.Find("Player").GetComponentInChildren<ObjectEventTriger>();
        #region 1F
        if (scene.name.Equals("1F"))                           //1층에서 사용
        {
            gear = GameObject.Find("Gear_Plane").GetComponent<GearLozic>();
            color_lozic = GameObject.Find("Lozic").GetComponent<ColorButtonLozic>();
            steam = GameObject.Find("Steam_Plane").GetComponent<SteamLozic>();
            sundial = GameObject.Find("Sundial_Object").GetComponent<SundialLozic>();
            sound = GameObject.Find("Ui_Manager").GetComponent<SoundManager>();
            hint = GameObject.Find("Hint").GetComponent<HintManager>();
            for (int i = 0; i < planets.Length; i++)
            {
                planets[i] = GameObject.Find(planets_name[i]);
            }
            
        }
        #endregion

        #region 2F
        if(scene.name.Equals("2F"))
        {
            RightBookUsingPoint = GameObject.Find("RightBookUsingPoint").GetComponent<Book_Puzzle>();
            LeftBookUsingPoint = GameObject.Find("LeftBookUsingPoint").GetComponent<Book_Puzzle>();
            MiddleBookUsingPoint = GameObject.Find("MiddleBookUsingPoint").GetComponent<Book_Puzzle>();
            hintStateManager = GameObject.Find("2F_Hint_State_Manager").GetComponent<HintStateManager>();
            inventoryUI = GameObject.Find("InventoryCanvas").GetComponent<InventoryUI>();
        }
        #endregion

        #region 3F
        if (scene.name.Equals("3F"))
        {
            puzzle3FManager = GameObject.Find("3F_Manager").GetComponent<Puzzle3FManager>();
            inventoryUI = GameObject.Find("InventoryCanvas").GetComponent<InventoryUI>();
        }
        #endregion
        inven = Inventory.instance;
    }
    private void Update()
    {
        if (inven.items.Count > slotNum)
        {
            if (onClick == false)
            {
                gameObject.GetComponent<Image>().sprite = backImg[1];
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = backImg[0];
            }
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = backImg[2];
        }
    }
    public void UpdateSlotUI()                    //인벤토리에 탬먹을시 해당 아이콘 출력
    {                                             //
        itemIcon.sprite = item.itemImage;         //아이템이미지를 아이콘으로 복사
        itemIcon.gameObject.SetActive(true);      //아이콘 활성화
        item_name.text = item.itemName;
    }
    public void RemoveSlot()                             //슬롯 제거
    {
        item = null;                                     //아이템을 비우고
        item_name.text = "";
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
        onClick = true;
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
                    if (scene.name.Equals("1F"))                   //1F씬에서만 작동
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
                                        Debug.Log("Gear");
                                        gear.GearSetActive(true);
                                        gear.on_gear = true;
                                        FieldItem plant_item = planets[0].GetComponent<FieldItem>();
                                        inven.AddItem(plant_item.GetItem());
                                        
                                        hint.OffHintBtn();
                                        iD_Controll = GameObject.Find("Cylinder015").GetComponent<TextID_Controll>();
                                        iD_Controll.ChangeTxt(101);
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
                                        FieldItem plant_item = planets[1].GetComponent<FieldItem>();
                                        inven.AddItem(plant_item.GetItem());
                                        Debug.Log("별연료 사용");
                                        hint.OffHintBtn();
                                        iD_Controll = GameObject.Find("Object043").GetComponent<TextID_Controll>();
                                        iD_Controll.ChangeTxt(122);
                                        sound.SoundPlay(4, 5);
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
                                    if (steam.on_starFuel && color_lozic.lozicClear && steam.lozicClear == false)
                                    {
                                        //실행시킬내용
                                        //--------------------------
                                        steam.on_toolBox = true;
                                        FieldItem plant_item = planets[2].GetComponent<FieldItem>();
                                        inven.AddItem(plant_item.GetItem());
                                        Debug.Log("공구박스 사용");
                                        hint.OffHintBtn();
                                        iD_Controll = GameObject.Find("Object043").GetComponent<TextID_Controll>();
                                        iD_Controll.ChangeTxt(123);
                                        TextID_Controll sundialiD_Controll;
                                        sundialiD_Controll = GameObject.Find("Sundial_Object").GetComponent<TextID_Controll>();
                                        sundialiD_Controll.ChangeTxt(131);
                                        sound.SoundPlay(4, 7);
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
                                        FieldItem plant_item = planets[3].GetComponent<FieldItem>();
                                        inven.AddItem(plant_item.GetItem());
                                        Debug.Log("해시계 사용");
                                        hint.OffHintBtn();
                                        iD_Controll = GameObject.Find("Sundial_Object").GetComponent<TextID_Controll>();
                                        iD_Controll.ChangeTxt(132);
                                        TextID_Controll leveriD_Controll;
                                        leveriD_Controll = GameObject.Find("Object029").GetComponent<TextID_Controll>();
                                        leveriD_Controll.ChangeTxt(141);
                                        sound.SoundPlay(5, 8);
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
                                    bool bookNothing = true;

                                    MiddleBookUsingPoint.isClear = true;
                                    GameObject.Find("Middle_Shelf").GetComponent<EvenetSelection>().ChangeID(163);

                                    //책 사용 시 힌트 효과 없애기
                                    for (int i=0;i<inventoryUI.slots.Length;i++)
                                        if (inventoryUI.slots[i].transform.GetChild(2).gameObject.activeSelf)
                                            inventoryUI.slots[i].transform.GetChild(2).gameObject.SetActive(false);

                                    RemoveSlot();
                                    Inventory.instance.RemoveItem(slotNum);

                                    for (int i = 0; i < Inventory.instance.items.Count; i++)
                                    {
                                        if (Inventory.instance.items[i].itemType == ItemType.Object_Book1 || Inventory.instance.items[i].itemType == ItemType.Object_Book2 || Inventory.instance.items[i].itemType == ItemType.Object_Book3)
                                        {
                                            bookNothing = false;
                                            HintStateManager.lastItem = Inventory.instance.items[i].itemName;
                                        }
                                    }

                                    if (bookNothing)
                                    {
                                        hintArrow.SetActive(false);
                                        HintStateManager.ChangePuzzleState(HintStateManager.PuzzleState.BookNothing);
                                        hintStateManager.ChangeTarget(HintStateManager.PuzzleState.BookNothing);
                                    }
                                    else
                                    {
                                        hintArrow.SetActive(false);
                                        hintStateManager.ChangeTarget(HintStateManager.PuzzleState.BookGetting);
                                    }

                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("마법책 사용");
                                    
                                }
                                else if(ChangeTimeButton.isDay && !MiddleBookUsingPoint.on_trigger)
                                {
                                    GameObject.Find("ObjectTriger").GetComponent<ObjectEventTriger>().GetText(171, true);
                                    PlayerMoveDetector.isItemUse = true;
                                }
                                break;
                            case ItemType.Object_Book2:
                                if (ChangeTimeButton.isDay && RightBookUsingPoint.on_trigger)
                                {
                                    bool bookNothing = true;

                                    RightBookUsingPoint.isClear = true;
                                    GameObject.Find("Right_Shelf").GetComponent<EvenetSelection>().ChangeID(167);

                                    //책 사용 시 힌트 효과 없애기
                                    for (int i = 0; i < inventoryUI.slots.Length; i++)
                                        if (inventoryUI.slots[i].transform.GetChild(2).gameObject.activeSelf)
                                            inventoryUI.slots[i].transform.GetChild(2).gameObject.SetActive(false);

                                    RemoveSlot();
                                    Inventory.instance.RemoveItem(slotNum);

                                    for (int i = 0; i < Inventory.instance.items.Count; i++)
                                    {
                                        if (Inventory.instance.items[i].itemType == ItemType.Object_Book1 || Inventory.instance.items[i].itemType == ItemType.Object_Book2 || Inventory.instance.items[i].itemType == ItemType.Object_Book3)
                                        {
                                            bookNothing = false;
                                            HintStateManager.lastItem = Inventory.instance.items[i].itemName;
                                        }
                                    }

                                    if (bookNothing)
                                    {
                                        hintArrow.SetActive(false);
                                        HintStateManager.ChangePuzzleState(HintStateManager.PuzzleState.BookNothing);
                                        hintStateManager.ChangeTarget(HintStateManager.PuzzleState.BookNothing);
                                    }
                                    else
                                    {
                                        hintArrow.SetActive(false);
                                        hintStateManager.ChangeTarget(HintStateManager.PuzzleState.BookGetting);
                                    }

                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("시계책 사용");
                                }
                                else if (ChangeTimeButton.isDay && !RightBookUsingPoint.on_trigger)
                                {
                                    GameObject.Find("ObjectTriger").GetComponent<ObjectEventTriger>().GetText(174, true);
                                    PlayerMoveDetector.isItemUse = true;
                                }
                                break;
                            case ItemType.Object_Book3:
                                if (ChangeTimeButton.isDay && LeftBookUsingPoint.on_trigger)
                                {
                                    bool bookNothing = true;
                                    LeftBookUsingPoint.isClear = true;
                                    GameObject.Find("Left_Shelf").GetComponent<EvenetSelection>().ChangeID(167);

                                    //책 사용 시 힌트 효과 없애기
                                    for (int i = 0; i < inventoryUI.slots.Length; i++)
                                        if (inventoryUI.slots[i].transform.GetChild(2).gameObject.activeSelf)
                                            inventoryUI.slots[i].transform.GetChild(2).gameObject.SetActive(false);

                                    RemoveSlot();
                                    Inventory.instance.RemoveItem(slotNum);

                                    for (int i = 0; i < Inventory.instance.items.Count; i++)
                                    {
                                        if (Inventory.instance.items[i].itemType == ItemType.Object_Book1 || Inventory.instance.items[i].itemType == ItemType.Object_Book2 || Inventory.instance.items[i].itemType == ItemType.Object_Book3)
                                        {
                                            bookNothing = false;
                                            HintStateManager.lastItem = Inventory.instance.items[i].itemName;
                                        }
                                    }

                                    if (bookNothing)
                                    {
                                        hintArrow.SetActive(false);
                                        HintStateManager.ChangePuzzleState(HintStateManager.PuzzleState.BookNothing);
                                        hintStateManager.ChangeTarget(HintStateManager.PuzzleState.BookNothing);
                                    }
                                    else
                                    {
                                        hintArrow.SetActive(false);
                                        hintStateManager.ChangeTarget(HintStateManager.PuzzleState.BookGetting);
                                    }

                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("톱니책 사용");
                                }
                                else if (ChangeTimeButton.isDay && !LeftBookUsingPoint.on_trigger)
                                {
                                    GameObject.Find("ObjectTriger").GetComponent<ObjectEventTriger>().GetText(177, true);
                                    PlayerMoveDetector.isItemUse = true;
                                }
                                break;
                        }
                    }
                    #endregion
                    #region 3F
                    if (scene.name.Equals("3F"))
                    {
                        if (puzzle3FManager.isClear)
                            return;

                        if (LiftTrigger.onLift && puzzle3FManager.CheckPlanet(item.itemType)) //맞는 행성이면 아이템 사용됨
                        {
                            puzzle3FManager.ActivePlanet();
                            RemoveSlot();
                            Inventory.instance.RemoveItem(slotNum);
                            for (int i = 0; i < inventoryUI.slots.Length; i++)
                                if (inventoryUI.slots[i].transform.GetChild(2).gameObject.activeSelf)
                                    inventoryUI.slots[i].transform.GetChild(2).gameObject.SetActive(false);
                        }
                        else if(LiftTrigger.onLift && !puzzle3FManager.CheckPlanet(item.itemType)) //틀린 행성이면 아이템 사용이 안되고, 사용됐던 아이템 원상 복귀
                        {
                            for (int i = 0; i < inventoryUI.slots.Length; i++)
                                if (inventoryUI.slots[i].transform.GetChild(2).gameObject.activeSelf)
                                    inventoryUI.slots[i].transform.GetChild(2).gameObject.SetActive(false);
                            puzzle3FManager.ResetPlanet();
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
