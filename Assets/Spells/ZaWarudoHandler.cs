using System.Collections;
using System.Collections.Generic;
using Cell_Folder;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZaWarudoHandler : MonoBehaviour
{
    private CircleCollider2D coll;
    private bool isStopped;
    public TerrainGenerator TerrainGenerator;
    private Collider2D[] colliders;
    private List<float> speeds;
    public Transform playerTrans;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
        //TerrainGenerator = GameObject.Find("TerrainGenerator").GetComponent<TerrainGenerator>();
        speeds = new List<float>();
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
        transform.position = playerTrans.position;
        if (Input.GetKeyDown(KeyCode.F) && !isStopped)
        {
            isStopped = true;
            anim.SetTrigger("isStopped");
            colliders = Physics2D.OverlapCircleAll(transform.position, coll.radius);
            StartCoroutine(startTime());
            foreach (var c in colliders)
            {   if(c == null) continue;
                if (c.tag.Equals("Sand") || c.tag.Equals("Fire") || c.tag.Equals("Oil") || c.tag.Equals("Water") || c.tag.Equals("Smoke"))
                {
                    CellLocationHandler cLoc = c.gameObject.GetComponent<CellLocationHandler>();
                    
                    Cell cell = TerrainGenerator.grid.gridArray[Mathf.RoundToInt(cLoc.x),
                        Mathf.RoundToInt(cLoc.y)];
                    if (cell != null)
                    {
                        speeds.Add(cell.speed);
                        cell.speed /= 1000f;
                    }
                }
                else if (c.tag.Equals("Enemy"))
                {
                    c.gameObject.GetComponent<AIPath>().maxSpeed /= 1000f;
                }
                else if (c.tag.Equals("FlyingEyeProjectile"))
                {
                    c.gameObject.GetComponent<FlyingEyeProjController>().rb.velocity /= 1000f;
                }
            }
            
        }
        
        
    }

    IEnumerator startTime()
    {
        yield return new WaitForSeconds(3f);
        isStopped = false;
        /*
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == null)
            {
                Debug.Log("hi");
                continue;
            }
            if (colliders[i].tag.Equals("Sand") || colliders[i].tag.Equals("Fire") || colliders[i].tag.Equals("Oil") || colliders[i].tag.Equals("Water") || colliders[i].tag.Equals("Smoke"))
            {
                CellLocationHandler cLoc = colliders[i].gameObject.GetComponent<CellLocationHandler>();
                Cell cell = TerrainGenerator.grid.gridArray[Mathf.RoundToInt(cLoc.x),
                    Mathf.RoundToInt(cLoc.y)];
                if (cell != null)
                {
                    cell.speed = speeds[i];
                    
                }
            }
        }
        */
        foreach (var c in colliders)
        {   if(c == null) continue;
            if (c.tag.Equals("Sand") || c.tag.Equals("Fire") || c.tag.Equals("Oil") || c.tag.Equals("Water") || c.tag.Equals("Smoke"))
            {
                CellLocationHandler cLoc = c.gameObject.GetComponent<CellLocationHandler>();
                    
                Cell cell = TerrainGenerator.grid.gridArray[Mathf.RoundToInt(cLoc.x),
                    Mathf.RoundToInt(cLoc.y)];
                if (cell != null)
                {
                    cell.speed *= 1000f;
                }
            }
            else if (c.tag.Equals("Enemy"))
            {
                c.gameObject.GetComponent<AIPath>().maxSpeed *= 1000f;
            }
            else if (c.tag.Equals("FlyingEyeProjectile"))
            {
                c.gameObject.GetComponent<FlyingEyeProjController>().rb.velocity *= 1000f;
                
            }
        }
    }
}
