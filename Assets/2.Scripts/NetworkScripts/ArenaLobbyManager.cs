using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArenaLobbyManager : NetworkLobbyManager {

    public Transform[] spawnPositions;

    public Transform GetSpawnPosition()
    {
        if (spawnPositions[0].childCount == 0)
        {
            return spawnPositions[0];
        }
        else
        {
            return spawnPositions[1];
        }
    }
    /*
    // OnLobbyServerCreateGamePlayer 에서  호스트의 로비 플레이어 생성 
    // 클라이언트의 로비 플레이어의 경우 PainterLobbyPlaer에서 다루는것으로 추측 
    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("OnLobbyServerCreateGamePlayer");

        // NetworkStartPosition 컴포넌트로 설정된 곳이 없으므로 0,0,0에 생성, 실제 위치를 설정하는건 PainterLobbyPlayer에서 
        var spawnPos = GetStartPosition();

        // 로비 플레이어 오브젝트 생성
        var obj = Instantiate(gamePlayerPrefab, spawnPos.position, spawnPos.rotation);

        return obj.gameObject;
    }
    */
}
