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
                    hintObj.transform.LookAt(new Vector3(target[i].transform.position.x, player.transform.position.y, target[i].transform.position.z));
                    hintObj.transform.position = RotationPos(R_CalculateRotation(player.transform.position, hintObj.transform.position, target[i].transform.position), player.transform.position, hintObj.transform.position);
                    break;
                }
            }
            
        }
    }
    float R_CalculateRotation(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        float temp;

        temp = Mathf.Atan((v2.z - v0.z) / (v2.x - v0.x)) - Mathf.Atan((v1.z - v0.z) / (v1.x - v0.x));
        
        return temp;
    }
    Vector3 RotationPos(float radian, Vector3 v0, Vector3 v1)
    {
        Vector3 temp;

        temp.x = Mathf.Cos(radian) * (v1.x - v0.x) - Mathf.Sin(radian) * (v1.z - v0.z);
        temp.z = Mathf.Sin(radian) * (v1.x - v0.x) + Mathf.Cos(radian) * (v1.z - v0.z);
        v1.x = temp.x + v0.x;
        v1.z = temp.z + v0.z;
        return v1;
    }

}
