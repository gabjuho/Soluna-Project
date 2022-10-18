using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalClickRange : MonoBehaviour
{
    public bool isTrigger;
    private void Awake()
    {
        isTrigger = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
    }

    public void OnTriggerExit(Collider other)
    {
        isTrigger = false;
    }
}
