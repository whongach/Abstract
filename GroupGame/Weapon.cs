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
        protected double angle;

        //properties
        public int Damage
        {
            get { return damage; }
        }

        //constructor
        public Weapon(Rectangle position, Texture2D sprite, bool circular, bool equipped) : base(position, sprite, circular, equipped)
        {

        }

        //methods
        public abstract void Attack();

        //overrides position to draw the weapon by the player at the correct angle
        public virtual void Update(Rectangle position, double angle)
        {
            this.position.X = position.X + position.Width / 2 - this.position.Width / 2 +
                (int)(Math.Cos(angle)*this.position.Height/2) + (int)(Math.Sin(angle) * position.Height / 2) - (int)(Math.Sin(angle) * this.position.Height / 2);
            this.position.Y = position.Y + position.Height / 2 - this.position.Height / 2 + 
                (int)(Math.Sin(angle) * this.position.Height / 2) - (int)(Math.Cos(angle) * position.Width / 2) + (int)(Math.Cos(angle) * this.position.Height / 2);
            this.angle = angle;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(sprite, new Rectangle(position.X+position.Width/2, position.Y+position.Height/2, position.Width, position.Height), null, Color.White, (float)angle, new Vector2(position.Height/2,position.Width/2), SpriteEffects.None, 1);
        }
    }
}
