using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    public int stompDmg = 1;
    public float bounceForce = 12f;
    public PlayerController thePlayer;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.instance.canFight)
            {
                other.GetComponent<PlayerHealthController>().DamagePlayer(stompDmg);
            }
            

            thePlayer.rb2d.velocity = new Vector2(thePlayer.rb2d.velocity.x, bounceForce);

            AudioManager.instance.PlaySFX(2);
        }
    }
}
