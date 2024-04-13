using System;
using System.Collections;
using System.Collections.Generic;
using Cell_Folder;
using DG.Tweening;
using UnityEngine;

public class TornadoController : MonoBehaviour
{
    public static bool isRune1Active = false;
    public static bool isRune2Active = false;
    public static bool isRune3Active = false;
    
    public static List<bool> activationFlags = new List<bool>(){isRune1Active,isRune2Active,isRune3Active};
    
    private Vector3 tornadoTarget;
    private float damage;
    private Tween movement;
    private GameObject temp;
    private Animator anim;
    public float speed = 5;
    private bool isEnded = false;
    private Transform visualTransform;

    private Collider2D col;
   // private Collider2D[] targetsInRange;
   private List<Collider2D> targetsInRange;
   public TerrainGenerator TerrainGenerator;
   
   
    void Awake()
    {
        TerrainGenerator = GameObject.Find("TerrainGenerator").GetComponent<TerrainGenerator>();
        targetsInRange = new List<Collider2D>();
        col = GetComponent<Collider2D>();
        damage = 10;
        if (activationFlags[0])
        {
            damage = 20;
        }

        if (activationFlags[1])
        {
            transform.localScale = new Vector3(20f,20f,1);
        }
        visualTransform = GetComponentInChildren<Transform>();
        anim = GetComponentInChildren<Animator>();
        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = Camera.main.nearClipPlane;
        tornadoTarget = Camera.main.ScreenToWorldPoint(cursorPosition);
        tornadoTarget.z = 0;

      
        transform.right = tornadoTarget - transform.position;
        transform.up = Vector3.up;
        // visualTransform.up = Vector3.up;
        // transform.right = tornadoTarget - transform.position;
        
        //movement = transform.DOMove(tornadoTarget,7).SetEase(Ease.Linear).SetSpeedBased().SetAutoKill(false);
        //Vector3.MoveTowards(transform.position, tornadoTarget, 100);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
       transform.position = Vector3.MoveTowards(transform.position, tornadoTarget, step);
        
        
        if(Vector3.Distance(transform.position,tornadoTarget)<0.001f && !isEnded)
        {
            if (activationFlags[2])
            {
                Invoke("endAnimationLate",5);
                isEnded = true;
            }
            else
            {
                anim.SetTrigger("end");
                isEnded = true; 
            }
            
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
                if (target.tag.Equals("Enemy") || target.CompareTag("Boss"))
                {
                    target.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!targetsInRange.Contains(other) && (other.CompareTag("Enemy") || other.CompareTag("Boss")))
        {
            targetsInRange.Add(other);
        }

        if (other.tag.Equals("Sand"))
        {
            GameObject target = other.gameObject;

            int[] points = new int[]
            {
                Mathf.RoundToInt(target.transform.position.x),
                Mathf.RoundToInt(target.transform.position.y)
            };
            int x = points[0];
            int y = points[1];
            List<int[]> neighbours = HelperScript.GetNeighbours(x, y, 4);
            foreach (var point in neighbours)
            {
                x = point[0];
                y = point[1];

                if (x > 0 && x < TerrainGenerator.grid.width && y > 0 && y < TerrainGenerator.grid.height)
                {
                    Cell cell = TerrainGenerator.grid.gridArray[x, y];
                    if (cell == null)
                    {
                        continue;
                    }
                    if (cell.code == 1)
                    {
                        float step = 6;
                        cell.rb.position = Vector3.MoveTowards(cell.rb.position, new Vector3(x, y + 15),
                            step * Time.fixedDeltaTime);
                        
                    }
                }
            }
        }



    }

    

    void endAnimationLate()
    {
        anim.SetTrigger("end");
    }

   
}
