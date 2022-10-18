using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    [SerializeField]
    LimitTimer timer;
    [SerializeField]
    float solveLozic;
    [SerializeField]
    int solveLozic_int;
    [SerializeField]
    float waitTime;
    [SerializeField]
    LozicManager lozicManager;

    [SerializeField]
    Button hintBtn;

    [SerializeField]
    AudioClip hint_creat;
    [SerializeField]
    AudioClip hint_used;

    [SerializeField]
    GameObject hint_arrow;

    [SerializeField]
    HintStateManager hintStateManager;
    public Material rock_puzzle_material;

    public bool on_Hint;
    int count;
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
        if (SceneManager.GetActiveScene().name.Equals("1F"))
        {
            on_Hint = false;
            hint_arrow.SetActive(true);
            hintBtn.gameObject.SetActive(on_Hint);
            hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
            hintBtn.gameObject.GetComponent<AudioSource>().Play();
            solveLozic = 0;
        }
        else if(SceneManager.GetActiveScene().name.Equals("2F"))
        {
            if (HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.AllPuzzleClear)
                return;
            //낮일 때 힌트 버튼 클릭
            if (HintStateManager.currentTime == HintStateManager.TimeState.Day && (HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.BookNothing || HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.BookGetting)) 
            {
                on_Hint = false;
                hint_arrow.SetActive(true);
                hintBtn.gameObject.SetActive(on_Hint);
                hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
                hintBtn.gameObject.GetComponent<AudioSource>().Play();
                solveLozic = 0;
            }
            //밤일 때 수정 퍼즐 때 힌트 버튼 클릭
            else if (HintStateManager.currentTime == HintStateManager.TimeState.Night && HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.CrystalCorrect)
            {
                on_Hint = false;
                hint_arrow.SetActive(true);
                hintBtn.gameObject.SetActive(on_Hint);
                hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
                hintBtn.gameObject.GetComponent<AudioSource>().Play();
                solveLozic = 0;
            }
            //밤일 때 돌 퍼즐 때 힌트 버튼 클릭
            else if(HintStateManager.currentTime == HintStateManager.TimeState.Night && HintStateManager.currentPuzzleState == HintStateManager.PuzzleState.RockPuzzle)
            {
                on_Hint = false;
                hint_arrow.SetActive(true);
                //힌트 버튼 클릭 시 힌트 돌에 이펙트 활성화
                hintStateManager.currentRock.transform.GetChild(0).gameObject.SetActive(true);
                hintBtn.gameObject.SetActive(on_Hint);
                hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
                hintBtn.gameObject.GetComponent<AudioSource>().Play();
                solveLozic = 0;
            }
            else
            {
                //대사 출력
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("3F"))
        {

        }
    }

}
