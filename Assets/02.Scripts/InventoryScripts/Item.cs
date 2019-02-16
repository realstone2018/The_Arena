using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum ItemSort { None, Helmet, Bracer, Armor, Accessory, Consum, Other }
    public ItemSort sort;

    // 오브젝트가 클릭되었을 때, 클릭 해제 되었을 때 호출 
    // 눌렀을 때 인자로 true 전달, collider = false가 된다.
    void OnPress(bool pressed)
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = !pressed;

        if(!pressed)
        {
            // 레이케스트가 충돌한 마지막 충돌체
            GameObject col = UICamera.hoveredObject;
            Debug.Log(col);
            // 충돌된게 없거나 충돌된곳이 Equipment slot이 아니라면 
            // Table을 재정렬해서 아이템을 원위치 
            if (col == null || col.GetComponent<EquipmentSlot>() == null)
            {
                //UITable invenTable = NGUITools.FindInParents<UITable>(gameObject);  // 부모의 부모도 찾나??????????????
                //invenTable.repositionNow = true;

                gameObject.transform.localPosition = Vector2.zero;
            }
        }
    }
}
