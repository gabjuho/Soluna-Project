using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    [SerializeField]
    LimitTimer timer;
    public float solveLozic;
    [SerializeField]
    int solveLozic_int;
    [SerializeField]
    float waitTime;

    [SerializeField]
    Button hintBtn;

    [SerializeField]
    AudioClip hint_creat;
    [SerializeField]
    AudioClip hint_used;

    public HintArrow hint_arrow;

    public bool on_Hint;
    int count;

    int thislozic_index;
    public int have_hint;
    private void Awake()
    {
        solveLozic = 0f;
        solveLozic_int = 0;
        on_Hint = false;
        count = 0;
    }
    private void Start()
    {
        hintBtn.gameObject.SetActive(on_Hint);
        hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_creat;
        //hint_arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        solveLozic += Time.deltaTime;
        solveLozic_int = (int)Mathf.Round(solveLozic);
        if (solveLozic_int > waitTime)
        {
            
            on_Hint = true;
            if(count == 0)
            {
                hintBtn.gameObject.GetComponent<AudioSource>().Play();
                count++;
            }
            hintBtn.gameObject.SetActive(on_Hint);
        }
    }

    public void OnClickButton()
    {
        if (have_hint > 0)
        {
            on_Hint = false;
            hint_arrow.on_ArrowObj = true;
            have_hint--;
            hintBtn.gameObject.SetActive(on_Hint);
            hintBtn.gameObject.GetComponent<AudioSource>().clip = hint_used;
            hintBtn.gameObject.GetComponent<AudioSource>().Play();
            solveLozic = 0;
        }
        
    }

    public void OffHintBtn()
    {
        if (on_Hint)
        {
            on_Hint = false;
            solveLozic = 0f;
            hint_arrow.on_ArrowObj = false;
        }
    }
}
