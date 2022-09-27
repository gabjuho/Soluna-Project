using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Puzzle : MonoBehaviour
{
    public GameObject[] rock = new GameObject[4];
    public GameObject[] rockTrigger = new GameObject[4];
    private GameObject[] destroyRock;
    public GameObject movementRange;
    public bool angelIsCool;
    public int angelCoolTime;
    // Start is called before the first frame update
    void Start()
    {
        destroyRock = GameObject.FindGameObjectsWithTag("Destroy_Rock");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RockPuzzleSet()
    {
        movementRange.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            rock[i].GetComponent<Rigidbody>().useGravity = true;
            rock[i].transform.localScale = new Vector3(0.782992f, 0.782992f, 0.782992f);
            rockTrigger[i].SetActive(true);
        }

        int length = destroyRock.Length;

        for (int i = 0; i < length; i++) //���� ����� ������Ʈ�� ������ ������ ������ ��Ȱ��ȭ �ϱ�
            destroyRock[i].SetActive(false);
    }

    public void CheckClear()
    {
        if (ChangeTimeButton.isDay)
            return;
        for (int i = 0; i < 4; i++)
            if (!rock[i].GetComponent<Rock>().isCorrect)
                return;

        for(int i=0;i<4;i++)
            rock[i].GetComponent<Rigidbody>().isKinematic = true;

        SecondFloorManager.currentState = SecondFloorManager.SecondFloorState.ThirdPuzzle;
        Debug.Log("3�ܰ� ���� Ŭ����");
    }
    public void AngelResetPuzzle() //õ������� ����
    {
        if (!angelIsCool)//õ��� ��Ÿ���� �ƴϸ�
        {
            rock[0].transform.position = new Vector3(0f, 8.338691f, 1.443823e-15f);
            rock[1].transform.position = new Vector3(4.121958f, 7.157758f, 2.954749f);
            rock[2].transform.position = new Vector3(-1.235358f, 7.539322f, -4.474335f);
            rock[3].transform.position = new Vector3(-3.683578f, 6.711401f, 2.730377f);
            angelIsCool = true;
            Invoke("CoolIsDone", 3f);
        }
    }
    public void ResetPuzzle() //15�� ������ �ڵ����� ����
    {
        rock[0].transform.position = new Vector3(0f, 8.338691f, 1.443823e-15f);
        rock[1].transform.position = new Vector3(4.121958f, 7.157758f, 2.954749f);
        rock[2].transform.position = new Vector3(-1.235358f, 7.539322f, -4.474335f);
        rock[3].transform.position = new Vector3(-3.683578f, 6.711401f, 2.730377f);
    }
    public void CoolIsDone()
    {
        angelIsCool = false;
    }
}
