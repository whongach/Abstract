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
    class MouseCursor : GameObject
    {
        //constructor
        public MouseCursor(Rectangle position, Texture2D sprite) : base(position, sprite)
        {

        }

        //methods

        public void Update(MouseState mouseState)
        {
            position.X = mouseState.X - position.Width / 2;
            position.Y = mouseState.Y - position.Height / 2;
        }

    }
}
