using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintStateManager : MonoBehaviour
{
    public GameObject[] hintObject;
    public Book_Puzzle[] bookUsingPoint;
    Rock_Puzzle rock_Puzzle;
    public GameObject currentRock;
    public static string lastItem;

    public enum TimeState
    {
        Day = 10,
        Night
    }

    public enum PuzzleState
    {
        BookNothing = 20, //å �ƹ��͵� �ֿ�� ���� ��
        BookGetting, //å �־���
        CrystalCorrect, //�ùٸ� ũ����Ż �� �� �̻� Ŭ��
        RockPuzzle, //�� ���� ���� ��
        AllPuzzleClear
    }

    public static TimeState currentTime;
    public static PuzzleState currentPuzzleState;

    private void Awake()
    {
        currentTime = TimeState.Day;
        currentPuzzleState = PuzzleState.BookNothing;
    }

    private void Start()
    {
        rock_Puzzle = GameObject.Find("2F_Rock_Puzzle").GetComponent<Rock_Puzzle>();
        ChangeTarget(currentPuzzleState);
    }
    public static void ChangePuzzleState(PuzzleState state)
    {
        currentPuzzleState = state;
    }
    public static void ChangeTimeState(TimeState state)
    {
        currentTime = state;
    }

    public void ChangeTarget(PuzzleState state)
    {
        switch (state)
        {
            case PuzzleState.BookNothing:
                if (!bookUsingPoint[0].isClear && !bookUsingPoint[1].isClear && !bookUsingPoint[2].isClear) //�ƹ��͵� Ŭ����X
                {
                    HintArrow.target2F = hintObject[0];
                }
                else if (bookUsingPoint[0].isClear && !bookUsingPoint[1].isClear && !bookUsingPoint[2].isClear) //�ð�å�� Ŭ����
                {
                    HintArrow.target2F = hintObject[0];
                }
                else if (bookUsingPoint[0].isClear && bookUsingPoint[1].isClear && !bookUsingPoint[2].isClear) //�ð�å�� ����å Ŭ����
                {
                    HintArrow.target2F = hintObject[2];
                }
                else if (bookUsingPoint[0].isClear && !bookUsingPoint[1].isClear && bookUsingPoint[2].isClear) //�ð�å�� ���å Ŭ����
                {
                    HintArrow.target2F = hintObject[0];
                }
                else if (!bookUsingPoint[0].isClear && bookUsingPoint[1].isClear && !bookUsingPoint[2].isClear) //����å�� Ŭ����
                {
                    HintArrow.target2F = hintObject[1];
                }
                else if (!bookUsingPoint[0].isClear && bookUsingPoint[1].isClear && bookUsingPoint[2].isClear) //����å�� ���å Ŭ����
                {
                    HintArrow.target2F = hintObject[1];
                }
                else if (bookUsingPoint[0].isClear && !bookUsingPoint[1].isClear && bookUsingPoint[2].isClear) //���å�� Ŭ����
                {
                    HintArrow.target2F = hintObject[1];
                }
                break;
            case PuzzleState.BookGetting:
                if (lastItem.Equals("Magic_Book") || lastItem.Equals("����å"))
                {
                    HintArrow.target2F = hintObject[4];
                    //�κ��丮 �߱� ȿ������ �־����
                }
                else if (lastItem.Equals("Clock_Book") || lastItem.Equals("�ð�å"))
                {
                    HintArrow.target2F = hintObject[3];
                    //�κ��丮 �߱� ȿ������ �־����
                }
                else if (lastItem.Equals("Gear_Book") || lastItem.Equals("���å"))
                {
                    HintArrow.target2F = hintObject[5];
                    //�κ��丮 �߱� ȿ������ �־����
                }
                break;
            case PuzzleState.CrystalCorrect:
                if (Crystal_Puzzle.currentIndex == 0)
                {
                    HintArrow.target2F = hintObject[6];
                }
                else if (Crystal_Puzzle.currentIndex == 1)
                {
                    HintArrow.target2F = hintObject[7];
                }
                else if (Crystal_Puzzle.currentIndex == 2)
                {
                    HintArrow.target2F = hintObject[8];
                }
                else if (Crystal_Puzzle.currentIndex == 3)
                {
                    HintArrow.target2F = hintObject[9];
                }
                break;
            case PuzzleState.RockPuzzle:
                GameObject tempRock = null;
                int tempIndex = -1;
                for (int i = 0; i < 4; i++)
                {
                    if (!rock_Puzzle.rock[i].GetComponent<Rock>().isCorrect)
                    {
                        tempRock = rock_Puzzle.rock[i];
                        tempIndex = i;
                    }
                }

                currentRock = tempRock;
                if (tempRock == null)
                    return;
                if (tempRock.name.Equals("Red_Rock"))
                {
                    HintArrow.target2F = hintObject[10];
                    //�ش� ���� �߱� ǥ��
                }
                else if (tempRock.name.Equals("Green_Rock"))
                {
                    HintArrow.target2F = hintObject[11];
                }
                else if (tempRock.name.Equals("Blue_Rock"))
                {
                    HintArrow.target2F = hintObject[12];
                }
                else if (tempRock.name.Equals("Purple_Rock"))
                {
                    HintArrow.target2F = hintObject[13];
                }

                currentRock = rock_Puzzle.rock[tempIndex]; //�߱��� �� ����

                break;
        }
    }
}