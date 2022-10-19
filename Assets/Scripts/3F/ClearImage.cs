using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearImage : MonoBehaviour
{
    public GameObject textPanel;
    private Image image;
    private bool fading;
    private float time = 0f;
    private float animeTime = 1f;

    public void FadeReady() //FadeOut 준비
    {
        gameObject.SetActive(true);
        Invoke("FadeStart", 2f);
    }
    public void FadeStart() //FadeOut 시작
    {
        fading = true;
    }

    public void FadeOut()
    {
        if (time >= animeTime)
        {
            fading = false;
            gameObject.SetActive(false);
            textPanel.SetActive(false);
            return;
        }

        time += Time.deltaTime / animeTime;

        Color color = image.color;
        color.a = Mathf.Lerp(1, 0, time);
        image.color = color;
    }

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (fading)
            FadeOut();
    }
}
