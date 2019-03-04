using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    public Transform[] walls;
    public Gate[] gates;
    public GameObject[] Landforms;

    public int maxLRx = 9;
    public int maxLRy = 12;
    public int maxTBx = 11;
    public int maxTy = 15;
    public bool bossRoom = false;

    private void Start()
    {
        walls = new Transform[4];       
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i] = transform.GetChild(i);
        }

        gates = new Gate[4];
        gates = transform.GetComponentsInChildren<Gate>();
        // 배열을 Sort함수와 람다함수를 사용하여 moveDirection 순으로 정리 
        System.Array.Sort<Gate>(gates, (x, y) => x.gateDirection.CompareTo(y.gateDirection));

        Landforms = new GameObject[transform.childCount - 4];
        for (int i = 4; i < transform.childCount; i++)
        {
            Landforms[i-4] = transform.GetChild(i).gameObject;
        }

        if (bossRoom == true)
            Invoke("SetBossField", 0.1f);
        else 
            Invoke("SetField", 0.1f);
    }

    private void SetField()
    {
        // Botoom은 수직 이동 x 
        walls[0].position += new Vector3(Random.Range(-maxTBx, maxTBx + 1), 0, 0); 
        walls[1].position += new Vector3(Random.Range(-maxTBx, maxTBx + 1), Random.Range(0, maxTy), 0);
        
        // Left, Right wall의 y축 랜덤 최대값은 Top wall의 Y값 + 11 이다.  
        maxLRy += (int)walls[1].localPosition.y + 1;
        walls[2].position += new Vector3(Random.Range(-maxLRx, 0), Random.Range(0, maxLRy), 0);
        walls[3].position += new Vector3(Random.Range(0, maxLRx + 1), Random.Range(0, maxLRy), 0);

        Landforms[Random.Range(0, Landforms.Length)].SetActive(true);
    }

    private void SetBossField()
    {
        RandomBossController.instance.RandomBoss(this);
    }

    public Vector3 GetGatePos(int inDirection)
    {
        // B로 들어가면 -> 다음 Room의 T에서 나온다.   
        // inDirection이 1일 때, 1번째 인덱스에 접근
        //               2       0
        //               3       3
        //               4       2
        // 인자가 홀수면 인자값 인덱스에 접근, 짝수면 2감소한 인덱스에 접근 

        int outDirection = (inDirection % 2 == 0 ? inDirection - 2 : inDirection);
        return gates[outDirection].transform.position;
    }
}
