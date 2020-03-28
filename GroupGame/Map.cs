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

        //constructor
        public Map(Texture2D wallSprite, Texture2D floorSprite, int tileSize, int[,] layout)
        {
            //initializes variables
            this.wallSprite = wallSprite;
            this.floorSprite = floorSprite;
            this.tileSize = tileSize;
            this.layout = new Tile[16,16];
            for (int i = 0; i < 16; i++) 
            {
                for (int j = 0; j < 16; j++) 
                {
                    if (layout[i, j] == 1)
                        this.layout[i, j] = new Wall(new Rectangle(i * tileSize, j * tileSize, tileSize, tileSize), wallSprite);
                    else
                        this.layout[i, j] = new Tile(new Rectangle(i * tileSize, j * tileSize, tileSize, tileSize), floorSprite);
                }
            }
        }

    }
}
