using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDestroyerHandler : MonoBehaviour
{
    private TerrainGenerator TerrainGenerator;
    private Collider2D coll;
    private Vector3[] points;
    void Awake()
    {
        TerrainGenerator = GameObject.Find("TerrainGenerator").GetComponent<TerrainGenerator>();
        coll = GetComponent<BoxCollider2D>();
        points = getPointsOfObject(gameObject);

    }

    private void OnEnable()
    {
        destroyCell(points);
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
        
        return new[] {startPoint, endPoint};
    }
    
    private void destroyCell(Vector3[] locs)
    {
        Vector3 startPoint = locs[0];
        Vector3 endPoint = locs[1];
        Cell_Folder.Grid grid = TerrainGenerator.grid; 
        for (int x = (int) startPoint.x; x < endPoint.x; x++)
        {
            for (int y = (int) startPoint.y; y < endPoint.y; y++)
            {
                if (x < 0 || x > grid.width || y < 0 || y > grid.height)
                {
                    continue;
                }
                if (TerrainGenerator.grid.gridArray[x, y] != null)
                {
                    TerrainGenerator.grid.gridArray[x, y].Destroy();
                }
                
            }
        }
    }
}
