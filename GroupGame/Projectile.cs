using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class Projectile : GameObject
    {
        //fields
        private int speed;
        private double angle;
        private int damage;

        //properties
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public double Angle
        {
            get { return angle; }
            set { angle = value % 2*Math.PI; }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        //constructor
        public Projectile(double angle, Rectangle position, int speed, int damage, Texture2D sprite) : base(position, sprite)
        {
            this.speed = speed;
            this.angle = angle;
            this.damage = damage;
        }

        /// <summary>
        /// moves the projectile speed distance at the given angle
        /// </summary>
        public override void Update()
        {
            position.X += (int)Math.Cos(angle)*speed;
            position.Y += (int)Math.Sin(angle)*speed;
        }

    }
}
