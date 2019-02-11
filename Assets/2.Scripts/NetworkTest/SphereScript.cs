using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SphereScript : NetworkBehaviour
{
    Text text;
    Text text2;

    public GameObject cube;

    private int CubeCount = 0;

    [SyncVar(hook = "OnCountChange")]
    int count = 0;

    private static System.DateTime startTime = System.DateTime.Now;

    string[] names = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    [ClientCallback]
    void OnCountChange(int newVal)
    {
        //Debug.Log(this.netId + " Value: " + newVal);
        text.text = "Clinet: " + count;
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            text = GameObject.Find("MsgText").GetComponent<Text>();
            text2 = GameObject.Find("MsgText2").GetComponent<Text>();
        }
    }

    [ClientCallback]
    private void Update()
    {
        if (isServer)
            Count();
        if (isLocalPlayer)
        {
            //hook 사용으로 따로 Update해줄 필요 x 
            //UpdateCount();
            Move();
        }

        if (Input.GetKeyDown(KeyCode.Space)) { CmdSpawnIt(); }
    }

    [ServerCallback]
    void Count()
    {
        if (!isServer) { return; }
        int count2 = (int)((System.DateTime.Now - startTime).TotalSeconds);
        RpcSetCount(count);
        ++count;
    }

    [ClientRpc]
    void RpcSetCount(int n)
    {
        if (text != null)
            text2.text = "Client2: " + n;
    }

    [ClientCallback]
    void UpdateCount()
    {
        text.text = "Clinent: " + count;
    }

    [ClientCallback]
    private void Move()
    {
        if (!isLocalPlayer) { return; }

        // 메인카메라 위치 조정
        Vector3 v = transform.position;
        v.z -= 5;
        v.y += 3;
        Camera.main.transform.position = v;
    }

    [ClientCallback]
    private void FixedUpdate()
    {
        if (!isLocalPlayer) { return; }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        CmdMoveSphere(x, z);
    }

    [Command]
    public void CmdMoveSphere(float x, float z)
    {
        Vector3 v = new Vector3(x, 0, z) * 10f;
        GetComponent<Rigidbody>().AddForce(v);
    }

    [Command]
    void CmdSpawnIt()
    {
        Debug.Log("spawned.");

        GameObject obj = Instantiate<GameObject> (cube, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

        // < >.() 말고, 요렇게 Getcomponent하는 방법도 있네 
        CubeScript cubescript = (CubeScript)obj.GetComponent("CubeScript");
        CubeCount++;
        cubescript.Name = names[CubeCount % 10];
        cubescript.Number = CubeCount;

        NetworkServer.Spawn(obj);

        Rigidbody r = obj.GetComponent<Rigidbody>();
        Vector3 v = Camera.main.transform.forward;
        v.y += 1f;
        r.AddForce(v * 500);
        //  AddTorque ??
        r.AddTorque(new Vector3(10f, 0f, 10) * 100);
    }
    

    public override void OnStartLocalPlayer()
    {
        Debug.Log("SphereScript::OnStartLocalPlayer");

        base.OnStartLocalPlayer();
        Renderer r = GetComponent<Renderer>();
        Color c = Color.red;
        r.material.color = c;
    }
}
