using System.Collections;
using System.Collections.Generic;
using Cell_Folder;
using UnityEngine;
using Grid = Cell_Folder.Grid;

public class Oil : Cell
{
    
    private int bias;
    private GameObject fireObject;
    private List<Fire> fires;
    private GameObject burnParticle;
    
    public Oil(int x, int y, GameObject gameObject, float speed, Grid grid, int code,List<Fire> fires, GameObject fireObject, GameObject burnParticle) : base(x, y, gameObject, speed, grid, code)
    {
        bias = Random.Range(0, 2);
        this.speed = speed / 2f;
        realSpeed = this.speed;
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        //int random = Random.Range(100, 127);
        //sr.color = new Color(78/255.0f, random/255.0f, 255/255.0f);
        this.fires = fires;
        this.fireObject = fireObject;
        this.burnParticle = burnParticle;
        zaWarudo = false;

    }
  

    public void Move()
    {
        UpdateColliders();

        if (y > 0 && x > 0 && x < grid.width - 1 && y < grid.height - 1)
        {
            Vector3 offset = new Vector3(x, y) - trans.position;
            float distance = offset.sqrMagnitude;
            if (distance <= 0.001)
            {
                isMoving = false;
                //checkFires();
                checkMoves();
                
            }
        }

        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void checkMoves()
    {
        if (!checkDown())
        {
            if (bias == 0)
            {
                if(checkRight()) return;
                checkLeft();
            }
            else
            {
                if(checkLeft()) return;
                checkRight();
            }
        }
    }
    private bool checkRight()
    {
        if (grid.gridArray[x + 1, y] == null)
        {
            grid.Update(x, y, null); 
            x += 1;
            Update();
            return true;
        }

        

        return false;
    }

    private bool checkLeft()
    {
        if (grid.gridArray[x - 1, y] == null)
        {
            grid.Update(x, y, null);
            x -= 1;
            Update();
            return true;
        }
        

        return false;
    }

    private bool checkDown()
    {
        if (grid.gridArray[x, y - 1] == null)
        {
            grid.Update(x, y, null);
            y -= 1;
            Update();
            return false;
        }
       

        return false;
    }

    private void checkFires()
    {
        if (checkDownFire()) return;
        
        if (checkLeftFire()) return;

        if (checkRightFire()) return;

        checkUpFire();
    }
    
    private bool checkUpFire()
    {
        Debug.Log(y+1);
        Cell cell = grid.gridArray[x, y + 1];
        if (cell == null) return false; 
        if (cell.code == 4)
        {
            instantiateFire();
            return true;
        }

        return false;

    }
    private bool checkDownFire()
    {
        Cell cell = grid.gridArray[x, y - 1];
        if (cell == null) return false; 
        if (cell.code == 4)
        {
            instantiateFire();
            return true;
        }

        return false;

    }
    private bool checkRightFire()
    {
        Cell cell = grid.gridArray[x + 1, y];
        if (cell == null) return false; 
        if (cell.code == 4)
        {
            instantiateFire();
            return true;
        }

        return false;

    }
    private bool checkLeftFire()
    {
        
        Cell cell = grid.gridArray[x - 1, y];
        if (cell == null) return false; 
        if (cell.code == 4)
        {
            instantiateFire();
            return true;
        }

        return false;

    }

    public void instantiateFire()
    {
        this.Destroy();
        
        Fire fire = new Fire(x, y, fireObject, realSpeed, grid, 4, fires, fireObject, burnParticle, 0.0000000000001f);
        fires.Add(fire);
        fire.Stop();
        
        
    }
}
