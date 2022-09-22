using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTimeButton : MonoBehaviour
{
    private bool isCoolTime; //RŬ�� �� ��Ÿ�� true/Ŭ�� �Ұ���, false/Ŭ�� ����
    public float coolTime; //��Ÿ�� �ð�
    private bool isDay; //��, �� �Ǻ� ����
    public Camera mainCamera; //��ī�̹ڽ� ������ ī�޶�
    public Material day, night; //��, �� ��ī�̹ڽ� ���͸���
    public Material red, green, blue, purple, black;
    public GameObject leftWall, rightWall, middleWall; //������, ����, �߾� decoration ������Ʈ
    public GameObject leftBookShelf, rightBookShelf, middleBookShelf;
    public MeshRenderer RedCrystal, GreenCrystal, BlueCrystal, PurpleCrystal;

    void Start()
    {
        mainCamera.GetComponent<Skybox>().material = day; //����Ʈ�� ��
        isDay = true;
        isCoolTime = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isCoolTime) //RŬ�� �� ��Ÿ�� üũ �� �Ʒ� �ڵ� ����
        {
            if (mainCamera.GetComponent<Skybox>().material == day && isDay)
            {
                mainCamera.GetComponent<Skybox>().material = night;
                isDay = false;
            }
            else
            {
                mainCamera.GetComponent<Skybox>().material = day;
                isDay = true;
            }
            ChangeCrystalColor();
            isCoolTime = true;
            StartCoroutine(CoolTime(coolTime)); //��Ÿ�� ����
        }
    }
    void FixedUpdate()
    {
        if (isCoolTime && !isDay)
        {
            //�� ������
            leftWall.GetComponent<Transform>().position = Vector3.MoveTowards(leftWall.transform.position, new Vector3(5.0f, leftWall.transform.position.y, leftWall.transform.position.z), 5f * Time.deltaTime);
            middleWall.GetComponent<Transform>().position = Vector3.MoveTowards(middleWall.transform.position, new Vector3(middleWall.transform.position.x, middleWall.transform.position.y, -5.0f), 5f * Time.deltaTime);
            rightWall.GetComponent<Transform>().position = Vector3.MoveTowards(rightWall.transform.position, new Vector3(-5.0f, rightWall.transform.position.y, rightWall.transform.position.z), 5f * Time.deltaTime);

            //å�� ������
            leftBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(leftBookShelf.transform.position, new Vector3(0f, leftBookShelf.transform.position.y, leftBookShelf.transform.position.z), 6f * Time.deltaTime);
            middleBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(middleBookShelf.transform.position, new Vector3(middleBookShelf.transform.position.x, middleBookShelf.transform.position.y, -23f), 6f * Time.deltaTime);
            rightBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(rightBookShelf.transform.position, new Vector3(0f, rightBookShelf.transform.position.y, rightBookShelf.transform.position.z), 6f * Time.deltaTime);
        }
        else if(isCoolTime && isDay)
        {
            leftWall.GetComponent<Transform>().position = Vector3.MoveTowards(leftWall.transform.position, new Vector3(3.153789e-22f, leftWall.transform.position.y, leftWall.transform.position.z), 5f * Time.deltaTime);
            middleWall.GetComponent<Transform>().position = Vector3.MoveTowards(middleWall.transform.position, new Vector3(middleWall.transform.position.x, middleWall.transform.position.y, 1.550293e-06f), 5f * Time.deltaTime);
            rightWall.GetComponent<Transform>().position = Vector3.MoveTowards(rightWall.transform.position, new Vector3(3.153789e-22f, rightWall.transform.position.y, rightWall.transform.position.z), 5f * Time.deltaTime);

            leftBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(leftBookShelf.transform.position, new Vector3(5.0f, leftBookShelf.transform.position.y, leftBookShelf.transform.position.z), 6f * Time.deltaTime);
            middleBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(middleBookShelf.transform.position, new Vector3(middleBookShelf.transform.position.x, middleBookShelf.transform.position.y, -28.0f), 6f * Time.deltaTime);
            rightBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(rightBookShelf.transform.position, new Vector3(-5.0f, rightBookShelf.transform.position.y, rightBookShelf.transform.position.z), 6f * Time.deltaTime);
        }
    }
    void ChangeCrystalColor()
    {
        if (!isDay)
        {
            RedCrystal.material = red;
            GreenCrystal.material = green;
            BlueCrystal.material = blue;
            PurpleCrystal.material = purple;
        }
        else
        {
            RedCrystal.material = black;
            GreenCrystal.material = black;
            BlueCrystal.material = black;
            PurpleCrystal.material = black;
        }
    }

    IEnumerator CoolTime(float cool) //��Ÿ�� �Լ�
    {
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        isCoolTime = false;
    }
}
