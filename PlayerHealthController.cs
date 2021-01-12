using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthController : MonoBehaviour
{

    public int maxHP;
    private int currentHP;

    public SpriteRenderer[] heartDisplay;
    public Sprite heartFull, heartEmpty;

    public Transform heartHolder;

    public float invincibilityTime, healthFlashTime = .2f;
    private float invincCounter, flashCounter;


    void Start()
    {
        currentHP = maxHP; 
    }


    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter < 0)
            {
                flashCounter = healthFlashTime;

                heartHolder.gameObject.SetActive(!heartHolder.gameObject.activeInHierarchy);
            }
            if (invincCounter <= 0)
            {
                heartHolder.gameObject.SetActive(true);
            }
        }
    }

    private void LateUpdate()
    {
        heartHolder.localScale = transform.localScale;
    }

    public void UpdateHealthDisplay()
    {
        switch (currentHP)
        {
            case 3:
                heartDisplay[0].sprite = heartFull;
                heartDisplay[1].sprite = heartFull;
                heartDisplay[2].sprite = heartFull;
                break;

            case 2:
                heartDisplay[0].sprite = heartFull;
                heartDisplay[1].sprite = heartFull;
                heartDisplay[2].sprite = heartEmpty;
                break;

            case 1:
                heartDisplay[0].sprite = heartFull;
                heartDisplay[1].sprite = heartEmpty;
                heartDisplay[2].sprite = heartEmpty;
                break;

            case 0:
                heartDisplay[0].sprite = heartEmpty;
                heartDisplay[1].sprite = heartEmpty;
                heartDisplay[2].sprite = heartEmpty;
                break;
        }
    }

    public void DamagePlayer(int damageToTake)
    {
        if (invincCounter <= 0)
        {

            AudioManager.instance.PlaySFX(6);
            currentHP -= damageToTake;

            if (currentHP < 0)
            {
                currentHP = 0;
            }

            UpdateHealthDisplay();

            if (currentHP == 0)
            {
                gameObject.SetActive(false);
                AudioManager.instance.PlaySFX(5);
            }

            invincCounter = invincibilityTime;
        }
    }

    public void FillHealth()
    {
        currentHP = maxHP;
        UpdateHealthDisplay();

        flashCounter = 0;
        invincCounter = 0;

        heartHolder.gameObject.SetActive(true);
    }

    public void MakeInvincible(float invicLenght)
    {
        invincCounter = invicLenght;
    }
}
