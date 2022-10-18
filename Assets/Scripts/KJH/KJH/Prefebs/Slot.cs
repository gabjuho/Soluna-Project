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
    public Item item;                   //���Կ� ����� �������� ����
    public Image itemIcon;              //�������� ������
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
    public AudioSource source;
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
        if (scene.name.Equals("1F Test"))
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
        if (scene.name.Equals("1F Test"))                           //1������ ���
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
        }
        #endregion

        #region 3F
        if(scene.name.Equals("3F"))
            puzzle3FManager = GameObject.Find("3F_Manager").GetComponent<Puzzle3FManager>();
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
    public void UpdateSlotUI()                    //�κ��丮�� �Ƹ����� �ش� ������ ���
    {                                             //
        itemIcon.sprite = item.itemImage;         //�������̹����� ���������� ����
        itemIcon.gameObject.SetActive(true);      //������ Ȱ��ȭ
        item_name.text = item.itemName;
    }
    public void RemoveSlot()                             //���� ����
    {
        item = null;                                     //�������� ����
        item_name.text = "";
        itemIcon.gameObject.SetActive(false);            //�������� ��Ȱ��ȭ
    }

    ItemType OnButtonDoubleClick()                              //����Ŭ�� ->�ش������ ���
    {
        Debug.Log("����Ŭ��");
        return item.itemType;

    }

    void OnButtonClick()                                   //Ŭ�� ->�ش������ ���
    {
        Debug.Log("Ŭ��");
        clickTime = Time.time;
        onClick = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (inven.items.Count > slotNum)
        {
            
            if (Mathf.Abs(Time.time - clickTime) < 0.3f)    //����Ŭ�� ->�ش������ ���
            {
                if (OnButtonDoubleClick() != ItemType.ErrorType)
                {
                    Debug.Log("Del");
                    #region 1F
                    if (scene.name.Equals("1F Test"))                   //1F Test�������� �۵�
                    {
                        switch (item.itemType)
                        {
                            case ItemType.Object_Gear:
                                if (gear.on_triger)
                                {
                                    if (gear.lozicClear == false)
                                    {
                                        //�����ų����
                                        //--------------------------
                                        
                                        gear.GearSetActive(true);
                                        gear.on_gear = true;
                                        FieldItem plant_item = planets[0].GetComponent<FieldItem>();
                                        inven.AddItem(plant_item.GetItem());
                                        sound.sources[0].clip = sound.effectSound[0];
                                        sound.sources[0].Play();
                                        hint.OffHintBtn();
                                        iD_Controll = GameObject.Find("Cylinder015").GetComponent<TextID_Controll>();
                                        iD_Controll.ChangeTxt(101);
                                        //--------------------------
                                        item.SendItem();                        //������ ���� �ѱ�� ������ Item
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
                                        //�����ų����
                                        //--------------------------
                                        steam.on_starFuel = true;
                                        FieldItem plant_item = planets[1].GetComponent<FieldItem>();
                                        inven.AddItem(plant_item.GetItem());
                                        Debug.Log("������ ���");
                                        hint.OffHintBtn();
                                        iD_Controll = GameObject.Find("Object043").GetComponent<TextID_Controll>();
                                        iD_Controll.ChangeTxt(122);
                                        //--------------------------
                                        item.SendItem();                        //������ ���� �ѱ�� ������ Item
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
                                        //�����ų����
                                        //--------------------------
                                        steam.on_toolBox = true;
                                        FieldItem plant_item = planets[2].GetComponent<FieldItem>();
                                        inven.AddItem(plant_item.GetItem());
                                        Debug.Log("�����ڽ� ���");
                                        hint.OffHintBtn();
                                        iD_Controll = GameObject.Find("Object043").GetComponent<TextID_Controll>();
                                        iD_Controll.ChangeTxt(123);
                                        TextID_Controll sundialiD_Controll;
                                        sundialiD_Controll = GameObject.Find("Sundial_Object").GetComponent<TextID_Controll>();
                                        sundialiD_Controll.ChangeTxt(131);
                                        //--------------------------
                                        item.SendItem();                        //������ ���� �ѱ�� ������ Item
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
                                        //�����ų����
                                        //--------------------------
                                        sundial.on_starPiece = true;
                                        FieldItem plant_item = planets[3].GetComponent<FieldItem>();
                                        inven.AddItem(plant_item.GetItem());
                                        Debug.Log("�ؽð� ���");
                                        hint.OffHintBtn();
                                        iD_Controll = GameObject.Find("Sundial_Object").GetComponent<TextID_Controll>();
                                        iD_Controll.ChangeTxt(132);
                                        TextID_Controll leveriD_Controll;
                                        leveriD_Controll = GameObject.Find("Object029").GetComponent<TextID_Controll>();
                                        leveriD_Controll.ChangeTxt(141);
                                        //--------------------------
                                        item.SendItem();                        //������ ���� �ѱ�� ������ Item
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
                                    Debug.Log("����å ���");
                                    
                                }
                                break;
                            case ItemType.Object_Book2:
                                if (ChangeTimeButton.isDay && RightBookUsingPoint.on_trigger)
                                {
                                    bool bookNothing = true;

                                    RightBookUsingPoint.isClear = true;

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
                                        HintStateManager.lastItem = item.itemName;
                                        hintArrow.SetActive(false);
                                        hintStateManager.ChangeTarget(HintStateManager.PuzzleState.BookGetting);
                                    }

                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("�ð�å ���");
                                }
                                break;
                            case ItemType.Object_Book3:
                                if (ChangeTimeButton.isDay && LeftBookUsingPoint.on_trigger)
                                {
                                    bool bookNothing = true;
                                    LeftBookUsingPoint.isClear = true;

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
                                        HintStateManager.lastItem = item.itemName;
                                        hintArrow.SetActive(false);
                                        hintStateManager.ChangeTarget(HintStateManager.PuzzleState.BookGetting);
                                    }

                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("���å ���");
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

                        if (LiftTrigger.onLift && puzzle3FManager.CheckPlanet(item.itemType)) //�´� �༺�̸� ������ ����
                        {
                            puzzle3FManager.ActivePlanet();
                            RemoveSlot();
                            Inventory.instance.RemoveItem(slotNum);
                        }
                        else if(LiftTrigger.onLift && !puzzle3FManager.CheckPlanet(item.itemType)) //Ʋ�� �༺�̸� ������ ����� �ȵǰ�, ���ƴ� ������ ���� ����
                        {
                            puzzle3FManager.ResetPlanet();
                        }
                    }
                    #endregion
                }
            }
            else                                            //Ŭ�� ->�ش������ ���
            {
                OnButtonClick();
            }
        }

    }

}
