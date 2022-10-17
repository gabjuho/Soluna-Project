using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintArrow : MonoBehaviour
{
    [SerializeField]
    LozicManager lozicManager;
    [SerializeField]
    HintManager hintManager;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject hintObj;

    [SerializeField]
    GameObject[] target;

    public bool on_ArrowObj;

    // Update is called once per frame
    private void Awake()
    {
        on_ArrowObj = false;
    }
    private void Start()
    {
        hintObj.SetActive(on_ArrowObj);
    }
    void Update()
    {
        hintObj.SetActive(on_ArrowObj);
        if (on_ArrowObj)
        {
            for(int i = 0; i < lozicManager.solve_Lozic.Length; i++)
            {
                if(lozicManager.solve_Lozic[i] == false)
                {
                    hintObj.transform.LookAt(new Vector3(target[i].transform.position.x, player.transform.position.y, target[i].transform.position.z), Vector3.up);
                    hintObj.transform.Rotate(new Vector3(hintObj.transform.rotation.x, hintObj.transform.rotation.y + 90, hintObj.transform.rotation.z));

                    hintObj.transform.position = player.transform.position + -hintObj.transform.right * 1f;
                    break;
                }
            }
            for (int i = 0; i < lozicManager.solve_Lozic.Length; i++)
            {
                if (lozicManager.solve_Lozic[i] == false)
                {
                    return;
                }
            }
            hintObj.SetActive(false);
        }
    }
}
