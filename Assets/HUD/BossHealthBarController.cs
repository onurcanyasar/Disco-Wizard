using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour
{
    private String bossName;
    private Image bossHealthBarFill;
    public TextMeshProUGUI text;
    public Plane[] cameraView;
    private Collider2D bossCollider;
    private GameObject boss;
    public GameObject bossHealthBar;
    private Camera camera1;
    private bool isBossLevel = false;
    public Image fill;

    // Start is called before the first frame update
    void Awake()
    {
        camera1 = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Scene currScene = SceneManager.GetActiveScene();


        if (currScene.name.Equals("Level 3") && !isBossLevel)
        {
            
            boss = GameObject.Find("Minotaur");
            bossCollider = boss.GetComponent<Collider2D>();
            bossName = boss.name; 
            boss.GetComponent<EnemyHealth>().fill = fill;
            isBossLevel = true;
            Debug.Log("Boss Level. fill name: " + boss.GetComponent<EnemyHealth>().fill.gameObject.name);
        }

        if (isBossLevel)
        {
            cameraView = GeometryUtility.CalculateFrustumPlanes(camera1);
            if (GeometryUtility.TestPlanesAABB(cameraView, bossCollider.bounds))
            {
                Debug.Log(bossName+ " is visible");
                bossHealthBar.SetActive(true);
                text.text = bossName;
                return; 
            }
            else 
            {
                Debug.Log(bossName+" is NOT visible");
                bossHealthBar.SetActive(false); 
            }
        }
        
        
        // if (GeometryUtility.TestPlanesAABB(cameraView, bossCollider.bounds))
        // {
        //     Debug.Log("boss is visible");
        //     bossHealthBar.SetActive(true);
        //     text.text = bossName;
        // }
        // else
        // {
        //     Debug.Log("Boss is NOT visible");
        //     bossHealthBar.SetActive(false);
        // }
        
        // if (bossRenderer.isVisible)
        // {
        //     Debug.Log("boss is visible");
        //     gameObject.SetActive(true);
        //     text.text = bossName;
        // }
        // else
        // {
        //     Debug.Log("Boss is NOT visible");
        //     gameObject.SetActive(false);
        // }
    }
}
