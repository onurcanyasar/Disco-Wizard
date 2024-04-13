using System;
using System.Collections;
using System.Collections.Generic;
using Cell_Folder;
using DG.Tweening;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public static bool isRune1Active = false;
    public static bool isRune2Active = false;
    public static bool isRune3Active = false;
    
    public static List<bool> activationFlags = new List<bool>(){isRune1Active,isRune2Active,isRune3Active};
    
    private Vector3 fireballTarget;
    private GameObject temp;
    private Animator anim;
    public float speed = 20;
    public float damage;

    private Vector2 velocity;

    private Rigidbody2D rb;
    private bool isEnded = false;
    private RaycastHit2D enemy;
    private float distanceToEnemy;
    private TerrainGenerator TerrainGenerator;
    void Awake()
    {
        TerrainGenerator = GameObject.Find("TerrainGenerator").GetComponent<TerrainGenerator>();
        velocity = new Vector2(speed,0);
        rb = GetComponent<Rigidbody2D>();
        damage = 20;
        if (activationFlags[0])
        {
            damage = 35;
        }

        if (activationFlags[1])
        {
            transform.localScale = new Vector3(4,4,1);
        }

        
    
        anim = GetComponentInChildren<Animator>();

        
        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = Camera.main.nearClipPlane;
        fireballTarget = Camera.main.ScreenToWorldPoint(cursorPosition);
        fireballTarget.z = 0;
        
        Vector3 dir = fireballTarget - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        //transform.right = fireballTarget - transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        if (Vector3.Distance(pos,fireballTarget)< 0.5)
        {
            isEnded = true;
            anim.SetTrigger("end");
        }
        /*
        enemy = Physics2D.Raycast(pos, transform.right, 1000f,LayerMask.GetMask("Default"));
        if (enemy.collider != null && enemy.collider.tag.Equals("Enemy") && !isEnded)
        {
            distanceToEnemy = Vector3.Distance(pos, enemy.point);
            if (distanceToEnemy < 0.04)
            {
                isEnded = true;
                anim.SetTrigger("end");
                enemy.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
        */

       
    }

    private void FixedUpdate()
    {
        if (!isEnded)
        {
            rb.MovePosition(rb.position + (Vector2)transform.right * (Time.fixedDeltaTime * speed));
        }

    }

    private void OnTriggerEnter2D(Collider2D other) //onur
    {
        
        if (other.tag.Equals("Sand") && !isEnded)
        {
            isEnded = true;
            anim.SetTrigger("end");
            GameObject target = other.gameObject;
            int[] points = new int[]{Mathf.RoundToInt(target.transform.position.x),
                Mathf.RoundToInt(target.transform.position.y)};
            
            List<int[]> neighbours = HelperScript.GetNeighbours(points[0], points[1], 4);
            
            foreach (var point in neighbours)
            {
                
                int x = point[0];
                int y = point[1];
                
                if (x > 0 && x < TerrainGenerator.grid.width && y > 0 && y < TerrainGenerator.grid.height)
                {
                    Cell cell = TerrainGenerator.grid.gridArray[x, y];
                    if (cell == null)
                    {
                        continue;
                    }
                    if (cell.code == 1)
                    {
                        cell.Destroy();
                    }
                }


            }
        }
        else if (other.tag.Equals("Wood") && !isEnded)
        {
            isEnded = true;
            anim.SetTrigger("end");
            GameObject target = other.gameObject;
            int[] points = new int[]{Mathf.RoundToInt(target.transform.position.x),
                Mathf.RoundToInt(target.transform.position.y)};
            int x = points[0];
            int y = points[1];
            Cell cell = TerrainGenerator.grid.gridArray[x, y];
            cell?.Destroy();
            Fire fire = new Fire(x, y, TerrainGenerator.fireObject, speed, TerrainGenerator.grid, 4, TerrainGenerator.fires, TerrainGenerator.fireObject, TerrainGenerator.burnParticle);
            TerrainGenerator.fires.Add(fire);
        }
        else if ((other.CompareTag("Enemy") || other.CompareTag("Boss")) && !isEnded)
        {
            isEnded = true;
            anim.SetTrigger("end");
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        
        
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag.Equals("Enemy"))
    //     {
    //         isEnded = true;
    //         anim.SetTrigger("end");
    //         other.GetComponent<EnemyHealth>().TakeDamage(damage);
    //         
    //     }
    // }
}
