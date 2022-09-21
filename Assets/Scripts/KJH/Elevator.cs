using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject elevator;
    public bool isRasing = false;
    
    public void ControllAnimation()
    {
        if (!elevator.GetComponent<Animator>().GetBool("go_Down"))
        {
            elevator.GetComponent<Animator>().Play("Elevator Down");
            elevator.GetComponent<Animator>().SetBool("go_Down", true);
        }
        else
        {
            elevator.GetComponent<Animator>().SetBool("go_Down", false);
        }
    }
}
