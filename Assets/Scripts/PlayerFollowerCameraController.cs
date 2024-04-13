using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowerCameraController : MonoBehaviour
{
    public Transform playerTrans;
    public float yOffset;
    public float xOffset;
    public float stepSize;
    private bool isFollowingY;
    private bool isFollowingX;

    private bool goX;
    private Camera cam;
    private float width;
    private float height;
    private bool isGoingRight;
    private bool isGoingDown;
    public LayerMask stayAwayLayer;
    private bool goY;
    private void Start()
    {
         
        Camera cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
        var transform1 = transform;
        var position = transform1.position;
        var position1 = playerTrans.position;
        isGoingRight = position.x - position1.x < 0;
        isGoingDown = position.y - position1.y > 0;

        if (!isGoingRight && rayLeft())
        {
            goX = false;
        }
        else if (isGoingRight && rayRight())
        {
            goX = false;
        }
        else
        {
            goX = true;
        }

        if (isGoingDown && rayDown())
        {
            goY = false;
        }
        else if (!isGoingDown && rayUp())
        {
            goY = false;
        }
        else
        {
            goY = true;
        }

        if ((checkX(xOffset) || isFollowingX) && goX)
        {
            
            isFollowingX = true;
            position = Vector3.MoveTowards(position, new Vector3(playerTrans.position.x, position.y, position.z), stepSize * Time.deltaTime);
        }

        if ((checkY(yOffset) || isFollowingY) && goY)
        {
            isFollowingY = true;
            position = Vector3.MoveTowards(position, new Vector3(position.x, playerTrans.position.y, position.z), stepSize * Time.deltaTime);
        }
        
        handleFlags();
        transform1.position = position;
    }

    bool rayLeft()
    {
        float distance = 0.1f;
        var position = transform.position;
        Vector3 left = new Vector3(position.x - width, position.y, position.z);
        RaycastHit2D hit = Physics2D.Raycast(left, Vector2.left, distance, stayAwayLayer);
        
        return hit.collider != null;
    }
    
    bool rayRight()
    {
        float distance = 0.1f;
        var position = transform.position;
        Vector3 left = new Vector3(position.x + width, position.y, position.z);
        RaycastHit2D hit = Physics2D.Raycast(left, Vector2.right, distance, stayAwayLayer);
        
        return hit.collider != null;
    }
    bool rayDown()
    {
        float distance = 0.1f;
        var position = transform.position;
        Vector3 down = new Vector3(position.x, position.y - height, position.z);
        RaycastHit2D hit = Physics2D.Raycast(down, Vector2.down, distance, stayAwayLayer);
        
        return hit.collider != null;
    }
    bool rayUp()
    {
        float distance = 0.1f;
        var position = transform.position;
        Vector3 left = new Vector3(position.x, position.y + height, position.z);
        RaycastHit2D hit = Physics2D.Raycast(left, Vector2.up, distance, stayAwayLayer);
        
        return hit.collider != null;
    }
    bool checkX(float offset)
    {
        return Mathf.Abs(transform.position.x - playerTrans.position.x) > offset;
    }

    bool checkY(float offset)
    {
        return Mathf.Abs(transform.position.y - playerTrans.position.y) > offset;
    }

    void handleFlags()
    {
        if (isFollowingX && Mathf.Abs(transform.position.x - playerTrans.position.x) < 0.0001f)
        {
            isFollowingX = false;
        }
        if (isFollowingY && Mathf.Abs(transform.position.y - playerTrans.position.y) < 0.0001f)
        {
            isFollowingY = false;
        }
    }

    bool checkStayDistanceX(Transform trans1, Transform trans2)
    {
        
        return Mathf.Abs(trans1.position.x - trans2.position.x) < width;
    }
    
    bool checkStayDistanceY(Transform trans1, Transform trans2)
    {
        
        return Mathf.Abs(trans1.position.x - trans2.position.x) < height;
    }
}
