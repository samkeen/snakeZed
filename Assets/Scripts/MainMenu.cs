using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// see: https://www.youtube.com/watch?v=zc8ac_qUXQY
/// </summary>
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/Snake");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
        
    }
}
