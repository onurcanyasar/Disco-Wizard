using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cell_Folder
{
    public class GameHandler : MonoBehaviour
    {
        public GameObject sandObject;
        public GameObject waterObject;
        public GameObject fireObject;
        public GameObject woodObject;
        public GameObject burnParticle;
       
        private static int width = 1920;
        private static int height = 1080;
        private List<Sand> sands;
        private List<Water> waters;
        private List<Fire> fires;
        private List<Wood> woods;
        private Grid grid;
        private bool state;
        private bool state2;

        
        void Start()
        {
            state = false;
            state2 = false;
            sands = new List<Sand>();
            waters = new List<Water>();
            grid = new Grid(width, height);
            fires = new List<Fire>();
            woods = new List<Wood>();
        }

        // Update is called once per frame
        void Update()
        {
        
            if (Input.GetKeyDown(KeyCode.A))
            {
                state = !state;
            }
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log(grid.gridArray[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)]);
            }
            if (Input.GetMouseButtonDown(0))
            {

                pos = new Vector2(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
                //GameObject p = Instantiate(obj, pos, quaternion.identity);
                float speed = Random.Range(30, 90);
            
                if (state)
                {
                
                    //NewWater water = new NewWater((int) pos.x, (int) pos.y, waterObject, speed, grid, 2);
                    //waters.Add(water);
                    
                    Fire fire = new Fire((int) pos.x, (int) pos.y, fireObject, speed, grid, 4, fires, fireObject, burnParticle);
                    fires.Add(fire);
                
                }
                else
                {
                    //Instantiate(sandObject, pos, Quaternion.identity);

                    //NewSand sand = new NewSand((int)pos.x, (int)pos.y, sandObject, speed, grid, 1);
                   //sands.Add(sand);
                   //Wood wood = new Wood((int)pos.x, (int)pos.y, woodObject, grid, 3);
                   //woods.Add(wood);
                
                }
            
            
            }

            foreach (var fire in fires)
            {
                fire.Move();
            }
            foreach (var sand in sands)
            {
                sand.Move();
            }
            foreach (var water in waters)
            {
                water.Move();
            }
            
            
        }
    
    }
}
