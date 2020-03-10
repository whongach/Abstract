using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class MeleeWeapon : Weapon, ICollidable
    {
        //fields
        int rotationDegrees;
        int rotationSpeed;
        bool attacking;
        int attackFrame;
        int totalFrames;

        //properties
        public bool Attacking
        {
            get { return attacking; }
            set { attacking = value; }
        }

        //constructor
        public MeleeWeapon(Rectangle position, Texture2D sprite, bool circular, bool equipped, int rotationDegrees, int rotationSpeed) : base(position, sprite, circular, equipped)
        {
            this.rotationDegrees = rotationDegrees;
            this.rotationSpeed = rotationSpeed;
            totalFrames = rotationDegrees / rotationSpeed;
            this.attacking = false;
            this.attackFrame = 0;
        }

        /// <summary>
        /// overload for static weapons such as spears. allows them to be thrusted for a duration instead of spun
        /// </summary>
        /// <param name="duration">duration of the thrust</param>
        public MeleeWeapon(Rectangle position, Texture2D sprite, bool circular, bool equipped, int duration) : base(position, sprite, circular, equipped)
        {
            rotationDegrees = 0;
            rotationSpeed = 0;
            totalFrames = duration;
            this.attacking = false;
            this.attackFrame = 0;
        }

        //methods

        /// <summary>
        /// sets attacking to true if it is false and the attackframe is 0
        /// </summary>
        public override void Attack()
        {
            if (!attacking && attackFrame == 0)
                attacking = true;
        }

        /// <summary>
        /// sets the position and angle of the weapon and adjusts if the weapon is attacking
        /// </summary>
        public override void Update(Rectangle position, double angle)
        {
            //base
            this.angle = angle;

            //ends attack
            if (attackFrame >= totalFrames)
            {
                attacking = false;
                attackFrame = 0;
            }

            //updates for attack
            if (attacking || attackFrame != 0)
            {
                if (rotationSpeed != 0)
                {
                    //adjusts angle for swing
                    this.angle += (double)rotationDegrees / 180 * Math.PI / 2;
                    attackFrame++;
                    this.angle -= attackFrame * (double)rotationSpeed / 180 * Math.PI;
                }
                //extends weapon if attacking
                attackFrame++;
                this.position.X = position.X + position.Width / 2 - this.position.Width / 2 +
                    (int)(Math.Cos(angle) * this.position.Height) + (int)(Math.Sin(angle) * position.Height / 2) - (int)(Math.Sin(angle) * this.position.Height / 2);
                this.position.Y = position.Y + position.Height / 2 - this.position.Height / 2 +
                    (int)(Math.Sin(angle) * this.position.Height) - (int)(Math.Cos(angle) * position.Width / 2) + (int)(Math.Cos(angle) * this.position.Height / 2);

            }
            else
            {
                //pulls back the weapon if not attacking
                this.position.X = position.X + position.Width / 2 - this.position.Width / 2 +
                    (int)(Math.Cos(angle) * this.position.Height / 2) + (int)(Math.Sin(angle) * position.Height / 2) - (int)(Math.Sin(angle) * this.position.Height / 2);
                this.position.Y = position.Y + position.Height / 2 - this.position.Height / 2 +
                    (int)(Math.Sin(angle) * this.position.Height / 2) - (int)(Math.Cos(angle) * position.Width / 2) + (int)(Math.Cos(angle) * this.position.Height / 2);
            }
        }

        public override void DrawHands(SpriteBatch sb, Rectangle playerPosition, Texture2D handSprite, Color handColor)
        {
            if (!attacking)
            {
                sb.Draw(handSprite, new Rectangle(playerPosition.X + playerPosition.Width * 7 / 16 + (int)(playerPosition.Width / 8 * Math.Cos(angle)),
                playerPosition.Y + playerPosition.Height * 7 / 16 + (int)(playerPosition.Height / 8 * Math.Sin(angle)),
                playerPosition.Width / 8, playerPosition.Height / 8), handColor);
                if (angle < Math.PI / 2 && angle > 0 - Math.PI / 2)
                    sb.Draw(handSprite, new Rectangle(playerPosition.X + playerPosition.Width / 5, playerPosition.Y + playerPosition.Height / 2, playerPosition.Width / 8, playerPosition.Height / 8), handColor);
                else
                    sb.Draw(handSprite, new Rectangle(playerPosition.X + playerPosition.Width * 28 / 40, playerPosition.Y + playerPosition.Height / 2, playerPosition.Width / 8, playerPosition.Height / 8), handColor);

            }
            else
                base.DrawHands(sb, playerPosition, handSprite, handColor);
        }
    }
}
