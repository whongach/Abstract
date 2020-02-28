using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    abstract class Weapon : Collectible, ICollidable
    {
        //fields
        protected int damage;

        //constructor
        public Weapon(Rectangle position, Texture2D sprite) : base(position, sprite)
        {

        }

        //methods
        public abstract void Attack(Rectangle launchPoint, double angle);
    }
}
