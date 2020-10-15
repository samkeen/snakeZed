using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    /// <summary>
    /// https://www.youtube.com/watch?v=TdiN18PR4zk&feature=youtu.be
    /// </summary>
    public delegate void EatenDelegate();
    public event EatenDelegate eatenEvent;
    
    private void OnEaten()
    {
        // broadcast onEaten event
        eatenEvent?.Invoke();
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
