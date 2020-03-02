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

    class Enemy : Character
    {
        //Fields
        EnemyType type;
        int speed;
        Random rng;
        Player player;

        //Constructors
        public Enemy(int health, Weapon weapon, Rectangle position, Texture2D sprite, EnemyType type, int speed, Player player) : base(health, weapon, position, sprite)
        {
            this.type = type;
            this.speed = speed;
            this.player = player;
            rng = new Random();
        }

        //Methods
        //Enemy calls the corresponding move method based on its type
        protected void Move()
        {
            if(type == EnemyType.LeftRight)
            {
                LRWalk(rng.Next(100,600));
            }
            else if(type == EnemyType.Rectangle)
            {
                RectangleWalk(rng.Next(100, 600), rng.Next(100, 400));
            }
            else if(type == EnemyType.UpDown)
            {
                UDWalk(rng.Next(100, 400));
            }
            else if(type == EnemyType.Chase)
            {
                Chase(player);
            }
            else
            {
                RandomWalk(rng.Next(0, 100));
            }
        }

        //Enemy walks in a left-right pattern
        protected void LRWalk(int lineWidth)
        {
            //Sets default values for movement
            int travelled = 0;
            EnemyDirection direction = EnemyDirection.Right;

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
            //Sets default values for movement
            int travelled = 0;
            EnemyDirection direction = EnemyDirection.Right;

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
            //Sets default values for movement
            int travelled = 0;
            EnemyDirection direction = EnemyDirection.Down;

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
            //Sets default values for movement
            int travelled = 0;
            int direction = rng.Next(0, 8);

            //Enemy moves [speed] pixels in a random direction
            travelled += speed;
            if (direction == 0)
            {
                position.X += speed;
            }
            else if (direction == 1)
            {
                position.X -= speed;
            }
            else if (direction == 2)
            {
                position.Y -= speed;
            }
            else if(direction == 3)
            {
                position.Y += speed;
            }
            else if (direction == 4)
            {
                position.X += speed;
                position.Y -= speed;
            }
            else if (direction == 5)
            {
                position.X += speed;
                position.Y += speed;
            }
            else if (direction == 6)
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
                direction = rng.Next(0, 8);
            }
        }
    }
}
