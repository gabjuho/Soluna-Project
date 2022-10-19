using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenetSelection : MonoBehaviour
{
    public enum EventType { Item_Obj, EventObject, Globe }
    public enum eve_Type { decoration }
    public int ID;
    public bool isTextable;
    public bool isCharaTalk;

    [Space(20)]
    public eve_Type Event_Type;
    public EventType _eventType;
}
