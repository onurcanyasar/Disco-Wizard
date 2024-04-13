using UnityEngine;

namespace Cell_Folder
{
    public class Bedrock : Cell_Folder.Cell
    {
    

        public Bedrock(int x, int y, GameObject gameObject, Grid grid, int code) : base(x, y, gameObject, grid, code)
        {
            SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
            int random = Random.Range(100, 107);
            sr.color = new Color(103/255.0f, random/255.0f, 103/255.0f);
            
        }       
    }
}
