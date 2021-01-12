using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectButton : MonoBehaviour
{
    public SpriteRenderer sr;

    public Sprite buttonUp, buttonDown;

    public bool isPressed;

    public float waitToPopUp;
    private float popCounter;

    public AnimatorOverrideController thecontroler;


    void Update()
    {
        if (isPressed)
        {
            popCounter -= Time.deltaTime;

            if (popCounter < 0)
            {
                isPressed = false;

                sr.sprite = buttonUp;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isPressed)
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player.rb2d.velocity.y < -.1f)
            {
                player.anim.runtimeAnimatorController = thecontroler;

                isPressed = true;

                sr.sprite = buttonDown;

                popCounter = waitToPopUp;
            }

            AudioManager.instance.PlaySFX(12);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && isPressed)
        {
            isPressed = false;

            sr.sprite = buttonUp;
        }
    }
}
