using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// These are called from 
/// </summary>
public class ButtonsController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/Game");
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.GameUI);
    }
    public void Credits()
    {
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.CreditsScreen);
    }
    public void Options()
    {
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.OptionsScreen);
    }
    public void MainMenu()
    {
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.MainMenu);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
