using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLozic : MonoBehaviour
{
    public struct StarLever
    {
        public GameObject starLever;
        public bool on_lever;
    }

    public StarLever[] lever;

    public Camera getCamera;
    private RaycastHit hit;

    
    public bool lozicClear;

    [SerializeField]
    bool[] currentAnswer;

    SundialLozic sundial;

    public bool hintUsed;

    [SerializeField]
    GameObject clearImage;

    [SerializeField]
    AudioClip downSound;

    [SerializeField]
    AudioClip upSound;

    private void Awake()
    {
        
        lever = new StarLever[4];
        clearImage.SetActive(false);
        for (int i = 0; i < lever.Length; i++)
        {
            string obj_name = "Lever" + i;
            lever[i].starLever = GameObject.Find(obj_name);
            lever[i].on_lever = false;
        }
        lozicClear = false;
        hintUsed = false;
    }
    private void Start()
    {
        sundial = GameObject.Find("Sundial_Object").GetComponent<SundialLozic>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject target_Obj = hit.collider.gameObject;
                OnObjectClick(target_Obj);
            }
        }
    }
    void OnObjectClick(GameObject target) {
        if (sundial.lozicClear)
        {
            if (lozicClear == false)
            {
                if (target.CompareTag("Lever"))
                {
                    int lever_index = int.Parse(target.name[target.name.Length - 1].ToString());
                    if (!target.GetComponent<Animator>().GetBool("is_Click"))
                    {
                        target.GetComponent<Animator>().Play("Lever_Down");
                        target.GetComponent<Animator>().SetBool("is_Click", true);
                        
                        target.GetComponent<AudioSource>().clip = downSound;
                        target.GetComponent<AudioSource>().Play();
                        lever[lever_index].on_lever = true;

                        ;
                    }
                    else
                    {
                        target.GetComponent<Animator>().SetBool("is_Click", false);
                        target.GetComponent<AudioSource>().clip = upSound;
                        target.GetComponent<AudioSource>().Play();
                        lever[lever_index].on_lever = false;
                    }
                    CheckClearLozic();
                    //target.transform.Rotate(new Vector3())
                    //레버내리기 작업
                }
            }
        }
        
    }

    void CheckClearLozic()
    {
        if(lever[0].on_lever == currentAnswer[0] && lever[1].on_lever == currentAnswer[1] && lever[2].on_lever == currentAnswer[2] && lever[3].on_lever == currentAnswer[3])
        {
            lozicClear = true;
            
            StartCoroutine(clearImageSetActive());
        }
    }
    
    IEnumerator clearImageSetActive()
    {
        yield return new WaitForSeconds(1.5f);
        clearImage.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        clearImage.SetActive(false);
        StopCoroutine(clearImageSetActive());
    }
}
