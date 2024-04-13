using System;
using System.Collections;
using System.Collections.Generic;
using Cell_Folder;
using UnityEngine;

public class BasicAttackHandler : MonoBehaviour, IKillable
{
    private Rigidbody2D rb;
    public GameObject particles;
    private Collider2D playerColl;

    public float damage;
    
    private TerrainGenerator TerrainGenerator;
    void Awake()
    {
        TerrainGenerator = GameObject.Find("TerrainGenerator").GetComponent<TerrainGenerator>();
        damage = 5;
        rb = GetComponent<Rigidbody2D>();
        /*
        playerColl = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerColl, GetComponent<Collider2D>());
        */
    }   

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right.normalized * 50;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Sand") || other.gameObject.tag.Equals("Wood"))
        {
            GameObject target = other.gameObject;
            CellLocationHandler cLoc = target.GetComponent<CellLocationHandler>();
            Cell cell = TerrainGenerator.grid.gridArray[cLoc.x, cLoc.y];
            cell.Destroy();
            
        }

        if (other.gameObject.CompareTag("Enemy") ||other.gameObject.CompareTag("Boss") )
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        
      
        Vector3 point = other.GetContact(0).point;
        Instantiate(particles, point, Quaternion.identity);
        Kill();

    }

    public void Kill()
    {
        Destroy(gameObject);
    }

 
}
