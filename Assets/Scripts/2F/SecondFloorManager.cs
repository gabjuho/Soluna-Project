using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorManager: MonoBehaviour
{
    public static GameObject rightBookUsingPoint, leftBookUsingPoint, middleBookUsingPoint;
    public static bool bookPuzzleClear;
    public static Inventory inventory;
    static HintStateManager hintStateManager;

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
        currentState = SecondFloorState.NoSolve;
        bookPuzzleClear = false;
    }
    void Start()
    {
        rightBookUsingPoint = GameObject.Find("RightBookUsingPoint");
        leftBookUsingPoint = GameObject.Find("LeftBookUsingPoint");
        middleBookUsingPoint = GameObject.Find("MiddleBookUsingPoint");
        hintStateManager = GameObject.Find("2F_Hint_State_Manager").GetComponent<HintStateManager>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    public static void CheckBookPuzzleClear() //å ������ ���� �Ϸ�Ǿ��� �� Ȯ���ϴ� �Լ�
    {
        if (rightBookUsingPoint.GetComponent<Book_Puzzle>().isClear && leftBookUsingPoint.GetComponent<Book_Puzzle>().isClear && middleBookUsingPoint.GetComponent<Book_Puzzle>().isClear)
            bookPuzzleClear = true;

        if (bookPuzzleClear)
        {
            currentState = SecondFloorState.FirstPuzzle;
            
            HintStateManager.ChangePuzzleState(HintStateManager.PuzzleState.CrystalCorrect);
            hintStateManager.ChangeTarget(HintStateManager.PuzzleState.CrystalCorrect);
        }
    }
}
