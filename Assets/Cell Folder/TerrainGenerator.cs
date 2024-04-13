using System;
using System.Collections;
using System.Collections.Generic;
using Cell_Folder;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{

    public List<GameObject> objects;
    public GameObject sandObject;
    public GameObject waterObject;
    public GameObject fireObject;
    public GameObject woodObject;
    public GameObject smokeObject;
    public GameObject bedrockObject;
    public GameObject burnParticle;
    public GameObject oilObject;
    private List<Cell_Folder.Sand> sands;
    private List<Cell_Folder.Water> waters;
    public List<Wood> woods;
    public List<Fire> fires;
    public List<Smoke> smokes;
    public List<Bedrock> bedrocks;
    public List<Oil> oils;
    public  Cell_Folder.Grid grid;

    private ZaWarudoHandler zaWarudoHandler;
    private DiscoBallHandler discoBallHandler;
    public int gridWidth;
    public int gridHeight;

    private void Awake()
    {
        
        grid = new Cell_Folder.Grid(gridWidth, gridHeight);
        
    }

    void Start()
    {
        zaWarudoHandler = GameObject.Find("ZaWarudoCollider").GetComponent<ZaWarudoHandler>();
        discoBallHandler = GameObject.Find("DiscoBall").GetComponent<DiscoBallHandler>();
        zaWarudoHandler.TerrainGenerator = this;
        discoBallHandler.TerrainGenerator = this;
        
        bedrocks = new List<Bedrock>();
        sands = new List<Cell_Folder.Sand>();
        waters = new List<Cell_Folder.Water>();
        woods = new List<Wood>();
        fires = new List<Fire>();
        smokes = new List<Smoke>();
        oils = new List<Oil>();
        
        foreach (var obj in objects)
        {
            Vector3[] currPoints = getPointsOfObject(obj);
            createCells(currPoints, obj);
            Destroy(obj);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(grid.gridArray[(int) pos.x, (int) pos.y].code);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                   // grid.gridArray[Mathf.RoundToInt(pos.x + i), Mathf.RoundToInt(pos.y + j)].Destroy();
                }
            }
            
        }

       
        
        
        
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < waters.Count; i++)
        {
            if (!waters[i].isAlive)
            {
                waters.RemoveAt(i);
            }
            else
            {
                waters[i].Move();
            }
        }
        for (int i = 0; i < sands.Count; i++)
        {
            if (!sands[i].isAlive)
            {
                sands.RemoveAt(i);
            }
            else
            {
                sands[i].Move();
            }
        }

        for (int i = 0; i < woods.Count; i++)
        {
            if (!woods[i].isAlive)
            {
                woods.RemoveAt(i);
            }
            else
            {
                woods[i].Move();
            }
        }
        
        for (int i = 0; i < fires.Count; i++)
        {
            if (!fires[i].isAlive)
            {
                fires.RemoveAt(i);
            }
            else
            {
                fires[i].Move();
            }
        }

        for (int i = 0; i < smokes.Count; i++)
        {
            if (!smokes[i].isAlive)
            {
                smokes.RemoveAt(i);
            }
            else
            {
                smokes[i].Move();
            }
            
        }
        
        for (int i = 0; i < oils.Count; i++)
        {
            if (!oils[i].isAlive)
            {
                oils.RemoveAt(i);
            }
            else
            {
                oils[i].Move();
            }
            
        }
    }

    private Vector3[] getPointsOfObject(GameObject obj)
    {
        Collider2D coll = obj.GetComponent<Collider2D>();
        float width = coll.bounds.size.x / 2;
        float height = coll.bounds.size.y / 2;
        
        Vector3 startPoint = obj.transform.position - new Vector3(width, height, 0);
        Vector3 endPoint = obj.transform.position + new Vector3(width, height, 0);
        
        startPoint.x = Mathf.RoundToInt(startPoint.x);
        startPoint.y = Mathf.RoundToInt(startPoint.y);
        endPoint.x = Mathf.RoundToInt(endPoint.x);
        endPoint.y = Mathf.RoundToInt(endPoint.y);

        return new[] {startPoint, endPoint};
    }

    private void createCells(Vector3[] locs, GameObject gameObject)
    {
        Vector3 startPoint = locs[0];
        Vector3 endPoint = locs[1];
        String tag = gameObject.tag;
        for (int x = (int) startPoint.x; x < endPoint.x; x++)
        {
            for (int y = (int) startPoint.y; y < endPoint.y; y++)
            {
                float speed = Random.Range(30, 200);
                switch (tag)
                {
                    case "Sand":
                        
                        Cell_Folder.Sand sand = new Cell_Folder.Sand(x, y, sandObject, speed, grid, 1);
                        sands.Add(sand);
                        break;
                    case "Water":
                        
                        Cell_Folder.Water water = new Cell_Folder.Water(x, y, waterObject, speed, grid, 2);
                        waters.Add(water);
                        break;
                    case "Fire":
                        
                        Fire fire = new Fire(x, y, fireObject, speed, grid, 4, fires, fireObject, burnParticle);
                        fires.Add(fire);
                        break;
                    case "Wood":
                        Wood wood = new Wood(x, y, woodObject,  grid, 3, smokes, smokeObject);
                        woods.Add(wood);
                        break;
                    case "Smoke":
                        Smoke smoke = new Smoke(x, y, smokeObject, speed, grid, 5);
                        smokes.Add(smoke);
                        break;
                    case "Bedrock":
                        Bedrock bedrock = new Bedrock(x, y, bedrockObject, grid, 7);
                        bedrocks.Add(bedrock);
                        break;
                    case "Oil":
                        Oil oil = new Oil(x, y, oilObject, speed, grid, 8, fires, fireObject, burnParticle);
                        oils.Add(oil);
                        break;

                }

            }
        }
        

    }
    
    
}
