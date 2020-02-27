using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class GameObject
    {
        //fields
        Rectangle position;
        Texture2D sprite;

        //constructor
        public GameObject(Rectangle position, Texture2D sprite)
        {
            this.position = position;
            this.sprite = sprite;
        }

        //methods

        public virtual void Update()
        {

        }

        /// <summary>
        /// default draw method using a sprite and rectangle
        /// </summary>
        /// <param name="sb">the spritebatch used to draw the sprite</param>
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(sprite, position, Color.White);
        }
    }
}
