using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isDelayOn = false;

    public IEnumerator DelayTimer(float _time)
    {
        isDelayOn = true;
        yield return new WaitForSeconds(_time);
        isDelayOn = false;  
    }

}
