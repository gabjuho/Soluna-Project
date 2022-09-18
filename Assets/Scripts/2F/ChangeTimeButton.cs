using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTimeButton : MonoBehaviour
{
    private bool isCoolTime = false; //RŬ�� �� ��Ÿ�� true/Ŭ�� �Ұ���, false/Ŭ�� ����
    public float coolTime; //��Ÿ�� �ð�
    public Camera mainCamera; //��ī�̹ڽ� ������ ī�޶�
    public Material day, night; //��, �� ��ī�̹ڽ� ���͸���

    void Start()
    {
        mainCamera.GetComponent<Skybox>().material = day; //����Ʈ�� ��
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isCoolTime) //RŬ�� �� ��Ÿ�� üũ �� �Ʒ� �ڵ� ����
        {
            if (mainCamera.GetComponent<Skybox>().material == day)
                mainCamera.GetComponent<Skybox>().material = night;
            else
                mainCamera.GetComponent<Skybox>().material = day;
            isCoolTime = true;
            StartCoroutine(CoolTime(coolTime)); //��Ÿ�� ����
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
