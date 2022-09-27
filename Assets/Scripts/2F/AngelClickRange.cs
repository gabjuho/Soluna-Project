using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelClickRange : MonoBehaviour
{
    public bool isTrigger;
    private void Awake()
    {
        isTrigger = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals("Player"))
            isTrigger = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Player"))    
            isTrigger = false;
    }
}
