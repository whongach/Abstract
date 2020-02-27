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
    class Player : Character
    {
        //fields
        Weapon offHand;
        Item currentItem;

        //constructor
        public Player(int health, Weapon weapon, Rectangle position, Texture2D sprite) : base(health, weapon, position, sprite)
        {
            offHand = null;
            currentItem = null;
        }


        //methods

        /// <summary>
        /// determines the angle between the mouse and this player and calls attack at this player's position and in that angle
        /// </summary>
        public void Attack()
        {
            MouseState mousePosition = Mouse.GetState();
            double angle = Math.Atan((mousePosition.Y - position.Y) / (mousePosition.X - position.X));
            weapon.Attack(new Vector2(position.X, position.Y), angle);
        }
    }
}
