using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObstacle : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude;
    private float coolTime;
    private Rock_Puzzle rock_Puzzle;
    private bool isPush;


    private void Awake()
    {
        coolTime = 15f;
        isPush = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rock_Puzzle = GameObject.Find("2F_Rock_Puzzle").GetComponent<Rock_Puzzle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SecondFloorManager.currentState == SecondFloorManager.SecondFloorState.SecondPuzzle)
        {
            if (isPush && coolTime > 0)
                coolTime -= Time.deltaTime;
            else if (isPush && coolTime <= 0)
            {
                rock_Puzzle.ResetPuzzle();
                isPush = false;
                coolTime = 15f;
            }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        GameObject puzzle_rock = hit.gameObject;

        if(rigidbody != null && puzzle_rock.CompareTag("Puzzle_Rock"))
        {
            isPush = true;
            coolTime = 15f;
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.VelocityChange);
        }
    }
}
