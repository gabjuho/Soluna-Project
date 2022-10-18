using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearImage : MonoBehaviour
{
    private Image image;
    private bool fading;
    private float time = 0f;
    private float animeTime = 1f;

    public void FadeReady() //FadeOut �غ�
    {
        gameObject.SetActive(true);
        Invoke("FadeStart", 2f);
    }
    public void FadeStart() //FadeOut ����
    {
        fading = true;
    }

    public void FadeOut()
    {
        if (time >= animeTime)
        {
            fading = false;
            gameObject.SetActive(false);
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
