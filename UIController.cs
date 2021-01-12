using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class UIController : MonoBehaviour
{
    public static UIController instance;

    public TMP_Text playerwinText;
    public GameObject winBar, roundCompleteText;

    public GameObject pauseScreen, loadingScreen;

    public string mainMenuScene;

    public GameObject firstPauseButton;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        pauseScreen.SetActive(false);
    }


    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame)
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeInHierarchy)
        {
            pauseScreen.SetActive(false);

            Time.timeScale = 1f;
        }
        else
        {
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstPauseButton);
        }
    }

    public void MainMenu()
    {
        foreach (PlayerController player in GameManager.instance.activePlayers)
        {
            Destroy(player.gameObject);
        }

        Destroy(GameManager.instance.gameObject);
        GameManager.instance = null;

        SceneManager.LoadScene(mainMenuScene);

        Time.timeScale = 1f;

    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }
}
