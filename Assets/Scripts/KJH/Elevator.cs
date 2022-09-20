using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject elevator;
    public bool isRasing = false;
    
    public IEnumerator MoveElevator_Down()
    {
        isRasing = true;
        
        yield return new WaitForSeconds(1f);
        elevator.GetComponent<Animator>().Play("Elevator Down");
        yield return new WaitForSeconds(3f);
       
        elevator.GetComponent<Animator>().Play("New State");
        isRasing = false;
        
    }
}
