using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public SpriteRenderer sr;

    public Sprite downSprite, upSprite;

    public float stayUpTime, bouncePower;
    private float upCounter;



    void Update()
    {
        if (upCounter > 0)
        {
            upCounter -= Time.deltaTime;

            if (upCounter <= 0)
            {
                sr.sprite = downSprite;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            upCounter = stayUpTime;
            sr.sprite = upSprite;

            Rigidbody2D rb2d = other.GetComponent<Rigidbody2D>();
            rb2d.velocity = new Vector2(rb2d.velocity.x, bouncePower);

            AudioManager.instance.PlaySFX(7);
        }
    }
}
