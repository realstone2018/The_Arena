using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFieldController : MonoBehaviour {

    public GameObject player;
    private int enterDirection;

    public static MoveFieldController instance = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MoveField(int gateDirection)
    {
        enterDirection = gateDirection;
        PlayerIcon.instance.MoveIcon(enterDirection);

        // playerIcon이 이동한 후 이동한 room의 자식으로 갈 때까지 대기 
        Invoke("FindLinkedGate", 0.1f);
    }

    public void FindLinkedGate()
    {
        // PlayerIcon은 이동한 방의 자식이 되므로, 부모를 가져온다.
        Transform arrivedRoom = PlayerIcon.instance.transform.parent;

        // 이동한 방이 생성한 Field를 가져온다. 
        GameObject nextField = arrivedRoom.GetComponent<RoomController>().createdField;
        // FieldController의 movefield배열에서 transofrm.Position을 반환받아 저장
        Vector3 outGatePos = nextField.GetComponent<FieldController>().GetGatePos(enterDirection);
    
        // B -> T ,    T -> B ,   L -> R,    R 로 가면 -> L 에서 등장
        switch (enterDirection)
        {
            case 1:
                player.transform.position = outGatePos + new Vector3(0, -3, 0);
                break;
            case 2:
                player.transform.position = outGatePos + new Vector3(4, 4.5f, 0);
                break;
            case 3:
                player.transform.position = outGatePos + new Vector3(-5, 0, 0);
                break;
            case 4:
                player.transform.position = outGatePos + new Vector3(5, 0, 0);
                break;
        }
    }
}
