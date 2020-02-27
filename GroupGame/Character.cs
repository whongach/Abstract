using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    abstract class Character : GameObject , ICollidable
    {
        //fields
        protected int health;
        protected Weapon weapon;
        protected Rectangle hitbox;
        protected bool circleBox;

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
        public Character(int health, Weapon weapon, Rectangle position, Texture2D sprite) : base(position, sprite)
        {
            this.health = health;
            this.weapon = weapon;
        }

       
    }
}
