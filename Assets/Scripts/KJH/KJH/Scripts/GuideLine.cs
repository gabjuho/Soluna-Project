using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideLine : MonoBehaviour
{
    public GameObject[] guide;
    public GameObject[] guideDeco;
    [SerializeField]
    Sprite guide_ClearImg;
    [SerializeField]
    Sprite guideDeco_ClearImg;
    [SerializeField]
    LozicManager lozic;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < lozic.solve_Lozic.Length; i++)
        {
            if (lozic.solve_Lozic[i])
            {
                guide[i].GetComponent<Image>().sprite = guide_ClearImg;
                for(int j = 0; j < 3; j++)
                {
                    if (i > 0)
                    {
                        guideDeco[i - 1].transform.GetChild(j).GetComponent<Image>().sprite = guideDeco_ClearImg;
                    }
                    
                }
                
            }
        }
    }
}
