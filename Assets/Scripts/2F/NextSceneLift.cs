using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextSceneLift : MonoBehaviour
{
    Vector3 destination = new Vector3(0f, 25f, -9.023893e-17f);
    bool liftWorking = false;
    public GameObject fadeObject;
    private Image fadeImage;
    private bool fading;
    private float time = 0f;
    private float animeTime = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (SecondFloorManager.currentState == SecondFloorManager.SecondFloorState.ThirdPuzzle && other.gameObject.name.Equals("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<MeshCollider>().enabled = true;
            fadeObject.SetActive(true);
            fadeImage = fadeObject.GetComponent<Image>();
            Invoke("WorkLiftTrigger", 2f);
            Invoke("FadeStart", 6f);
        }
    }

    private void FixedUpdate()
    {
        if (liftWorking)
        {
            Vector3 speed = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref speed, 1f);
        }
    }

    private void WorkLiftTrigger()
    {
        liftWorking = true;
    }
    void Update()
    {
        if (fading)
            FadeIn();
    }
    public void FadeStart() //FadeIn ½ÃÀÛ
    {
        fading = true;
    }

    public void FadeIn()
    {
        if (time >= animeTime)
        {
            fading = false;
            gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        time += Time.deltaTime / animeTime;

        Color color = fadeImage.color;
        color.a = Mathf.Lerp(0, 1, time);
        fadeImage.color = color;
    }


}
