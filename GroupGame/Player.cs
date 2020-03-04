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
    class Player : Character, ICollidable
    {
        //fields
        Weapon offHand;
        Item currentItem;
        double angle;

        //constructor
        public Player(int health, Weapon weapon, Rectangle position, Texture2D sprite, bool circular) : base(health, weapon, position, sprite, circular)
        {
            offHand = null;
            currentItem = null;
            this.position = position;
            angle = 0;
        }


        //methods

        /// <summary>
        /// determines the angle between the mouse and this player and calls attack at this player's position and in that angle
        /// </summary>
        public void Attack()
        {
            weapon.Attack();
        }

        /// <summary>
        /// necessary updates made for player
        /// </summary>
        public void Update(MouseState mouseState, MouseState previousMouseState, KeyboardState keyState)
        {
            
            if (mouseState.X - position.X == 0)
                angle = Math.Atan(((double)mouseState.Y - (double)position.Y) / ((double)mouseState.X - (double)position.X - .00000001));
            else
                angle = Math.Atan(((double)mouseState.Y - (double)position.Y) / ((double)mouseState.X - (double)position.X));
            if (mouseState.X < position.X)
                angle -= Math.PI;
            weapon.Update(position, angle);
            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                Attack();
        }

        public void Move(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            {
                this.position.X -= 3;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                this.position.X += 3;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                this.position.Y -= 3;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                this.position.Y += 3;
            }
        }

        /// <summary>
        /// necessary draws made for player
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            weapon.Draw(sb);
        }
    }
}
