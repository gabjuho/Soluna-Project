using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorManager: MonoBehaviour
{
    public static GameObject rightBookUsingPoint, leftBookUsingPoint, middleBookUsingPoint;
    public static bool bookPuzzleClear;

    public enum SecondFloorState
    {
        NoSolve, //�ƹ��͵� Ŭ���� ���� ���� �ʱ� ����
        FirstPuzzle, //ù��° ���� Ŭ���� �Ϸ�
        SecondPuzzle, //�ι�° ���� Ŭ���� �Ϸ�
        ThirdPuzzle //����° ���� Ŭ���� �Ϸ�
    };

    public static SecondFloorState currentState; //���� ���� Ŭ���� ����

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

    public static void CheckBookPuzzleClear() //å ������ ���� �Ϸ�Ǿ��� �� Ȯ���ϴ� �Լ�
    {
        if (rightBookUsingPoint.GetComponent<Book_Puzzle>().isClear && leftBookUsingPoint.GetComponent<Book_Puzzle>().isClear && middleBookUsingPoint.GetComponent<Book_Puzzle>().isClear)
            bookPuzzleClear = true;

        if (bookPuzzleClear)
            currentState = SecondFloorState.FirstPuzzle;
    }
}
