using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public int gateDirection;
    // B : 1,  T : 2,  L : 3,  R : 4    나중에 enum으로 openDirection과 같이 정리하자.
    // B → y -= 0.2         T → y += 0.2       L → x -= 0.2        R → x += 0.2

    // x축 1 → 0      2 → 0    3 → -0.2    4 → 0.2
    // y축 1 → -0.2   2 → 0.2  3 → 0       4 → 0
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MoveFieldController.instance.MoveField(gateDirection);
        }
    }
}
