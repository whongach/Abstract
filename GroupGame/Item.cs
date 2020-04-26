// Namespace References
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Class for Item type Collectibles.
    /// </summary>
    class Item : Collectible
    {
        // Constructors
        /// <summary>
        /// Constructs an Item Collectible.
        /// </summary>
        /// <param name="position">The Rectangle representing the Item's position and size.</param>
        /// <param name="texture">The Texture2D representing the Item's texture.</param>
        /// <param name="collected">Whether or not the Item is collected.</param>
        public Item(Rectangle position, Texture2D texture, bool collected) : base(position, texture, collected) { }

        // Methods
        /// <summary>
        /// Draws the Item if it is not Collected.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the Item.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // If the Item is not Collected
            if (!Collected)
            {
                // Draw the Item
                base.Draw(spriteBatch);
            }
        }
    }
}