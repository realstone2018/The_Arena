using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    public Gate[] gates;

    private void Start()
    {
        gates = new Gate[4];
        gates = transform.GetComponentsInChildren<Gate>();

        // 배열을 Sort함수와 람다함수를 사용하여 moveDirection 순으로 정리 
        System.Array.Sort<Gate>(gates, (x, y) => x.gateDirection.CompareTo(y.gateDirection));
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
