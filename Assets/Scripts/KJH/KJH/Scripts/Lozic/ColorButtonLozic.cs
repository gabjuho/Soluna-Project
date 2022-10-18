using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButtonLozic : MonoBehaviour
{
    [SerializeField]
    Material m_leftbutton;
    [SerializeField]
    Material m_rightbutton;

    int left;
    int right;

    [SerializeField]
    bool[] answer; //왼쪽 true 오른쪽 false
    [SerializeField]
    int answser_index;

    public bool lozicClear;

    [SerializeField]
    Color[] rightColor;
    [SerializeField]
    Color[] leftColor;
    [SerializeField]
    GameObject reset_Obj;

    [SerializeField]
    bool is_lock;

    public Camera getCamera;

    private RaycastHit hit;

    [SerializeField]
    GameObject on_gear;

    public bool hintUsed;
    public Inventory inven;
    public GameObject[] planets;

    [SerializeField]
    SoundManager sound;

    [SerializeField]
    HintManager hint;
    private void Awake()
    {
        /*
        rightColor = new Color[8];
        leftColor = new Color[8];
        */
        #region 오른쪽 버튼 색상
        /*
        rightColor[0].r = 0xF9;  
        rightColor[0].g = 0x4B;  
        rightColor[0].b = 0x8F;
        rightColor[0].a = 255;

        rightColor[1].r = 0xFF;  
        rightColor[1].g = 0xFB;
        rightColor[1].b = 0x52;
        rightColor[1].a = 255;

        rightColor[2].r = 0x5E;  
        rightColor[2].g = 0xCF;
        rightColor[2].b = 0x95;
        rightColor[2].a = 255;

        rightColor[3].r = 0xF9;
        rightColor[3].g = 0x4B;
        rightColor[3].b = 0x8F;
        rightColor[3].a = 255;

        rightColor[4].r = 0xFE;
        rightColor[4].g = 0xAA;
        rightColor[4].b = 0x42;
        rightColor[4].a = 255;

        rightColor[5].r = 0x26;
        rightColor[5].g = 0x26;
        rightColor[5].b = 0x26;
        rightColor[5].a = 255;

        rightColor[6].r = 0x4B;
        rightColor[6].g = 0xC6;
        rightColor[6].b = 0xE8;
        rightColor[6].a = 255;

        rightColor[7].r = 0x9B;
        rightColor[7].g = 0x4D;
        rightColor[7].b = 0xEC;
        rightColor[7].a = 255;
        */
        #endregion

        #region 왼쪽 버튼 색상
        /*
        leftColor[0].r = 0x4B;
        leftColor[0].g = 0xC6;
        leftColor[0].b = 0xE8;
        leftColor[0].a = 255;

        leftColor[1].r = 0x42;
        leftColor[1].g = 0x96;
        leftColor[1].b = 0x1F;
        leftColor[1].a = 255;

        leftColor[2].r = 0x5E;
        leftColor[2].g = 0xCF;
        leftColor[2].b = 0x95;
        leftColor[2].a = 255;

        leftColor[3].r = 0xF9;
        leftColor[3].g = 0x4B;
        leftColor[3].b = 0x8F;
        leftColor[3].a = 255;

        leftColor[4].r = 0xFE;
        leftColor[4].g = 0xAA;
        leftColor[4].b = 0x42;
        leftColor[4].a = 255;

        leftColor[5].r = 0x26;
        leftColor[5].g = 0x26;
        leftColor[5].b = 0x26;
        leftColor[5].a = 255;

        leftColor[6].r = 0x4B;
        leftColor[6].g = 0xC6;
        leftColor[6].b = 0xE8;
        leftColor[6].a = 255;

        leftColor[7].r = 0x9B;
        leftColor[7].g = 0x4D;
        leftColor[7].b = 0xEC;
        leftColor[7].a = 255;
        */
        #endregion
        lozicClear = false;
        is_lock = true;
        answser_index = 0;
        left = 0;
        right = 0;

        hintUsed = false;
    }

    void Start()
    {
        m_rightbutton.color = rightColor[0];
        m_leftbutton.color = leftColor[0];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                GameObject target_Obj = hit.collider.gameObject;
                OnObjectClick(target_Obj);
            }
        }
    }

    void OnObjectClick(GameObject target)
    {
        if (on_gear.activeSelf)
        {
            if(lozicClear == false)
            {
                if (target.name.Equals("Cylinder037_ButtonObj"))    //오른쪽버튼
                {
                    if (!CheckAnswer(false))
                    {
                        ResetLozic();
                        return;
                    }
                    answser_index++;
                    right++;
                    m_rightbutton.color = rightColor[right % 8];
                    left++;
                    m_leftbutton.color = leftColor[left % 8];
                }
                if (target.name.Equals("Object053_ButtonObj"))    //왼쪽버튼
                {
                    if (!CheckAnswer(true))
                    {
                        ResetLozic();
                        return;
                    }
                    answser_index++;
                    right++;
                    m_rightbutton.color = rightColor[right % 8];
                    left++;
                    m_leftbutton.color = leftColor[left % 8];
                }
                if (target.name.Equals("Object052_ButtonObj"))    //초기화버튼
                {
                    ResetLozic();
                }
                if (answser_index == 8)
                {
                    Debug.Log("색상로직 클리어");
                    lozicClear = true;
                    FieldItem plant_item = planets[4].GetComponent<FieldItem>();
                    inven.AddItem(plant_item.GetItem());
                    hint.OffHintBtn();
                }
            }
            
        }
        
    }

    bool CheckAnswer(bool check)
    {
        if(answer[answser_index % 8] == check)
        {
            Debug.Log("정답");
            
            return true;
        }
        else
        {
            Debug.Log("오답");
            return false;
        }
    }
    void ResetLozic()
    {
        answser_index = 0;
        left = 0;
        m_leftbutton.color = leftColor[left % 8];
        right = 0;
        m_rightbutton.color = rightColor[right % 8];
    }
}
