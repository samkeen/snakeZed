using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// see: https://www.youtube.com/watch?v=zc8ac_qUXQY
/// </summary>
public class MainMenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
        
    }
}
