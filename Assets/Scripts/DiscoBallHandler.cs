using System;
using System.Collections;
using System.Collections.Generic;
using Cell_Folder;
using UnityEngine;

public class DiscoBallHandler : MonoBehaviour, IKillable
{
    private SpriteRenderer sr;
    private float width;
    private float height;

    public Transform rightTrans;
    public Transform leftTrans;
    public Transform downLeftTrans;
    
    private String status;
    private String defaultState;

    private bool isOrdered;

    public LayerMask playerMask;

    public float speed;
    private Collider2D coll;
    private bool isDestroying;
    public TerrainGenerator TerrainGenerator;
    
    void Start()
    {
        //TerrainGenerator = GameObject.Find("TerrainGenerator").GetComponent<TerrainGenerator>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        var bounds = coll.bounds;
        width = bounds.size.x / 2; 
        height = bounds.size.y / 2;
        status = "left";
        defaultState = "left";
        isOrdered = false;
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        
        if (rayUpwards())
        {
            status = "down";
            
            
        }
        else if (rayDownwards())
        {
            status = "left";
            
        }

        if (rayRight())
        {
            if (status == "right")
            {
                status = "left";
            }
            else
            {
                status = "right";
            }
            
        }
        else if (rayLeft())
        {
            if (status == "left")
            {
                status = "right";
            }
            else
            {
                status = "left";
            }
            
        }
    
        if (status != defaultState && !isOrdered)
        {
            Invoke(nameof(checkDefault), 5f);
            isOrdered = true;

        }
        if (Input.GetKey(KeyCode.R))
        {
            destroyMode();
        }
        else
        {
            transform.rotation = Quaternion.identity;
            isDestroying = false;
            handleStatus(); 
        }
        
        

       
    }

    private void destroyMode()
    {
        isDestroying = true;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.Rotate(new Vector3(0, 0 ,1), 30);
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,pos, step);
    }
    void handleStatus()
    {
        
        float step = speed * Time.deltaTime;
        switch (status)
        {
            case "down":
                if (Vector2.Distance(transform.position, downLeftTrans.position) > 0.001f)
                {
                    transform.position = Vector2.MoveTowards(transform.position,downLeftTrans.position, step);
                }
                break;
            case "left":
                if (Vector2.Distance(transform.position, leftTrans.position) > 0.001f)
                {
                    transform.position = Vector2.MoveTowards(transform.position,leftTrans.position, step);
                }
                break;
            case "right":
                if (Vector2.Distance(transform.position, rightTrans.position) > 0.001f)
                {
                    transform.position = Vector2.MoveTowards(transform.position,rightTrans.position, step);
                }
                break;
        }
    }

    void checkDefault()
    {
        if (!rayUpwards() && !rayDownwards() && !rayRight() && !rayLeft())
        {
            status = "left";
        }
        isOrdered = false;
    }

    bool rayUpwards()
    {
        float rayDistance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center + new Vector3(0,height,0 ), Vector2.up, rayDistance, ~playerMask);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    
    bool rayDownwards()
    {
        float rayDistance = -0.1f;
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center + new Vector3(0,-height,0 ), Vector2.up, rayDistance, ~playerMask);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    
    bool rayRight()
    {
        float rayDistance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center + new Vector3(width,0,0 ), Vector2.right, rayDistance, ~playerMask);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    
    bool rayLeft()
    {
        float rayDistance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center + new Vector3(-width,0,0 ), Vector2.left, rayDistance, ~playerMask);
        
        if (hit.collider != null)
        {
            return true;
        }
        
        return false;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroying)
        {
            CellLocationHandler cLoc = other.gameObject.GetComponent<CellLocationHandler>();
            if(cLoc == null) return;
            
            
            int[] coords = {cLoc.x, cLoc.y};
            Cell cell = TerrainGenerator.grid.gridArray[coords[0], coords[1]];
            if (cell != null)
            {
                if (cell.code == 1)
                {
                    cell.Destroy();
                }
            }
            
        }
        
        
        
        
    }
}
