using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;


    private void OnEnable()
    {
        Debug.Log("CHECKING IF SHOW SCORE");
        if (PlayerStats.Points > 0)
        {
            score.enabled = true;
            score.text = $"Your score was: {PlayerStats.Points}. Great job!!";
            // reset players point to zero
            PlayerStats.Points = 0;
        }
    }
}
