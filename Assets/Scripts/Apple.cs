using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    /// <summary>
    /// https://www.youtube.com/watch?v=TdiN18PR4zk
    /// </summary>
    public event Action EatenEvent;

    private void OnTriggerEnter(Collider other)
    {
        // broadcast "Was Eaten"
        if (other.CompareTag("SnakeHead"))
        {
            // broadcast onEaten event
            EatenEvent?.Invoke();
        }
    }
}
