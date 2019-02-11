using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Discovery는 singleton을 사용하지 않으므로 상속하여 사용한다. 
public class ArenaDiscoveryScript : NetworkDiscovery {

    public void OnInitButton()
    {
        if (running)
        {
            StopBroadcast();
        }

        Initialize();
    }

	public void OnStartServerButton()
    {
        StartAsServer();
    }

    public void OnStartClientButton()
    {
        StartAsClient();
    }
}
