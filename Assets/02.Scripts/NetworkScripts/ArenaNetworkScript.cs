using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArenaNetworkScript : MonoBehaviour {

    public GameObject mainMenuPanel;
    public GameObject lobbyPanel;

    private void Awake()
    {
        //NetworkManager.singleton.StartMatchMaker();
    }

    // 만들어진 로비가 없으면 자동으로 Create, 있다면 들어가기 

    public void OnCreateLobbyButton()
    {
        mainMenuPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        //ArenaLobbyManager.singleton.StartHost();
        NetworkLobbyManager.singleton.StartHost();
    }

    public void OnCompetitionButton()
    {

    }

    // 검색되는 로비가 있다면 클라이언트로 참여, 검색 되는 로비가 없다면 호스트로 로비 생성
    public void OnCooperationButton()
    {
        mainMenuPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        //ArenaLobbyManager.singleton.StartClient();
        NetworkLobbyManager.singleton.StartClient();
    }

    public void OnReadyButton()
    {
        ArenaLobbyPlayer.localPlayer.SendReadyToBeginMessage();

    }

}
