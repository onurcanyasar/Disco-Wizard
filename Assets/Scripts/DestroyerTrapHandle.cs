using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerTrapHandle : MonoBehaviour
{
    public GameObject cellDestroyer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            cellDestroyer.SetActive(true);    
        }
        
    }
}
