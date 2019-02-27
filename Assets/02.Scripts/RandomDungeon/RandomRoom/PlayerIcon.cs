using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour {

    public static PlayerIcon instance;

    private void Awake()
    {
        instance = this;
    }

    public void MoveIcon(int moveDirection)
    {
        float x = 0.2f * (moveDirection / 3) * Mathf.Pow(-1, moveDirection);
        float y = 0.2f * (1 + (-1) * (moveDirection / 3)) * Mathf.Pow(-1, moveDirection);
        
        //float x = 0.2f * Mathf.CeilToInt(Mathf.Cos(Mathf.PI / 2.0f * moveDirection + Mathf.PI / 4.0f)) * Mathf.Pow(-1, moveDirection);
        //float y = 0.2f * Mathf.CeilToInt(Mathf.Cos(Mathf.PI / 4.0f * moveDirection + 0.1f)) * Mathf.Pow(-1, moveDirection);

        Vector3 value = new Vector3(x, y, 0);
        transform.position += value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            transform.SetParent(collision.transform);
            transform.localPosition = Vector2.zero;
        }
    }
}
