using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class CustomNetworkLobbyManager : NetworkLobbyManager {

	//호스트로서 실행
    public override void OnLobbyStartHost()
    {
        Debug.Log("OnLobbyStartHost");
    }

    //호스트를 정지
    public override void OnLobbyStopHost()
    {
        Debug.Log("OnLobbyStopHost");
    }

    //서버로서 실행
    public override void OnLobbyStartServer()
    {
        Debug.Log("OnLobbyStartServer");
    }

    //서버 연결
    public override void OnLobbyServerConnect (NetworkConnection conn)
    {
        Debug.Log("OnLobbyServerConnect " + conn.connectionId);
    }

    //서버 연결을 해제
    public override void OnLobbyServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("OnLobbyServerDisconnect " + conn.connectionId);
    }

    //서버 측에서 씬이 변경되었다
    public override void OnLobbyServerSceneChanged(string sceneName)
    {
        Debug.Log("OnLobbyServerSceneChanged " + sceneName);
    }

    //서버 측에서 LobbyPlayer가 생성
    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerController)
    {
        Debug.Log("OnLobbyServerCreateLobbyPlayer " + conn.connectionId + ":" + playerController);
        return base.OnLobbyServerCreateLobbyPlayer(conn, playerController);
    }

    //서버측에서 GamePlayer가 생성되었다.
    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerController)
    {
        Debug.Log("OnLobbyServerCreateGamePlayer " + conn.connectionId + ":" + playerController);
        return base.OnLobbyServerCreateGamePlayer(conn, playerController);
    }

    //서버측에서 플레이어가 제거되었다
    public override void OnLobbyServerPlayerRemoved(NetworkConnection conn, short playerController)
    {
        Debug.Log("OnLobbyServerPlayerRemoved " + conn.connectionId + ":" + playerController);
    }

    //서버 측에서 플레이어용으로 씬이 로딩되었다.
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        Debug.Log("ONLobbyServerSceneLoadedForPlayer " + lobbyPlayer.name + ":" + gamePlayer.name);
        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);
    }

    //플레이어들의 준비가 끝났다.
    public override void OnLobbyServerPlayersReady()
    {
        Debug.Log("OnlObbyServerPlayerReady");
        base.OnLobbyServerPlayersReady();
    }

    //클라이언트가 로비에 들어왔다.
    public override void OnLobbyClientEnter()
    {
        Debug.Log("OnLobbyClientEnter");
        base.OnLobbyClientEnter();
    }

    //클라이언트가 로비에서 나갔다.
    public override void OnLobbyClientExit()
    {
        Debug.Log("OnLobbyClientExit");
        base.OnLobbyClientExit();
    }

    //클라이언트가 연결되었다.
    public override void OnLobbyClientConnect(NetworkConnection conn)
    {
        Debug.Log("OnLobbyClientConnect " + conn.connectionId);
        base.OnLobbyClientConnect(conn);
    }

    //클라이언트의 연결이 해제되었다.
    public override void OnLobbyClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("OnLobbyClientDisconnect " + conn.connectionId);
        base.OnLobbyClientDisconnect(conn);
    }

    //클라이언트가 스타트했다.
    public override void OnLobbyStartClient(NetworkClient lobbyClient)
    {
        Debug.Log("OnLobbyStartClient");
        base.OnLobbyStartClient(lobbyClient);
    }

    //클라이언트가 정지했다.
    public override void OnLobbyStopClient()
    {
        Debug.Log("OnLobbyStopClient");
        base.OnLobbyStopClient();
    }

    //클라이언트 측에서 씬이 변경되었다.
    public override void OnLobbyClientSceneChanged(NetworkConnection conn)
    {
        Debug.Log("OnLobbyClientSceneChanged " + conn.connectionId);
        base.OnLobbyClientSceneChanged(conn);
    }

    //클라이언트 측에서 플레이어의 추가가 실패했다.
    public override void OnLobbyClientAddPlayerFailed()
    {
        Debug.Log("OnLobbyClientAddPlayerFailed");
    }
}

