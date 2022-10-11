using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    [SerializeField]
    LimitTimer timer;
    [SerializeField]
    float solveLozic;
    [SerializeField]
    int solveLozic_int;
    [SerializeField]
    float waitTime;
    [SerializeField]
    LozicManager lozicManager;

    [SerializeField]
    Button hintBtn;

    [SerializeField]
    AudioClip hint_creat;
    [SerializeField]
    AudioClip hint_used;

    [SerializeField]
    GameObject hint_arrow;

    public bool on_Hint;
    private void Awake()
    {
        solveLozic = 0f;
        solveLozic_int = 0;
        on_Hint = false;
    }
    private void Start()
    {
        hintBtn.gameObject.SetActive(on_Hint);
        hint_arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        solveLozic += Time.deltaTime;
        solveLozic_int = (int)Mathf.Round(solveLozic);
        if (solveLozic_int > waitTime)
        {
            on_Hint = true;
            hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_creat;
            hintBtn.gameObject.GetComponent<AudioSource>().Play();
            hintBtn.gameObject.SetActive(on_Hint);
        }
    }

    public void OnClickButton()
    {
        on_Hint = false;
        hint_arrow.SetActive(true);
        hintBtn.gameObject.SetActive(on_Hint);
        hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
        hintBtn.gameObject.GetComponent<AudioSource>().Play();
        solveLozic = 0;
    }
}
