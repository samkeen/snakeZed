using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private float snakeSpeed = 10;

    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, z);
        transform.Translate(move * Time.deltaTime * snakeSpeed);
    }
}
