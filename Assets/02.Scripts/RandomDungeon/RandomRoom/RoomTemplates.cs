using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomTemplates : MonoBehaviour
{

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime = 2.0f;
    private bool spawnedBoss;
    public GameObject boss;

    public GameObject[] blockingWalls;

    public delegate void ArrangementFinish();
    public static ArrangementFinish CreateField; 

    // 방 갯수 조절 방법 
    // 1. RoomTemplates의 rooms의 Count가 조건에 만족할 때까지 다시 방 생성 
    // 2.RoomSpawner에서 rooms의 Count를 조건에 만족 할 때 까지만 방 생성, 이 후 방들은 CloseRoom으로 생성 
    // 3.             "                                                  ,  연결되지 않은 통로들 (Spawn == false)인 곳들의 room 넘버를 확인,
    // 뚫려있는 벽이면 막다른 벽으로 대체 


    // 보스룸 및 특정 룸 생성 방식
    void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {
            // 단순히 rooms[romms.Count-1].transform.position에 생성하면 안되나 ?
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    NGUITools.AddChild(rooms[i], boss);
                    spawnedBoss = true;

                    CreateField();
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
