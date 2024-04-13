using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float maxHP;

    public float currHP;

    public Image fill;

    public SpriteRenderer sprite;

    private Animator anim;

    private AIPath aı;

    private Rigidbody2D rb;

    private Sequence colorSeq;
    
    public bool isInvulnerable = false;
    public bool isSecondPhase = false;

    public string name;

    private EnemyCounter enemyCounter; //onur
    // Start is called before the first frame update
    void Awake()
    {
        enemyCounter = GameObject.Find("EnemyCounter").GetComponent<EnemyCounter>();//onur
        currHP = maxHP;
        anim = GetComponentInChildren<Animator>();
        aı = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Start()
    {
        enemyCounter.EnemyCount += 1;
    }

    public void TakeDamage(float damage)
    {
        if (!isInvulnerable)
        {
            currHP -= damage;
            if (name.Equals("M"))
            {
                if (currHP <= maxHP / 2 && !isSecondPhase)
                {
                    aı.maxSpeed *= 2;
                    anim.SetTrigger("SecondPhase");
                    anim.SetBool("isSecondPhase",true);
                    isSecondPhase = true;
                } 
            }
        
            colorSeq = DOTween.Sequence();
            colorSeq.Append(sprite.DOColor(Color.red, 0.04f).SetEase(Ease.Flash));
            colorSeq.Append(sprite.DOColor(Color.white, 0.03f).SetEase(Ease.Flash));
        
            fill.fillAmount = currHP / maxHP;
            if (currHP <= 0)
            {
                enemyCounter.EnemyCount -= 1;
                if (name.Equals("EW"))
                {
                    Debug.Log("EW dead");
                    aı.enabled = false;
                    rb.velocity = Vector2.down*3;
                    anim.SetBool("isAttacking",false);
                    anim.SetFloat("Speed",0);
                    anim.SetBool("isDead",true);
                }
                else if (name.Equals("FE"))
                {
                    Debug.Log("FE dead");
                    aı.enabled = false;
                    rb.velocity = Vector2.down*3;
                    anim.SetBool("isDead",true);

                }
            
                else if (name.Equals("M"))
                {
                    Debug.Log("M dead");
                    aı.enabled = false;
                    anim.SetFloat("Speed",0);
                    anim.SetBool("isDead", true);
                }
                
            }
        }
        // Debug.Log("enemy took "+ damage +" damage");
    }
}
