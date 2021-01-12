using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayMenu : MonoBehaviour
{
    public string gameMenuScene;
    public GameObject playStationCanvas;
    public GameObject xboxCanvas;
    public GameObject keyboardCanvas;
    public GameObject gameRulesCanvas;

    public void PlaystationActive()
    {
        playStationCanvas.SetActive(true);
        xboxCanvas.SetActive(false);
        keyboardCanvas.SetActive(false);
        gameRulesCanvas.SetActive(false);
    }

    public void XboxActive()
    {
        playStationCanvas.SetActive(false);
        xboxCanvas.SetActive(true);
        keyboardCanvas.SetActive(false);
        gameRulesCanvas.SetActive(false);
    }

    public void KeyboardActive()
    {
        playStationCanvas.SetActive(false);
        xboxCanvas.SetActive(false);
        keyboardCanvas.SetActive(true);
        gameRulesCanvas.SetActive(false);
    }

    public void GameRulesActive()
    {
        playStationCanvas.SetActive(false);
        xboxCanvas.SetActive(false);
        keyboardCanvas.SetActive(false);
        gameRulesCanvas.SetActive(true);
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene(gameMenuScene);
    }
}
