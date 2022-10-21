using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuideLine : MonoBehaviour
{
    public GameObject[] guide;
    public GameObject[] guideDeco;
    public static int currentPuzzle = 0; //2Ãþ Àü¿ë ÇöÀç ÆÛÁñ »óÅÂ
    public static int current3FPuzzle = 0;  //3Ãþ Àü¿ë ÇöÀç ÆÛÁñ »óÅÂ
    [SerializeField]
    Sprite guide_ClearImg;
    [SerializeField]
    Sprite guideDeco_ClearImg;
    [SerializeField]
    Sprite guide_NotClearImg;
    [SerializeField]
    Sprite guideDeco_NotClearImg;
    [SerializeField]
    LozicManager lozic;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("1F"))
        {
            for (int i = 0; i < lozic.solve_Lozic.Length; i++)
            {
                if (lozic.solve_Lozic[i])
                {
                    guide[i].GetComponent<Image>().sprite = guide_ClearImg;
                    for (int j = 0; j < 3; j++)
                    {
                        if (i > 0)
                        {
                            guideDeco[i - 1].transform.GetChild(j).GetComponent<Image>().sprite = guideDeco_ClearImg;
                        }

                    }

                }
            }
        }
        else if(SceneManager.GetActiveScene().name.Equals("2F"))
        {
            for (int i = 0; i < currentPuzzle; i++)
            {
                guide[i].GetComponent<Image>().sprite = guide_ClearImg;
                for (int j = 0; j < 3; j++)
                {
                    if (i > 0)
                    {
                        guideDeco[i - 1].transform.GetChild(j).GetComponent<Image>().sprite = guideDeco_ClearImg;
                    }

                }
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("3F"))
        {
            if (current3FPuzzle == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    guide[i].GetComponent<Image>().sprite = guide_NotClearImg;
                    for (int j = 0; j < 3; j++)
                    {
                        if (i > 0)
                        {
                            guideDeco[i - 1].transform.GetChild(j).GetComponent<Image>().sprite = guideDeco_NotClearImg;
                        }

                    }
                }
            }
            else
            {
                for (int i = 0; i < current3FPuzzle; i++)
                {
                    guide[i].GetComponent<Image>().sprite = guide_ClearImg;
                    for (int j = 0; j < 3; j++)
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
}
