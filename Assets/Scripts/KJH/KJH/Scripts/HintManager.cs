using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    [SerializeField]
    LimitTimer timer;
    public float solveLozic;
    [SerializeField]
    int solveLozic_int;
    [SerializeField]
    float waitTime;

    [SerializeField]
    Button hintBtn;

    [SerializeField]
    AudioClip hint_creat;
    [SerializeField]
    AudioClip hint_used;

    public HintArrow hint_arrow;

    [SerializeField]
    HintStateManager hintStateManager;
    public Material rock_puzzle_material;

    [SerializeField]
    InventoryUI inventoryUI;
    [SerializeField]
    Puzzle3FManager puzzle3FManager;

    public bool on_Hint;
    int count;

    int thislozic_index;
    public int have_hint;
    private void Awake()
    {
        solveLozic = 0f;
        solveLozic_int = 0;
        on_Hint = false;
        count = 0;
    }
    private void Start()
    {
        hintBtn.gameObject.SetActive(on_Hint);
        hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_creat;
        hint_arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        solveLozic += Time.deltaTime;
        solveLozic_int = (int)Mathf.Round(solveLozic);
        if (solveLozic_int > waitTime)
        {
            
            on_Hint = true;
            if(count == 0)
            {
                hintBtn.gameObject.GetComponent<AudioSource>().Play();
                count++;
            }
            hintBtn.gameObject.SetActive(on_Hint);
        }
    }

    public void OnClickButton()
    {
        if (have_hint > 0&&SceneManager.GetActiveScene().name.Equals("1F"))
        {
            on_Hint = false;
            hint_arrow.on_ArrowObj = true;
            have_hint--;
            hintBtn.gameObject.SetActive(on_Hint);
            hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
            hintBtn.gameObject.GetComponent<AudioSource>().Play();
            solveLozic = 0;
        }
        #endregion
        //2�� ��Ʈ��ư ó��
        #region 2F
        else if (SceneManager.GetActiveScene().name.Equals("2F"))
        {
            if (HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.AllPuzzleClear)
                return;
            //낮일 때 힌트 버튼 클릭
            if (HintStateManager.currentTime == HintStateManager.TimeState.Day && (HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.BookNothing || HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.BookGetting)) 
            {
                on_Hint = false;
                if(HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.BookGetting)
                {
                    for (int i = 0; i < inventoryUI.slots.Length; i++)
                    {
                        if (HintStateManager.lastItem.Equals(inventoryUI.slots[i].item.itemName))
                        {
                            Debug.Log(HintStateManager.lastItem);
                            inventoryUI.slots[i].gameObject.transform.GetChild(2).gameObject.SetActive(true);
                            break;
                        }
                    }
                }
                hint_arrow.SetActive(true);
                
                hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
                hintBtn.gameObject.GetComponent<AudioSource>().Play();
                hintBtn.gameObject.SetActive(on_Hint);
                solveLozic = 0;
            }
            //���� �� ���� ���� �� ��Ʈ ��ư Ŭ��
            else if (HintStateManager.currentTime == HintStateManager.TimeState.Night && HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.CrystalCorrect)
            {
                on_Hint = false;
                hint_arrow.SetActive(true);
                
                hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
                hintBtn.gameObject.GetComponent<AudioSource>().Play();
                hintBtn.gameObject.SetActive(on_Hint);
                solveLozic = 0;
            }
            //���� �� �� ���� �� ��Ʈ ��ư Ŭ��
            else if(HintStateManager.currentTime == HintStateManager.TimeState.Night && HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.RockPuzzle)
            {
                on_Hint = false;
                hint_arrow.SetActive(true);
                //��Ʈ ��ư Ŭ�� �� ��Ʈ ���� ����Ʈ Ȱ��ȭ
                hintStateManager.currentRock.transform.GetChild(0).gameObject.SetActive(true);
                
                hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
                hintBtn.gameObject.GetComponent<AudioSource>().Play();
                hintBtn.gameObject.SetActive(on_Hint);
                solveLozic = 0;
            }
            else
            {
                //대사 출력
            }
            
    }

    public void OffHintBtn()
    {
        if (on_Hint)
        {
            on_Hint = false;
            solveLozic = 0f;
            hint_arrow.on_ArrowObj = false;

        }
        #endregion
        //3�� ��Ʈ��ư ó��
        #region 3F
        else if (SceneManager.GetActiveScene().name.Equals("3F"))
        {
            //3�� Ŭ���� �Ǹ� ��Ʈ Ŭ�� �ȵǰ� �ϱ�
            if (puzzle3FManager.isClear)
                return;

            on_Hint = false;
            for (int i = 0; i < inventoryUI.slots.Length; i++)
            {
                if (inventoryUI.slots[i].item.itemName.Equals(puzzle3FManager.item[puzzle3FManager.count].itemName))
                {
                    inventoryUI.slots[i].gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    break;
                }
            }
            
            hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
            hintBtn.gameObject.GetComponent<AudioSource>().Play();
            hintBtn.gameObject.SetActive(on_Hint);
            solveLozic = 0;
        }
        #endregion
    }

}
