using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectEventTriger : MonoBehaviour
{
    public GameObject TrigerAbleUI;
    public GameObject InteractionUI;
    public GameObject hintArrow;
    HintStateManager hintStateManager;
    InventoryUI inventoryUI;
    #region 3F
    GameObject mainCamera; //플레이어 카메라
    #endregion

    GameManager gameManager;

    static public EvenetSelection eventSelection;
    TextManager textmanager;
    int talkindex;
    #region MikangMark
    Elevator elevator;
    public bool onTriger;
    public Inventory inventory;


    LeverLozic lever;
    #endregion
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        textmanager = GameObject.Find("TextManager").GetComponent<TextManager>();
        #region MikangMark
        //elevator = GameObject.Find("ElevatorManager").GetComponent<Elevator>();
        onTriger = false;
        #endregion

        //2층 힌트 스테이트 매니저 초기화
        #region 2F
        if (SceneManager.GetActiveScene().name.Equals("2F"))
        {
            inventoryUI = GameObject.Find("InventoryCanvas").GetComponent<InventoryUI>();
            hintStateManager = GameObject.Find("2F_Hint_State_Manager").GetComponent<HintStateManager>();
            mainCamera = GameObject.Find("CM_PlayerFollowCamera");
        }
        #endregion
        //3층 카메라 오브젝트 초기화
        #region 3F
        if (SceneManager.GetActiveScene().name.Equals("3F"))
            mainCamera = GameObject.Find("CM_PlayerFollowCamera");
        #endregion
    }

    private void Start()
    {
        TrigerAbleUI.SetActive(false);
        InteractionUI.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("EventTriger"))
        {
            //트리거 UI를 게임오브젝트 위에 표시 && 범위 내에 상호작용 오브젝트가 있을시 활성화
            #region TrigrUI
            TrigerAbleUI.SetActive(true);
            Debug.Log("aa");
            TrigerAbleUI.transform.position = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0, 0.9f, 0));
            #endregion

            #region Triger
            if (Input.GetKey(KeyCode.E)) ClickTriger(other);
            #endregion

        }
        #region PuzzleTrigger
        if (other.gameObject.CompareTag("Item_Obj"))
        {
            InteractionUI.SetActive(true);
            TrigerAbleUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "E";
            //TrigerAbleUI.SetActive(true);
            InteractionUI.transform.position = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0, 0.9f, 0));

            if (Input.GetKey(KeyCode.Q))
            {
                ClickTriger(other);

                //2층 책에 Q 클릭 시 실행 내용
                #region 2F
                InteractionUI.SetActive(false);
                if (other.gameObject.name.Equals("Magic_Book") || other.gameObject.name.Equals("Clock_Book") || other.gameObject.name.Equals("Gear_Book"))
                {
                    HintStateManager.ChangePuzzleState(HintStateManager.PuzzleState.BookGetting); //힌트 책 가진 상태 변경
                    HintStateManager.lastItem = other.gameObject.name;
                    hintStateManager.ChangeTarget(HintStateManager.PuzzleState.BookGetting);
                    hintArrow.SetActive(false);
                    for (int i = 0; i < inventoryUI.slots.Length; i++)
                        if (inventoryUI.slots[i].transform.GetChild(2).gameObject.activeSelf)
                            inventoryUI.slots[i].transform.GetChild(2).gameObject.SetActive(false);
                }
                #endregion

                TrigerAbleUI.SetActive(false);

            }
            //2층 책에 E 클릭 시 대사 출력
            #region 2F
            else if (Input.GetKeyDown(KeyCode.E) && (other.gameObject.name.Equals("Magic_Book") || other.gameObject.name.Equals("Clock_Book") || other.gameObject.name.Equals("Gear_Book")))
            {
                GetText(other.GetComponent<EvenetSelection>().ID, other.GetComponent<EvenetSelection>().isCharaTalk);
                Debug.Log("대사 출력");
            }
            #endregion
        }
        #endregion

        //2층 돌 퍼즐에 돌 Q 클릭 시 음악 출력
        #region 2F
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
        #endregion

        if (other.gameObject.CompareTag("Globe"))
        {
            #region TrigrUI
            TrigerAbleUI.SetActive(true);

            TrigerAbleUI.transform.position = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0, 0.9f, 0));
            #endregion

            #region Triger
            if (Input.GetKey(KeyCode.E)) ClickTriger(other);
            #endregion
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
        if (other.gameObject.CompareTag("NextStage"))
        {
            lever = GameObject.Find("Lozic").GetComponent<LeverLozic>();
            if (lever.lozicClear)
            {
                Debug.Log("다음씬으로 이동");
            }

            //SceneManager.LoadScene("2F");
        }
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
            #region InteractionUI
            InteractionUI.SetActive(false);
            #endregion

            #region TrigrUI
            TrigerAbleUI.SetActive(false);
            #endregion
        }
        if (other.gameObject.CompareTag("Puzzle_Rock"))
        {
            #region TrigrUI
            TrigerAbleUI.SetActive(false);
            TrigerAbleUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "E";
            #endregion
        }
        if (other.gameObject.CompareTag("Globe")) //지구본 오브젝트 (카메라 전환) 모든 씬 통용
        {
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
            case EvenetSelection.EventType.EventObject:
                GetText(eventSelection.ID, eventSelection.isCharaTalk);
                Debug.Log("a");
                break;

            case EvenetSelection.EventType.Item_Obj:
                FieldItem fieldItems = other.GetComponent<FieldItem>();                //타겟아이템을 변수에 저장
                if (inventory.AddItem(fieldItems.GetItem()))                            //그아이템을 저장 제대로 저장시 true 아닐시 false
                {
                    fieldItems.DestroyItem();                                           //필드의 아이템을 제거
                }
                Debug.Log("Gear");
                break;
            case EvenetSelection.EventType.Globe: //지구본 클릭 시 플레이어 카메라 비활성화 혹은 활성화
                if (mainCamera.activeSelf)
                    mainCamera.SetActive(false);
                else
                    mainCamera.SetActive(true);
                break;
        }

        Debug.Log("check");
    }

    void GetText(int id, bool isCharaTalk)
    {
        Debug.Log(id);
        string textData = textmanager.GetTalk(id, talkindex);

        if (textData == null)
        {
            textmanager.isText = false;
            talkindex = 0;
            textmanager.TalkCon();
            return;
        }

        if (isCharaTalk)
        {
            textmanager.DescText.text = textData.Split(':')[0];
            textmanager.CharaFace_Img.sprite = textmanager.GetPortrait(id, int.Parse(textData.Split(':')[1]));

            textmanager.CharaFace_Img.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            textmanager.DescText.text = textData;

            textmanager.CharaFace_Img.color = new Color32(255, 255, 255, 0);
        }

        textmanager.isText = true;
        talkindex++;

        textmanager.TalkCon();
    }
}