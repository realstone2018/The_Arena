using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//    인벤창 밖으로 드래그하면 아이템 떨굼
//    장비 슬롯엔 DropSurface를 이용 


// EventListener를 이용하여 더블 클릭시 아이템의 종류에 따라 해당 아이템의 이벤트 호출 
// 인벤창에 있는 데이터 Json으로 저장? 

public class Pickup : MonoBehaviour {

    private Inventory inventory;
    public GameObject itemBtnPrefab;
    public Animator animator;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slotIsFull.Length; i++)
            {
                if (inventory.slotIsFull[i] == 0)
                {
                    animator.SetTrigger("Pickup");
                    NGUITools.AddChild(inventory.slot[i], itemBtnPrefab as GameObject);
                    inventory.slotIsFull[i] = 1;
                    break;
                }
            }

            Destroy(gameObject);
        }
    }
}
