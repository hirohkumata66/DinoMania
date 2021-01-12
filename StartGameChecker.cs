using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGameChecker : MonoBehaviour
{
    public string levelToLoad;

    private int playersInZone;

    public TMP_Text startCountText;

    public float timeToStart = 3f;
    private float startCounter;


    void Update()
    {
        if(playersInZone > 1 && playersInZone == GameManager.instance.activePlayers.Count)
        {
            if (!startCountText.gameObject.activeInHierarchy)
            {
                AudioManager.instance.PlaySFX(4);
            }

            startCountText.gameObject.SetActive(true);

            startCounter -= Time.deltaTime;

            startCountText.text = Mathf.CeilToInt(startCounter).ToString();

            if (startCounter <= 0)
            {

                GameManager.instance.StartFirstRound();
            }
        }
        else
        {
            startCountText.gameObject.SetActive(false);
            startCounter = timeToStart;
        }
          
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playersInZone++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playersInZone--;
        }
    }
}
