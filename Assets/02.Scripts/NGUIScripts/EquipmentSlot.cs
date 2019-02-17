using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour {
    /*
       Case 01.
       Item이 Equipment slot에 drop되면 Item의 종류 확인, 
       다르다면 원래 자리로 
       Equipment slot과 종류가 일치한다면 장착, 이미 자식이 하나 있다면 교체, 원래 있던 아이템은 비어 있는 슬롯의 자식으로 
       
        Case 02.
        Equipment slot을 클릭시 자식이 있다면 비어있는 슬롯으로
    */
    public Item.ItemSort slotSort;

    // DropSurface에 오브젝트가 드랍되면 호출 된다.
    public void OnDrop(GameObject dropped)
    {
        Item dragItem = dropped.GetComponent<Item>();

        if (dragItem == null)
            return;

        if (dragItem.sort == slotSort)
        {
            // 슬롯이 이미 사용 중이라면 inventory slot으로 이동 
            if (gameObject.transform.childCount != 0)
            {
                Transform usedItem = gameObject.transform.GetChild(0);
                usedItem.transform.SetParent(dropped.transform);

                NGUITools.MarkParentAsChanged(usedItem.gameObject);
                usedItem.transform.localPosition = Vector2.zero;

            }
            dropped.transform.SetParent(gameObject.transform);
            dropped.transform.localPosition = Vector2.zero;

            NGUITools.MarkParentAsChanged(dropped);
        }
        else
            dropped.transform.localPosition = Vector2.zero;
    }
}
