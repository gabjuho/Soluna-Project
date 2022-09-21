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

    private void Awake()
    {
        lever = new StarLever[4];
        for(int i = 0; i < lever.Length; i++)
        {
            string obj_name = "Lever" + i;
            lever[i].starLever = GameObject.Find(obj_name);
            lever[i].on_lever = false;
        }
        lozicClear = false;
    }
    void Start()
    {
        
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
        if(lozicClear == false)
        {
            if (target.CompareTag("Lever"))
            {
                StartCoroutine(MoveLever_Down(target));
                //target.transform.Rotate(new Vector3())
                //���������� �۾�
            }
        }
    }

    public IEnumerator MoveLever_Down(GameObject target)
    {
        target.GetComponent<Animator>().Play("Lever_Down");
        yield return new WaitForSeconds(1.5f);
        target.GetComponent<Animator>().Play("New State");

    }
}
