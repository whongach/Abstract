using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class Item : Collectible
    {
        //constructor
        public Item(Rectangle position, Texture2D sprite) : base(position, sprite)
        {

        }
    }
}
