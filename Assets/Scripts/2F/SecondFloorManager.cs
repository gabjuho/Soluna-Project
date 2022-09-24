using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorManager: MonoBehaviour
{
    public static GameObject rightBookUsingPoint, leftBookUsingPoint, middleBookUsingPoint;
    public static bool bookPuzzleClear;

    public enum SecondFloorState
    {
        NoSolve, //아무것도 클리어 하지 않은 초기 상태
        FirstPuzzle, //첫번째 퍼즐 클리어 완료
        SecondPuzzle, //두번째 퍼즐 클리어 완료
        ThirdPuzzle //세번째 퍼즐 클리어 완료
    };

    public static SecondFloorState currentState; //현재 퍼즐 클리어 상태

    private void Awake()
    {
        currentState = SecondFloorState.FirstPuzzle;
        bookPuzzleClear = false;
    }
    void Start()
    {
        rightBookUsingPoint = GameObject.Find("RightBookUsingPoint");
        leftBookUsingPoint = GameObject.Find("LeftBookUsingPoint");
        middleBookUsingPoint = GameObject.Find("MiddleBookUsingPoint");
    }

    public static void CheckBookPuzzleClear() //책 퍼즐이 전부 완료되었는 지 확인하는 함수
    {
        if (rightBookUsingPoint.GetComponent<Book_Puzzle>().isClear && leftBookUsingPoint.GetComponent<Book_Puzzle>().isClear && middleBookUsingPoint.GetComponent<Book_Puzzle>().isClear)
            bookPuzzleClear = true;

        if (bookPuzzleClear)
            currentState = SecondFloorState.FirstPuzzle;
    }
}
