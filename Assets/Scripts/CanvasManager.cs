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
    [SerializeField] private TextMeshProUGUI scoreField;
    [SerializeField] private TextMeshProUGUI InstructionsField;
    

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

    public string getScoreText()
    {
        return scoreField.text;
    }
    public void setScoreText(string score)
    {
        scoreField.text = score;
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
        }
        else { Debug.LogWarning("The desired canvas was not found!"); }
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