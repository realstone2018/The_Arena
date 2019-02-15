using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ArenaLobbyScript : MonoBehaviour {

    public Text readyText;
    public bool ready = false;

	public void OnMakeLobbyButton()
    {

        NetworkLobbyManager.singleton.StartHost();
    }

    public void OnFindLobbyButton()
    {
        NetworkLobbyManager.singleton.StartClient();
    }
    /*
    public void OnReadyButton()
    {
        ready = !ready;

        if (ready)
        {
            //ChangeReady();  자동으로 호출되는 함수인지 확인 필요 
            ArenaLobbyPlayer.localPlayer.SendReadyToBeginMessage();
        }
        //else  ChagneNotReady();     
    }

    public void ChangeReady()
    {
        // NGUI 버튼 색상 변경 
        ChangeReadyText("Ready");
    }

    public void ChangeNotReady()
    {
        // NGUI 버튼 색상 변경 
        ChangeReadyText("Not Ready");
    }

    public void ChangeReadyText(string t)
    {
        readyText.text = t;
    }
    */  
}
