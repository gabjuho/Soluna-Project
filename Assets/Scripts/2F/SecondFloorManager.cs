using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorManager: MonoBehaviour
{
    public enum SecondFloorState
    {
        NoSolve, //아무것도 클리어 하지 않은 초기 상태
        FirstPuzzle, //첫번째 퍼즐 클리어 완료
        SecondPuzzle, //두번째 퍼즐 클리어 완료
        ThirdPuzzle //세번째 퍼즐 클리어 완료
    };

    public SecondFloorState currentState; //현재 퍼즐 클리어 상태
    // Start is called before the first frame update
    void Start()
    {
        currentState = SecondFloorState.NoSolve;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
