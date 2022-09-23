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
        bool alreadyActive = false; //Ŭ���� ������ �̹� Ȱ��ȭ �� �����ΰ�

        if (currentIndex >= 4) //���� Ŭ���� �� ���� ��Ȱ��ȭ ����
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
            if (CheckAllActive()) //���� �� Ŭ���� �� ���� ���� Ŭ���� ���·� �ٲ�
            {
                SecondFloorManager.currentState = SecondFloorManager.SecondFloorState.SecondPuzzle;
                Debug.Log("�ι�° ���� Ŭ����");
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
