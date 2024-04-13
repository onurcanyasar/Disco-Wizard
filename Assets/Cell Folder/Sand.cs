using UnityEngine;

namespace Cell_Folder
{
    public class Sand : Cell
    {
        
        public Sand(int x, int y, GameObject gameObject, float speed, Grid grid, int code) : base(x, y, gameObject, speed, grid, code)
        {
            SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
            int random = Random.Range(235, 255);
            sr.color = new Color(random/255.0f, 166/255.0f, 1/255.0f);

        }

        

        
        public void Move()
        {
            
            UpdateColliders();
            
            if (y > 0 && x > 0 && x < grid.width - 1)
            {
                
                //float distance = Vector2.Distance(rb.position, new Vector2(x, y));
                Vector3 offset = new Vector3(x, y) - trans.position;
                float distance = offset.sqrMagnitude;
                if (distance <= 0.001)
                {
                    
                    isMoving = false;
                    
                    if (grid.gridArray[x, y -1] == null)
                    {
                        grid.Update(x, y, null);
                        y -= 1;
                        Update();
                        
                    }
                    else if (grid.gridArray[x, y - 1].code == 2)
                    {
                        Water water = (Water)grid.gridArray[x, y - 1];
                        water.y += 1;
                        water.Update();
                        y -= 1;
                        Update();


                    }
                    else if (grid.gridArray[x, y - 1].code == 4)
                    {
                        
                        Fire fire = (Fire)grid.gridArray[x, y - 1];
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
                        }
                        
                    }
                }
                else
                {
                    isMoving = true;
                }
            }

            if (isMoving)
            {
                MoveToTarget();    
            }
            
        
        }
        

    }
}
