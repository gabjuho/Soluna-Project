using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamLozic : MonoBehaviour
{
    public bool on_triger;
    public bool on_starFuel;
    public bool on_toolBox;
    public bool lozicClear;
    private void Awake()
    {
        on_triger = false;
        on_starFuel = false;
        on_toolBox = false;
        lozicClear = false;
    }
    private void FixedUpdate()
    {
        if (on_starFuel && on_toolBox)
        {
            lozicClear = true;
        }
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
