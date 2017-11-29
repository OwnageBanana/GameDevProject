using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static HUDController HUD;

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
