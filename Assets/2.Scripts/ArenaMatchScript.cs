using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

public class ArenaMatchScript : MonoBehaviour {

    public Canvas canvas;

    int serverPort = 9000;
    string matchName = "default";

    
    private void Start()
    {
        // 1. MatchMaker Start
        NetworkManager.singleton.StartMatchMaker();
    }

    public void OnCreateMatchButtomClick()
    {
        // 2. MatchMaker Create
        NetworkManager.singleton.matchMaker.CreateMatch(matchName, 4, true, "", "", "", 0, 0, OnMatchCreate);
    }

    // CreateMatch()의 콜백 
    private  void OnMatchCreate(bool success, string extededInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            MatchInfo hostInfo = matchInfo;
            NetworkServer.Listen(hostInfo, serverPort);

            // 호스트로서 게임 실행
            NetworkManager.singleton.StartHost(hostInfo);
            canvas.enabled = false;
        }
        else
            Debug.LogError("OnMatchCreate ERROR.");
    }

    public void OnFindMatchButtonClick()
    {
        NetworkManager.singleton.matchMaker.ListMatches(0, 10, matchName, true, 0, 0, OnGetMatchList);
    }

    // ListMatches()의 콜백
    private void OnGetMatchList(bool success, string extendedIno, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            if (matches.Count != 0)
                NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinMatch);
            else
                Debug.Log("JoinMatch ERROR.");
        }
        else
            Debug.LogError("OnGetMatchList ERROR.");
    }

    // JoinMatdh()의 콜백 
    private void OnJoinMatch(bool success, string extededInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            MatchInfo hostInfo = matchInfo;

            // 클라이언트로서 게임을 실행 
            NetworkManager.singleton.StartClient(hostInfo);
            canvas.enabled = false;
        }
        else
            Debug.LogError("OnJoinMatch ERROR.");
    }
}


