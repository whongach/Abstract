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
    /// <summary>
    /// Class for MeleeWeapon type Weapons.
    /// </summary>
    class MeleeWeapon : Weapon
    {
        // Fields
        private bool attacking;
        private int attackFrame;
        private readonly int rotationDegrees;
        private readonly int rotationSpeed;
        private readonly int totalFrames;

        // Properties
        /// <summary>
        /// Gets or sets whether the MeleeWeapon is in the process of attacking.
        /// </summary>
        public bool Attacking { get { return attacking; } set { attacking = value; } }

        // Constructors
        /// <summary>
        /// Constructs a spinning MeleeWeapon Weapon in the Character's hand.
        /// </summary>
        /// <param name="size">The Point representing the size of the MeleeWeapon.</param>
        /// <param name="texture">The Texture2D representing the MeleeWeapon's texture.</param>
        /// <param name="damage">The damage that the MeleeWeapon deals.</param>
        /// <param name="rotationDegrees">The number of degrees that the MeleeWeapon should rotate.</param>
        /// <param name="rotationSpeed">The speed that the MeleeWeapon should rotate.</param>
        public MeleeWeapon(Point size, Texture2D texture, int damage, int rotationDegrees, int rotationSpeed) : base(new Rectangle(Point.Zero, size), texture, true, damage)
        {
            // Initialize Fields
            this.attacking = false;
            this.attackFrame = 0;
            this.rotationDegrees = rotationDegrees;
            this.rotationSpeed = rotationSpeed;
            this.totalFrames = rotationDegrees / rotationSpeed;
        }

        /// <summary>
        /// Constructs a MeleeWeapon Weapon in the Character's hand.
        /// </summary>
        /// <param name="size">The Point representing the size of the MeleeWeapon.</param>
        /// <param name="texture">The Texture2D representing the MeleeWeapon's texture.</param>
        /// <param name="damage">The damage that the MeleeWeapon deals.</param>
        /// <param name="totalFrames">The number of frames that it takes for the MeleeWeapon to attack.</param>
        public MeleeWeapon(Point size, Texture2D texture, int damage, int totalFrames) : base(new Rectangle(Point.Zero, size), texture, true, damage)
        {
            // Initialize Fields
            this.attacking = false;
            this.attackFrame = 0;
            this.rotationDegrees = 0;
            this.rotationSpeed = 0;
            this.totalFrames = totalFrames;
        }

        /// <summary>
        /// Constructs an uncollected spinning MeleeWeapon Weapon on the ground.
        /// </summary>
        /// <param name="position">The Rectangle representing the MeleeWeapon's position and size.</param>
        /// <param name="texture">The Texture2D representing the MeleeWeapon's texture.</param>
        /// <param name="damage">The damage that the MeleeWeapon deals.</param>
        /// <param name="rotationDegrees">The number of degrees that the MeleeWeapon should rotate.</param>
        /// <param name="rotationSpeed">The speed that the MeleeWeapon should rotate.</param>
        public MeleeWeapon(Rectangle position, Texture2D texture, int damage, int rotationDegrees, int rotationSpeed) : base(position, texture, false, damage)
        {
            // Initialize Fields
            this.attacking = false;
            this.attackFrame = 0;
            this.rotationDegrees = rotationDegrees;
            this.rotationSpeed = rotationSpeed;
            this.totalFrames = rotationDegrees / rotationSpeed;
        }

        /// <summary>
        /// Constructs an uncollected MeleeWeapon Weapon on the ground.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture">The Texture2D representing the MeleeWeapon's texture.</param>
        /// <param name="damage">The damage that the MeleeWeapon deals.</param>
        /// <param name="totalFrames">The number of frames that it takes for the MeleeWeapon to attack.</param>
        public MeleeWeapon(Rectangle position, Texture2D texture, int damage, int totalFrames) : base(position, texture, false, damage)
        {
            // Initialize Fields
            this.attacking = false;
            this.attackFrame = 0;
            this.rotationDegrees = 0;
            this.rotationSpeed = 0;
            this.totalFrames = totalFrames;
        }

        /// <summary>
        /// Constructs a copy of a Melee Weapon.
        /// </summary>
        /// <param name="melee">The MeleeWeapon to copy.</param>
        public MeleeWeapon(MeleeWeapon melee) : base(melee)
        {
            // Initialize Fields
            this.attacking = false;
            this.attackFrame = 0;
            this.rotationDegrees = melee.rotationDegrees;
            this.rotationSpeed = melee.rotationSpeed;
            this.totalFrames = melee.totalFrames;
        }

        // Methods
        /// <summary>
        /// Sets the MeleeWeapon to attack.
        /// </summary>
        public override void Attack()
        {
            // If the MeleeWeapon is not attacking and is at the original frame
            if (!attacking && attackFrame == 0)
            {
                // Attack
                attacking = true;
            }
        }

        /// <summary>
        /// Updates the position and the angle of the MeleeWeapon.
        /// </summary>
        /// <param name="position">The Rectangle representing the Player's position and size.</param>
        /// <param name="angle">The angle from the Player to the MouseCursor.</param>
        public override void Update(Rectangle position, double angle)
        {
            // Set the MeleeWeapon angle to the passed in angle
            this.angle = angle;

            // If the attack goes through it's totalFrames
            if (attackFrame >= totalFrames)
            {
                // Stop attacking
                attacking = false;

                // Reset the attackFrame
                attackFrame = 0;
            }

            // If the MeleeWeapon is attacking or is in attack frame
            if (attacking || attackFrame != 0)
            {
                // Update the angle and attackFrame
                this.angle = angle;
                attackFrame++;

                // If there is a set speed
                if (rotationSpeed != 0)
                {
                    // Update the angle of the MeleeWeapon based on the rotationSpeed
                    this.angle += (double)rotationDegrees / 180 * Math.PI / 2;
                    this.angle -= attackFrame * (double)rotationSpeed / 180 * Math.PI;
                }

                // Extends the MeleeWeapon's reach if attacking
                this.position.X = position.X + position.Width / 2 + (int)(Math.Cos(this.angle) * position.Width) - this.position.Width / 2;
                this.position.Y = position.Y + position.Height / 2 + (int)(Math.Sin(this.angle) * position.Height) - this.position.Height / 2;
            }
            else
            {
                // Reset the MeleeWeapon's reach and angle if they are not attacking
                this.position.X = position.X + position.Width / 2 + (int)(Math.Cos(angle) * position.Width / 2) - this.position.Width / 2;
                this.position.Y = position.Y + position.Height / 2 + (int)(Math.Sin(angle) * position.Height / 2) - this.position.Height / 2;
                this.angle = angle;
            }
        }

        /// <summary>
        /// Draws hands associated with holding the MeleeWeapon.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the MeleeWeapon.</param>
        /// <param name="playerPosition">The Rectangle representing the Player's position and size.</param>
        /// <param name="handTexture">The Texture2D representing the MeleeWeapon's hand texture.</param>
        /// <param name="handColor">The Color of the hands holding the Weapon.</param>
        public override void DrawHands(SpriteBatch spriteBatch, Rectangle playerPosition, Texture2D handTexture, Color handColor)
        {
            if (!attacking)
            {
                // Draw the hand attached the the MeleeWeapon
                spriteBatch.Draw(handTexture, new Rectangle(
                                 playerPosition.X + playerPosition.Width * 7 / 16 + (int)(playerPosition.Width / 5 * Math.Cos(angle)),
                                 playerPosition.Y + playerPosition.Height * 7 / 16 + (int)(playerPosition.Height / 5 * Math.Sin(angle)),
                                 playerPosition.Width / 8,
                                 playerPosition.Height / 8),
                                 handColor);

                // Draw the off hand
                if (angle < Math.PI / 2 && angle > 0 - Math.PI / 2)
                {
                    spriteBatch.Draw(handTexture, new Rectangle(
                                     playerPosition.X + playerPosition.Width / 5,
                                     playerPosition.Y + playerPosition.Height / 2,
                                     playerPosition.Width / 8,
                                     playerPosition.Height / 8),
                                     handColor);
                }
                else
                {
                    spriteBatch.Draw(handTexture, new Rectangle(
                                     playerPosition.X + playerPosition.Width * 28 / 40,
                                     playerPosition.Y + playerPosition.Height / 2,
                                     playerPosition.Width / 8,
                                     playerPosition.Height / 8),
                                     handColor);
                }
            }
            else
            {
                // Draw the hand attached the the MeleeWeapon
                spriteBatch.Draw(handTexture, new Rectangle(
                                 playerPosition.X + playerPosition.Width * 7 / 16 + (int)(playerPosition.Width * 3 / 5 * Math.Cos(angle)),
                                 playerPosition.Y + playerPosition.Height * 7 / 16 + (int)(playerPosition.Height * 3 / 5 * Math.Sin(angle)),
                                 playerPosition.Width / 8,
                                 playerPosition.Height / 8),
                                 handColor);

                // Draw the off hand
                if (angle < Math.PI / 2 && angle > 0 - Math.PI / 2)
                {
                    spriteBatch.Draw(handTexture, new Rectangle(
                                     playerPosition.X + playerPosition.Width / 5,
                                     playerPosition.Y + playerPosition.Height / 2,
                                     playerPosition.Width / 8,
                                     playerPosition.Height / 8),
                                     handColor);
                }
                else
                {
                    spriteBatch.Draw(handTexture, new Rectangle(
                                     playerPosition.X + playerPosition.Width * 28 / 40,
                                     playerPosition.Y + playerPosition.Height / 2,
                                     playerPosition.Width / 8,
                                     playerPosition.Height / 8),
                                     handColor);
                }
            }
        }
    }
}
