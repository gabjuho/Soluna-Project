using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTrigger : MonoBehaviour
{
    public static bool onLift = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            onLift = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            onLift = false;
    }
}
