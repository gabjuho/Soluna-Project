using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveDetector : MonoBehaviour
{
    public bool isPlayerMove = false;
    public GameObject textPanel;
    public GameObject textManager;
    public static bool isItemUse = false;
    public static bool isAngelClick = false;
    public static bool isCrystalClick = false;
    private Vector3 lastPos;

    private void Start()
    {
        lastPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (lastPos.x != this.transform.position.x && lastPos.z != this.transform.position.z)
        {
            isPlayerMove = true;
            lastPos = transform.position;

            if (isItemUse && textPanel.activeSelf)
            {
                textPanel.SetActive(false);
                isItemUse = false;
            }
            else if(isAngelClick && textPanel.activeSelf)
            {
                textPanel.SetActive(false);
                isAngelClick = false;
            }
            else if (isCrystalClick && textPanel.activeSelf)
            {
                textPanel.SetActive(false);
                isCrystalClick = false;
            }
        }
        else if(lastPos.x == this.transform.position.x && lastPos.z == this.transform.position.z)
            isPlayerMove = false;

        
    }
}
