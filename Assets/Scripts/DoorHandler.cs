using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour
{
    public String nextLevelName;
    public Transform playerTrans;
    public Transform camTrans;
    public Transform discoBallTrans;
    public SpriteRenderer blackScreen;
    void Start()
    {
        playerTrans = GameObject.Find("AfroWizard").GetComponent<Transform>();
        camTrans = GameObject.Find("Player Cam").GetComponent<Transform>();
        discoBallTrans = GameObject.Find("DiscoBall").GetComponent<Transform>();
        blackScreen = GameObject.Find("BlackScreen").GetComponent<SpriteRenderer>();
        
        /*
        SceneManager.LoadScene(nextLevelName);
        playerTrans.position = new Vector3(9, 26, 10);
        camTrans.position = new Vector3(3, 30, -10);
        discoBallTrans.position = new Vector3(9, 26, 10);
        */

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            blackScreen.enabled = true;
            SceneManager.LoadScene(nextLevelName);
            playerTrans.position = new Vector3(9, 26, 10);
            camTrans.position = new Vector3(50, 30, -10);
            discoBallTrans.position = new Vector3(9, 26, 10);
        }
            
        
    }
}
