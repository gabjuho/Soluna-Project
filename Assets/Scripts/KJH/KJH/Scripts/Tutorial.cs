using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    Sprite[] tuto;
    int index;
    [SerializeField]
    Button tuto_Obj;
    private void Awake()
    {
        index = 0;
        
    }
    private void Start()
    {
        tuto_Obj.gameObject.SetActive(true);
        tuto_Obj.gameObject.GetComponent<Image>().sprite = tuto[index];
    }
    public void OnclickBtn()
    {
        if (index < tuto.Length)
        {
            tuto_Obj.gameObject.GetComponent<Image>().sprite = tuto[index];
            index++;
        }
        else
        {
            tuto_Obj.interactable = false;
            tuto_Obj.gameObject.SetActive(false);
        }
    }
}
