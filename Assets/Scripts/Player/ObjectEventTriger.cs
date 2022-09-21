using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //트리거 UI를 게임오브젝트 위에 표시 && 범위 내에 상호작용 오브젝트가 있을시 활성화
            #region TrigrUI
            TrigerAbleUI.SetActive(true);

            TrigerAbleUI.transform.position = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0, 0.9f, 0));
            #endregion

            #region Triger
            if (Input.GetKey(KeyCode.E)) ClickTriger(other);
            #endregion

        }
        #region MikangMark
        #region Item_Obj
        if (other.gameObject.CompareTag("Item_Obj"))
        {
            TrigerAbleUI.SetActive(true);

            TrigerAbleUI.transform.position = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0, 0.9f, 0));

            if (Input.GetKey(KeyCode.E)) 
            {
                ClickTriger(other);
                TrigerAbleUI.SetActive(false);
            } 


        }
        #endregion
        
        #endregion
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
            //트리거 UI  범위 에서 나가면 비활성화
            #region TrigrUI
            TrigerAbleUI.SetActive(false);
            #endregion
        }
        if (other.gameObject.CompareTag("Item_Obj"))
        {
            //트리거 UI  범위 에서 나가면 비활성화
            #region TrigrUI
            TrigerAbleUI.SetActive(false);
            #endregion
        }
    }

    void ClickTriger(Collider other)
    {
        //딜레이중이라면 반환.
        if (gameManager.isDelayOn == true) return;

        //현재 충돌한 오브젝트의 이벤트 타입을 가져옵니다.
        eventSelection = other.gameObject.GetComponent<EvenetSelection>();

        //여러번 클릭하는걸 막기 위해서 딜레이 주기 [1.5초]
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
                FieldItem fieldItems = other.GetComponent<FieldItem>();                //타겟아이템을 변수에 저장
                if (inventory.AddItem(fieldItems.GetItem()))                            //그아이템을 저장 제대로 저장시 true 아닐시 false
                {
                    fieldItems.DestroyItem();                                           //필드의 아이템을 제거
                }
                Debug.Log("Gear");
                break;

        }

        Debug.Log("check");
    }
}

