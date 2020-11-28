using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
/// <summary>
/// see: https://www.youtube.com/watch?v=vmKxLibGrMo
/// </summary>
public enum CanvasType
{
    MainMenu,
    GameUI,
    OptionsScreen,
    CreditsScreen
}

public class CanvasManager : Singleton<CanvasManager>
{
    List<CanvasController> canvasControllerList;
    CanvasController lastActiveCanvas;
    [SerializeField] private TextMeshProUGUI inGameScoreField;
    [SerializeField] private TextMeshProUGUI InstructionsField;
    [SerializeField] private TextMeshProUGUI mainMenuGameFeedback;
    

    protected override void Awake()
    {
        base.Awake();
        InitMenus();
    }

    private void InitMenus()
    {
        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));
        SwitchCanvas(CanvasType.MainMenu);
    }

    public void setInGameScoreText(string score)
    {
        inGameScoreField.text = score;
    }

    public void SwitchCanvas(CanvasType _type)
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }

        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveCanvas = desiredCanvas;
            if (_type == CanvasType.MainMenu)
            {
                DisplayObtainedScore();
            }
        }
        else { Debug.LogWarning("The desired canvas was not found!"); }
    }

    private void DisplayObtainedScore()
    {
        var obtainedScore = PlayerStats.Points;
        var message = "";
        if (obtainedScore >= 100)
        {
            message = "Great job!!";
            
        }
        else if(obtainedScore >= 50)
        {
            message = "Good job!";
        }
        else
        {
            message = "Better luck next time";
        }
        mainMenuGameFeedback.SetText($"Your score was: {obtainedScore}. {message}");
    }

    public void ShowInstructions()
    {
        InstructionsField.enabled = true;
    }
    public void HideInstructions()
    {
        InstructionsField.enabled = false;
    }
    
    
}