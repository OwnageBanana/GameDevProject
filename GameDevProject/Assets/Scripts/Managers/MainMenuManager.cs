using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //on start game button click
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    //on instruction button click
    public void ShowInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    // on return button click
    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
