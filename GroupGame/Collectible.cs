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
        bool circleBox;

        //properties 
        public bool CircleBox
        {
            get { return circleBox; }
        }

        //constructor
        //constructor
        public Collectible(Rectangle position, Texture2D sprite, bool circular) : base(position, sprite)
        {
            circleBox = circular;
        }
    }
}
