// Namespace References
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Abstract class for Collectible type GameObjects.
    /// </summary>
    abstract class Collectible : GameObject
    {
        // Fields
        private bool collected;

        // Properties 
        /// <summary>
        /// Gets and sets whether the Collectible has been Picked Up or not.
        /// </summary>aaaaa
        public bool Collected { get { return collected; } set { collected = value; } }

        // Constructors
        /// <summary>
        /// Constructs a Collectible GameObject.
        /// </summary>
        /// <param name="position">The Rectangle representing the Collectible's position and size.</param>
        /// <param name="texture">The Texture2D representing the Collectible's texture.</param>
        /// <param name="collected">Whether or not the Collectible is collected.</param>
        public Collectible(Rectangle position, Texture2D texture, bool collected) : base(position, texture)
        {
            // Initialize fields
            this.collected = collected;
        }

        // Methods
        /// <summary>
        /// Draws the Collectible in the inventory.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the Collectible GameObject.</param>
        /// <param name="displayPosition">The Rectangle representing the Collectible's display position and size</param>
        public virtual void Draw(SpriteBatch spriteBatch, Rectangle displayPosition)
        {
            // Draw the Collectible
            spriteBatch.Draw(texture, displayPosition, Color.White);
        }
    }
}
