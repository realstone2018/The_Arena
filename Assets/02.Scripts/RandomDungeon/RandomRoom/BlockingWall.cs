using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BlockingWall : NetworkBehaviour {

    public GameObject fieldPrefab;
    public GameObject createdField;
    public int blockDirection;

    void Start()
    {
        RoomTemplates.CreateField += this.CreateField;
    }

    private void CreateField()
    {
        // 기본 Field 생성시간 동안  잠시 대기, 기본 Field를 기준으로 BlockingWall 생성 
        Invoke("CreateBlockingWall", 0.2f);
    }

    [ServerCallback]
    private void CreateBlockingWall()
    {
        Room parentRoom = transform.GetComponentInParent<Room>();
        Field parentField = parentRoom.createdField.GetComponent<Field>();

        createdField = (GameObject)Instantiate(fieldPrefab);
        createdField.transform.SetParent(parentField.walls[blockDirection - 1]);
        createdField.transform.localPosition = Vector2.zero;

        NetworkServer.Spawn(createdField);

        GameObject gridObj = GameObject.Find("Grid");
        gridObj.GetComponent<Grid>().ServerToClient(createdField.GetComponent<NetworkIdentity>().netId);

    }
}
