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
    //Enumerations to determine enemy movement
    public enum EnemyType
    { LeftRight, Rectangle, UpDown, Chase, Random}

    public enum EnemyDirection
    { Up, Right, Down, Left}

    class Enemy : Character, ICollidable
    {
        //Fields
        private EnemyType type;
        private EnemyDirection direction;
        private int numDirection; //This field is specifically for Random enemy types so that their direction can be generated easily
        private int speed;
        private int travelled;
        private int maxWidth;
        private int maxHeight;
        private int bodyDamage;
        private Random rng;
        Player player;

        //properties
        public int BodyDamage
        {
            get { return bodyDamage; }
        }

        //Constructors
        public Enemy(int health, Weapon weapon, Rectangle position, Texture2D sprite, EnemyType type, int speed, int bodyDamage, Player player, bool circular) : base(health, weapon, position, sprite, circular)
        {
            this.type = type;
            this.speed = speed;
            this.player = player;
            this.bodyDamage = bodyDamage;
            travelled = 0;
            rng = new Random();

            //Sets start direction and lengths for certain types of enemies
            if (type == EnemyType.LeftRight)
            {
                direction = EnemyDirection.Right;
                maxWidth = rng.Next(100, 600);
            }
            else if (type == EnemyType.Rectangle)
            {
                direction = EnemyDirection.Right;
                maxWidth = rng.Next(100, 600);
                maxHeight = rng.Next(100, 400);
            }
            else if (type == EnemyType.UpDown)
            {
                direction = EnemyDirection.Down;
                maxHeight = rng.Next(100, 400);
            }
            else if(type == EnemyType.Random)
            {
                numDirection = rng.Next(0, 8);
                maxWidth = rng.Next(0, 100);
            }
        }

        //Methods
        //Enemy calls the corresponding move method based on its type
        public void Move()
        {
            if(type == EnemyType.LeftRight)
            {
                LRWalk(maxWidth);
            }
            else if(type == EnemyType.Rectangle)
            {
                RectangleWalk(maxHeight, maxWidth);
            }
            else if(type == EnemyType.UpDown)
            {
                UDWalk(maxHeight);
            }
            else if(type == EnemyType.Chase)
            {
                Chase(player);
            }
            else
            {
                RandomWalk(maxWidth);
            }
        }

        //Enemy walks in a left-right pattern
        protected void LRWalk(int lineWidth)
        {
            //Enemy moves [speed] pixels in the appropriate direction
            travelled += speed;
            if(direction == EnemyDirection.Right)
            {
                position.X += speed;
            }
            else
            {
                position.X -= speed;
            }

            //
            //CODE TO CHECK FOR WALL COLLISION WILL BE ADDED ONCE IMPLEMENTED
            //

            //Checks if enemy has travelled full distance of pattern direction
            if(travelled >= lineWidth)
            {
                travelled = 0;
                if(direction == EnemyDirection.Right)
                {
                    direction = EnemyDirection.Left;
                }
                else
                {
                    direction = EnemyDirection.Right;
                }
            }
        }
        //Enemy walks in a square pattern
        protected void RectangleWalk(int rectWidth, int rectHeight)
        {
            //Enemy moves [speed] pixels in the appropriate direction, if they complete the direction, they change it
            travelled += speed;
            if (direction == EnemyDirection.Right)
            {
                position.X += speed;        
                if(travelled >= rectWidth)
                {
                    travelled = 0;
                    direction = EnemyDirection.Down;
                }
            }
            else if(direction == EnemyDirection.Left)
            {
                position.X -= speed;
                if (travelled >= rectWidth)
                {
                    travelled = 0;
                    direction = EnemyDirection.Up;
                }
            }
            else if(direction == EnemyDirection.Up)
            {
                position.Y -= speed;
                if (travelled >= rectHeight)
                {
                    travelled = 0;
                    direction = EnemyDirection.Right;
                }
            }
            else
            {
                position.Y += speed;
                if (travelled >= rectHeight)
                {
                    travelled = 0;
                    direction = EnemyDirection.Left;
                }
            }

            //
            //CODE TO CHECK FOR WALL COLLISION WILL BE ADDED AS SOON AS
            //
        }

        //Enemy walks in an up-down pattern
        protected void UDWalk(int lineHeight)
        {
            //Enemy moves [speed] pixels in the appropriate direction
            travelled += speed;
            if (direction == EnemyDirection.Down)
            {
                position.Y += speed;
            }
            else
            {
                position.Y -= speed;
            }

            //
            //CODE TO CHECK FOR WALL COLLISION WILL BE ADDED ONCE IMPLEMENTED
            //

            //Checks if enemy has travelled full distance of pattern direction
            if (travelled >= lineHeight)
            {
                travelled = 0;
                if (direction == EnemyDirection.Down)
                {
                    direction = EnemyDirection.Up;
                }
                else
                {
                    direction = EnemyDirection.Down;
                }
            }
        }

        //Enemy chases player directly
        protected void Chase(Player player)
        {
            if(player.Position.X > position.X)
            {
                position.X += speed;
            }
            else if(player.Position.X < position.X)
            {
                position.X -= speed;
            }

            if (player.Position.Y > position.Y)
            {
                position.Y += speed;
            }
            else if (player.Position.Y < position.Y)
            {
                position.Y -= speed;
            }

            //
            //CODE TO CHECK FOR WALL COLLISION WILL BE ADDED ONCE IMPLEMENTED
            //
        }

        //Enemy moves randomly, one direction at a time
        protected void RandomWalk(int lineLength)
        {
            //Enemy moves [speed] pixels in a random direction
            travelled += speed;
            if (numDirection == 0)
            {
                position.X += speed;
            }
            else if (numDirection == 1)
            {
                position.X -= speed;
            }
            else if (numDirection == 2)
            {
                position.Y -= speed;
            }
            else if(numDirection == 3)
            {
                position.Y += speed;
            }
            else if (numDirection == 4)
            {
                position.X += speed;
                position.Y -= speed;
            }
            else if (numDirection == 5)
            {
                position.X += speed;
                position.Y += speed;
            }
            else if (numDirection == 6)
            {
                position.X -= speed;
                position.Y -= speed;
            }
            else
            {
                position.X -= speed;
                position.Y += speed;
            }

            //
            //CODE TO CHECK FOR WALL COLLISION WILL BE ADDED ONCE IMPLEMENTED
            //

            //Changes direction if enemy reaches lineLength
            if (travelled >= lineLength)
            {
                travelled = 0;
                numDirection = rng.Next(0, 8);
            }
        }

        public override void Update()
        {
            Move();
        }
    }
}
