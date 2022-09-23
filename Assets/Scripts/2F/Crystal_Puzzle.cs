using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Puzzle : MonoBehaviour
{
    public string[] ActiveCrystalName = new string[4];
    private string[] AnswerCrystalName = new string[4];
    private int currentIndex;
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
    }

    public void SetActiveCrystal(string pillar_name)
    {
        bool alreadyActive = false; //클릭한 수정이 이미 활성화 된 수정인가

        if (currentIndex >= 4) //퍼즐 클리어 후 수정 재활성화 방지
            return;

        for (int i = 0; i < currentIndex; i++)
        {
            if (ActiveCrystalName[i].Equals(pillar_name))
                alreadyActive = true;
        }

        if (!alreadyActive)
            ActiveCrystalName[currentIndex++] = pillar_name;

        if (currentIndex >= 4)
        {
            if (CheckAllActive()) //전부 다 클리어 시 수정 퍼즐 클리어 상태로 바꿈
            {
                SecondFloorManager.currentState = SecondFloorManager.SecondFloorState.SecondPuzzle;
                Debug.Log("두번째 퍼즐 클리어");
            }
            else
            {
                currentIndex = 0;
                for (int i = 0; i < 4; i++)
                    ActiveCrystalName[i] = "\0";
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
}
