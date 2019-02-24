using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    // Entry Room에 Close Room이 생기는 것을 방지 
	void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Destroyer Work");
        Destroy(other.gameObject);
	}
}
