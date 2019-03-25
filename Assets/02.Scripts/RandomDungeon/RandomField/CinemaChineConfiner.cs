using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaChineConfiner : MonoBehaviour {

    public PolygonCollider2D polygon;
    int numOfPoints;
    private Vector3[] gatePos;
    public Vector2[] pointPos;

    private void Awake()
    {
        polygon = GetComponent<PolygonCollider2D>();
        numOfPoints = polygon.GetTotalPointCount();
        gatePos = new Vector3[4];
        pointPos = new Vector2[4];

    }

    private void Start()
    {
        Invoke("SetInit", 3.0f);
    }

    private void SetInit()
    {
        GameObject startField = GameObject.Find("FieldTBRL(Clone)");
        SetCollider(startField);
    }

    public void SetCollider(GameObject field)
    {
        //transform.SetParent(field.transform);
        //transform.localPosition = Vector3.zero;

        field.GetComponent<Field>().GetGateLocalPos(gatePos);

        pointPos[0] = new Vector3(gatePos[2].x, gatePos[1].y);
        pointPos[1] = new Vector3(gatePos[3].x, gatePos[1].y);
        pointPos[2] = new Vector3(gatePos[3].x, gatePos[0].y);
        pointPos[3] = new Vector3(gatePos[2].x, gatePos[0].y);

        polygon.SetPath(0, pointPos);
    }
}
