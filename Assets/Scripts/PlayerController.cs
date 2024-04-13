using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable<float>, IKillable
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;
    public float moveSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;
    private Vector2 moveVector;
    private float input;
    private bool isJumping;
    private bool isAttacking;
    private bool onGround;
    public GameObject attack1Point;
    public GameObject magic;
    public Transform cam;
    public Transform basicAttackPoint;
    public GameObject basicAttack;
    private Collider2D coll;
    private AudioSource source;
    public AudioClip basicAttackSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        isJumping = false;
        isAttacking = false;
        coll = GetComponent<BoxCollider2D>();
        source = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        
        input = Input.GetAxisRaw("Horizontal");
        onGround = isGrounded();
        if (Input.GetKey(KeyCode.Space) && onGround && !isAttacking)
        {
            anim.SetBool("jump", true);
            isJumping = true;
        }
        
        
        if (isAttacking)
        {
            input = 0;
            
        }
        
        if (Input.GetButtonDown("Fire1") && !InventoryManager.isInventoryOpen)
        {
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            basicAttackPoint.transform.right = mousePos - (Vector2) basicAttackPoint.transform.position;
           
            float angle = Vector2.Angle(basicAttackPoint.position, mousePos);
            //Debug.Log(wrapAngle(basicAttackPoint.eulerAngles.x));
            GameObject clone = Instantiate(basicAttack, basicAttackPoint.position, basicAttackPoint.transform.rotation);
            source.PlayOneShot(basicAttackSound);
            
        
            Rigidbody2D rbClone = clone.GetComponent<Rigidbody2D>();
            Vector2 resultant = mousePos - (Vector2) basicAttackPoint.position;
            
          

        }

        animationHandler();

        
    }

    private void move(float direction, float speed)
    {

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    private void jump(float jumpForce)
    {
        rb.AddForce(Vector2.up * (jumpForce * 30));
    }

    public void TurnWizard(string rotation)
    {
        switch (rotation)
        {
            case "L":
                transform.rotation = Quaternion.Euler(0,180,0);
               // attack1Point.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            
            case "R":
                //transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                transform.rotation = Quaternion.Euler(0,0,0);
               // attack1Point.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    } 
    private void animationHandler()
    {
        if (input > 0)
        {
            anim.SetBool("run", true);
            transform.rotation = Quaternion.Euler(0,0,0);

            

        }
        if (input < 0)
        {
            anim.SetBool("run", true);
            transform.rotation = Quaternion.Euler(0,180,0);


        }
        if (input == 0 || isAttacking)
        {
            anim.SetBool("run", false);
        }
        
        if (onGround)
        {
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        else
        {
            anim.SetBool("jump", true);
        }
        
        
    }
    private void FixedUpdate()
    {
        if (isAttacking)
        {
            move(0, moveSpeed);    
        }
        else
        {
            move(input, moveSpeed);    
        }
        
        if (isJumping)
        {
            jump(jumpForce);
            isJumping = false;
        }
        
    }

    bool isGrounded()
    {

        Bounds bounds = coll.bounds;
        float width = bounds.size.x / 2;
        float height = bounds.size.y / 2;
        Vector3 bottomCenter = bounds.center + new Vector3(0, -height);
        Vector3 bottomLeft = bounds.center + new Vector3(-width, -height);
        Vector3 bottomRight = bounds.center + new Vector3(+width, -height);
        Vector2 direction = Vector2.down;
        float distance = 0.1f;
        
        RaycastHit2D hitCenter = Physics2D.Raycast(bottomCenter, direction, distance, groundLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(bottomLeft, direction, distance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(bottomRight, direction, distance, groundLayer);
        bool res = hitCenter.collider != null || hitRight.collider != null || hitLeft.collider != null;



        return res;
    }

    void attack()
    {
        Instantiate(magic, attack1Point.transform.position, attack1Point.transform.rotation);
    }

    void endAttack()
    {
        isAttacking = false;
        anim.SetBool("attack1", false);
    }
    
    private float  wrapAngle(float angle)
    {
        angle%=360;
        if(angle >180)
            return angle - 360;
 
        return angle;
    }

    public void Kill()
    {
        throw new NotImplementedException();
    }

    public void Damage(float damageTaken)
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Rune>() != null)
        {
            source.Play();
        }
    }
}
