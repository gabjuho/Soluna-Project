using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SundialLozic : MonoBehaviour
{
    public bool on_triger;
    public bool on_starPiece;

    public bool lozicClear;

    public bool hintUsed;

    private void Awake()
    {
        on_triger = false;
        on_starPiece = false;
        lozicClear = false;
        hintUsed = false;
    }
    private void FixedUpdate()
    {
        if (on_starPiece)
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
