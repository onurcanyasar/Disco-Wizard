using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RainController : MonoBehaviour
{
    public static bool isRune1Active = false;
    public static bool isRune2Active = false;
    public static bool isRune3Active = false;
    
    public static List<bool> activationFlags = new List<bool>(){isRune1Active,isRune2Active,isRune3Active};
    public float damage;
    
    private Animator anim;
    private float distanceToFloor;
    private RaycastHit2D floor;
    private bool isEnded = false;
    private Rigidbody2D rb;
    public Vector2 velocity;
    public Collider2D col;
    public float increaseSpeed = 1;
    //public Collider2D[] targetsInRange;
    private List<Collider2D> targetsInRange;

    private void Awake()
    {
        targetsInRange = new List<Collider2D>();

        col = GetComponent<Collider2D>();
        damage = 30;
        if (activationFlags[0])
        {
            damage = 60;
        }

        if (activationFlags[1])
        {
            transform.localScale = new Vector3(20,20,0);
        }
        
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(0,-5);
    }

    // Update is called once per frame
    void Update()
    { 
        floor = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity,LayerMask.GetMask("Cell"));

        if (floor.collider != null)
        {
            distanceToFloor = Vector3.Distance(transform.position, floor.point);
            if (distanceToFloor <= 0.8)
            {
                //Debug.Log("trigger ve Terrain");
                anim.SetTrigger("end");
                isEnded = true;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!isEnded)
        {
            rb.MovePosition(rb.position + velocity * (Time.fixedDeltaTime * increaseSpeed));
            increaseSpeed += 0.2f;
        }
    }

    public void inflictDamage()
    {
        //targetsInRange = Physics2D.OverlapBoxAll(transform.position, col.bounds.size, 0);

        if (targetsInRange != null)
        {
            foreach (var target in targetsInRange)
            {
                if (target == null)
                {
                    targetsInRange.Remove(target);
                    continue;
                }
                if (target.CompareTag("Enemy") || target.CompareTag("Boss"))
                {
                    target.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!targetsInRange.Contains(other) && (other.CompareTag("Enemy") || other.CompareTag("Boss")) && isEnded)
        {
            targetsInRange.Add(other);
        }
    }
}
