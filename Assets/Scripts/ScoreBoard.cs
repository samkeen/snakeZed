using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI introScreen;
    [SerializeField] private int pointsForEatenApple = 10;
    [SerializeField] private float introTimeInSeconds = 4;

    private void Start()
    {
        scoreText.text = "0";
        FindObjectOfType<Apple>().EatenEvent += OnAppleEaten;
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

    private void OnAppleEaten()
    {
        Debug.Log("**ScoreBoard saw apple eaten event**");
        var score = Int32.Parse(scoreText.text);
        score += pointsForEatenApple;
        scoreText.text = score.ToString();
    }
}
