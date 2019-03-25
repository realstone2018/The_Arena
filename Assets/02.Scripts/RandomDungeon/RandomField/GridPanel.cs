using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GridPanel : NetworkBehaviour {

    // 서버의 Room에서 field 생성 
    // 클라이언트의 Grid에서 Getchild()함수 사용

    [ServerCallback]
    public void SetParentInClient(NetworkInstanceId netID)
    {
        RpcGetChild(netID);
    }

    [ClientRpc]
	public void RpcGetChild(NetworkInstanceId netID)
    {
        GameObject child = ClientScene.FindLocalObject(netID);
        child.transform.SetParent(transform);
    }
}
