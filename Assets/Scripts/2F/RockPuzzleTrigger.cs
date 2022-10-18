using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPuzzleTrigger : MonoBehaviour
{
    private Rock_Puzzle rock_Puzzle;
    private bool isOn, isPlayerOn;
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
        isPlayerOn = true;
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
                //맞는 돌을 트리거에 올릴 시 힌트 이펙트 비활성화
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.GetComponent<AudioSource>().Play();
                Debug.Log("맞는 돌");
                other.gameObject.GetComponent<Rock>().isCorrect = true;
                rock_Puzzle.CheckClear();
            }
            isOn = false;
        }
        else if(isOn && other.gameObject.CompareTag("Player") && SecondFloorManager.currentState == SecondFloorManager.SecondFloorState.SecondPuzzle)
        {
            gameObject.GetComponent<AudioSource>().Play();
            isPlayerOn = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Puzzle_Rock"))
        {
            isOn = true;
        }
        else if (other.gameObject.CompareTag("Player"))
            isPlayerOn = true;
    }
}
