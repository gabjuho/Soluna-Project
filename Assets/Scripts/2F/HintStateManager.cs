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
        BookNothing = 20, //책 아무것도 주운게 없을 때
        BookGetting, //책 주었음
        CrystalCorrect, //올바른 크리스탈 한 개 이상 클릭
        RockPuzzle, //돌 퍼즐 진행 중
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
                if (!bookUsingPoint[0].isClear && !bookUsingPoint[1].isClear && !bookUsingPoint[2].isClear) //아무것도 클리어X
                {
                    HintArrow.target2F = hintObject[0];
                }
                else if (bookUsingPoint[0].isClear && !bookUsingPoint[1].isClear && !bookUsingPoint[2].isClear) //시계책만 클리어
                {
                    HintArrow.target2F = hintObject[0];
                }
                else if (bookUsingPoint[0].isClear && bookUsingPoint[1].isClear && !bookUsingPoint[2].isClear) //시계책과 마법책 클리어
                {
                    HintArrow.target2F = hintObject[2];
                }
                else if (bookUsingPoint[0].isClear && !bookUsingPoint[1].isClear && bookUsingPoint[2].isClear) //시계책과 톱니책 클리어
                {
                    HintArrow.target2F = hintObject[0];
                }
                else if (!bookUsingPoint[0].isClear && bookUsingPoint[1].isClear && !bookUsingPoint[2].isClear) //마법책만 클리어
                {
                    HintArrow.target2F = hintObject[1];
                }
                else if (!bookUsingPoint[0].isClear && bookUsingPoint[1].isClear && bookUsingPoint[2].isClear) //마법책과 톱니책 클리어
                {
                    HintArrow.target2F = hintObject[1];
                }
                else if (bookUsingPoint[0].isClear && !bookUsingPoint[1].isClear && bookUsingPoint[2].isClear) //톱니책만 클리어
                {
                    HintArrow.target2F = hintObject[1];
                }
                break;
            case PuzzleState.BookGetting:
                if (lastItem.Equals("Magic_Book") || lastItem.Equals("마법책"))
                {
                    HintArrow.target2F = hintObject[4];
                    //인벤토리 발광 효과까지 넣어야함
                }
                else if (lastItem.Equals("Clock_Book") || lastItem.Equals("시계책"))
                {
                    HintArrow.target2F = hintObject[3];
                    //인벤토리 발광 효과까지 넣어야함
                }
                else if (lastItem.Equals("Gear_Book") || lastItem.Equals("톱니책"))
                {
                    HintArrow.target2F = hintObject[5];
                    //인벤토리 발광 효과까지 넣어야함
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
                    //해당 돌에 발광 표시
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

                currentRock = rock_Puzzle.rock[tempIndex]; //발광할 돌 지정

                break;
        }
    }
}