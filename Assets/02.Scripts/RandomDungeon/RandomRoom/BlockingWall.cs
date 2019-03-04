using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingWall : MonoBehaviour {

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

    private void CreateBlockingWall()
    {
        Room parentRoom = transform.GetComponentInParent<Room>();
        Field parentField = parentRoom.createdField.GetComponent<Field>();

        createdField = Instantiate(fieldPrefab);
        createdField.transform.SetParent(parentField.walls[blockDirection - 1]);
        createdField.transform.localPosition = Vector2.zero;
    }
}
