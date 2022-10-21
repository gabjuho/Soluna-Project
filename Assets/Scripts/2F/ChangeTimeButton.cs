using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTimeButton : MonoBehaviour
{
    private bool isCoolTime; //RŬ�� �� ��Ÿ�� true/Ŭ�� �Ұ���, false/Ŭ�� ����
    public float coolTime; //��Ÿ�� �ð�
    static public bool isDay; //��, �� �Ǻ� ����
    public Camera mainCamera; //��ī�̹ڽ� ������ ī�޶�
    public Material day, night; //��, �� ��ī�̹ڽ� ���͸���
    public Material red, green, blue, purple, black;
    public Material redRockDayMat, greenRockDayMat, blueRockDayMat, purpleRockDayMat;
    public Material redRockNightMat, greenRockNightMat, blueRockNightMat, purpleRockNightMat;
    public GameObject leftWall, rightWall, middleWall; //������, ����, �߾� decoration ������Ʈ
    public GameObject leftBookShelf, rightBookShelf, middleBookShelf;
    public MeshRenderer RedCrystal, GreenCrystal, BlueCrystal, PurpleCrystal;
    public MeshRenderer RedRockMesh, GreenRockMesh, BlueRockMesh, PurpleRockMesh;
    public GameObject RedRock, GreenRock, BlueRock, PurpleRock;

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
            if (mainCamera.GetComponent<Skybox>().material == day && isDay) //���� ��
            {
                mainCamera.GetComponent<Skybox>().material = night;
                isDay = false;
                HintStateManager.ChangeTimeState(HintStateManager.TimeState.Night);
            }
            else //���� ��
            {
                mainCamera.GetComponent<Skybox>().material = day;
                isDay = true;
                HintStateManager.ChangeTimeState(HintStateManager.TimeState.Day);
            }
            ChangeCrystalRockColor();
            isCoolTime = true;
            StartCoroutine(CoolTime(coolTime)); //��Ÿ�� ����
        }
    }
    void FixedUpdate()
    {
        if (isCoolTime && !isDay)
        {
            leftWall.GetComponent<Transform>().position = Vector3.MoveTowards(leftWall.transform.position, new Vector3(3.153789e-22f, leftWall.transform.position.y, leftWall.transform.position.z), 3f * Time.deltaTime);
            middleWall.GetComponent<Transform>().position = Vector3.MoveTowards(middleWall.transform.position, new Vector3(middleWall.transform.position.x, middleWall.transform.position.y, 1.550293e-06f), 3f * Time.deltaTime);
            rightWall.GetComponent<Transform>().position = Vector3.MoveTowards(rightWall.transform.position, new Vector3(3.153789e-22f, rightWall.transform.position.y, rightWall.transform.position.z), 3f * Time.deltaTime);

            leftBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(leftBookShelf.transform.position, new Vector3(5.0f, leftBookShelf.transform.position.y, leftBookShelf.transform.position.z), 4f * Time.deltaTime);
            middleBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(middleBookShelf.transform.position, new Vector3(middleBookShelf.transform.position.x, middleBookShelf.transform.position.y, -28.0f), 4f * Time.deltaTime);
            rightBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(rightBookShelf.transform.position, new Vector3(-5.0f, rightBookShelf.transform.position.y, rightBookShelf.transform.position.z), 4f * Time.deltaTime);
        }
        else if(isCoolTime && isDay)
        {
            //�� ������
            leftWall.GetComponent<Transform>().position = Vector3.MoveTowards(leftWall.transform.position, new Vector3(5.0f, leftWall.transform.position.y, leftWall.transform.position.z), 3f * Time.deltaTime);
            middleWall.GetComponent<Transform>().position = Vector3.MoveTowards(middleWall.transform.position, new Vector3(middleWall.transform.position.x, middleWall.transform.position.y, -5.0f), 3f * Time.deltaTime);
            rightWall.GetComponent<Transform>().position = Vector3.MoveTowards(rightWall.transform.position, new Vector3(-5.0f, rightWall.transform.position.y, rightWall.transform.position.z), 3f * Time.deltaTime);

            //å�� ������
            leftBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(leftBookShelf.transform.position, new Vector3(-1.16f, leftBookShelf.transform.position.y, leftBookShelf.transform.position.z), 4f * Time.deltaTime);
            middleBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(middleBookShelf.transform.position, new Vector3(middleBookShelf.transform.position.x, middleBookShelf.transform.position.y, -23.5f), 4f * Time.deltaTime);
            rightBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(rightBookShelf.transform.position, new Vector3(1.28f, rightBookShelf.transform.position.y, rightBookShelf.transform.position.z), 4f * Time.deltaTime);
        }
    }
    void ChangeCrystalRockColor()
    {
        if (!isDay)
        {
            //���� ���� ����
            RedCrystal.material = red;
            GreenCrystal.material = green;
            BlueCrystal.material = blue;
            PurpleCrystal.material = purple;

            //�� ���� ����
            RedRockMesh.material = redRockNightMat;
            GreenRockMesh.material = greenRockNightMat;
            BlueRockMesh.material = blueRockNightMat;
            PurpleRockMesh.material = purpleRockNightMat;

            RedRock.GetComponent<Rigidbody>().isKinematic = false;
            GreenRock.GetComponent<Rigidbody>().isKinematic = false;
            BlueRock.GetComponent<Rigidbody>().isKinematic = false;
            PurpleRock.GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            //���� ���� ����
            RedCrystal.material = black;
            GreenCrystal.material = black;
            BlueCrystal.material = black;
            PurpleCrystal.material = black;

            //�� ���� ����
            RedRockMesh.material = redRockDayMat;
            GreenRockMesh.material = greenRockDayMat;
            BlueRockMesh.material = blueRockDayMat;
            PurpleRockMesh.material = purpleRockDayMat;

            RedRock.GetComponent<Rigidbody>().isKinematic = true;
            GreenRock.GetComponent<Rigidbody>().isKinematic = true;
            BlueRock.GetComponent<Rigidbody>().isKinematic = true;
            PurpleRock.GetComponent<Rigidbody>().isKinematic = true;
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
