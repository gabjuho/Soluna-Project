using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorManager: MonoBehaviour
{
    public enum SecondFloorState
    {
        NoSolve, //�ƹ��͵� Ŭ���� ���� ���� �ʱ� ����
        FirstPuzzle, //ù��° ���� Ŭ���� �Ϸ�
        SecondPuzzle, //�ι�° ���� Ŭ���� �Ϸ�
        ThirdPuzzle //����° ���� Ŭ���� �Ϸ�
    };

    public SecondFloorState currentState; //���� ���� Ŭ���� ����
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
