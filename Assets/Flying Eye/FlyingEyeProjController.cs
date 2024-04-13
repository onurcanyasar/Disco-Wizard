using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlyingEyeProjController : MonoBehaviour
{
    private Transform player;
    private Animator anim;
    public float damage;
    public float speed;
    private Tween movement;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        speed = 15f;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("AfroWizard").transform;
        anim = GetComponentInChildren<Animator>();
        
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.velocity = transform.right.normalized * speed;
        //movement = transform.DOMove(player.position, speed).SetEase(Ease.Linear).SetSpeedBased().SetAutoKill(false);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthAndMana>().TakeDamage(damage);
            movement.Kill();
            anim.SetTrigger("End");
        }
        else if (other.tag.Equals("Sand") || other.tag.Equals("Fire") || other.tag.Equals("Oil") || other.tag.Equals("Water") ||
                 other.tag.Equals("Smoke") || other.tag.Equals("Bedrock"))
        {
            movement.Kill();
            anim.SetTrigger("End");
        }
    }
}
