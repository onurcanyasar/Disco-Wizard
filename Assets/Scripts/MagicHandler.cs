using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHandler : MonoBehaviour, IKillable
{
    private Rigidbody2D rb;
    public float force;
    public GameObject particles;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        force = 10;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.right * force);
        
    }
    
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(particles, other.GetContact(0).point, Quaternion.identity);
        Destroy(gameObject);
        
    }

    public void Kill()
    {
        throw new NotImplementedException();
    }
}
