using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject elevator;
    public bool isRasing = false;
    public float e_Time;

    public void Update()
    {
        e_Time += Time.deltaTime;
        if (e_Time >= 5.0f)
        {
            e_Time = 0f;
            ControllAnimation();
        }
    }
    public void ControllAnimation()
    {
        if (!elevator.GetComponent<Animator>().GetBool("go_Down"))
        {
            elevator.GetComponent<Animator>().SetBool("go_Down", true);
            elevator.GetComponent<Animator>().Play("Elevator Down");
            
        }
        else
        {
            elevator.GetComponent<Animator>().SetBool("go_Down", false);
        }
    }
}
