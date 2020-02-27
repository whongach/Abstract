using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class Collectible : GameObject, ICollidable
    {
        //fields
        Rectangle hitbox;
        bool circleBox;

        //properties
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }

        public bool CircleBox
        {
            get { return circleBox; }
        }

        //constructor
        //constructor
        public Collectible(Rectangle position, Texture2D sprite) : base(position, sprite)
        {

        }
    }
}
