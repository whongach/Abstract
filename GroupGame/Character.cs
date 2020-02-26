using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class Character : GameObject , ICollidable
    {
        int health;
        Weapon weapon;
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
    }
}
