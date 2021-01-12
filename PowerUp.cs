using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isHealth, isInvincible, isSpeed, isGravity;

    public float powerUpLenght, powerUpAmount;

    public GameObject pickUpEffect;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isHealth)
            {
                other.GetComponent<PlayerHealthController>().FillHealth();
                AudioManager.instance.PlaySFX(10);
            }

            if (isInvincible)
            {
                other.GetComponent<PlayerHealthController>().MakeInvincible(powerUpLenght);
                AudioManager.instance.PlaySFX(11);
            }

            if (isSpeed)
            {
                PlayerController player = other.GetComponent<PlayerController>();
                player.moveSpeed = powerUpAmount;
                player.powerUpCounter = powerUpLenght;
                AudioManager.instance.PlaySFX(0);
                Destroy(gameObject);
            }

            if (isGravity)
            {
                PlayerController player = other.GetComponent<PlayerController>();
                player.powerUpCounter = powerUpLenght;
                player.rb2d.gravityScale = powerUpAmount;
                AudioManager.instance.PlaySFX(9);
                Destroy(gameObject);
            }

            Instantiate(pickUpEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
