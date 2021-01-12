using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FireBallDamage : MonoBehaviour
{

    public int damageToDeal;
    private Rigidbody2D rb2d;
    public float speed = 3f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = transform.right * speed;       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && GameManager.instance.canFight)
        {
            other.GetComponent<PlayerHealthController>().DamagePlayer(damageToDeal);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Walls" || other.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
