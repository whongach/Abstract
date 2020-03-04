using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class Projectile : GameObject, ICollidable
    {
        //fields
        private int speed;
        private double angle;
        double x;
        double y;
        private int damage;
        private bool circleBox;

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

        public bool CircleBox
        {
            get { return circleBox; }
        }

        //constructor
        public Projectile(double angle, Rectangle position, int speed, int damage, Texture2D sprite, bool circular) : base(position, sprite)
        {
            this.speed = speed;
            this.angle = angle;
            this.damage = damage;
            x = position.X;
            y = position.Y;
            circleBox = circular;
        }

        /// <summary>
        /// moves the projectile speed distance at the given angle
        /// </summary>
        public  void Update()
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
            sb.Draw(sprite, new Rectangle((int)(x)+position.Width/2, (int)(y)+position.Height/2, position.Width, position.Height), null, Color.White, (float)angle, new Vector2(position.Width / 2, position.Height / 2), SpriteEffects.None, 1);
        }

        /// <summary>
        /// estroys the projectile on collision with a character
        /// </summary>
        public void Destroy()
        {
            this.Damage = -1;
        }
    }
}
