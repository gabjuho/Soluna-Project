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

    public static GameObject target2F; //2Ãþ ÈùÆ® Å¸°Ù

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (hintObj.activeSelf && !hintManager.on_Hint)
        {
            if (scene.name.Equals("1F Test"))
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
            }
            else if (scene.name.Equals("2F"))
            {
                if (target2F == null)
                {
                    hintObj.SetActive(false);
                    return;
                }
                //ÈùÆ® È­»ìÇ¥ Ãâ·Â
                hintObj.transform.LookAt(new Vector3(target2F.transform.position.x, player.transform.position.y, target2F.transform.position.z), Vector3.up);
                hintObj.transform.Rotate(new Vector3(hintObj.transform.rotation.x, hintObj.transform.rotation.y + 90, hintObj.transform.rotation.z));

                hintObj.transform.position = player.transform.position + -hintObj.transform.right * 1f;
            }
            else if(scene.name.Equals("3F"))
            {
                //ÈùÆ® È­»ìÇ¥ Ãâ·Â
                hintObj.transform.LookAt(new Vector3(target2F.transform.position.x, player.transform.position.y, target2F.transform.position.z), Vector3.up);
                hintObj.transform.Rotate(new Vector3(hintObj.transform.rotation.x, hintObj.transform.rotation.y + 90, hintObj.transform.rotation.z));

                hintObj.transform.position = player.transform.position + -hintObj.transform.right * 1f;
            }
        }
    }
}
