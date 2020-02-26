using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GroupGame
{
    class Enemy
    {
        //Fields
        //Graphic fields
        private Texture2D enemyTexture;
        private int x;
        private int y;

        //Properties
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        //Constructors
        public Enemy(Texture2D enemyTexture, int x, int y)
        {
            this.enemyTexture = enemyTexture;
            this.x = x;
            this.y = y;
        }

        //Methods
        //Enemy walks in a left-right pattern
        protected void LRWalk()
        {
            
        }
        //Enemy walks in a square pattern
        protected void SquareWalk()
        {

        }

        //Enemy walks in an up-down pattern
        protected void UDWalk()
        {

        }

        //Enemy chases player directly
        protected void Chase()
        {

        }
    }
}
