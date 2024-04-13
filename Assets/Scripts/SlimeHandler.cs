using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float input;
    
    public float speed;
    
    private float width;
    private float height;
    private SpriteRenderer sr;
    public LayerMask enemyLayer;
    private BoxCollider2D coll;
    private float forwardDistance;
    private float downDistance;
    private float direction;
    private RaycastHit2D hitForward;
    private int cycle;
    public Transform player;
    private bool isJumped;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        speed = 2f;
        coll = GetComponent<BoxCollider2D>();
        width = coll.bounds.size.x / 2;
        height = coll.bounds.size.y / 2;
        direction = 1;
        cycle = 1;
        isJumped = false;

    }

    // Update is called once per frame
    void Update()
    {
        animationHandler();
        Debug.DrawRay(coll.bounds.center + new Vector3(width * transform.localScale.x, -height, 0), Vector2.down * 1, Color.black);
        if (rb.velocity.y < 0 )
        {
            isJumped = false;
        }
        
    }

    private void FixedUpdate()
    {
        
        aggroTarget(player);
        
    }

    private void aggroTarget(Transform target)
    {
        
        float distanceX = target.transform.position.x - transform.position.x;
        float distanceY = target.transform.position.y - transform.position.y;
        
        if (distanceX > 0)
        {
            direction = 1;
            
        }
        else if(distanceX <0)
        {
            direction = -1;
        }
        forwardDistance = rayForward();
        Debug.Log(distanceY);
        if (forwardDistance < 1f && forwardDistance > 0)
        {
            jump(12);
        }
        else if (distanceY > 0)
        { 
            jump(12);
        }
        
        move(direction, speed);
    }
    private void idleMovement()
    {
        
        downDistance = rayDown();
        if (forwardDistance < 1.2 && forwardDistance > 0 || downDistance == -1)
        {
            direction *= -1;
        }
        
        if (forwardDistance != -1)
        {
            if (hitForward.collider.CompareTag("Player"))
            {
                cycle = 2; 
            }
            
        }
    }

    public void stop()
    {
        direction = 0;
    }
    private void jump(float jumpForce)
    {
        if (isGrounded() && !isJumped)
        {
            rb.AddForce(Vector2.up * jumpForce * 30);
            isJumped = true;
            Debug.Log("jumped");
        }
        
        
        
    }

    private bool isGrounded()
    {
        
        float distance = rayDown();
        
        if (distance < 0.3f && distance > -1f)
        {
            return true;
            
        }
        

        return false;
    }
    public void move(float direction, float speed)
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    private float rayForward()
    {
        float rayDistance = 4;
        hitForward = Physics2D.Raycast(coll.bounds.center , Vector2.right * transform.localScale.x, rayDistance, ~enemyLayer);
        if (hitForward.collider != null)
        {
            return hitForward.distance;
        }

        return -1 ;
    }
    
    private float rayDown()
    {
        float rayDistance = 0.3f;
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center + new Vector3(width * transform.localScale.x, -height, 0) , Vector2.down, rayDistance, ~enemyLayer);
        if (hit.collider != null)
        {
            return hit.distance;
        }

        return -1;
    }
    
    private void animationHandler()
    {
        if (direction > 0)
        {
            anim.SetBool("run", false);
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        if (direction < 0)
        {
            anim.SetBool("run", false);
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            

        }
        if (direction == 0)
        {
            anim.SetBool("run", false);
        }
    }
}
