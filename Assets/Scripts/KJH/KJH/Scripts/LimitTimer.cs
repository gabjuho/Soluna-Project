using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LimitTimer : MonoBehaviour
{
    public float LimitTime;
    public int LimitTime_int;
    public TextMeshProUGUI LimitText;

    public int h;
    public int m;
    public int s;

    private void Awake()
    {
        LimitTime = 1800;
    }
    void Update()
    {
        LimitTime -= Time.deltaTime;
        LimitTime_int = int.Parse(Mathf.Round(LimitTime).ToString());
        s = LimitTime_int;
        if (s > 60)
        {
            m = s / 60;
            s = s % 60;
            if (m > 60)
            {
                h = m / 60;
                m = m % 60;
            }
        }
        LimitText.text = string.Format("{0:D2}",h) + " : " + string.Format("{0:D2}", m) + " : " + string.Format("{0:D2}", s);
    }
}
