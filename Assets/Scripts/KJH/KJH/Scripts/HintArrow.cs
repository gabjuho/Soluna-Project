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

    // Update is called once per frame
    void Update()
    {
        hintObj.SetActive(hintManager.on_Hint);
        if (hintManager.on_Hint)
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
