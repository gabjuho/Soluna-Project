using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public Item item;                            //아이템의 정보 받기
    public MeshRenderer mesh;                    //아이템의 3D매쉬
    public Sprite image;                         //아이템의 스프라이트

    public void SetItem(Item _item)              //아이템 기본 셋팅
    {
        item.itemName = _item.itemName;          //아이템이름
        item.itemMaterial = _item.itemMaterial;   //머테리얼
        item.itemType = _item.itemType;           //아이템 타입
        item.itemImage = _item.itemImage;         //슬롯에 사용할 이미지
                                                  //
        mesh.material = item.itemMaterial;        //
        image = item.itemImage;                   //
    }

    public Item GetItem()                         //외부에서 아이템 정보받는 메소드
    {
        return item;
    }
    public void DestroyItem()                    //아이템 지울시 사용하는 메서드
    {
        Destroy(gameObject);
    }
}
