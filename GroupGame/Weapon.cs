using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    abstract class Weapon : Collectible, ICollidable
    {
        int damage;

        Rectangle hitbox;
        bool circleBox;

        public Rectangle Hitbox
        {
            get { return hitbox; }
        }

        public bool CircleBox
        {
            get { return circleBox; }
        }

        public abstract void Attack(Rectangle position, double angle);
    }
}
