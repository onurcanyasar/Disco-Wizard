using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoRangeController : MonoBehaviour
{
    private MinotaurController minotaurController;
    // Start is called before the first frame update
    void Awake()
    {
        minotaurController = transform.parent.GetComponent<MinotaurController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            minotaurController.isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            minotaurController.isPlayerInRange = false;
        }
    }
}
