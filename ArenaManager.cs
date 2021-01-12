using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArenaManager : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();

    private bool roundOver;

  

    public GameObject[] powerUps;
    public float timeBetweenPowerUps;
    private float powerUpCounter;


    void Start()
    {
        foreach (PlayerController player in GameManager.instance.activePlayers)
        {
            int randomPoint = Random.Range(0, spawnPoints.Count);
            player.transform.position = spawnPoints[randomPoint].position;

            if (GameManager.instance.activePlayers.Count <= spawnPoints.Count)
            {
                spawnPoints.RemoveAt(randomPoint);
            }  
        }

        GameManager.instance.canFight = true;
        GameManager.instance.ActivatePlayers();

        powerUpCounter = timeBetweenPowerUps * Random.Range(.75f, 1.25f);
    }


    void Update()
    {
        if (GameManager.instance.CheckActivePlayers() == 1 && !roundOver)
        {
            roundOver = true;
            StartCoroutine(EndRoundCo());
        }


        if (powerUpCounter > 0)
        {
            powerUpCounter -= Time.deltaTime;

            if (powerUpCounter <= 0)
            {
                int randomPoint = Random.Range(0, spawnPoints.Count);
                Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);

                powerUpCounter = timeBetweenPowerUps * Random.Range(.75f, 1.25f);
            }
        }
    }


    IEnumerator EndRoundCo()
    {
        UIController.instance.winBar.SetActive(true);
        UIController.instance.roundCompleteText.SetActive(true);
        UIController.instance.playerwinText.gameObject.SetActive(true);
        UIController.instance.playerwinText.text = "Player " + (GameManager.instance.lastPlayerNumber + 1) + " wins!";

        GameManager.instance.AddRoundWin();

        yield return new WaitForSeconds(5f);

        UIController.instance.loadingScreen.SetActive(true);

        GameManager.instance.GoToNextArena();
    }
}
