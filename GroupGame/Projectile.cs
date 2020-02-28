﻿using Microsoft.Xna.Framework;
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
        double x;
        double y;
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
            x = position.X;
            y = position.Y;
        }

        /// <summary>
        /// moves the projectile speed distance at the given angle
        /// </summary>
        public override void Update()
        {
            x += (Math.Cos(angle) * speed);
            y += (Math.Sin(angle) * speed);
        }

        /// <summary>
        /// allows the projectiles to move in 360 degrees
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(sprite, new Rectangle((int)(x),(int)(y),position.Width, position.Height), Color.White);
        }

    }
}
