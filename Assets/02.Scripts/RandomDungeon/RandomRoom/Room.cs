using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Room : NetworkBehaviour
{
    private StageController stageController;        // 생성할 room의 개수 및 정보를 가지고있다.
    private RoomTemplates templates;
    public GameObject fieldPrefab;
    public Transform grid;
    public GameObject createdField;


    void Start()
    {
        RoomTemplates.CreateField += this.CreateField;

        stageController = FindObjectOfType<StageController>();
        templates = GameObject.FindGameObjectWithTag("RoomTemplate").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);

        grid = GameObject.Find("Grid").GetComponent<Transform>();
    }

    // Room은 호스트에만 존재함으로 별다른 처리 필요 x 
    [ServerCallback]
    private void CreateField()
    {
        Vector2 setPosition = transform.position * 500f;
        createdField = (GameObject)Instantiate(fieldPrefab, setPosition, Quaternion.identity);
        createdField.transform.SetParent(grid);

        NetworkServer.Spawn(createdField);


        NetworkInstanceId fieldNetID = createdField.GetComponent<NetworkIdentity>().netId;

        grid.GetComponent<Grid>().ServerToClient(fieldNetID);
    }

}
