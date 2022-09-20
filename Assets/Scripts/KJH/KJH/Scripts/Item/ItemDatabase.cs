using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public int itemCount;
    private void Awake()
    {
        instance = this;
    }
    public List<Item> itemDB = new List<Item>();                                          //아이템 종류 저장하는 리스트

    public GameObject fieldItemPrefab;                                                    //필드아이템 프리펩 정보
    public Vector3[] pos;                                                                 //필드아이템을 떨굴 좌표

    private void Start()
    {
        for(int i = 0; i < itemCount; i++)                                                         //반복문 조건문이 최대 아이템의 갯수 유니티인스펙터 창에 POS 값과 일치할것
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);     //유니티 아이템의 갯수 만큼 인스펙터창에서 좌표 입력 
            go.GetComponent<FieldItem>().SetItem(itemDB[0]);//Random.Range(0, 3)           //아이템의 갯수가 여러개 이면 랜덤을 사용해서 드랍하거나 따로 지정해서 배치
        }
    }
}
