using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private void OnEaten()
    {
        GameEvents.current.AppleEaten();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // broadcast "Was Eaten"
        Debug.Log($"Trigger - Other: {other.name}");
        if (other.CompareTag("SnakeHead"))
        {
            OnEaten();
        }
    }
}
