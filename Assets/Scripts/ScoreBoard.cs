using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI introScreen;
    [SerializeField] private int pointsForEatenFood = 10;
    [SerializeField] private float introTimeInSeconds = 4;

    private void Start()
    {
        scoreText.text = "0";
        FindObjectOfType<Food>().EatenEvent += OnFoodEaten;
        // freeze until start UI button calls this.StartGame()
        FindObjectOfType<SnakeHead>().IsFrozen = true;
    }

    public void StartGame()
    {
        introScreen.enabled = false;
        AudioManager.instance.StopPlay("Menu Music");
        AudioManager.instance.Play("Game Music");
        FindObjectOfType<SnakeHead>().IsFrozen = false;
    }

    private void OnFoodEaten()
    {
        Debug.Log("**ScoreBoard saw food eaten event**");
        var score = Int32.Parse(scoreText.text);
        PlayerStats.Points += pointsForEatenFood;
        score += pointsForEatenFood;
        scoreText.text = score.ToString();
    }
}
