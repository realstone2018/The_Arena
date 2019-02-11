using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TLScript : MonoBehaviour {

    public Text text;
    public InputField field;
    public int maxConnection = 10;
    private int hostId = 0;
    public string serverAddress = "127.0.0.1";
    public int serverPort = 5000;
    private int connectionId;
    private int channelId = 1;
    public int MaxBufferSize = 1000;

    private void Start()
    {
        // 초기화
        NetworkTransport.Init();
        // 네트워크 설정 작성
        ConnectionConfig config = new ConnectionConfig();
        config.AddChannel(QosType.Reliable);
        config.AddChannel(QosType.Unreliable);
        // 토폴로지 작성
        HostTopology topology = new HostTopology(config, maxConnection);
        Debug.Log("NetworkTransport init.");

        // 호스트작성
        for (int i = serverPort; i < serverPort + 10; i++)
        {
            hostId = NetworkTransport.AddHost(topology, i);
            if (hostId != -1)
            {
                Debug.Log("AddHost :" + hostId + "port:" + i);
                break;
            }
        }

        // 연결 시작
        byte error;
        connectionId = NetworkTransport.Connect(hostId, serverAddress, serverPort, 0, out error);
        Debug.Log("Connected :" + connectionId);
    }


    // 송신 버튼의 콜백 
    public void OnClickToServerButton()
    {
        byte[] data = System.Text.Encoding.ASCII.GetBytes(field.text);
        SendData(data, QosType.Reliable);
    }


    // 데이터 송신
    public void SendData(byte[] data, QosType qos = QosType.Reliable)
    {
        byte error;
        NetworkTransport.Send(hostId, connectionId, channelId, data, data.Length, out error);

        Debug.Log("SendData: " + hostId + ":" + connectionId + ":" + channelId + " -> " + field.text);
    }


    // 데이터를 수신한다.
    private void Update()
    {
        // NetworkTransport.Receive()메소드 내에서 값을 저장할 변수 선언 
        int outHostId;
        int outConnectionId;
        int outChannelId;
        byte[] recBuffer = new byte[MaxBufferSize];
        int bufferSize = MaxBufferSize;
        int dataSize;
        byte error;

        // 데이터 수신 
        NetworkEventType reciveEvent = NetworkTransport.Receive(out outHostId, out outConnectionId, out outChannelId,
                                                                        recBuffer, bufferSize, out dataSize, out error);
        // 이벤트별 대응
        switch (reciveEvent)
        {
            case NetworkEventType.ConnectEvent:
                Debug.Log("Connect: " + outHostId + ":" + outConnectionId + ":" + outChannelId);
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log("Disconnect: " + outHostId + ":" + outConnectionId + ":" + outChannelId);
                break;
            case NetworkEventType.DataEvent:
                string message = System.Text.Encoding.ASCII.GetString(recBuffer);
                Debug.Log("DataEvent: " + outHostId + ":" + outConnectionId + ":" + outChannelId + " -> " + message);
                text.text = message;
                break;
            case NetworkEventType.Nothing:
                Debug.Log("Nothing... " + outHostId + ":" + outConnectionId + ":" + outChannelId);
                break;
        }
    }
}