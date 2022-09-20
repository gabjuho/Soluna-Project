using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType                           //아이템 타입
{
    ErrorType = -1,
    Object = 0,
    KeyItem,
    #region by Triglavian
    Object_Gear,                               //톱니바퀴
    Object_Fuel,
    Object_ToolBox,
    Object_StarPiece,
    Object_Book1,
    Object_Book2,
    Object_Book3,
    Object_Sun,
    Object_Mercury,
    Object_Venus,
    Object_Earth,
    Object_Mars,
    Object_Jupiter,
    Object_Saturn,
    Object_Uranus,
    Object_Neptune,
    #endregion                              
    MaxItemType
}


[System.Serializable]
public class Item
{
    public ItemType itemType;                //아이템 타입
    public string itemName;                  //아이템 이름
    public Material itemMaterial;            //아이템 3D 머테리얼
    public Sprite itemImage;                 //슬롯에 사용될 2D 스프라이트

    public Item SendItem()
    {
        return this;
    }
}
