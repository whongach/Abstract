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
            this.position.X = position.X + position.Width / 2 + (int)(Math.Cos(angle) * position.Width / 2);
            this.position.Y = position.Y + position.Height / 2 + (int)(Math.Sin(angle) * position.Height / 2);
            this.position.X -= this.position.Width / 2;
            this.position.Y -= this.position.Height / 2;
            this.angle = angle;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(sprite, new Rectangle(position.X+position.Width/2, position.Y+position.Height/2, position.Width, position.Height), null, Color.White, (float)angle, new Vector2(sprite.Width/2, sprite.Height/2), SpriteEffects.None, 1);
        }

        public virtual void DrawHands(SpriteBatch sb, Rectangle playerPosition, Texture2D handSprite, Color handColor)
        {
            sb.Draw(handSprite, new Rectangle(playerPosition.X + playerPosition.Width * 7 / 16 + (int)(playerPosition.Width / 2 * Math.Cos(angle)),
                playerPosition.Y + playerPosition.Height * 7 / 16 + (int)(playerPosition.Height / 2 * Math.Sin(angle)),
                playerPosition.Width / 8, playerPosition.Height / 8), handColor);
            if(angle<Math.PI/2&&angle>0-Math.PI/2)
                sb.Draw(handSprite, new Rectangle(playerPosition.X + playerPosition.Width / 5, playerPosition.Y + playerPosition.Height / 2, playerPosition.Width / 8, playerPosition.Height / 8), handColor);
            else
                sb.Draw(handSprite, new Rectangle(playerPosition.X + playerPosition.Width * 28 / 40, playerPosition.Y + playerPosition.Height / 2, playerPosition.Width / 8, playerPosition.Height / 8), handColor);
        }
    }
}
