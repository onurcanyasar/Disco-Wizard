using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTerrainInteractionHandler : MonoBehaviour
{

    private bool isBurning;
    public GameObject fireObject;
    private PlayerHealthAndMana playerHealth;


    private void Start()
    {
        playerHealth = GetComponent<PlayerHealthAndMana>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isBurning && other.tag.Equals("Fire"))
        {
            startBurn();
        }

        if (other.tag.Equals("Water"))
        {
            isBurning = false;
        }
    }


    private void startBurn()
    {
        isBurning = true;
        fireObject.SetActive(true);

        StartCoroutine(BurnCycle());
    }

    IEnumerator BurnCycle()
    {
        StartCoroutine(TakeDamage());
        yield return new WaitForSeconds(3);
        fireObject.SetActive(false);
        isBurning = false;
    }

    IEnumerator TakeDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            playerHealth.TakeDamage(0.2f);
            if (!isBurning)
            {
                fireObject.SetActive(false);
                break;
            }
        }
        
    }
}
