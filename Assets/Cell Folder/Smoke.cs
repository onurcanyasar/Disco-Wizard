using UnityEngine;

namespace Cell_Folder
{
    public class Smoke : Cell
    {
        
        
        private int bias;
        public Smoke(int x, int y, GameObject gameObject, float speed, Grid grid, int code) : base(x, y, gameObject, speed, grid, code)
        {
            isMoving = true;
            bias = Random.Range(0, 2);
        }
       
        public void Move()
        {
            UpdateColliders();
        
            if (y > 0 && x > 0 && x < grid.width -1  && y < grid.height - 1)
            {
                //float distance = Vector2.Distance(rb.position, new Vector2(x, y));
                Vector3 offset = new Vector3(x, y) - trans.position;
                float distance = offset.sqrMagnitude;
                if (distance <= 0.001)
                {
                    
                    isMoving = false;
                    
                    if (grid.gridArray[x, y + 1] == null)
                    {
                        grid.Update(x, y, null);
                        y += 1;
                        Update();
                    }
                    
                    else 
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            if (grid.gridArray[x - 1, y + 1] == null)
                            {
                                grid.Update(x, y, null);
                                y += 1;
                                x -= 1;
                                Update();
                                
                            }
                            else if (grid.gridArray[x + 1, y + 1] == null)
                            {
                                grid.Update(x, y, null);
                                y += 1;
                                x += 1;
                                Update();
                            }
                            else
                            {
                                checkSides();
                            }
                        }
                        else if (rand == 2)
                        {
                            
                            if (grid.gridArray[x + 1, y + 1] == this)
                            {
                                grid.Update(x, y, null);
                                y += 1;
                                x += 1;
                                Update();
                                
                            }
                            else if (grid.gridArray[x - 1, y + 1] == this)
                            {
                                grid.Update(x, y, null);
                                y += 1;
                                x -= 1;
                                Update();
                            }
                            else
                            {
                                checkSides();
                            }
                        }
                        
                    }
                }
            }

            if (isMoving)
            {
                    MoveToTarget();    
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
        public void checkSides()
        {   
            
            if (bias == 0)
            {
                if (!checkLeft())
                {
                    checkRight();
                }
            }
            else if (bias == 1)
            {
                if (!checkRight())
                {
                    checkLeft();
                }
            }
        }

        
        
    }
}
