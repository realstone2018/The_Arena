using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private StageController stageController;
    private RoomTemplates templates;
    public GameObject fieldPrefab;
    public Transform grid;
    public GameObject createdField;


    void Start()
    {
        RoomTemplates.CreateField += this.CreateField;

        stageController = FindObjectOfType<StageController>();
        templates = GameObject.FindGameObjectWithTag("RoomTemplate").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);

        grid = GameObject.Find("Grid").GetComponent<Transform>();
    }

    private void CreateField()
    {
        Vector2 setPosition = transform.position * 500f;
        createdField = Instantiate(fieldPrefab, setPosition, Quaternion.identity);
        createdField.transform.SetParent(grid);
    }
}
