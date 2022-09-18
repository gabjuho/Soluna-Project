using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearLozic : MonoBehaviour
{
    //[SerializeField]
    //GameObject gear_plane;
    public bool on_triger;
    public GameObject gearObj;

    private void Awake()
    {
        on_triger = false;
    }
    private void Start()
    {
        gearObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        on_triger = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        on_triger = false;
    }

}
