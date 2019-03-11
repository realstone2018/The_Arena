using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FieldSetValue
{
    public float[] wallPos = new float[8];
    public int activeLandform = 0;

    public FieldSetValue() { }

    public FieldSetValue(Transform[] walls, int landform)
    {
        for(int i = 0; i < walls.Length; i++)
        {
            wallPos[i * 2] = walls[i].position.x;
            wallPos[i * 2 + 1] = walls[i].position.y; 
        }

        activeLandform = landform;
    }
}

public class Field : NetworkBehaviour {

    public Transform[] walls;
    public Gate[] gates;
    public GameObject[] landforms;

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

        landforms = new GameObject[transform.childCount - 4];
        for (int i = 4; i < transform.childCount; i++)
        {
            landforms[i - 4] = transform.GetChild(i).gameObject;
        }

        if (isServer)
        {
            if (bossRoom == true)
                Invoke("SetBossField", 0.1f);
            else
                Invoke("SetField", 0.1f);
        }
    }


    [ServerCallback]
    private void SetBossField()
    {
        RandomBossController.instance.RandomBoss(this);
    }


    [ServerCallback]
    private void SetField()
    {
        // Botoom은 수직 이동 x 
        walls[0].position += new Vector3(Random.Range(-maxTBx, maxTBx + 1), 0, 0); 
        walls[1].position += new Vector3(Random.Range(-maxTBx, maxTBx + 1), Random.Range(0, maxTy), 0);
        
        // Left, Right wall의 y축 랜덤 최대값은 Top wall의 Y값 + 11 이다.  
        maxLRy += (int)walls[1].localPosition.y + 1;
        walls[2].position += new Vector3(Random.Range(-maxLRx, 0), Random.Range(0, maxLRy), 0);
        walls[3].position += new Vector3(Random.Range(0, maxLRx + 1), Random.Range(0, maxLRy), 0);

        int ranLandform = Random.Range(0, landforms.Length);
        landforms[ranLandform].SetActive(true);

        FieldSetValue fieldSetValue = new FieldSetValue(walls, ranLandform);
        //NetworkInstanceId gridNetID = GameObject.Find("Grid").GetComponent<NetworkIdentity>().netId;

        RpcSetField(fieldSetValue);
    }


    [ClientRpc]
    private void RpcSetField(FieldSetValue fieldSetValue)
    {
        // Client에서 Start()함수실행 후 Rpc가 실행되게 해야 함으로 Start()문을 대체해야하지만 임시로 코루틴 
        StartCoroutine(SetField(fieldSetValue));
      
        /*
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].position = new Vector3(fieldSetValue.wallPos[i * 2], fieldSetValue.wallPos[i * 2 + 1], 0);
        }

        Debug.Log("Landforms.Length : " + landforms.Length + "  fieldSetValue.ActiveLandform : " + fieldSetValue.activeLandform);
        StartCoroutine(SetLandform(fieldSetValue.activeLandform));
        */
    }

    private IEnumerator SetField(FieldSetValue fieldSetValue)
    {
        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].position = new Vector3(fieldSetValue.wallPos[i * 2], fieldSetValue.wallPos[i * 2 + 1], 0);
        }

        Debug.Log("Landforms.Length : " + landforms.Length + "  fieldSetValue.ActiveLandform : " + fieldSetValue.activeLandform);
        landforms[fieldSetValue.activeLandform].SetActive(true);
    }


    /*
    private IEnumerator SetLandform(int activeLandform)
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            if (landforms.Length != 0)
            {
                landforms[activeLandform].SetActive(true);
                break;
            }
        }
    }
    */


    public Vector3 GetGatePos(int inDirection)
    {
        // B로 들어가면 -> 다음 Room의 T에서 나온다.   
        // inDirection이 1일 때, 1(T)번째 인덱스에 접근
        //               2       0(B)
        //               3       3(R)
        //               4       2(L)
        // 인자가 홀수면 인자값 인덱스에 접근, 짝수면 2감소한 인덱스에 접근 

        int outDirection = (inDirection % 2 == 0 ? inDirection - 2 : inDirection);
        Debug.Log(outDirection);
        return gates[outDirection].transform.position;
    }
}
