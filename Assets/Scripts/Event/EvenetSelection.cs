using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenetSelection : MonoBehaviour
{
    public enum EventType { First, Second, Third, Item, Puzzle_Rock, Globe}; //기존 것
    public enum eveObj { NONE, EventObject }
    public enum eve_Type { decoration }

    public EventType _eventType; //기존 것

    public int ID;
    public bool isTextable;
    public bool isCharaTalk;

    [Space(20)]
    public eveObj ObjectType;
    public eve_Type Event_Type;

    [Space(20)]
    public Material outline;

    Renderer renderers;
    List<Material> materialList = new List<Material>();
}
