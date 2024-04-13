using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvilWizardAttack : MonoBehaviour
{
    public Transform attackPointRight;
    public Animator anim;

    public float damage = 10;

    public float attackRange = 0.5f;

    public LayerMask targetLayer;

    private Collider2D targetCollider;

    private Collider2D[] targetsInRight;
    // Update is called once per frame

    private void Awake()
    {
        targetCollider = GameObject.Find("AfroWizard").GetComponent<Collider2D>();
    }

    void Update()
    { 
        targetsInRight = Physics2D.OverlapCircleAll(attackPointRight.position,attackRange,targetLayer);
       
       

       if (targetsInRight.Contains(targetCollider))
       {
          // Debug.Log("target in range");
           
               anim.SetTrigger("Attack");
               anim.SetBool("isAttacking", true);
       }
       else
       {
           anim.SetBool("isAttacking", false);
       }
       // else
       // {
       //     if (anim.GetBool("isAttacking"))
       //     {
       //         anim.SetBool("isAttacking",false);
       //     }
       // }
       
       
    }

    public void Attack()
    {
        if (targetsInRight.Contains(targetCollider))
        {
            targetCollider.GetComponent<PlayerHealthAndMana>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPointRight == null)
            return;
        
        Gizmos.DrawWireSphere(attackPointRight.position,attackRange);

    }

    
}
