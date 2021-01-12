using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    public TMP_Text winText;
    public Image playerImage;

    public string mainMenuScene, charSelectScene;


    void Start()
    {
        winText.text = "Player " + (GameManager.instance.lastPlayerNumber + 1) + " won the Game!";
        playerImage.sprite = GameManager.instance.activePlayers[GameManager.instance.lastPlayerNumber].GetComponent<SpriteRenderer>().sprite;
    }


    void Update()
    {
        
    }

    public void PlayAgain()
    {
        GameManager.instance.StartFirstRound();
    }

    public void SelectCharacter()
    {
        ClearGame();

        SceneManager.LoadScene(charSelectScene);
    }

    public void MainMenu()
    {
        ClearGame();

        SceneManager.LoadScene(mainMenuScene);
    }

    public void ClearGame()
    {
        foreach (PlayerController player in GameManager.instance.activePlayers)
        {
            Destroy(player.gameObject);
        }

        Destroy(GameManager.instance.gameObject);
        GameManager.instance = null;
    }
}
