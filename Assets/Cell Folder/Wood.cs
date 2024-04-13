using System.Collections.Generic;
using UnityEngine;

namespace Cell_Folder
{
    public class Wood : Cell
    {

        
        private List<Smoke> smokes;
        private GameObject smokeObject;
        public Wood(int x, int y, GameObject gameObject, Grid grid, int code, List<Smoke> smokes, GameObject smokeObject) : base(x, y, gameObject, grid, code)
        {
            isAlive = true;
            this.smokes = smokes;
            this.smokeObject = smokeObject;
            SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
            int random = Random.Range(60, 70);
            sr.color = new Color(113/255.0f, random/255.0f, 0/255.0f);
            loc.x = x;
            loc.y = y;

        }

        public void Burn()
        {
            
            grid.Update(x ,y, null);
            float speed = Random.Range(30, 90);
            Smoke smoke = new Smoke(x, y, smokeObject, speed, grid, 5);
            smokes.Add(smoke);
            isAlive = false;
            Object.Destroy(gameObject);
        
        }

        public void Move()
        {
           UpdateColliders();
           
           
        }
    }
}
