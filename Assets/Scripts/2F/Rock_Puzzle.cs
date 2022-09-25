using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Puzzle : MonoBehaviour
{
    public GameObject[] rock = new GameObject[4];
    private GameObject[] destroyRock;
    public GameObject movementRange;
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
        }

        int length = destroyRock.Length;

        for (int i = 0; i < length; i++) //���� ����� ������Ʈ�� ������ ������ ������ ��Ȱ��ȭ �ϱ�
            destroyRock[i].SetActive(false);
    }
}
