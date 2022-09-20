using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Texture2D CursorTexture;

    //[Space(20)]
    //public Image SunMoonImg;

    public void MouseImage()
    {
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void OptionBtn()
    {
        Debug.Log("check");
    }
}
