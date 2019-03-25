using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MoveFieldController : NetworkBehaviour {

    public GameObject hostPlayer;
    public GameObject remotePlayer;
    private int enterDirection;
    private CinemaChineConfiner cinemaChineConfiner;

    public static MoveFieldController instance = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hostPlayer = NetworkManager.singleton.client.connection.playerControllers[0].gameObject;
        cinemaChineConfiner = FindObjectOfType<CinemaChineConfiner>();
    }

    public void MoveField(int gateDirection)
    {
        if (isServer)
        {
            enterDirection = gateDirection;
            PlayerIcon.instance.MoveIcon(enterDirection);

            // playerIcon이 이동한 후 이동한 room의 자식으로 갈 때까지 대기 
            Invoke("FindLinkedGate", 0.1f);
        }
    }

    [ServerCallback]
    private void FindLinkedGate()
    {
        // PlayerIcon은 이동한 방의 자식이 되므로, 부모를 가져온다.
        Transform arrivedRoom = PlayerIcon.instance.transform.parent;

        // 이동한 방이 생성한 Field를 가져온다. 
        GameObject nextField = arrivedRoom.GetComponent<Room>().createdField;
        // FieldController의 movefield배열에서 transofrm.Position을 반환받아 저장
        Vector3 outGatePos = nextField.GetComponent<Field>().GetGatePos(enterDirection);

        // cinemaChine Camera Confiner Collider 설정 
        cinemaChineConfiner.SetCollider(nextField);

        // B -> T ,    T -> B ,   L -> R,    R 로 가면 -> L 에서 등장
        switch (enterDirection)
        {
            case 1:
                hostPlayer.transform.position = outGatePos + new Vector3(0, -3, 0);
                break;
            case 2:
                hostPlayer.transform.position = outGatePos + new Vector3(4, 4.5f, 0);
                break;
            case 3:
                hostPlayer.transform.position = outGatePos + new Vector3(-5, 0, 0);
                break;
            case 4:
                hostPlayer.transform.position = outGatePos + new Vector3(5, 0, 0);
                break;
        }

        if (NetworkServer.connections.Count == 2)
        {
            // NetworkServer를 통해 연결된 클라이언트의 정보를 가져온다. 
            remotePlayer = NetworkServer.connections[1].playerControllers[0].gameObject;

            Vector3 nextSeat = hostPlayer.transform.position + new Vector3(1, 0, 0);
            remotePlayer.transform.position = nextSeat;
        }
    }
}
