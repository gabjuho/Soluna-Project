using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_UI : MonoBehaviour
{
    [SerializeField]
    GameObject Opt_Ui;
    [SerializeField]
    GameObject controll_Ui;
    [SerializeField]
    GameObject sound_Ui;
    bool onclick;

    bool onControll;
    bool onSound;
    // Start is called before the first frame update
    void Start()
    {
        onclick = false;
        onControll = true;
        onSound = false;
        Opt_Ui.SetActive(onclick);
        controll_Ui.SetActive(onControll);
        sound_Ui.SetActive(onSound);
    }
    public void OnClickButton()
    {
        onclick = !onclick;
        Opt_Ui.SetActive(onclick);
        controll_Ui.SetActive(onControll);
        sound_Ui.SetActive(onSound);
    }

    public void OnControllBtn()
    {
        onControll = !onControll;
        onSound = !onSound;
        controll_Ui.SetActive(onControll);
        sound_Ui.SetActive(onSound);
    }
}
