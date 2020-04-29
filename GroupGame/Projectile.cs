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
    /// Class for Projectile type GameObjects.
    /// </summary>
    class Projectile : GameObject
    {
        // Fields
        private double angle;
        private double x;
        private double y;
        private int damage;
        private int speed;
        private Vector2 textureOrigin;

        // Properties
        /// <summary>
        /// Gets or sets the angle of the Projectile.
        /// </summary>
        public double Angle
        {
            // Gets the angle of the Projectile
            get { return angle; }

            // Sets the angle of the Projectile in radians
            set { angle = value % 2 * Math.PI; }
        }

        /// <summary>
        /// Gets or sets the damage of the Projectile.
        /// </summary>
        public int Damage { get { return damage; } set { damage = value; } }

        /// <summary>
        /// Gets or sets the speed of the Projectile.
        /// </summary>
        public int Speed { get { return speed; } set { speed = value; } }

        // Constructors
        /// <summary>
        /// Constructs a Projectile ammunition type.
        /// </summary>
        /// <param name="size">The Point representing the size of the Projectile.</param>
        /// <param name="texture">The Texture2D representing the Projectile's texture.</param>
        /// <param name="speed">The number of pixels that the Projectile travels every Update.</param>
        public Projectile(Point size, Texture2D texture, int speed) : base(new Rectangle(Point.Zero, size), texture)
        {
            // Initialize Fields
            this.angle = 0;
            this.x = position.X;
            this.y = position.Y;
            this.damage = 0;
            this.speed = speed;
            this.textureOrigin = new Vector2(position.Width / 2, position.Height / 2);
        }

        /// <summary>
        /// Constructs a Projectile fired by a RangedWeapon.
        /// </summary>
        /// <param name="position">The Rectangle representing the Projectile's position and size.</param>
        /// <param name="texture">The Texture2D representing the Projectile's texture.</param>
        /// <param name="angle">The angle from the Player to the MouseCursor.</param>
        /// <param name="damage">The damage that the Projectile deals.</param>
        /// <param name="speed">The number of pixels that the Projectile travels every Update.</param>
        public Projectile(Rectangle position, Texture2D texture, double angle, int damage, int speed) : base(position, texture)
        {
            // Initialize Fields
            this.angle = angle;
            this.x = position.X;
            this.y = position.Y;
            this.damage = damage;
            this.speed = speed;
            this.textureOrigin = new Vector2(position.Width / 2, position.Height / 2);
        }

        // Methods
        /// <summary>
        /// Updates the Projectile's position.
        /// </summary>
        public override void Update()
        {
            // Calculate the x and y positions of the Projectile
            x += Math.Cos(angle) * speed;
            y += Math.Sin(angle) * speed;

            // Update the x and y positions of the Projectile
            position.X = (int)x;
            position.Y = (int)y;
        }

        /// <summary>
        /// Draws the Projectile.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the Projectile.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Update Projectile to draw in the correct position
            position.X += position.Width / 2;
            position.Y += position.Height / 2;

            // Draw the Projectile
            spriteBatch.Draw(texture,
                             position,
                             null,
                             Color.White,
                             (float)angle,
                             textureOrigin,
                             SpriteEffects.None,
                             1);
        }

        /// <summary>
        /// Destroys the Projectile.
        /// </summary>
        public void Destroy()
        {
            // Set the damage so that GameObjects do not take damage
            damage = 0;
        }
    }
}
