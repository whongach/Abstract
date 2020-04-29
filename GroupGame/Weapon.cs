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
    /// Abstract class for Weapon type Collectibles.
    /// </summary>
    abstract class Weapon : Collectible
    {
        // Fields
        protected double angle;
        protected int damage;
        protected Vector2 textureOrigin;

        // Properties
        /// <summary>
        /// Gets the damage that the Weapon deals.
        /// </summary>
        public int Damage { get { return damage; } }

        // Constructors
        /// <summary>
        /// Constructs a Weapon Collectible.
        /// </summary>
        /// <param name="position">The Rectangle representing the Weapon's position and size.</param>
        /// <param name="texture">The Texture2D representing the Weapon's texture.</param>
        /// <param name="collected">Whether or not the Weapon is collected.</param>
        /// <param name="damage">The damage that the Weapon deals.</param>
        public Weapon(Rectangle position, Texture2D texture, bool collected, int damage) : base(position, texture, collected)
        {
            // Initialize Fields
            this.damage = damage;
            this.textureOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        /// <summary>
        /// Constructs a copy of a Weapon.
        /// </summary>
        /// <param name="weapon">The Weapon to copy.</param>
        public Weapon(Weapon weapon) : base(weapon.Position, weapon.Texture, weapon.Collected)
        {
            this.damage = weapon.Damage;
            this.textureOrigin = weapon.textureOrigin;
        }

        // Methods
        /// <summary>
        /// The required method for each Weapon to Attack.
        /// </summary>
        public abstract void Attack();

        /// <summary>
        /// Updates Weapon position to draw by the Player at the correct angle.
        /// </summary>
        /// <param name="position">The Rectangle representing the Player's position and size.</param>
        /// <param name="angle">The angle from the Player to the MouseCursor.</param>
        public virtual void Update(Rectangle position, double angle)
        {
            // Update the position of the Weapon
            this.position.X = position.X + position.Width / 2 + (int)(Math.Cos(angle) * position.Width / 2) - this.position.Width / 2;
            this.position.Y = position.Y + position.Height / 2 + (int)(Math.Sin(angle) * position.Height / 2) - this.position.Height / 2;

            // Update the angle of the Weapon
            this.angle = angle;
        }

        /// <summary>
        /// Draws the Weapon.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the Weapon.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the Weapon
            spriteBatch.Draw(texture,
                             new Rectangle(position.X + position.Width / 2, position.Y + position.Height / 2, position.Width, position.Height),
                             null,
                             Color.White,
                             (float)angle,
                             textureOrigin,
                             SpriteEffects.None,
                             1);
        }

        /// <summary>
        /// Draws hands associated with holding the Weapon.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the Weapon Collectible.</param>
        /// <param name="playerPosition">The Rectangle representing the Player's position and size.</param>
        /// <param name="handTexture">The Texture2D representing the Weapon's hand texture.</param>
        /// <param name="handColor">The Color of the hands holding the Weapon.</param>
        public virtual void DrawHands(SpriteBatch spriteBatch, Rectangle playerPosition, Texture2D handTexture, Color handColor)
        {
            // Draw the hand attached to the Weapon
            spriteBatch.Draw(handTexture, new Rectangle(
                             playerPosition.X + playerPosition.Width * 7 / 16 + (int)(playerPosition.Width / 2 * Math.Cos(angle)),
                             playerPosition.Y + playerPosition.Height * 7 / 16 + (int)(playerPosition.Height / 2 * Math.Sin(angle)),
                             playerPosition.Width / 8,
                             playerPosition.Height / 8),
                             handColor);

            // Draw the off hand
            if (angle < Math.PI / 2 && angle > 0 - Math.PI / 2)
            {
                spriteBatch.Draw(handTexture, new Rectangle(playerPosition.X + playerPosition.Width / 5, playerPosition.Y + playerPosition.Height / 2, playerPosition.Width / 8, playerPosition.Height / 8), handColor);
            }
            else
            {
                spriteBatch.Draw(handTexture, new Rectangle(playerPosition.X + playerPosition.Width * 28 / 40, playerPosition.Y + playerPosition.Height / 2, playerPosition.Width / 8, playerPosition.Height / 8), handColor);
            }
        }
    }
}
