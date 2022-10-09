using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rock_Puzzle : MonoBehaviour
{
    public GameObject[] rock = new GameObject[4];
    public GameObject[] rockTrigger = new GameObject[4];
    private GameObject[] destroyRock;
    public GameObject movementRange;
    public GameObject clearImage;
    public bool angelIsCool;
    public int angelCoolTime;
    private bool isFadeOut;
    private float time;
    public float animeTime; //페이드 아웃 실행시간
    // Start is called before the first frame update
    private void Awake()
    {
        isFadeOut = false;
    }
    void Start()
    {
        destroyRock = GameObject.FindGameObjectsWithTag("Destroy_Rock");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeOut && time < 1)
            ClearFadeOut();
        else if (isFadeOut && time >= 1)
        {
            time = 0;
            isFadeOut = false;
            clearImage.SetActive(false);
        }
    }

    public void RockPuzzleSet()
    {
        movementRange.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            rock[i].GetComponent<Rigidbody>().useGravity = true;
            rock[i].transform.localScale = new Vector3(0.782992f, 0.782992f, 0.782992f);
            rockTrigger[i].SetActive(true);
        }

        int length = destroyRock.Length;

        for (int i = 0; i < length; i++) //퍼즐에 사용할 오브젝트를 제외한 나머지 돌들은 비활성화 하기
            destroyRock[i].SetActive(false);
    }

    public void CheckClear()
    {
        if (ChangeTimeButton.isDay)
            return;
        for (int i = 0; i < 4; i++)
            if (!rock[i].GetComponent<Rock>().isCorrect)
                return;

        for(int i=0;i<4;i++)
            rock[i].GetComponent<Rigidbody>().isKinematic = true;

        SecondFloorManager.currentState = SecondFloorManager.SecondFloorState.ThirdPuzzle;

        clearImage.SetActive(true);
        Invoke("ChangeFadeOut", 2f);

        Debug.Log("3단계 퍼즐 클리어");
    }
    public void AngelResetPuzzle() //천사상으로 리셋
    {
        if (!angelIsCool)//천사상 쿨타임이 아니면
        {
            rock[0].transform.position = new Vector3(0f, 8.338691f, 1.443823e-15f);
            rock[1].transform.position = new Vector3(4.121958f, 7.157758f, 2.954749f);
            rock[2].transform.position = new Vector3(-1.235358f, 7.539322f, -4.474335f);
            rock[3].transform.position = new Vector3(-3.683578f, 6.711401f, 2.730377f);
            angelIsCool = true;
            Invoke("CoolIsDone", 3f);
        }
    }
    public void ResetPuzzle() //15초 지나면 자동으로 리셋
    {
        rock[0].transform.position = new Vector3(0f, 8.338691f, 1.443823e-15f);
        rock[1].transform.position = new Vector3(4.121958f, 7.157758f, 2.954749f);
        rock[2].transform.position = new Vector3(-1.235358f, 7.539322f, -4.474335f);
        rock[3].transform.position = new Vector3(-3.683578f, 6.711401f, 2.730377f);
    }
    public void CoolIsDone()
    {
        angelIsCool = false;
    }

    public void ChangeFadeOut()//isFadeOut bool값 true로 변경
    {
        isFadeOut = true;
    }

    public void ClearFadeOut()
    {
        time += Time.deltaTime / animeTime;
        Color color = clearImage.GetComponent<Image>().color;
        color.a = Mathf.Lerp(1f, 0f, time);
        clearImage.GetComponent<Image>().color = color;
    }
}
