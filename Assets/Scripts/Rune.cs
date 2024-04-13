using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public Vector3 hover;

    public RuneSlot runeSlot;

    // Start is called before the first frame update
    void Awake()
    {
        hover = new Vector3(0,0.05f,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += hover * Mathf.Cos(Time.time);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            runeSlot.isCollected = true;
            Destroy(gameObject);
        }
    }
}
