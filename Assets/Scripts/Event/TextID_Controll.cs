using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextID_Controll : MonoBehaviour
{
    EvenetSelection selection;
    private void Start()
    {
        selection = gameObject.GetComponent<EvenetSelection>();
    }
    public void ChangeTxt(int _id)
    {
        selection.ID = _id;
    }
}
