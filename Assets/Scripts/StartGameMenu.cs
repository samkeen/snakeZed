using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameMenu : MonoBehaviour
{
    private ScoreBoard scoreBoard;
    private bool checkForAnyKey = false;
    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnEnable()
    {
        checkForAnyKey = true;
    }

    private void OnDisable()
    {
        checkForAnyKey = false;
    }

    void Update()
    {
        if (checkForAnyKey && Input.anyKey)
        {
            Debug.Log("ANY KEY");
            
            checkForAnyKey = false;
            scoreBoard.StartGame();
            
        }
    }
}
