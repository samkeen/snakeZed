using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    /// <summary>
    /// If eaten by snake, broadcasts event
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
