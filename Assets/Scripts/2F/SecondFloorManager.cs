using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondFloorManager: MonoBehaviour
{
    public static GameObject rightBookUsingPoint, leftBookUsingPoint, middleBookUsingPoint;
    public static bool bookPuzzleClear;
    public static Inventory inventory;
    public Sprite image;
    public static Item item = new Item();
    static HintStateManager hintStateManager;

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
        item.itemName = "Saturn";
        item.itemType = ItemType.Object_Saturn;
        item.itemImage = image;
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

    public static void CheckBookPuzzleClear() //책 퍼즐이 전부 완료되었는 지 확인하는 함수
    {
        if (rightBookUsingPoint.GetComponent<Book_Puzzle>().isClear && leftBookUsingPoint.GetComponent<Book_Puzzle>().isClear && middleBookUsingPoint.GetComponent<Book_Puzzle>().isClear)
            bookPuzzleClear = true;

        if (bookPuzzleClear)
        {
            currentState = SecondFloorState.FirstPuzzle;
            GuideLine.currentPuzzle++;

            inventory.AddItem(item);

            GameObject.Find("Middle_Shelf").GetComponent<EvenetSelection>().ChangeID(164);
            GameObject.Find("Left_Shelf").GetComponent<EvenetSelection>().ChangeID(164);
            GameObject.Find("Right_Shelf").GetComponent<EvenetSelection>().ChangeID(164);
            GameObject.Find("Blue_Pillar").GetComponent<EvenetSelection>().ChangeID(183);
            GameObject.Find("Red_Pillar").GetComponent<EvenetSelection>().ChangeID(183);
            GameObject.Find("Green_Pillar").GetComponent<EvenetSelection>().ChangeID(183);
            GameObject.Find("Purple_Pillar").GetComponent<EvenetSelection>().ChangeID(183);

            HintStateManager.ChangePuzzleState(HintStateManager.PuzzleState.CrystalCorrect);
            hintStateManager.ChangeTarget(HintStateManager.PuzzleState.CrystalCorrect);
        }
    }
}
