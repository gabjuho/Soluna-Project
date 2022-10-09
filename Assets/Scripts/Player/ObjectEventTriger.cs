using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectEventTriger : MonoBehaviour
{
    public GameObject TrigerAbleUI;

    GameManager gameManager;
    EvenetSelection eventSelection;
    #region MikangMark
    Elevator elevator;
    public bool onTriger;
    public Inventory inventory;

    #endregion
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        #region MikangMark
        //elevator = GameObject.Find("ElevatorManager").GetComponent<Elevator>();
        onTriger = false;
        #endregion
    }

    private void Start()
    {

        TrigerAbleUI.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("EventTriger"))
        {
            //Ʈ���� UI�� ���ӿ�����Ʈ ���� ǥ�� && ���� ���� ��ȣ�ۿ� ������Ʈ�� ������ Ȱ��ȭ
            #region TrigrUI
            TrigerAbleUI.SetActive(true);

            TrigerAbleUI.transform.position = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0, 0.9f, 0));
            #endregion

            #region Triger
            if (Input.GetKey(KeyCode.E)) ClickTriger(other);
            #endregion

        }
        #region PuzzleTrigger
        if (other.gameObject.CompareTag("Item_Obj"))
        {
            TrigerAbleUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "E";
            TrigerAbleUI.SetActive(true);

            TrigerAbleUI.transform.position = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0, 0.9f, 0));

            if (Input.GetKey(KeyCode.E))
            {
                ClickTriger(other);
                TrigerAbleUI.SetActive(false);
            }
            else if((other.gameObject.name.Equals("Magic_Book") || other.gameObject.name.Equals("Clock_Book") || other.gameObject.name.Equals("Gear_Book")) && Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("��� ���");
            }
        }
        #endregion

        if (other.gameObject.CompareTag("Puzzle_Rock") && !ChangeTimeButton.isDay)
        {
            TrigerAbleUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Q";
            TrigerAbleUI.SetActive(true);

            TrigerAbleUI.transform.position = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0, 0.9f, 0));
            
            if (Input.GetKey(KeyCode.Q))
            {
                other.gameObject.GetComponent<RockSound>().sound.Play();
                ClickTriger(other);
                TrigerAbleUI.SetActive(false);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        #region Elevator
        if (other.gameObject.CompareTag("Elevator"))
        {

            GameObject.Find("ElevatorManager").GetComponent<Elevator>().ControllAnimation();
        }
        #endregion
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EventTriger"))
        {
            //Ʈ���� UI  ���� ���� ������ ��Ȱ��ȭ
            #region TrigrUI
            TrigerAbleUI.SetActive(false);
            #endregion
        }
        if (other.gameObject.CompareTag("Item_Obj"))
        {
            //Ʈ���� UI  ���� ���� ������ ��Ȱ��ȭ
            #region TrigrUI
            TrigerAbleUI.SetActive(false);
            #endregion
        }
        if(other.gameObject.CompareTag("Puzzle_Rock"))
        {
            #region TrigrUI
            TrigerAbleUI.SetActive(false);
            TrigerAbleUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "E";
            #endregion
        }
    }

    void ClickTriger(Collider other)
    {
        //���������̶�� ��ȯ.
        if (gameManager.isDelayOn == true) return;

        //���� �浹�� ������Ʈ�� �̺�Ʈ Ÿ���� �����ɴϴ�.
        eventSelection = other.gameObject.GetComponent<EvenetSelection>();

        //������ Ŭ���ϴ°� ���� ���ؼ� ������ �ֱ� [1.5��]
        StartCoroutine(gameManager.DelayTimer(1.5f));

        switch (eventSelection._eventType)
        {
            case EvenetSelection.EventType.First:
                Debug.Log("First");
                break;
            case EvenetSelection.EventType.Second:
                Debug.Log("Second");
                break;
            case EvenetSelection.EventType.Third:
                Debug.Log("Third");
                break;
            case EvenetSelection.EventType.Item:
                FieldItem fieldItems = other.GetComponent<FieldItem>();                //Ÿ�پ������� ������ ����
                if (inventory.AddItem(fieldItems.GetItem()))                            //�׾������� ���� ����� ����� true �ƴҽ� false
                {
                    fieldItems.DestroyItem();                                           //�ʵ��� �������� ����
                }
                Debug.Log("Gear");
                break;
            case EvenetSelection.EventType.Puzzle_Rock: //2�� 3��° ���� �� ��ȣ�ۿ� ��
                
                break;

        }

        Debug.Log("check");
    }
}