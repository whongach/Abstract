// Generated Namespace References
using System;

// Namespace References
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    // Enumerations
    /// <summary>
    /// Enumeration for various Enemy types.
    /// </summary>
    public enum EnemyMovementType { Chase, LeftRight, Random, Rectangle, UpDown }

    /// <summary>
    /// Enumeration for Enemy movement directions.
    /// </summary>
    public enum EnemyDirection { Down, Left, Right, Up }

    /// <summary>
    /// Class for Enemy type Characters.
    /// </summary>
    class Enemy : Character
    {
        // Fields
        private double weaponAngle;
        private readonly int attackInterval;
        private int bodyDamage;
        private int currentFrame;
        private readonly int maxHeight;
        private readonly int maxWidth;
        private int randomDirectionNumber;
        private readonly int speed;
        private int travelled;
        private readonly string name;
        private readonly EnemyMovementType enemyMovementType;
        private EnemyDirection enemyDirection;
        private readonly Player player;
        private readonly Random random;

        // Properties
        /// <summary>
        /// Gets the damage that the Enemy deals when it collides with something.
        /// </summary>
        public int BodyDamage { get { return bodyDamage; } set { bodyDamage = value; } }

        /// <summary>
        /// Gets the name of the Enemy.
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Gets the movement type of the enemy
        /// </summary>
        public EnemyMovementType MovementType
        {
            get { return enemyMovementType; }
        }

        // Constructors
        /// <summary>
        /// Constructs an Enemy Character.
        /// </summary>
        /// <param name="position">The Rectangle representing the Enemy's position and size.</param>
        /// <param name="texture">The Texture2D representing the Enemy's texture.</param>
        /// <param name="health">The Enemy's health.</param>
        /// <param name="weapon">The Enemy's Weapon.</param>
        /// <param name="attackInterval">The time between the Enemy's attacks.</param>
        /// <param name="bodyDamage">The damage the Enemy deals with it's body.</param>
        /// <param name="maxWidth">The distance the Enemy can walk in the x direction.</param>
        /// <param name="maxHeight">The disatance the Enemy can walk in the y direction.</param>
        /// <param name="speed">The Enemy's speed.</param>
        /// <param name="name">The Enemy's name.</param>
        /// <param name="enemyMovementType">The Enemy's type of movement.</param>
        /// <param name="player">The Player Character.</param>
        /// <param name="random">A Random number generator.</param>
        public Enemy(Rectangle position, Texture2D texture, int health, Weapon weapon, int attackInterval, int bodyDamage, int maxWidth, int maxHeight, int speed, string name, EnemyMovementType enemyMovementType, Player player, Random random) : base(position, texture, health, weapon)
        {
            // Initialize Fields
            this.attackInterval = attackInterval;
            this.bodyDamage = bodyDamage;
            this.currentFrame = random.Next(1000);
            this.maxHeight = maxHeight;
            this.maxWidth = maxWidth;
            this.speed = speed;
            this.travelled = 0;
            this.name = name;
            this.enemyMovementType = enemyMovementType;
            this.player = player;
            this.random = random;

            // Initialize start direction dependent on the EnemyMovementType
            if (enemyMovementType == EnemyMovementType.LeftRight)
            {
                enemyDirection = EnemyDirection.Right;
            }
            else if (enemyMovementType == EnemyMovementType.Random)
            {
                randomDirectionNumber = random.Next(0, 8);
            }
            else if (enemyMovementType == EnemyMovementType.Rectangle)
            {
                enemyDirection = EnemyDirection.Right;
            }
            else if (enemyMovementType == EnemyMovementType.UpDown)
            {
                enemyDirection = EnemyDirection.Down;
            }
        }

        /// <summary>
        /// Constructs a copy of an Enemy Character.
        /// </summary>
        /// <param name="enemy">The Enemy to copy.</param>
        public Enemy(Enemy enemy) : base(new Rectangle(enemy.Position.X, enemy.Position.Y, enemy.Position.Width, enemy.Position.Height), enemy.Texture, enemy.Health, enemy.Weapon)
        {
            // Initialize Fields
            this.attackInterval = enemy.attackInterval;
            this.bodyDamage = enemy.bodyDamage;
            this.maxHeight = enemy.maxHeight;
            this.maxWidth = enemy.maxWidth;
            this.speed = enemy.speed;
            this.travelled = 0;
            this.name = enemy.name;
            this.enemyMovementType = enemy.enemyMovementType;
            this.player = enemy.player;
            this.random = enemy.random;
            this.currentFrame = random.Next(1000);

            // Initialize start direction dependent on the EnemyMovementType
            if (enemyMovementType == EnemyMovementType.LeftRight)
            {
                enemyDirection = EnemyDirection.Right;
            }
            else if (enemyMovementType == EnemyMovementType.Random)
            {
                randomDirectionNumber = random.Next(0, 8);
            }
            else if (enemyMovementType == EnemyMovementType.Rectangle)
            {
                enemyDirection = EnemyDirection.Right;
            }
            else if (enemyMovementType == EnemyMovementType.UpDown)
            {
                enemyDirection = EnemyDirection.Down;
            }
        }

        // Methods
        /// <summary>
        /// Moves the Enemy based on it's movement pattern.
        /// </summary>
        public void Move()
        {
            // Check EnemyMovementType and move the Enemy in that manner
            if (enemyMovementType == EnemyMovementType.Chase)
            {
                Chase(player);
            }
            else if (enemyMovementType == EnemyMovementType.LeftRight)
            {
                LRWalk(maxWidth);
            }
            else if (enemyMovementType == EnemyMovementType.Rectangle)
            {
                RectangleWalk(maxHeight, maxWidth);
            }
            else if (enemyMovementType == EnemyMovementType.UpDown)
            {
                UDWalk(maxHeight);
            }
            else
            {
                RandomWalk(maxWidth);
            }

            // Increment the frame that the Enemy is on
            currentFrame++;

            // If the Enemy has a Weapon
            if (weapon != null)
            {
                // Attack every attackInterval
                if (currentFrame%(50*attackInterval) == 0)
                {
                    weapon.Attack();
                }
            }
        }

        /// <summary>
        /// Moves the Enemy in the Chase movement pattern.
        /// </summary>
        /// <param name="player">The Player Character.</param>
        protected void Chase(Player player)
        {
            // Check the Player's x position and move Enemy towards it
            if (player.Position.X > position.X)
            {
                if (player.Position.X - position.X < speed)
                {
                    position.X += player.Position.X - position.X;
                }
                else
                {
                    position.X += speed;
                }
            }
            else if (player.Position.X < position.X)
            {
                if (position.X - player.Position.X < speed)
                {
                    position.X -= position.X - player.Position.X;
                }
                else
                {
                    position.X -= speed;
                }
            }

            // Check the Player's y position and move Enemy towards it
            if (player.Position.Y > position.Y)
            {
                if (player.Position.Y - position.Y < speed)
                {
                    position.Y += player.Position.Y - position.Y;
                }
                else
                {
                    position.Y += speed;
                }
            }
            else if (player.Position.Y < position.Y)
            {
                if (position.Y - player.Position.Y < speed)
                {
                    position.Y -= position.Y - player.Position.Y;
                }
                else
                {
                    position.Y -= speed;
                }
            }
        }

        /// <summary>
        /// Moves the Enemy in the LeftRight movement pattern.
        /// </summary>
        /// <param name="pathLength">The length of the path for the Enemy to move along.</param>
        protected void LRWalk(int pathLength)
        {
            // Track the number of pixels the Enemy moves
            travelled += speed;

            // Check direction for the Enemy and move appropriately
            if (enemyDirection == EnemyDirection.Right)
            {
                position.X += speed;
            }
            else
            {
                position.X -= speed;
            }

            // Checks if Enemy has traveled full distance of pattern direction
            if (travelled >= pathLength)
            {
                // Reset distance traveled
                travelled = 0;

                // Reverse the Enemy's direction
                if (enemyDirection == EnemyDirection.Right)
                {
                    enemyDirection = EnemyDirection.Left;
                }
                else
                {
                    enemyDirection = EnemyDirection.Right;
                }
            }
        }

        /// <summary>
        /// Moves the Enemy in the Random movement pattern
        /// </summary>
        /// <param name="pathLength">The length of the path for the Enemy to move along.</param>
        protected void RandomWalk(int pathLength)
        {
            // Track the number of pixels the Enemy moves
            travelled += speed;

            // Check direction and move in that direction
            if (randomDirectionNumber == 0)
            {
                position.X += speed;
            }
            else if (randomDirectionNumber == 1)
            {
                position.X -= speed;
            }
            else if (randomDirectionNumber == 2)
            {
                position.Y -= speed;
            }
            else if (randomDirectionNumber == 3)
            {
                position.Y += speed;
            }
            else if (randomDirectionNumber == 4)
            {
                position.X += speed;
                position.Y -= speed;
            }
            else if (randomDirectionNumber == 5)
            {
                position.X += speed;
                position.Y += speed;
            }
            else if (randomDirectionNumber == 6)
            {
                position.X -= speed;
                position.Y -= speed;
            }
            else
            {
                position.X -= speed;
                position.Y += speed;
            }

            // Changes direction if follows the traveled distance
            if (travelled >= pathLength)
            {
                // Reset distance traveled
                travelled = 0;

                // Change the Enemy's direction randomly
                randomDirectionNumber = random.Next(0, 8);
            }
        }

        /// <summary>
        /// Moves the Enemy in the Rectangle movement pattern.
        /// </summary>
        /// <param name="pathXLength">The length of the path for the Enemy to move in the x direction.</param>
        /// <param name="pathYLength">The length of the path for the Enemy to move in the y direction.</param>
        protected void RectangleWalk(int pathXLength, int pathYLength)
        {
            // Track the number of pixels the Enemy moves
            travelled += speed;

            // Check direction for the Enemy and move appropriately
            if (enemyDirection == EnemyDirection.Right)
            {
                // Move in the x direction
                position.X += speed;

                // Change direction if distance has been traveled
                if (travelled >= pathXLength)
                {
                    // Reset distance traveled
                    travelled = 0;

                    // Change the Enemy's direction
                    enemyDirection = EnemyDirection.Down;
                }
            }
            else if (enemyDirection == EnemyDirection.Left)
            {
                // Move in the x direction
                position.X -= speed;

                // Change direction if distance has been traveled
                if (travelled >= pathXLength)
                {
                    // Reset distance traveled
                    travelled = 0;

                    // Change the Enemy's direction
                    enemyDirection = EnemyDirection.Up;
                }
            }
            else if (enemyDirection == EnemyDirection.Up)
            {
                // Move in the y direction
                position.Y -= speed;

                // Change direction if distance has been traveled
                if (travelled >= pathYLength)
                {
                    // Reset distance traveled
                    travelled = 0;

                    // Change the Enemy's direction
                    enemyDirection = EnemyDirection.Right;
                }
            }
            else
            {
                // Move in the y direction
                position.Y += speed;

                // Change direction if distance has been traveled
                if (travelled >= pathYLength)
                {
                    // Reset distance traveled
                    travelled = 0;

                    // Change the Enemy's direction
                    enemyDirection = EnemyDirection.Left;
                }
            }
        }

        /// <summary>
        /// Moves the Enemy in the UpDown movement pattern.
        /// </summary>
        /// <param name="pathLength">The length of the path for the Enemy to move along.</param>
        protected void UDWalk(int pathLength)
        {
            // Track the number of pixels the Enemy moves
            travelled += speed;

            // Check direction of Enemy and move appropriately
            if (enemyDirection == EnemyDirection.Down)
            {
                position.Y += speed;
            }
            else
            {
                position.Y -= speed;
            }

            // Change direction if distance has been traveled
            if (travelled >= pathLength)
            {
                // Reset distance traveled
                travelled = 0;

                // Reverse the Enemy's direction
                if (enemyDirection == EnemyDirection.Down)
                {
                    enemyDirection = EnemyDirection.Up;
                }
                else
                {
                    enemyDirection = EnemyDirection.Down;
                }
            }
        }

        /// <summary>
        /// Updates the Enemy Character.
        /// </summary>
        public override void Update()
        {
            // Move the Enemy
            Move();

            // Calculate the angle of the Weapon to the Player
            weaponAngle = Math.Atan(((double)player.Position.Y - (double)position.Y) / ((double)player.Position.X - (double)position.X));

            if (player.Position.X < position.X)
            {
                weaponAngle -= Math.PI;
            }

            // Update the Weapon
            if (weapon != null)
            {
                weapon.Update(position, weaponAngle);
            }
        }

        /// <summary>
        /// Draws the Enemy and the Weapon.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the Enemy and Weapon.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the Enemy
            base.Draw(spriteBatch);

            // If the Enemy has a Weapon
            if (weapon != null)
            {
                // Draw the Weapon
                weapon.Draw(spriteBatch);
            }
        }
    }
}
