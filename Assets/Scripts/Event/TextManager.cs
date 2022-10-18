using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public GameObject TextPanel;
    public TextMeshProUGUI DescText;

    [Space(20)]
    public Image CharaFace_Img;

    [HideInInspector]
    public bool isText = false;

    [SerializeField]
    public Dictionary<int, string[]> textData;
    [SerializeField]
    private Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    private void Awake()
    {
        textData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();

        GenerateData();

        TextPanel.SetActive(isText);
    }

    // : 뒤에 숫자는 표정
    // 기본 = 0 / 웃음 = 1 / 찌푸림 = 2 / 놀람 = 3
    void GenerateData()
    {

        #region 모든 층 공통 대사

        //틀린 아이템 사용/올바르지 못한 퍼즐 풀이
        textData.Add(1, new string[] { "이렇게 하는게 아닌거 같아:2" });
        //아직 풀 수 없는 퍼즐을 풀려고 할 시
        textData.Add(2, new string[] { "지금은 다른거부터 해결하고 오자:0" });
        //이미 푼 퍼즐과 상호작용 하려고 할 시
        textData.Add(3, new string[] { "거기에는 더 이상 문제가 없는거 같아:0" });
        //주울 수 있는 아이템과 Q로 상호작용 할 시 
        textData.Add(4, new string[] { "내 가방에 들어갈 수 있는 사이즈 같아. 넣어둘까?:0" });
        //시간이 15분 남았을 시
        textData.Add(5, new string[] { "개기일식 시간까지 지구 시간으로 15분 정도 남은 거 같아:0" });
        //시간이 10분 남았을 시
        textData.Add(6, new string[] { "조금 서둘러야 할 거 같아 10분 남았어.:2" });
        //시간이 5분 남았을 시
        textData.Add(7, new string[] { "이제 5분 뒤면 시작이야!:3" });
        #endregion

        #region 거대 톱니바퀴
        //퍼즐 해금전
        textData.Add(100, new string[] { "엄청 크다!:3", "그런데 왜 작동을 안하지?:2", "아마도 이게 시간이 어긋난 원인 중 하나인 것 같아.:0" });

        //퍼즐 해결
        //작은 톱니바퀴 아이템 사용
        textData.Add(101, new string[] { "작은 톱니바퀴가 빠져있었구나!:1", "오 톱니바퀴가 끼워졌어.:3", "그런데 아직 작동을 안하네.. 무슨 일일까?:2", " 앗 톱니바퀴가 맞물리지 않고 있어 조금 돌려보면 될 거 같아.:3" });
        #endregion

        #region 톱니바퀴 조절기
        //퍼즐 활성화 전
        textData.Add(110, new string[] { "톱니바퀴와 관련된 기계 같은데... :0", "지금은 아무런 반응도 일어나지 않아.:0" });
        //거대 톱니 해결 후
        textData.Add(111, new string[] { "이 기계는 어떻게 작동하는걸까?:0", "내가 듣기로 증기기관을 돌리려면 톱니바퀴가 먼저 돌아가야한다고 들었는데… :0 ", "이걸로 돌릴 수 있으려나? :0" });
        //올바른 순서대로 풀이
        textData.Add(112, new string[] { "됐어! 이제 증기기관이 돌아가겠지?:1" });
        #endregion

        #region 증기기관
        //퍼즐 활성화 전
        textData.Add(120, new string[] { "증기기관이야..:0" });
        //톱니바퀴 조절기 해결 후
        textData.Add(121, new string[] { "증기기관은 왜 또 안돌아가는거야..?:2" });
        //1차 해결 - 별 연료 사용
        textData.Add(122, new string[] { "별 연료가 부족했구나:1", "어라…이상한 연기가 새어나오잖아. 뭔가 위험해보여! 얼른 고쳐야할 거 같아. :3" });
        //2차 해결 - 공구박스 사용
        textData.Add(123, new string[] { "다행이다 이상한 연기가 멎은거 같아.:1", "하지만 이런 소소한 문제로 태양과 달의 시간이 달라진거 같지는 않아. 방을 조금 더 살펴보자.:0" });

        #endregion

        #region 해시계
        //퍼즐 활성화 전
        textData.Add(130, new string[] { "해시계야.:0", "일반 해시계와는 달리 스스로가 빛을 내서 해시계라고 불려.:0" });
        //이전 퍼즐 해결 후
        textData.Add(131, new string[] { "해시계의 빛이 꺼졌네. :0", "별 조각이 하나 빠져있는거 같아.. :0" });
        //퍼즐 해결  
        //별 조각 사용
        textData.Add(132, new string[] { "제대로 끼워진 거 같아.:1" });
        #endregion

        #region 별 조절기
        //퍼즐 활성화 전  
        textData.Add(140, new string[] { "별을 조절하는 레버야. 나는 별 조절기라고 불러.:0" });
        //이전 퍼즐 해결 후
        textData.Add(141, new string[] { "이걸 어떻게 켜야 하는지 기억이 나지 않는걸..:2" });
        //퍼즐 해결  
        //올바른 순서대로 풀이
        textData.Add(142, new string[] { "해결했다! :1" });
        #endregion

        #region 문
        //1층 퍼즐 해결 전
        textData.Add(150, new string[] { "문이 열리지 않아..:2", "어차피 탑의 모든 문제를 해결해야 하니까 다 해결하고 생각해보자.:0" });
        //모든 퍼즐 해결 후
        textData.Add(151, new string[] { "문이 열렸어!:3", "2층으로 올라가자. :0" });
        #endregion

        #region 2층 책상 
        //활성화 전
        textData.Add(160, new string[] { "책장에 책이 몇 권 빠져있어.. :0", "정리해야하지 않을까? :2" });
        //해결 - 책 아이템 3권 다 지정 위치에서 사용
        textData.Add(161, new string[] { "깔끔하게 정리되었는걸 :1" });
        #endregion

        #region 2층 책
        textData.Add(162, new string[] { "책장에 책이 몇 권 빠져있어.. :0", "정리해야하지 않을까? :2" });
        #endregion

        #region 천사상
        //퀴즈전
        textData.Add(170, new string[] { "천사상이야. 손에 칼이 들려있네 :0" });
        //퀴즈 해결후 - 빨간색, 파란색, 초록색, 보라색
        textData.Add(171, new string[] { "무슨 소리가 나네? 어디선가 들었던 소리 같은데:3" });
        #endregion

        #region 수정탑
        //활성화 전
        textData.Add(180, new string[] { "예쁜 수정이 위에 달려있는 수정탑이야. 지금은 신경쓰지 않아도 괜찮을 거 같아.:0" });
        // 책장 퀴즈 해결 후
        textData.Add(181, new string[] { "이 수정탑을 조정해서 흐트러진 중력을 바꿔야해.:0" });
        //해결
        textData.Add(182, new string[] { "중력이 정상화 되었어. :1" });
        #endregion

        #region 거대 별 조각
        //퀴즈 전
        textData.Add(190, new string[] { "불이 켜지지 않았어..:0", "이게 고쳐져야 시계탑을 완전히 고치는 건데.. :2", "행성 조각을 맞게 끼우면 될 거 같아! :0" });
        //아이템 사용 - 거대 별조각
        textData.Add(191, new string[] { "빛이 켜졌어!:1" });
        #endregion

        #region 천체 시계
        textData.Add(200, new string[] { "우와 엄청 크다!:3", "이게 개기일식이 흐트러진 원인이구나:0", "얼른 고쳐야겠어:0" });
        #endregion

        #region 3층 퍼즐 끝낸 후
        textData.Add(210, new string[] { "고쳐졌어!:3", "이제 개기일식이 정상화 될거야!:1" });
        #endregion

        textData.Add(1000, new string[] { "ID-1000  TEXT test:0", "1:0", "2:0", "3:0", "4:0", "5:0", "Nice, its not have bug:1" });
        portraitData.Add(0, portraitArr[0]);
        portraitData.Add(1, portraitArr[1]);
        portraitData.Add(2, portraitArr[2]);
        portraitData.Add(3, portraitArr[3]);
    }

    public string GetTalk(int id, int talkINdex)
    {
        if (talkINdex == textData[id].Length) return null;
        else return textData[id][talkINdex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[portraitIndex];
    }

    public void TalkCon()
    {
        TextPanel.SetActive(isText);
    }
}