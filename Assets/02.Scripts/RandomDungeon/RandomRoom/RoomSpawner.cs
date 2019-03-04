using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door


    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;
    public float waitTime = 4f;

    public int spawnerId;
    private bool blocked = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("RoomTemplate").GetComponent<RoomTemplates>();

        // 방을 생성하기전 Block 여부 판단, Destoryer와 충돌여부 판단을 위해 일정시간 후 생성
        Invoke("Spawn", 0.15f);
    }

    void Spawn()
    {
        GameObject spawnObj;
        GameObject selectedObj = SelectRoomOrWall();

        // room을 생성하는 경우 생성한 roomd의 Spawner들 Id 지정
        if (blocked == false)
        {
            spawnObj = NGUITools.AddChild(templates.transform, selectedObj);

            foreach (RoomSpawner childRoomSpawner in spawnObj.GetComponentsInChildren<RoomSpawner>())
            {
                childRoomSpawner.spawnerId = this.spawnerId + 1;
            }
        }
        else
        {
            spawnObj = NGUITools.AddChild(transform.GetComponentInParent<Room>().transform, selectedObj);
        }

        spawnObj.transform.position = transform.position;
        spawnObj.transform.rotation = Quaternion.identity;
        spawned = true;

        Destroy(gameObject, waitTime);
    }
 

    // OnTriggerEnter()의 결과에 blocked값 결정
    private GameObject SelectRoomOrWall()
    {
        GameObject selectedObj = null;

        if (blocked == true)
        {
            selectedObj = templates.blockingWalls[openingDirection - 1];
        }
        else
        {
            switch (openingDirection)
            {
                case 1:
                    selectedObj = templates.bottomRooms[Random.Range(0, templates.bottomRooms.Length)];
                    break;
                case 2:
                    selectedObj = templates.topRooms[Random.Range(0, templates.topRooms.Length)];
                    break;
                case 3:
                    selectedObj = templates.leftRooms[Random.Range(0, templates.leftRooms.Length)];
                    break;
                case 4:
                    selectedObj = templates.rightRooms[Random.Range(0, templates.rightRooms.Length)];
                    break;
            }
        }

        return selectedObj;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            BetweenSpawnPoint(other);
        }
    }

    private void BetweenSpawnPoint(Collider2D otherSpawner)
    {
        // 아직 방을 생성하지 않은 Spawner 간 충돌
        if (otherSpawner.GetComponent<RoomSpawner>().spawned == false && spawned == false)
        {
            blocked = true;
        }
        else
        {
            // 충돌한게 parent spawner이 아닌 경우, Spawn()함수에서 room 대신 BlockWall 생성
            bool otherIsParent = otherSpawner.GetComponent<RoomSpawner>().spawnerId == (this.spawnerId - 2);

            if (otherIsParent)
            {
                Destroy(gameObject);
            }
            else
            {
                blocked = true;
            }
        }
    }
}
