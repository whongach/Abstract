using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class Tile : GameObject
    {
        //fields
        private bool isWall;

        //properties
        public bool IsWall
        {
            get { return isWall; }
        }

        //constructor
        public Tile(Rectangle position, Texture2D sprite, bool isWall) : base(position, sprite)
        {
            this.isWall = isWall;
        }
    }
}
