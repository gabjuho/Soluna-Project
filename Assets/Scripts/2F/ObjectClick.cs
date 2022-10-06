using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    Crystal_Puzzle crystal_Puzzle;
    Camera mainCamera = null;
    private GameObject target;

    void Awake()
    {
        mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        crystal_Puzzle = GameObject.Find("2F_Crystal_Puzzle").GetComponent<Crystal_Puzzle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!ChangeTimeButton.isDay && SecondFloorManager.currentState == SecondFloorManager.SecondFloorState.FirstPuzzle &&Input.GetMouseButtonDown(0)) //���� ���� + 1�� Ŭ���� ���� + ��Ŭ��
        {
            target = GetClickedObject(); //Ÿ���� ������Ʈ ��������

            if (gameObject.name.Equals(target.name) && gameObject.transform.GetChild(0).GetComponent<CrystalClickRange>().isTrigger)
            {                crystal_Puzzle.SetActiveCrystal(gameObject.name);
            }
        }
    }

    private GameObject GetClickedObject() //���콺�� Ŭ���� ������Ʈ �޾ƿ���
    {
        RaycastHit hit;
        GameObject target = null;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray.origin,ray.direction*10,out hit))
            target = hit.collider.gameObject;

        return target;
    }
}
