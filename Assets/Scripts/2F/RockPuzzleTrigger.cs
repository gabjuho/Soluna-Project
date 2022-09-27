using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPuzzleTrigger : MonoBehaviour
{
    private Rock_Puzzle rock_Puzzle;
    private bool isOn;
    public enum RockType
    {
        Red,
        Green,
        Blue,
        Purple
    }
    public RockType rockType;
    // Start is called before the first frame update
    private void Awake()
    {
        isOn = true;
    }

    void Start()
    {
        rock_Puzzle = GameObject.Find("2F_Rock_Puzzle").GetComponent<Rock_Puzzle>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (isOn && other.gameObject.CompareTag("Puzzle_Rock") && this.GetComponent<Collider>().bounds.Contains(other.bounds.min) && this.GetComponent<Collider>().bounds.Contains(other.bounds.max))
        {
            if (rockType == other.gameObject.GetComponent<Rock>().rockType)
            {
                Debug.Log("¸Â´Â µ¹");
                other.gameObject.GetComponent<Rock>().isCorrect = true;
                rock_Puzzle.CheckClear();
            }
            gameObject.GetComponent<AudioSource>().Play();
            isOn = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Puzzle_Rock"))
        {
            isOn = true;
        }
    }
}
