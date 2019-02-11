using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArenaLobbyPlayer : NetworkLobbyPlayer
{

    public static ArenaLobbyPlayer localPlayer { get; private set; }

    private static ArenaLobbyManager lobbyManager { get { return NetworkLobbyManager.singleton as ArenaLobbyManager; } }

    private void Awake()
    {
        localPlayer = this;
    }
    
    // LobbyManager에선 생성만하고  여기서 위치 설정
    public override void OnStartClient()
    {
        Transform pos = lobbyManager.GetSpawnPosition();
       
        transform.SetParent(pos);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(0.3f, 0.3f, 1f);
    }
    
    /*
    [Command]
    public void CmdChangePlayerStatus(bool isReady, uint playerId)
    {
        // 로컬 플레이어의 ready상태가 바뀔 경우 서버의 Button, text를 변경

        // 서버의 변경된 상태를 리모트 플레이어의 화면에서도 변경해준다. 
        //RpgChangePlayerStatus(isReady, playerId);
    }
    
    [ClientRpc]
    public void RpgChangePlayerStatus(bool isReady, uint playerId)
    {  
        
    }
    */
}
