using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace for the Game.
/// </summary>
namespace GroupGame
{
    // Enumerations
    /// <summary>
    /// Enumeration for various Enemy types.
    /// </summary>
    public enum EnemyType { LeftRight, Rectangle, UpDown, Chase, Random }

    /// <summary>
    /// Enumeration for Enemy movement directions.
    /// </summary>
    public enum EnemyDirection
    { Up, Right, Down, Left}

    /// <summary>
    /// Class for Enemy type Characters.
    /// </summary>
    class Enemy : Character, ICollidable
    {
        // Fields
        private EnemyType type; // Type of Enemy movement
        private EnemyDirection direction;
        private int numDirection; //This field is specifically for Random enemy types so that their direction can be generated easily
        private int speed;
        private int travelled;
        private int maxWidth;
        private int maxHeight;
        private int bodyDamage;
        private int attackInterval;
        private double weaponAngle;
        private Random rng;
        private Player player;

        // Properties
        /// <summary>
        /// Gets the damage that the Player takes when they collide with the Enemy.
        /// </summary>
        public int BodyDamage { get { return bodyDamage; } }

        // Constructors
        /// <summary>
        /// Constructs an Enemy Character object that is Collidable.
        /// </summary>
        /// <param name="health">The Enemy's health.</param>
        /// <param name="weapon">The Enemy's weapon.</param>
        /// <param name="position">The Enemy's position.</param>
        /// <param name="sprite">The Enemy's sprite.</param>
        /// <param name="type">The Enemy's type of movement.</param>
        /// <param name="speed">The Enemy's speed.</param>
        /// <param name="attackInterval">The time between the Enemy's attacks.</param>
        /// <param name="bodyDamage">The damage the Enemy deals at close range.</param>
        /// <param name="player">The Player character.</param>
        /// <param name="circular">Whether or not the Enemy's shape is circular or rectangular.</param>
        public Enemy(int health, Weapon weapon, Rectangle position, Texture2D sprite, EnemyType type, int speed, int attackInterval, int bodyDamage, Player player, bool circular) : base(health, weapon, position, sprite, circular)
        {
            // Initializes fields
            this.type = type;
            this.speed = speed;
            this.player = player;
            this.bodyDamage = bodyDamage;
            this.attackInterval = attackInterval;
            travelled = 0;
            rng = new Random();

            // Sets start direction and length of movement
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
                maxWidth = rng.Next(25, 100);
            }
        }

        // Methods
        /// <summary>
        /// Moves the enemy based on it's movement pattern.
        /// </summary>
        public void Move()
        {
            // Check movement type
            if (type == EnemyType.LeftRight)
            {
                LRWalk(maxWidth);
            }
            else if (type == EnemyType.Rectangle)
            {
                RectangleWalk(maxHeight, maxWidth);
            }
            else if (type == EnemyType.UpDown)
            {
                UDWalk(maxHeight);
            }
            else if (type == EnemyType.Chase)
            {
                Chase(player);
            }
            else
            {
                RandomWalk(maxWidth);
            }

            // Attack every interval
            if (rng.Next(attackInterval*200) == 0)
                weapon.Attack();
        }

        /// <summary>
        /// Enemy Left-Right walk pattern.
        /// </summary>
        /// <param name="lineWidth">The length of the path for the Enemy to walk.</param>
        protected void LRWalk(int lineWidth)
        {
            // Move number of pixels
            travelled += speed;

            // Check direction for the Enemy and move appropriately
            if(direction == EnemyDirection.Right)
                position.X += speed;
            else
                position.X -= speed;

            //
            //CODE TO CHECK FOR WALL COLLISION WILL BE ADDED ONCE IMPLEMENTED
            //

            // Checks if Enemy has traveled full distance of pattern direction
            if(travelled >= lineWidth)
            { 
                // Reset distance traveled
                travelled = 0;

                // Transition direction to move
                if(direction == EnemyDirection.Right)
                    direction = EnemyDirection.Left;
                else
                    direction = EnemyDirection.Right;
            }
        }

        /// <summary>
        /// Enemy Square walk pattern.
        /// </summary>
        /// <param name="rectWidth">Distance to walk in the x direction.</param>
        /// <param name="rectHeight">Distance to walk in the y direction.</param>
        protected void RectangleWalk(int rectWidth, int rectHeight)
        {
            // Move number of pixels
            travelled += speed;

            // Check direction for the Enemy and move appropriately
            if (direction == EnemyDirection.Right)
            {
                // Move in the x direction
                position.X += speed;      
                
                // Change direction if distance has been traveled
                if(travelled >= rectWidth)
                {
                    travelled = 0;
                    direction = EnemyDirection.Down;
                }
            }
            else if(direction == EnemyDirection.Left)
            {
                // Move in the x direction
                position.X -= speed;

                // Change direction if distance has been traveled
                if (travelled >= rectWidth)
                {
                    travelled = 0;
                    direction = EnemyDirection.Up;
                }
            }
            else if(direction == EnemyDirection.Up)
            {
                // Move in the y direction
                position.Y -= speed;

                // Change direction if distance has been traveled
                if (travelled >= rectHeight)
                {
                    travelled = 0;
                    direction = EnemyDirection.Right;
                }
            }
            else
            {
                // Move in the y direction
                position.Y += speed;

                // Change direction if distance has been traveled
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

        /// <summary>
        /// Enemy Up-Down walk pattern.
        /// </summary>
        /// <param name="lineHeight">Distance to walk.</param>
        protected void UDWalk(int lineHeight)
        {
            // Move number of pixels
            travelled += speed;

            // Check direction of Enemy and move appropriately
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

            // Change direction if distance has been traveled
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

        /// <summary>
        /// Enemy Chase movement pattern.
        /// </summary>
        /// <param name="player">The Player Character.</param>
        protected void Chase(Player player)
        {
            // Check Player x position and move Enemy towards them
            if (player.Position.X > position.X)
                position.X += speed;
            else if (player.Position.X < position.X)
                position.X -= speed;

            // Check Player y position and move Enemy towards them
            if (player.Position.Y > position.Y)
                position.Y += speed;
            else if (player.Position.Y < position.Y)
                position.Y -= speed;

            //
            //CODE TO CHECK FOR WALL COLLISION WILL BE ADDED ONCE IMPLEMENTED
            //
        }

        /// <summary>
        /// Random Enemy movement pattern.
        /// </summary>
        /// <param name="lineLength"></param>
        protected void RandomWalk(int lineLength)
        {
            // Move number of pixes
            travelled += speed;

            // Check direction and move in that direction
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

            // Changes direction if follows the traveled distance
            if (travelled >= lineLength)
            {
                travelled = 0;
                numDirection = rng.Next(0, 8);
            }
        }

        /// <summary>
        /// Updates the Enemy Character.
        /// </summary>
        public override void Update()
        {
            // Moves Enemy
            Move();

            // Calculate angle of Weapon to Player
            weaponAngle = Math.Atan(((double)player.Position.Y - (double)position.Y) / ((double)player.Position.X - (double)position.X));
            
            if(player.Position.X < position.X)
            {
                weaponAngle -= Math.PI;
            }

            // Update the Weapon
            if(weapon!=null)
                weapon.Update(position, weaponAngle);
        }

        /// <summary>
        /// Draws the Enemy object.
        /// </summary>
        /// <param name="sb">The SpriteBatch object.</param>
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            if(weapon!=null)
                weapon.Draw(sb);
        }
    }
}
