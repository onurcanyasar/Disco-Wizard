using System.Security.Cryptography;
using UnityEngine;

namespace Cell_Folder
{
    public class Water : Cell
    {
        private int bias;
        private int health;
        public Water(int x, int y, GameObject gameObject, float speed, Grid grid, int code) : base(x, y, gameObject, speed, grid, code)
        {
            bias = Random.Range(0, 2);
            this.speed = speed / 2f;
            SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
            int random = Random.Range(100, 127);
            sr.color = new Color(78/255.0f, random/255.0f, 255/255.0f, 0.6f);
            health = 3;

        }

        
        
        public void Move()
        {
            UpdateColliders();
            if (health <= 0)
            {
                Destroy();
            }
            else if (y > 0 && x > 0 && x < grid.width - 1)
            {
                checkUp();
                //float distance = Vector2.Distance(rb.position, new Vector2(x, y));
                Vector3 offset = new Vector3(x, y) - trans.position;
                float distance = offset.sqrMagnitude;
                if (distance <= 0.001)
                {
                    isMoving = false;
                    
                    if (grid.gridArray[x, y - 1] == null)
                    {
                        grid.Update(x, y, null);
                        y -= 1;
                        Update();
                        
                    }
                    else if (grid.gridArray[x, y - 1].code == 4)
                    {
                        Fire fire = (Fire) grid.gridArray[x, y - 1];
                        fire.Die();
                        grid.Update(x, y, null);
                        y -= 1;
                        Update();
                    }

                    else 
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            if (grid.gridArray[x - 1, y - 1] == null)
                            {
                                grid.Update(x, y, null);
                                y -= 1;
                                x -= 1;
                                Update();
                                
                            }
                            else if (grid.gridArray[x + 1, y - 1] == null)
                            {
                                grid.Update(x, y, null);
                                y -= 1;
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
                            
                            if (grid.gridArray[x + 1, y - 1] == this)
                            {
                                grid.Update(x, y, null);
                                y -= 1;
                                x += 1;
                                Update();
                                
                            }
                            else if (grid.gridArray[x - 1, y - 1] == this)
                            {
                                grid.Update(x, y, null);
                                y -= 1;
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
        private bool checkRight()
        {
            if (grid.gridArray[x + 1, y] == null)
            {
                grid.Update(x, y, null); 
                x += 1;
                Update();
                return true;
            }

            if (grid.gridArray[x + 1, y].code == 4)
            {
                Fire fire = (Fire) grid.gridArray[x + 1, y];
                fire.Die();
                grid.Update(x, y, null);
                x += 1;
                Update();
                health -= 1;
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
            if (grid.gridArray[x - 1, y].code == 4)
            {
                Fire fire = (Fire) grid.gridArray[x - 1, y];
                fire.Die();
                grid.Update(x, y, null);
                x -= 1;
                Update();
                health -= 1;
                return true;
                
            }

            return false;
        }

        private void checkUp()
        {
            if(grid.gridArray[x, y + 1] == null) return;
            if (grid.gridArray[x, y + 1].code == 4)
            {
                Fire fire = (Fire)grid.gridArray[x, y + 1];
                fire.Die();
                health -= 1;
            }
        }

     
        
        

    }
}
