using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class LootTile : Tile
    {
        //fields
        Collectible loot;

        //constructor
        public LootTile(Rectangle position, Texture2D sprite) : base(position, sprite)
        {
            loot = null;
        }
    }
}
