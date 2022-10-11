using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_UI : MonoBehaviour
{
    [SerializeField]
    GameObject exit_Ui;
    bool on_Ui;

    private void Awake()
    {
        on_Ui = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        exit_Ui.SetActive(on_Ui);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickBtn();
        }
    }
    public void OnClickBtn()
    {
        on_Ui = !on_Ui;
        exit_Ui.SetActive(on_Ui);
        Application.Quit();
    }
}
