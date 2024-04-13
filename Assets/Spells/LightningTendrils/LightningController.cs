using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class LightningController : MonoBehaviour
{
    public static bool isRune1Active = false;
    public static bool isRune2Active = false;
    public static bool isRune3Active = false;
    
    public static List<bool> activationFlags = new List<bool>(){isRune1Active,isRune2Active,isRune3Active};
    public float damage;
    
    private Animator anim;
    private Transform visualTransform;
    private Vector3 lightningTarget;
    private GameObject castingPoint;
    
    private Collider2D col;
    //private Collider2D[] targetsInRange;
    private List<Collider2D> targetsInRange;
    private float angle;
    
    private void Awake()
    {
        targetsInRange = new List<Collider2D>();
       
        col = GetComponent<Collider2D>();
        castingPoint = GameObject.Find("Skill Point");
        damage = 5;
        if (activationFlags[0])
        {
            damage = 10;
        }

        if (activationFlags[1])
        {
            transform.localScale = new Vector3(10f,10f,1);
        }
        
        visualTransform = GetComponentInChildren<Transform>();
        anim = GetComponentInChildren<Animator>();

        if (!activationFlags[2])
        {
            Vector3 cursorPosition = Input.mousePosition;
            cursorPosition.z = Camera.main.nearClipPlane;
            lightningTarget = Camera.main.ScreenToWorldPoint(cursorPosition);
            lightningTarget.z = 0;

            var normalizedTarget = (lightningTarget - transform.position).normalized;
            angle = Mathf.Atan2(normalizedTarget.y, normalizedTarget.x) * Mathf.Rad2Deg;
            Quaternion rotation = new Quaternion();
        
            rotation.eulerAngles = new Vector3(0,0,angle);
            transform.rotation = rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = castingPoint.transform.position;
    }
    
    public void inflictDamage()
    {
        //targetsInRange = Physics2D.OverlapBoxAll(visualTransform.position, col.bounds.size, Mathf.Atan2(transform.right.y,transform.right.x)* Mathf.Rad2Deg);
        
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
        if (!targetsInRange.Contains(other) && (other.CompareTag("Enemy") || other.CompareTag("Boss")))
        {
            targetsInRange.Add(other);
        }
    }
    
    
}
