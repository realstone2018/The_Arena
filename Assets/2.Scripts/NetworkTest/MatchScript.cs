using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class MatchScript : MonoBehaviour {

    public Canvas canvas;
    int serverPort = 9000;
    string matchName = "default";

    // Match Maker를 스타트한다.
    private void Start()
    {
        CustomNetworkManager.singleton.StartMatchMaker();
    }


    // 매치 생성 버튼의 OnClick 콜백
    public void OnCreatematchButtonClick()
    {
        CustomNetworkManager.singleton.matchMaker.CreateMatch(matchName, 4, true, "", "", "", 0, 0, OnMatchCreate);
    }

    // CreateMatch의 콜백
    private void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            MatchInfo hostInfo = matchInfo;
            NetworkServer.Listen(hostInfo, serverPort);
            // 호스트로서 게임을 실행
            CustomNetworkManager.singleton.StartHost(hostInfo);
            canvas.enabled = false;
        }
        else
        {
            Debug.LogError("OnMatchCreate ERROR.");
        }
    }

    // Find Match 벝ㄴ의 OnClick 콜백
    public void OnFindMatchButtonClick()
    {
        CustomNetworkManager.singleton.matchMaker.ListMatches(0, 10, matchName, true, 0, 0, OnGetMatchList);
    }

    // ListMatches의 콜백 
    private void OnGetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            if (matches.Count != 0)
                CustomNetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinMatch);
            else
                Debug.Log("JoinMatch ERROR.");
        }
        else
        {
            Debug.LogError("OnGetMatchList ERROR");
        }
    }

    // JoinMatch의 콜백
    private void OnJoinMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            MatchInfo hostInfo = matchInfo;
            // 클라이언트로서 게임을 실행
            NetworkManager.singleton.StartClient(hostInfo);
            canvas.enabled = false;
        }
        else
        {
            Debug.LogError("OnJoinMatch ERROR.");
        }
    }
}
