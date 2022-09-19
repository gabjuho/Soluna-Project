using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isDelayOn = false;

    #region MikangMark
    bool isGameRun = true;
    [HideInInspector]
    public bool isAlt = false;
    [HideInInspector]
    public KeyCode ObjectTriger;
    [HideInInspector]
    public KeyCode AltKey;

    UI_Manager Um;

    [SerializeField]
    Camera mainCamera;

    Quaternion camera_rotateLock;
    #endregion

    private void Awake()
    {

        Um = GameObject.Find("Ui_Manager").GetComponent<UI_Manager>();

        ObjectTriger = KeyCode.E;
        AltKey = KeyCode.LeftAlt;
    }

    private void FixedUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.5f);

        if (isGameRun)
        {
            if (!Input.GetKey(AltKey)) isAlt = false;
            else isAlt = true;
            /*if (Input.GetKeyDown(AltKey))
            {
                camera_rotateLock = mainCamera.transform.rotation;
            }*/

            //마우스 관련
            if (isAlt)
            {
                Um.MouseImage();
                //mainCamera.transform.rotation = camera_rotateLock;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public IEnumerator DelayTimer(float _time)
    {
        isDelayOn = true;
        yield return new WaitForSeconds(_time);
        isDelayOn = false;  
    }

}
