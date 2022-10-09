using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelClick : MonoBehaviour
{
    private Rock_Puzzle rock_Puzzle;
    Camera mainCamera = null;
    private GameObject target;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        rock_Puzzle = GameObject.Find("2F_Rock_Puzzle").GetComponent<Rock_Puzzle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ChangeTimeButton.isDay && SecondFloorManager.currentState == SecondFloorManager.SecondFloorState.SecondPuzzle && Input.GetMouseButtonDown(0)) //���� ���� + 1�� Ŭ���� ���� + ��Ŭ��
        {
            target = GetClickedObject(); //Ÿ���� ������Ʈ ��������

            if (target != null && gameObject.name.Equals(target.name) && gameObject.transform.GetChild(0).GetComponent<AngelClickRange>().isTrigger)
            {
                rock_Puzzle.AngelResetPuzzle();
            }
        }
    }

    private GameObject GetClickedObject() //���콺�� Ŭ���� ������Ʈ �޾ƿ���
    {
        int layerMask = 1 << LayerMask.NameToLayer("Angel");
        RaycastHit hit;
        GameObject target = null;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity,layerMask))
            target = hit.collider.gameObject;

        return target;
    }
}
