using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public GameObject resetButton;
    public GameObject invenTable;
    public GameObject backpackTable;

    // 가방에 들어 있는 아이템을 저장 할 리스트 
    private List<Transform> itemsInBackpack = new List<Transform>();
    // 가방과 인벤토리에 있는 모든 아이템을 저장할 리스트 
    private List<GameObject> allItems = new List<GameObject>();

    private void Start()
    {
        // 이벤트 리스너를 이용해 리셋 버튼의 onClick이벤트에 이벤트 함수를 추가 
        // Notify 이외의 다른 이벤트 추가 방법 
        UIEventListener.Get(resetButton).onClick += Button;

        // 가방과 인벤토리 있는 모든 아이템을 allItem리스트에 담는다.
        foreach(Transform itemTransform in backpackTable.transform)
        {
            allItems.Add(itemTransform.gameObject);
        }
        foreach(Transform itemTransform in invenTable.transform)
        {
            allItems.Add(itemTransform.gameObject);
        }

        // allItems에 있는 모든 아이템을 onDoubleClick 이벤트에 이벤트 함수 추가 
        // DoubleClick시 추가된 이벤트 함수 호출 
        foreach (GameObject item in allItems)
        {
            UIEventListener.Get(item).onDoubleClick += Button;
        }
        
    }

    // 전달 받은 버튼에 따라 다른 이벤트를 진행, 이벤트 관리하기 편함 
    void Button(GameObject go)
    {
        if (go == resetButton)
        {
            // 가방에 있는 모든 아이템을 itemInBackpack 리스트에 담는다.
            foreach(Transform itemTransform in backpackTable.transform)
            {
                itemsInBackpack.Add(itemTransform);
            }

            // 리스트에 있는 모든 아이템의 부모를 인벤토리의 테이블로 바꾼다.
            foreach(Transform item in itemsInBackpack)
            {
                item.parent = invenTable.transform;

                // MarkParentAsChanged 함수를 호출해서 부모가 바뀌었음을 자식 위젯에 알린다.  
                // 호출하지 않을 시 Hierarchy상에서는 부모가 바뀌엇지만 위젯이 다시 그려지지 않아 그대로인 문제 발생
                NGUITools.MarkParentAsChanged(item.gameObject);
            }

            // 인벤토리 테이블에 있는 아이템을 정렬 
            invenTable.GetComponent<UITable>().repositionNow = true;
            // 아이템을 모두 비웠으면 리스트를 지운다. 
            itemsInBackpack.Clear();
        }

        // Contains를 사용하여 이벤트를 발생한 게임오브젝트가 allItems 리스트에 있는지 확인 
        if (allItems.Contains(go))
        {
            if (go.transform.parent == invenTable.transform)
            {
                go.transform.parent = backpackTable.transform; 
            }
            else if (go.transform.parent == backpackTable.transform)
            {
                go.transform.parent = invenTable.transform;
            }
        }

        // 하위 위젯에게 부모가 변경되었음을 알리고 두 테이블을 모두 재 정렬
        NGUITools.MarkParentAsChanged(go);
        backpackTable.GetComponent<UITable>().repositionNow = true;
        invenTable.GetComponent<UITable>().repositionNow = true;
    }
    
    // 타샤렌 Tasharen에서  NGUI공식튜토리얼, NGUI포럼 제공 
}
