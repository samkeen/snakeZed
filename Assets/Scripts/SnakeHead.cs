using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private float snakeSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(move * Time.deltaTime * snakeSpeed);
        // Debug.Log($"y = {move.y}");
        // controller.Move(move * Time.deltaTime * snakeSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger - Other: {other.name}");
    }
}
