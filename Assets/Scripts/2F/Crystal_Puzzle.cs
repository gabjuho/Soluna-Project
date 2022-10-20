using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Puzzle : MonoBehaviour
{
    HintStateManager hintStateManager;
    public string[] ActiveCrystalName = new string[4];
    public AudioClip[] audioClips = new AudioClip[3];
    private string[] AnswerCrystalName = new string[4];
    public static int currentIndex;
    private Rock_Puzzle rock_Puzzle;
    public Inventory inventory;
    public Item haewang;
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
        hintStateManager = GameObject.Find("2F_Hint_State_Manager").GetComponent<HintStateManager>();
    }

    public void SetActiveCrystal(string pillar_name)
    {
        if(IsInvoking("initializeCrystalPuzzle")) //ó�� ���� Ŭ�� �� �κ�ũ �Ǿ����� �����Ƿ�, �κ�ũ �Ǿ��ִ� üũ, �Ǿ� ������ ĵ��
            CancelInvoke("initializeCrystalPuzzle");

        Invoke("initializeCrystalPuzzle", 15f); //15�� �� ���� �ʱ�ȭ


        bool alreadyActive = false; //Ŭ���� ������ �̹� Ȱ��ȭ �� �����ΰ�

        if (currentIndex >= 4) //���� Ŭ���� �� ���� ��Ȱ��ȭ ����
            return;

        for (int i = 0; i < currentIndex; i++) //Ŭ���� ������ �̹� Ȱ��ȭ �� �������� üũ
        {
            if (ActiveCrystalName[i].Equals(pillar_name))
                alreadyActive = true;
        }

        if (!alreadyActive) //Ŭ���� ������ Ȱ��ȭ �� ������ �ƴϸ�, ������ �ش� ������� �߰�
            ActiveCrystalName[currentIndex++] = pillar_name;

        if (currentIndex >= 4) //4���� ������ ���� Ŭ���ϸ�
        {
            CancelInvoke("initializeCrystalPuzzle");
            if (CheckAllActive()) //���� ������, ���� ���� Ŭ���� ���·� �ٲ�
            {
                SecondFloorManager.currentState = SecondFloorManager.SecondFloorState.SecondPuzzle;
                rock_Puzzle.RockPuzzleSet();
                gameObject.GetComponent<AudioSource>().Play();
                HintStateManager.ChangePuzzleState(HintStateManager.PuzzleState.RockPuzzle);

                inventory.AddItem(haewang);

                GameObject.Find("ObjectTriger").GetComponent<ObjectEventTriger>().GetText(184, true);
                PlayerMoveDetector.isCrystalClick = true;
                GuideLine.currentPuzzle++;

                Debug.Log("�ι�° ���� Ŭ����");
            }
            else //��� Ʋ����
            {
                GameObject.Find("ObjectTriger").GetComponent<ObjectEventTriger>().GetText(185, true);
                PlayerMoveDetector.isCrystalClick = true;
                initializeCrystalPuzzle(); //���� �ʱ�ȭ
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

    public void initializeCrystalPuzzle() //���� �������� �� �ʱ�ȭ
    {
        Debug.Log("���� �ʱ�ȭ");
        currentIndex = 0;
        for (int i = 0; i < 4; i++)
            ActiveCrystalName[i] = "\0";

        hintStateManager.ChangeTarget(HintStateManager.currentPuzzleState);
    }
}