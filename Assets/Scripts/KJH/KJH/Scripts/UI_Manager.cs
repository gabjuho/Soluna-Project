using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Texture2D CursorTexture;
    [SerializeField]
    GameObject tutorial;
    [SerializeField]
    GameObject option;
    private void Start()
    {
        //tutorial.SetActive(true);
    }

    public void MouseImage()
    {
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void TutorialBtn()
    {
        tutorial.SetActive(false);
        option.SetActive(true);
    }
    public void n_TutorialBtn()
    {
        tutorial.SetActive(false);
    }
}
