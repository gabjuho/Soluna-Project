using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTimeButton : MonoBehaviour
{
    private bool isCoolTime; //R클릭 후 쿨타임 true/클릭 불가능, false/클릭 가능
    public float coolTime; //쿨타임 시간
    private bool isDay; //낮, 밤 판별 변수
    public Camera mainCamera; //스카이박스 보여질 카메라
    public Material day, night; //낮, 밤 스카이박스 메터리얼
    public Material red, green, blue, purple, black;
    public GameObject leftWall, rightWall, middleWall; //오른쪽, 왼쪽, 중앙 decoration 오브젝트
    public GameObject leftBookShelf, rightBookShelf, middleBookShelf;
    public MeshRenderer RedCrystal, GreenCrystal, BlueCrystal, PurpleCrystal;

    void Start()
    {
        mainCamera.GetComponent<Skybox>().material = day; //디폴트는 낮
        isDay = true;
        isCoolTime = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isCoolTime) //R클릭 시 쿨타임 체크 후 아래 코드 진행
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
            StartCoroutine(CoolTime(coolTime)); //쿨타임 시작
        }
    }
    void FixedUpdate()
    {
        if (isCoolTime && !isDay)
        {
            //벽 움직임
            leftWall.GetComponent<Transform>().position = Vector3.MoveTowards(leftWall.transform.position, new Vector3(5.0f, leftWall.transform.position.y, leftWall.transform.position.z), 5f * Time.deltaTime);
            middleWall.GetComponent<Transform>().position = Vector3.MoveTowards(middleWall.transform.position, new Vector3(middleWall.transform.position.x, middleWall.transform.position.y, -5.0f), 5f * Time.deltaTime);
            rightWall.GetComponent<Transform>().position = Vector3.MoveTowards(rightWall.transform.position, new Vector3(-5.0f, rightWall.transform.position.y, rightWall.transform.position.z), 5f * Time.deltaTime);

            //책장 움직임
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

    IEnumerator CoolTime(float cool) //쿨타임 함수
    {
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        isCoolTime = false;
    }
}
