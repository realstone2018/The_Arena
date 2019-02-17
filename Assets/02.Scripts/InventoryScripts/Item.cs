using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum ItemSort { None, Helmet, Bracer, Armor, Accessory, Consum, Other }
    public ItemSort sort;

    private Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    // 오브젝트가 클릭되었을 때, 클릭해제 되었을 때 호출 
    // 눌렀을 때 인자로 true 전달, collider = false가 된다.
    void OnPress(bool pressed)
    {
        col.enabled = !pressed;

        if(!pressed)
        {
            // 현재 마우스 커서가 올라가있는 오브젝트
            GameObject col = UICamera.hoveredObject;

            // 충돌된게 없거나 충돌된곳이 Equipment slot이 아니라면 
            if (col == null || col.GetComponent<EquipmentSlot>() == null)
            {
                gameObject.transform.localPosition = Vector2.zero;
            }
        }
    }
}
