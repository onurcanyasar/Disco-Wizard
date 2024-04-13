using System.Collections.Generic;
using UnityEngine;

namespace Cell_Folder
{
    public class Fire : Cell
    {
        private int bias;
        private List<Fire> fires;
        private GameObject fireObject;
        private float initTime;
        private bool isStopped;
        private float stopTime;
        private GameObject burnParticle;
        private bool zaWarudo;
        
        public Fire(int x, int y, GameObject gameObject, float speed, Grid grid, int code, List<Fire> fires, GameObject fireObject, GameObject burnParticle) : base(x, y, gameObject, speed, grid, code)
        {
            
            bias = Random.Range(0, 2);
            stopTime = Random.Range(0.5f, 1.0f);
            this.speed = speed / 2f;
            realSpeed = this.speed;
            this.fires = fires;
            this.fireObject = fireObject;
            this.burnParticle = burnParticle;
            GameObject obj =Object.Instantiate(burnParticle, trans.position, Quaternion.Euler(-90, 0, 0));
            obj.transform.parent = trans;
            zaWarudo = false;

        }
        public Fire(int x, int y, GameObject gameObject, float speed, Grid grid, int code, List<Fire> fires, GameObject fireObject, GameObject burnParticle, float stopTime) : base(x, y, gameObject, speed, grid, code)
        {
            bias = Random.Range(0, 2);
            this.stopTime = stopTime;
            this.speed = speed / 2f;
            realSpeed = this.speed;
            this.fires = fires;
            this.fireObject = fireObject;
            this.burnParticle = burnParticle;
            GameObject obj =Object.Instantiate(burnParticle, trans.position, Quaternion.Euler(-90, 0, 0));
            obj.transform.parent = trans;
            zaWarudo = false;

        }

