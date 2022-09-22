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
    public Item item;                   //���Կ� ����� �������� ����
    public Image itemIcon;              //�������� ������
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
        if (scene.name.Equals("1F Test"))                           //1������ ���
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
    public void UpdateSlotUI()                    //�κ��丮�� �Ƹ����� �ش� ������ ���
    {                                             //
        itemIcon.sprite = item.itemImage;         //�������̹����� ���������� ����
        itemIcon.gameObject.SetActive(true);      //������ Ȱ��ȭ
    }
    public void RemoveSlot()                             //���� ����
    {
        item = null;                                     //�������� ����
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
                                        Debug.Log("������ ���");
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
                                    if (color_lozic.lozicClear && steam.lozicClear == false)
                                    {
                                        //�����ų����
                                        //--------------------------
                                        steam.on_toolBox = true;
                                        Debug.Log("�����ڽ� ���");
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
                                        Debug.Log("�ؽð� ���");
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
                                    MiddleBookUsingPoint.isClear = true;
                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("����å ���");
                                    RemoveSlot();
                                    Inventory.instance.RemoveItem(slotNum);
                                }
                                break;
                            case ItemType.Object_Book2:
                                if (ChangeTimeButton.isDay && RightBookUsingPoint.on_trigger)
                                {
                                    RightBookUsingPoint.isClear = true;
                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("�ð�å ���");
                                    RemoveSlot();
                                    Inventory.instance.RemoveItem(slotNum);
                                }
                                break;
                            case ItemType.Object_Book3:
                                if (ChangeTimeButton.isDay && LeftBookUsingPoint.on_trigger)
                                {
                                    LeftBookUsingPoint.isClear = true;
                                    SecondFloorManager.CheckBookPuzzleClear();
                                    Debug.Log("���å ���");
                                    RemoveSlot();
                                    Inventory.instance.RemoveItem(slotNum);
                                }
                                break;
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
