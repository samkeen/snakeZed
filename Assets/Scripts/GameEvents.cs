using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    public event Action onAppleEaten;
    
    public void AppleEaten()
    {
        if (onAppleEaten != null)
        {
            onAppleEaten();
        }
    }
    
    void Awake()
    {
        current = this;
    }

}