        public void Die()
        {
            grid.Update(x ,y, null);
            isAlive = false;
            Object.Destroy(gameObject);
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

        private bool checkUp()
        {
            if (grid.gridArray[x, y + 1] == null) return false;
            if (grid.gridArray[x, y + 1].code == 3)
            {
                Wood wood = (Wood) grid.gridArray[x, y + 1];
                wood.Burn();
                grid.Update(x, y, null);
                y += 1;
                Update();
                return true;  
            }
            

            return false;
        }
        
        private void checkUpAlt()
        {
            if (grid.gridArray[x, y + 1] == null) return;
            if (grid.gridArray[x, y + 1].code == 3)
            {
                Wood wood = (Wood) grid.gridArray[x, y + 1];
                wood.Burn();
                Fire fire = new Fire(x, y + 1, fireObject, realSpeed*3, grid, 4, fires, fireObject, burnParticle);
                fire.Stop();
                fires.Add(fire);
                //grid.Update(x, y, null);
                //y += 1;
                //Update();
                
            }
            else if (grid.gridArray[x, y + 1].code == 8)
            {
                Oil oil = (Oil) grid.gridArray[x, y + 1];
                oil.instantiateFire();
                
                
            }

            
        }

        private bool checkSideWoods()
        {
            if (grid.gridArray[x - 1, y] == null) return false;
            if (grid.gridArray[x + 1, y] == null) return false;
            if (grid.gridArray[x - 1, y].code == 3)
            {
                Wood wood = (Wood) grid.gridArray[x - 1, y];
                wood.Burn();
                grid.Update(x, y, null);
                x -= 1;
                Update();
                return true;
            }
            if (grid.gridArray[x + 1, y].code == 3)
            {
                Wood wood = (Wood) grid.gridArray[x + 1, y];
                wood.Burn();
                grid.Update(x, y, null);
                x -= 1;
                Update();
                return true;
            }

            return false;
        }
        
        private void checkSideWoodsAlt()
        {
            
            if (bias == 0)
            {
                
                if (grid.gridArray[x - 1, y] != null && grid.gridArray[x - 1, y].code == 3)
                {
                    Wood wood = (Wood) grid.gridArray[x - 1, y];
                    wood.Burn();
                    Fire fire = new Fire(x - 1, y, fireObject, realSpeed*3, grid, 4, fires, fireObject, burnParticle);
                    fire.Stop();
                    fires.Add(fire);
                    //grid.Update(x, y, null);
                    //x -= 1;
                    //Update();
                }
                else if (grid.gridArray[x + 1, y] != null && grid.gridArray[x + 1, y].code == 3)
                {
                    Wood wood = (Wood) grid.gridArray[x + 1, y];
                    wood.Burn();
                    Fire fire = new Fire(x + 1, y, fireObject, realSpeed*3, grid, 4, fires, fireObject, burnParticle);
                    fires.Add(fire);
                    fire.Stop();
                    //grid.Update(x, y, null);
                    //x -= 1;
                    //Update();
                }
                
                if (grid.gridArray[x - 1, y] != null && grid.gridArray[x - 1, y].code == 8)
                {
                    Oil oil = (Oil) grid.gridArray[x - 1, y];
                    oil.instantiateFire();
                    
                }
                else if (grid.gridArray[x + 1, y] != null && grid.gridArray[x + 1, y].code == 8)
                {
                    Oil oil = (Oil) grid.gridArray[x + 1, y];
                    oil.instantiateFire();

                }
            }
            else
            {
                if (grid.gridArray[x + 1, y] != null && grid.gridArray[x + 1, y].code == 3)
                {
                    Wood wood = (Wood) grid.gridArray[x + 1, y];
                    wood.Burn();
                    Fire fire = new Fire(x + 1, y, fireObject, realSpeed*3, grid, 4, fires, fireObject, burnParticle);
                    fires.Add(fire);
                    fire.Stop();
                    //grid.Update(x, y, null);
                    //x -= 1;
                    //Update();
                }
                else if (grid.gridArray[x - 1, y] != null && grid.gridArray[x - 1, y].code == 3)
                {
                    Wood wood = (Wood) grid.gridArray[x - 1, y];
                    wood.Burn();
                    Fire fire = new Fire(x - 1, y, fireObject, realSpeed*3, grid, 4, fires, fireObject, burnParticle);
                    fire.Stop();
                    fires.Add(fire);
                
                    //grid.Update(x, y, null);
                    //x -= 1;
                    //Update();
                }
                
                
                if (grid.gridArray[x + 1, y] != null && grid.gridArray[x + 1, y].code == 8)
                {
                    Oil oil = (Oil) grid.gridArray[x + 1, y];
                    oil.instantiateFire();

                }
                else if (grid.gridArray[x - 1, y] != null && grid.gridArray[x - 1, y].code == 8)
                {
                    Oil oil = (Oil) grid.gridArray[x - 1, y];
                    oil.instantiateFire();
                    
                    
                }
            }
            
            
            

        }

        public void Stop()
        {
            initTime = Time.time;
            isStopped = true;
            //Object.Instantiate(burnParticle, trans.position, Quaternion.Euler(-90, 0, 0));
        }
        
        public void Move()
        {
            UpdateColliders();
        
            if (y > 0 && x > 0 && x < grid.width - 1)
            {
                //float distance = Vector2.Distance(rb.position, new Vector2(x, y));
                Vector3 offset = new Vector3(x, y) - trans.position;
                float distance = offset.sqrMagnitude;
                if (isStopped)
                {
                    if (Time.time - initTime > stopTime)
                    {
                        isStopped = false;
                    }
                }
                else if (distance <= 0.01)
                {
                    
                    isMoving = false;
                    /*
                    if (checkUp())
                    {
                        
                    }
                    else if (checkSideWoods())
                    {
                        
                    }
                    */
                    checkUpAlt();
                    checkSideWoodsAlt();
                    if (grid.gridArray[x, y - 1] == null)
                    {
                        grid.Update(x, y, null);
                        y -= 1;
                        Update();
                        
                    }
                    else if (grid.gridArray[x, y - 1].code == 3)
                    {
                        //Wood wood = (Wood)grid.gridArray[x, y - 1];
                        //wood.Burn();
                        //rid.Update(x, y, null);
                        //y -= 1;
                        Wood wood = (Wood) grid.gridArray[x, y - 1];
                        wood.Burn();
                        Fire fire = new Fire(x - 1, y, fireObject, realSpeed*3, grid, 4, fires, fireObject, burnParticle);
                        fire.Stop();
                        fires.Add(fire);
                        Update();
                        
                    }
                    else if (grid.gridArray[x, y - 1].code == 8)
                    {
                        Oil oil = (Oil) grid.gridArray[x, y - 1];
                        oil.instantiateFire();
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
    }
}
