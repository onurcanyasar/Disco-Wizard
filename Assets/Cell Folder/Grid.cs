using UnityEngine;

namespace Cell_Folder
{
    public class Grid
    {
        public int width;
        public int height;
        public Cell[,] gridArray;
        public float cellSize;

        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;
            gridArray = new Cell[width, height];
            /*
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    Debug.DrawLine(new Vector2(x - 0.5f, y - 0.5f), new Vector2(x - 0.5f, y + 1 - 0.5f), Color.white, 100f);
                    Debug.DrawLine(new Vector2(x - 0.5f, y - 0.5f), new Vector2(x + 1 - 0.5f, y - 0.5f), Color.white, 100f);
                }
            }
            Debug.DrawLine(new Vector2(0 - 0.5f, height - 0.5f), new Vector2(width - 0.5f, height - 0.5f), Color.white, 100f);
            Debug.DrawLine(new Vector2(width - 0.5f, 0 - 0.5f), new Vector2(width - 0.5f, height - 0.5f), Color.white, 100f);
            */

        }

        public void Update(int x, int y, Cell cell)
        {
            gridArray[x, y] = cell;
        }

    }
}