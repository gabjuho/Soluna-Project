using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPuzzleTrigger : MonoBehaviour
{
    private bool isCollide;
    // Start is called before the first frame update

    private void Awake()
    {
        isCollide = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isCollide)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -1.5f, transform.position.z), 1f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -0.5f, transform.position.z), 1f * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Puzzle_Rock"))
        {
            isCollide = true;
            collision.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        }
    }
    private void OnCollisionExit(Collision collision) //이 코드도 문제있음
    {
        if (collision.gameObject.CompareTag("Puzzle_Rock"))
        {
            collision.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
            isCollide = false;
        }
    }
}
