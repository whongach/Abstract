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
        protected bool circleBox;

        //properties
        public Weapon Weapon
        {
            get { return weapon; }
        }

        public bool CircleBox
        {
            get { return circleBox; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        //constructor
        public Character(int health, Weapon weapon, Rectangle position, Texture2D sprite, bool circular) : base(position, sprite)
        {
            this.health = health;
            this.weapon = weapon;
            circleBox = circular;
        }

       
    }
}
