using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cell_Folder;
using UnityEditor;
using UnityEngine;
using Grid = Cell_Folder.Grid;

public class GameObjectGridHandler : MonoBehaviour
{
    private Vector3[] prevPoints;
    private Vector3[] currPoints;
    private Rigidbody2D rb;
    private Vector2 pos;
    private Collider2D coll;
    public LayerMask groundLayer;
    private TerrainGenerator TerrainGenerator;

    
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        TerrainGenerator = GameObject.Find("TerrainGenerator").GetComponent<TerrainGenerator>();
        coll = GetComponent<BoxCollider2D>();
        prevPoints = getPointsOfObject(gameObject);
        currPoints = getPointsOfObject(gameObject);
        updateCells(currPoints);
    }

    // Update is called once per frame
    void Update()
    {
        currPoints = getPointsOfObject(gameObject);
        if (Vector2.Distance(vecToVecInt(prevPoints[0]), vecToVecInt(currPoints[0])) >= 0.001)
        {
            deleteCells(prevPoints);
            updateCells(currPoints);
            prevPoints = currPoints;
        }

        
    }

    private Vector3 vecToVecInt(Vector3 vec)
    {
        return new Vector3((int) vec.x, (int) vec.y, (int) vec.z);
    }
    
    private Vector3[] getPointsOfObject(GameObject obj)
    {
        //coll = GetComponent<Collider2D>();
        float width = coll.bounds.size.x / 2;
        float height = coll.bounds.size.y / 2;

        var bounds = coll.bounds;
        Vector3 startPoint = bounds.center - new Vector3(width, height, 0);
        Vector3 endPoint = bounds.center + new Vector3(width, height, 0);
        
        
        startPoint.x = Mathf.RoundToInt(startPoint.x);
        startPoint.y = Mathf.RoundToInt(startPoint.y);
        endPoint.x = Mathf.RoundToInt(endPoint.x);
        endPoint.y = Mathf.RoundToInt(endPoint.y);
        pos = new Vector2(startPoint.x, startPoint.y);
        return new[] {startPoint, endPoint};
    }

    private Vector3[] getColliderBottom()
    {
        var bounds = coll.bounds;
        float width = bounds.size.x / 2;
        float height = bounds.size.y / 2;
        Vector3 bottomLeft = bounds.center - new Vector3(width, height);
        Vector3 bottomCenter = bounds.center - new Vector3(0, height);
        Vector3 bottomRight = bounds.center - new Vector3(-width, height);
        return new[] {bottomLeft, bottomCenter, bottomRight};
    }
    private void updateCells(Vector3[] locs)
    {
        Vector3 startPoint = locs[0];
        Vector3 endPoint = locs[1];
        
        for (int x = (int) startPoint.x; x < endPoint.x; x++)
        {
            for (int y = (int) startPoint.y; y < endPoint.y; y++)
            {
                if (x < 0 || x >= TerrainGenerator.grid.width || y < 0 || y > TerrainGenerator.grid.height)
                {
                    return;
                }
                if (TerrainGenerator.grid.gridArray[x, y] == null)
                {
                    Cell cell = new Cell(x, y, TerrainGenerator.grid, 6);
                }

            }
        }
        

    }

    private void deleteCells(Vector3[] locs)
    {
        Vector3 startPoint = locs[0];
        Vector3 endPoint = locs[1];
        
        for (int x = (int) startPoint.x; x < endPoint.x; x++)
        {
            for (int y = (int) startPoint.y; y < endPoint.y; y++)
            {
                if (x < 0 || x >= TerrainGenerator.grid.width || y < 0 || y > TerrainGenerator.grid.height)
                {
                    return;
                }
                if (TerrainGenerator.grid.gridArray[x, y] != null)
                {
                    if (TerrainGenerator.grid.gridArray[x, y].code == 6)
                    {
                        TerrainGenerator.grid.gridArray[x, y].DestroyCell();
                    }
                    

                }
                
            }
        }
    }

    private bool checkRight(Vector3[] locs)
    {
        Vector3 startPoint = locs[0];
        Vector3 endPoint = locs[1];
        int x = (int) endPoint.x;
        int startPointY = (int) startPoint.y;
        int endPointY = (int) endPoint.y;
        if (x + 1 >= TerrainGenerator.grid.width)
        {
            return false;
        }
        
        for (int y = startPointY; y < endPointY; y++)
        {
            if (TerrainGenerator.grid.gridArray[x + 1, y] != null)
            {
                Debug.Log(TerrainGenerator.grid.gridArray[x + 1, y].code);
                return false;
            }    
        }

        return true;
    }
    public bool checkLeft(Vector3[] locs)
    {
        Vector3 startPoint = locs[0];
        Vector3 endPoint = locs[1];
        int x = (int) startPoint.x;
        int startPointY = (int) startPoint.y;
        int endPointY = (int) endPoint.y;
        if (x - 1 >= TerrainGenerator.grid.width)
        {
            return false;
        }
        
        for (int y = startPointY; y < endPointY; y++)
        {
            if (TerrainGenerator.grid.gridArray[x - 1, y] != null)
            {
                Debug.Log(TerrainGenerator.grid.gridArray[x - 1, y].code);
                return false;
            }    
        }

        return true;
    }
    public bool checkDown()
    {
        Vector3 startPoint = currPoints[0];
        Vector3 endPoint = currPoints[1];
        
        int startPointX = (int) startPoint.x;

        int y = (int) startPoint.y;
        int endPointX = (int) endPoint.x;
        if (y - 1 < 0)
        {
            return false;
        }

        for (int x = startPointX; x < endPointX; x++)
        {
            if (TerrainGenerator.grid.gridArray[x, y - 1] != null)
            {

                return false;

            } 
        }

        return true;
    }

    public bool rayDown()
    {
        Vector3[] points = getColliderBottom();
        RaycastHit2D hitLeft = Physics2D.Raycast(points[0], Vector2.down, 0.5f, groundLayer);
        RaycastHit2D hitCenter = Physics2D.Raycast(points[1], Vector2.down, 0.5f, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(points[2], Vector2.down, 0.5f, groundLayer);
        Debug.DrawRay(points[0], Vector3.down);
        return hitLeft.collider != null || hitCenter.collider != null || hitRight.collider != null;
    }
    
}
