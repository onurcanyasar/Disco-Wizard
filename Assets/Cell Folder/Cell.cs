using System.Security.Cryptography;
using UnityEngine;

namespace Cell_Folder
{
    public class Cell
    {

        public int x;
        public int y;
        public GameObject gameObject;
        public float speed;
        public Grid grid;
        public int code;
        public Rigidbody2D rb;
        private Transform tr;
        public Collider2D col;
        public Transform trans;
        public bool isAlive;
        public CellLocationHandler loc;
        public bool isMoving;
        public bool zaWarudo;
        public float realSpeed;
        public Cell(int x, int y, GameObject gameObject, float speed, Grid grid, int code)
        {
            zaWarudo = false;
            this.x = x;
            this.y = y;
            isAlive = true;
            
            this.speed = speed;
            this.grid = grid;
            this.code = code;
            grid.gridArray[x, y] = this;
            if (gameObject != null)
            {
                this.gameObject = Object.Instantiate(gameObject, new Vector3(x, y), Quaternion.identity);
                rb = this.gameObject.GetComponent<Rigidbody2D>();
                col = this.gameObject.GetComponent<Collider2D>();
                trans = this.gameObject.GetComponent<Transform>();
                loc = this.gameObject.GetComponent<CellLocationHandler>();
            }

            isMoving = true;

        }

        public Cell(int x, int y, Grid grid, int code)
        {
            zaWarudo = false;
            this.x = x;
            this.y = y;
            this.code = code;
            this.grid = grid;
            grid.gridArray[x, y] = this;
            isAlive = true;

        }

        public Cell(int x, int y, GameObject gameObject, Grid grid, int code)
        {
            zaWarudo = false;
            this.x = x;
            this.y = y;
            this.grid = grid;
            this.code = code;
            grid.gridArray[x, y] = this;
            isAlive = true;

            if (gameObject != null)
            {
                this.gameObject = Object.Instantiate(gameObject, new Vector3(x, y), Quaternion.identity);
                rb = this.gameObject.GetComponent<Rigidbody2D>();
                col = this.gameObject.GetComponent<Collider2D>();
                trans = this.gameObject.GetComponent<Transform>();
                loc = this.gameObject.GetComponent<CellLocationHandler>();
            }
            isMoving = true;
        }
    
        public void MoveToTarget()
        {
            float step = speed * Time.fixedDeltaTime;
            rb.position = Vector3.MoveTowards(rb.position, new Vector3(x, y), step);
            //trans.localPosition = Vector3.MoveTowards(trans.position, new Vector3(x, y), step);
            //Vector3 vec = Vector3.MoveTowards(rb.position, new Vector3(x, y), step);
            //rb.MovePosition(vec);

        }
        
        public void UpdateColliders()
        {
            /*
            
            if (y - 1 > 0 && x - 1 > 0 && y + 1 < grid.height && x + 1 < grid.width)
            {
                if (grid.gridArray[x+1, y+1] != null && grid.gridArray[x+1, y-1] != null && 
                    grid.gridArray[x, y-1] != null && grid.gridArray[x, y+1] != null && 
                    grid.gridArray[x+1, y] != null && grid.gridArray[x-1,y] != null && 
                    grid.gridArray[x-1, y-1] != null && grid.gridArray[x-1,y+1] != null)
                {
                   

                    col.enabled = false;



                }
                else
                {
                    

                    col.enabled = true;

                }
            }
            
            */
        }

        public void DestroyCell()
        {
            
            grid.Update(x, y ,null);
            
        }
        
        public void Destroy()
        {
            grid.Update(x, y ,null);
            isAlive = false;
            Object.Destroy(gameObject);
        }

        public void Update()
        {
            grid.Update(x, y, this);
            isMoving = true;
            loc.x = x;
            loc.y = y;
        }
        
        

        
    }
}
