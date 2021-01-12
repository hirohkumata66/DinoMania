using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameStartScene;
    public string gameOptionsScene;
    public string howToPlayScene;


    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene(gameOptionsScene);
    }

    public void HowToPlayMenu()
    {
        SceneManager.LoadScene(howToPlayScene);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }

}
