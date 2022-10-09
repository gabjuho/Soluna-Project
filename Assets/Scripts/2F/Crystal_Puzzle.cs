using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Puzzle : MonoBehaviour
{
    public string[] ActiveCrystalName = new string[4];
    public AudioClip[] audioClips = new AudioClip[3];
    private string[] AnswerCrystalName = new string[4];
    private int currentIndex;
    private Rock_Puzzle rock_Puzzle;
    // Start is called before the first frame update

    private void Awake()
    {
        currentIndex = 0;
        AnswerCrystalName[0] = "Red_Pillar";
        AnswerCrystalName[1] = "Green_Pillar";
        AnswerCrystalName[2] = "Blue_Pillar";
        AnswerCrystalName[3] = "Purple_Pillar";
    }
    void Start()
    {
        rock_Puzzle = GameObject.Find("2F_Rock_Puzzle").GetComponent<Rock_Puzzle>();
    }

    public void SetActiveCrystal(string pillar_name)
    {
        if(IsInvoking("initializeCrystalPuzzle")) //처음 수정 클릭 시 인보크 되어있지 않으므로, 인보크 되어있는 체크, 되어 있으면 캔슬
            CancelInvoke("initializeCrystalPuzzle");

        Invoke("initializeCrystalPuzzle", 15f); //15초 뒤 퍼즐 초기화


        bool alreadyActive = false; //클릭한 수정이 이미 활성화 된 수정인가

        if (currentIndex >= 4) //퍼즐 클리어 후 수정 재활성화 방지
            return;

        for (int i = 0; i < currentIndex; i++) //클릭한 수정이 이미 활성화 된 수정인지 체크
        {
            if (ActiveCrystalName[i].Equals(pillar_name))
                alreadyActive = true;
        }

        if (!alreadyActive) //클릭한 수정이 활성화 된 수정이 아니면, 순서에 해당 수정기둥 추가
            ActiveCrystalName[currentIndex++] = pillar_name;

        if (currentIndex >= 4) //4개의 수정을 전부 클릭하면
        {
            CancelInvoke("initializeCrystalPuzzle");
            if (CheckAllActive()) //답이 맞으면, 수정 퍼즐 클리어 상태로 바꿈
            {
                SecondFloorManager.currentState = SecondFloorManager.SecondFloorState.SecondPuzzle;
                rock_Puzzle.RockPuzzleSet();
                gameObject.GetComponent<AudioSource>().Play();
                Debug.Log("두번째 퍼즐 클리어");
            }
            else //답과 틀리면
            {
                initializeCrystalPuzzle(); //퍼즐 초기화
            }
        }
        
    }

    public bool CheckAllActive()
    {
        bool isAllActive = true;

        for (int i = 0; i < 4; i++)
        {
            if (!AnswerCrystalName[i].Equals(ActiveCrystalName[i]))
                isAllActive = false;
        }

        return isAllActive;
    }

    public void initializeCrystalPuzzle() //퍼즐 진행중인 것 초기화
    {
        Debug.Log("퍼즐 초기화");
        currentIndex = 0;
        for (int i = 0; i < 4; i++)
            ActiveCrystalName[i] = "\0";
    }
}
