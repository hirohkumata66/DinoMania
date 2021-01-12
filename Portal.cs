using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform exitPoint;
    public GameObject warpEffect;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = exitPoint.position;

            Instantiate(warpEffect, transform.position, transform.rotation);
            Instantiate(warpEffect, exitPoint.position, exitPoint.rotation);

            AudioManager.instance.PlaySFX(8);
        }
    }
}
