using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int pointsForEatenApple = 10;
    


    private void Start()
    {
        scoreText.text = "0";
        // subscribe to Apple.eatenEvent
        FindObjectOfType<Apple>().EatenEvent += OnAppleEaten;
    }
    
    private void OnAppleEaten()
    {
        Debug.Log("**ScoreBoard saw apple eaten event**");
        var score = Int32.Parse(scoreText.text);
        score += pointsForEatenApple;
        scoreText.text = score.ToString();
    }
}
