using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStatus : MonoBehaviour
{

    public enum eveObj { NONE, EventObject }
    public enum eve_Type { decoration }

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