using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkScript : MonoBehaviour {

    public Canvas canvas;
    public NetworkDiscovery discovery;

    private void Awake()
    {
        Debug.Log("NetworkScript Start");
        discovery.Initialize();

        if (!discovery.StartAsServer())
        {
            discovery.StartAsServer();
            Debug.Log("NetworkDiscovery StartAsClient");
        }
        else
            Debug.Log("NetwrokDiscovery StartAsServer");
    }

    public void OnHostButton()
    {
        canvas.gameObject.SetActive(false);
        NetworkManager.singleton.StartHost();
    }

    public void OnClientButton()
    {
        canvas.gameObject.SetActive(false);
        NetworkManager.singleton.StartClient();
    }

    public void OnServerButton()
    {
        canvas.gameObject.SetActive(false);
        NetworkManager.singleton.StartServer();
    }
	
}
