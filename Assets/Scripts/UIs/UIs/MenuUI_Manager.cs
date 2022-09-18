using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Timeline;
using UnityEngine.Playables;

public class MenuUI_Manager : MonoBehaviour
{
    public TimelineAsset[] TimeLines;

    PlayableDirector pd;

    //MainMenu 씬 버튼 설정

    //시작 버튼
    public void StartBtn()
    {
        pd = GameObject.Find("UIs").GetComponent<PlayableDirector>();

        pd.playableAsset = TimeLines[0];
        pd.Play();
    }

    //세팅 버튼
    public void SettingsBtn()
    {
        Debug.Log("Option");
    }

    //Exit Game 버튼
    //게임 종료 박스창에서 Yes와 No 버튼중에 No버튼에도 쓸수 있다.
    public void ExitBoxCont_Btn(GameObject _exitobj)
    {
        if(_exitobj.activeSelf == false)
        {
            _exitobj.SetActive(true);
        }
        else
        {
            _exitobj.SetActive(false);
        }
    }

    public void Yes_Btn()
    {
        Application.Quit();
    }
}
