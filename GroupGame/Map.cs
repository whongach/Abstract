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
        public Map(Texture2D wallSprite, Texture2D floorSprite, int tileSize)
        {
            //initializes variables
            this.wallSprite = wallSprite;
            this.floorSprite = floorSprite;
            this.tileSize = tileSize;
        }

    }
}
