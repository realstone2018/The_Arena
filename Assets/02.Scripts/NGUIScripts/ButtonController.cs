using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    bool isClose = true;
    public GameObject inventoryPanel;

    public void OpenCloseBag()
    {
        if (isClose)
        {
            inventoryPanel.SetActive(true);
            isClose = false;
        }
        else
        {
            inventoryPanel.SetActive(false);
            isClose = true;
        }
    }
}
