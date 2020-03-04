using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    abstract class Collectible : GameObject, ICollidable
    {
        //fields
        bool circleBox;
        bool pickedUp;

        //properties 
        public bool CircleBox
        {
            get { return circleBox; }
        }

        public bool PickedUp
        {
            get { return pickedUp; }
            set { pickedUp = value; }
        }

        //constructor
        public Collectible(Rectangle position, Texture2D sprite, bool circular, bool equipped) : base(position, sprite)
        {
            circleBox = circular;
            pickedUp = equipped;
        }

        //methods

        /// <summary>
        /// draw method fro items in inventory
        /// </summary>
        /// <param name="sb">spritebatch used to draw item</param>
        /// <param name="displayPos">the position of the inventory box</param>
        public virtual void Draw(SpriteBatch sb, Rectangle displayPos)
        {
            sb.Draw(sprite, displayPos, Color.White);
        }
    }
}
