using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onAppleEaten += OnAppleEaten;
    }

    private void OnAppleEaten()
    {
        Debug.Log("**Event Trigger**");
    }

}
