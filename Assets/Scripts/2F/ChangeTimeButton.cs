using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTimeButton : MonoBehaviour
{
    private bool isCoolTime = false; //R클릭 후 쿨타임 true/클릭 불가능, false/클릭 가능
    public float coolTime; //쿨타임 시간
    public Camera mainCamera; //스카이박스 보여질 카메라
    public Material day, night; //낮, 밤 스카이박스 메터리얼

    void Start()
    {
        mainCamera.GetComponent<Skybox>().material = day; //디폴트는 낮
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isCoolTime) //R클릭 시 쿨타임 체크 후 아래 코드 진행
        {
            if (mainCamera.GetComponent<Skybox>().material == day)
                mainCamera.GetComponent<Skybox>().material = night;
            else
                mainCamera.GetComponent<Skybox>().material = day;
            isCoolTime = true;
            StartCoroutine(CoolTime(coolTime)); //쿨타임 시작
        }
    }

    IEnumerator CoolTime(float cool)
    {
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        isCoolTime = false;
    }
}
