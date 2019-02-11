using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class CubeScript : NetworkBehaviour
{
    const short Destroymsg = 12345;

    [SyncVar]
    public int Number;
    [SyncVar]
    public string Name;

    private void Start()
    {
        // DestroyMsg 메세지의 등록
        NetworkServer.RegisterHandler(Destroymsg, OnDestroyMsg);
    }

    // DestroyMsg용 핸들러
    void OnDestroyMsg (NetworkMessage msg)
    {
        Debug.Log("OnDestroyMsg : " + msg.ReadMessage<StringMessage>().value);
    }

    private void Update()
    {
        if (transform.position.y < -100)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Terrain")
        {
            // ????
            int number = new System.Random().Next(1000);

            // 모든 NetworkClient에게 메세지를 송신 
            foreach(NetworkClient client in NetworkClient.allClients)
            {
                StringMessage msg = new StringMessage("Delet " + name + " : random number " + number + " : connection id " + client.connection.connectionId);
                client.Send(Destroymsg, msg);
            }

            GameObject.Destroy(gameObject);
        }
    }
}

// 오리지널 메시지 클래스 
public class CubeMessage : MessageBase
{
    public string Name;
    public int Number;
    public Vector3 LostPosition;

    // 기본 컨스트럭터 . 필수 
    public CubeMessage() { }

    // 필요한 정보를 인수로 전달하는 컨스트럭터
    public CubeMessage(string name, int number, Vector3 pos)
    {
        this.Name = name;
        this.Number = number;
        this.LostPosition = pos;
    }

    // 저장 데이터를 String으로 출력 
    public string getMessage()
    {
        return "Destroy " + Name + "(" + Number + ", [x:" + LostPosition.x + ", y:" + LostPosition.y + ", z: " + LostPosition.z + "])";
    }
}
