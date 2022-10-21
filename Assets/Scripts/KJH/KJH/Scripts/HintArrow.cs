using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HintArrow : MonoBehaviour
{
    private Scene scene;
    [SerializeField]
    LozicManager lozicManager;
    [SerializeField]
    HintManager hintManager;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject hintObj;

    [SerializeField]
    GameObject[] target;

    public bool on_ArrowObj;
    public static GameObject target2F; //2층 힌트 타겟

    // Update is called once per frame
    private void Awake()
    {
        on_ArrowObj = false;
    }
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        hintObj.SetActive(on_ArrowObj);
    }

    void Update()
    {
        if (scene.name.Equals("1F"))
        {
            hintObj.SetActive(on_ArrowObj);
            if (!hintManager.on_Hint)
            {

                for (int i = 0; i < lozicManager.solve_Lozic.Length; i++)
                {
                    if (lozicManager.solve_Lozic[i] == false)
                    {
                        hintObj.transform.LookAt(new Vector3(target[i].transform.position.x, player.transform.position.y, target[i].transform.position.z), Vector3.up);
                        hintObj.transform.Rotate(new Vector3(hintObj.transform.rotation.x, hintObj.transform.rotation.y + 90, hintObj.transform.rotation.z));

                        hintObj.transform.position = player.transform.position + -hintObj.transform.right * 1f;

                        break;
                    }
                }

                for (int i = 0; i < lozicManager.solve_Lozic.Length; i++)
                {
                    if (lozicManager.solve_Lozic[i] == false)
                    {
                        return;
                    }
                }

                hintObj.SetActive(false);
            }
        }
        else if (scene.name.Equals("2F"))
        {
            if (hintObj.activeSelf && !hintManager.on_Hint)
            {
                if (target2F == null)
                {
                    hintObj.SetActive(false);
                    return;
                }
                //힌트 화살표 출력
                hintObj.transform.LookAt(new Vector3(target2F.transform.position.x, player.transform.position.y, target2F.transform.position.z), Vector3.up);
                hintObj.transform.Rotate(new Vector3(hintObj.transform.rotation.x, hintObj.transform.rotation.y + 90, hintObj.transform.rotation.z));

                hintObj.transform.position = player.transform.position + -hintObj.transform.right * 1f;
            }
        }
    }
}
