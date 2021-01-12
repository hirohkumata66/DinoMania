using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathpitandspikes : MonoBehaviour
{
    public int damageToDeal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && GameManager.instance.canFight)
        {
            other.GetComponent<PlayerHealthController>().DamagePlayer(damageToDeal);
        }
    }
}
