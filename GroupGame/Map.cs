using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class Map
    {
        //fields
        Tile[,] layout;
        List<Wall> walls;
        Texture2D wallSprite;
        Texture2D floorSprite;
        int tileSize;

        //properties
        public List<Wall> Walls
        {
            get { return walls; }
        }

        public Tile[,] Layout
        {
            get { return layout; }
        }

        //constructor
        public Map(Texture2D wallSprite, Texture2D floorSprite, int tileSize, int[,] layout)
        {
            //initializes variables
            this.wallSprite = wallSprite;
            this.floorSprite = floorSprite;
            this.tileSize = tileSize;
            this.layout = new Tile[16,16];
            walls = new List<Wall>();
            Wall currentWall = null;
            for (int i = 0; i < 16; i++) 
            {
                for (int j = 0; j < 16; j++) 
                {
                    if (layout[i, j] == 1)
                    {
                        currentWall = new Wall(new Rectangle(i * tileSize, j * tileSize, tileSize, tileSize), wallSprite);
                        this.layout[i, j] = currentWall;
                        walls.Add(currentWall);
                    }
                    else
                        this.layout[i, j] = new Tile(new Rectangle(i * tileSize, j * tileSize, tileSize, tileSize), floorSprite);
                }
            }
        }

        //methods

        /// <summary>
        /// calls draw for each tile
        /// </summary>
        /// <param name="sb">the spritebatch used to draw</param>
        public void Draw(SpriteBatch sb)
        {
            for(int i = 0; i<layout.GetLength(0); i++)
            {
                for(int j = 0; j<layout.GetLength(1); j++)
                {
                    layout[i, j].Draw(sb);
                }
            }
        }

        /// <summary>
        /// Shifts all tile in the map to adjust for the origin and tilesize
        /// </summary>
        /// <param name="origin">The top left corner of the map</param>
        /// <param name="tileSize">the width and height of each tile</param>
        public void SetOrigin(Point origin, int scaledSize)
        {
            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    layout[i, j].Position = new Rectangle(origin.X + i * scaledSize, origin.Y + j * scaledSize, scaledSize, scaledSize);
                }
            }
        }

    }
}
