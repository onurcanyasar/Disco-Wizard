﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeRange : MonoBehaviour
{
    private FlyingEyeController flyingEyeController;
    // Start is called before the first frame update
    void Awake()
    {
        flyingEyeController = transform.parent.GetComponent<FlyingEyeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            flyingEyeController.isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            flyingEyeController.isPlayerInRange = false;
        }
    }
}
