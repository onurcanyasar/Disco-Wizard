using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;
using Random = System.Random;

public class MinotaurAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRate;
    public float attackRange;
    public float attack1Damage;
    public float attack2Damage;
    public float attack4Damage;
    
    private Animator anim;
    private AIPath aiPath;
    private EnemyHealth enemyHealth;
    private MinotaurController minotaurController;
    private bool isAttacking = false;
    private bool isAttackStarted = false;
    private bool isSecondPhase = false;
    private string[] attacks;
    private Random rand;
    
    public LayerMask targetLayer;

    private Collider2D targetCollider;
    private PlayerHealthAndMana player;

    private Collider2D[] targetsInRight;
   
    // Start is called before the first frame update
    void Awake()
    {
        targetCollider = GameObject.Find("AfroWizard").GetComponent<Collider2D>();
        player = targetCollider.GetComponent<PlayerHealthAndMana>();
        minotaurController = GetComponent<MinotaurController>();

        attacks = new string[]{"Attack2","Attack1","Attack4"};
        anim = GetComponent<Animator>();
        aiPath = GetComponent<AIPath>();
        enemyHealth = GetComponent<EnemyHealth>();
        rand = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (minotaurController.isPlayerInRange)
        {
            if (enemyHealth.isSecondPhase && ! isSecondPhase)
            {
                attackRate /= 2;
                isSecondPhase = true;
            }
            if (Mathf.Abs(aiPath.desiredVelocity.x) <= 0.2 && ! isAttackStarted)
            {
                isAttacking = true;
                isAttackStarted = true;
                StartCoroutine(Attack());
            }
            else
            {
                isAttacking = false;
            }
        }
    }

    private IEnumerator Attack()
    {
        while (isAttacking)
        {
            if (isSecondPhase)
            {
                anim.SetTrigger(attacks[rand.Next(1,3)]);
            }
            else
            {
                anim.SetTrigger(attacks[rand.Next(0,2)]);

            }
            yield return  new WaitForSeconds(attackRate);
        }

        isAttackStarted = false;
        yield break;
    }

    public void Attack1()
    {
        targetsInRight = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,targetLayer);
       
       

        if (targetsInRight.Contains(targetCollider))
        {
            // Debug.Log("target in range");
            player.TakeDamage(attack1Damage);
            
        }
        
    }
    public void Attack2()
    {
        targetsInRight = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,targetLayer);
       
       

        if (targetsInRight.Contains(targetCollider))
        {
            // Debug.Log("target in range");
            player.TakeDamage(attack2Damage);
            
        }
    }
    public void Attack4()
    {
        targetsInRight = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,targetLayer);
       
       

        if (targetsInRight.Contains(targetCollider))
        {
            // Debug.Log("target in range");
            player.TakeDamage(attack4Damage);
            
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);

    }
    
}
