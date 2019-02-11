using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DiscoveryScript : NetworkDiscovery {

    private void Start()
    {
        if (isServer)
        {
            CustomNetworkManager.singleton.StartHost();
            Debug.Log("StartHost");
        }
        else
        {
            CustomNetworkManager.singleton.StartClient();
            Debug.Log("StartCllient");
        }
    }

    //초기화
    public void OnInitButton()
    {
        if (running)
        {
            StopBroadcast();
        }
        Initialize();
        Debug.Log("Initialize.");
    }

    //서버로서 실행
    public void OnServerButton()
    {
        StartAsServer();
        Debug.Log("Start As Server.");
    }
    
    //클라이언트로서 액세스
    public void OnClientButton()
    {
        StartAsClient();
        Debug.Log("Start As Client.");
    }

    public override void OnReceivedBroadcast(string address, string msg)
    {
        Debug.Log("OnReceivedBroadcast address=[" + address + "] message=[" + msg + "]");
    }
}
