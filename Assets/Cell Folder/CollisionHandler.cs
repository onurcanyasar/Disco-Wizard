using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public bool collisionEntered;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            collisionEntered = true;
        }
        else
        {
            collisionEntered = false;
        }
        
    }

    
}
