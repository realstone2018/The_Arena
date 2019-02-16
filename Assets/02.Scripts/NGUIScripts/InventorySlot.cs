using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour {

    public void OnDrop(GameObject dropped)
    {
        Item dragItem = dropped.GetComponent<Item>();

        if (dragItem == null)
            return;

        // 슬롯에 이미 아이템이 들어있다면 합치거나 스왑, 비어있는 슬롯이라면 단순히 아이템 이동
        if (gameObject.transform.childCount != 0)
        {
            Transform loadedItem = gameObject.transform.GetChild(0);

            // dropped 아이템이 소비 or 기타 아이템이고 && used 아이템의 이름과 같다면 중복
            // 그렇지 않다면 Swap
            if (dragItem.sort == Item.ItemSort.Consum || dragItem.sort == Item.ItemSort.Other)
            {
                if (dragItem.name.Contains(loadedItem.name))
                    CombineQuantity();
            }
            else
            {
                loadedItem.transform.SetParent(dropped.transform);

                NGUITools.MarkParentAsChanged(loadedItem.gameObject);
                loadedItem.transform.localPosition = Vector2.zero;
            }
        }

        dropped.transform.SetParent(gameObject.transform);
        dropped.transform.localPosition = Vector2.zero;

        // 아이템의 부모를 바꾸고, 하위 위젯들에게 부모가 바꼈음을 알려 위젯을 새부모 하위에 다시 그린다. 
        NGUITools.MarkParentAsChanged(dropped);      
    }

    private void CombineQuantity()
    {

    }
}
