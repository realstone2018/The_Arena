using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    public Gate[] gates;
    public Transform[] walls;   // walls를 
    public GameObject[] Landforms;

    public int maxLR = 9;
    public int maxTB = 11;
    public bool bossRoom = false;

    private void Start()
    {
        gates = new Gate[4];
        gates = transform.GetComponentsInChildren<Gate>();
        // 배열을 Sort함수와 람다함수를 사용하여 moveDirection 순으로 정리 
        System.Array.Sort<Gate>(gates, (x, y) => x.gateDirection.CompareTo(y.gateDirection));

        walls = new Transform[4];       
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i] = transform.GetChild(i);
        }

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
        int ran = Random.Range(-maxTB, maxTB + 1);
        walls[0].position += new Vector3(ran, 0, 0);

        ran = Random.Range(-maxTB, maxTB + 1);
        walls[1].position += new Vector3(ran, 0, 0);

        ran = Random.Range(0, maxLR);
        walls[2].position -= new Vector3(ran, 0, 0);

        ran = Random.Range(0, maxLR);
        walls[3].position += new Vector3(ran, 0, 0);

        ran = Random.Range(0, Landforms.Length);
        Landforms[ran].SetActive(true);
    }

    private void SetBossField()
    {
        walls[1].position += new Vector3(-8, 0, 0);
        walls[2].position -= new Vector3(8, 0, 0);

        //Landforms[ran].SetActive(true);
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
