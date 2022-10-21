using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTimeButton : MonoBehaviour
{
    private bool isCoolTime; //R클릭 후 쿨타임 true/클릭 불가능, false/클릭 가능
    public float coolTime; //쿨타임 시간
    static public bool isDay; //낮, 밤 판별 변수
    public Camera mainCamera; //스카이박스 보여질 카메라
    public Material day, night; //낮, 밤 스카이박스 메터리얼
    public Material red, green, blue, purple, black;
    public Material redRockDayMat, greenRockDayMat, blueRockDayMat, purpleRockDayMat;
    public Material redRockNightMat, greenRockNightMat, blueRockNightMat, purpleRockNightMat;
    public GameObject leftWall, rightWall, middleWall; //오른쪽, 왼쪽, 중앙 decoration 오브젝트
    public GameObject leftBookShelf, rightBookShelf, middleBookShelf;
    public MeshRenderer RedCrystal, GreenCrystal, BlueCrystal, PurpleCrystal;
    public MeshRenderer RedRockMesh, GreenRockMesh, BlueRockMesh, PurpleRockMesh;
    public GameObject RedRock, GreenRock, BlueRock, PurpleRock;

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
            if (mainCamera.GetComponent<Skybox>().material == day && isDay) //밤일 때
            {
                mainCamera.GetComponent<Skybox>().material = night;
                isDay = false;
                HintStateManager.ChangeTimeState(HintStateManager.TimeState.Night);
            }
            else //낮일 때
            {
                mainCamera.GetComponent<Skybox>().material = day;
                isDay = true;
                HintStateManager.ChangeTimeState(HintStateManager.TimeState.Day);
            }
            ChangeCrystalRockColor();
            isCoolTime = true;
            StartCoroutine(CoolTime(coolTime)); //쿨타임 시작
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
            //벽 움직임
            leftWall.GetComponent<Transform>().position = Vector3.MoveTowards(leftWall.transform.position, new Vector3(5.0f, leftWall.transform.position.y, leftWall.transform.position.z), 3f * Time.deltaTime);
            middleWall.GetComponent<Transform>().position = Vector3.MoveTowards(middleWall.transform.position, new Vector3(middleWall.transform.position.x, middleWall.transform.position.y, -5.0f), 3f * Time.deltaTime);
            rightWall.GetComponent<Transform>().position = Vector3.MoveTowards(rightWall.transform.position, new Vector3(-5.0f, rightWall.transform.position.y, rightWall.transform.position.z), 3f * Time.deltaTime);

            //책장 움직임
            leftBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(leftBookShelf.transform.position, new Vector3(-1.16f, leftBookShelf.transform.position.y, leftBookShelf.transform.position.z), 4f * Time.deltaTime);
            middleBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(middleBookShelf.transform.position, new Vector3(middleBookShelf.transform.position.x, middleBookShelf.transform.position.y, -23.5f), 4f * Time.deltaTime);
            rightBookShelf.GetComponent<Transform>().position = Vector3.MoveTowards(rightBookShelf.transform.position, new Vector3(1.28f, rightBookShelf.transform.position.y, rightBookShelf.transform.position.z), 4f * Time.deltaTime);
        }
    }
    void ChangeCrystalRockColor()
    {
        if (!isDay)
        {
            //수정 색깔 변경
            RedCrystal.material = red;
            GreenCrystal.material = green;
            BlueCrystal.material = blue;
            PurpleCrystal.material = purple;

            //돌 색깔 변경
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
            //수정 색깔 변경
            RedCrystal.material = black;
            GreenCrystal.material = black;
            BlueCrystal.material = black;
            PurpleCrystal.material = black;

            //돌 색깔 변경
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
