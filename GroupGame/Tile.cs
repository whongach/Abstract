// Namespace References
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Class for Tile type GameObjects.
    /// </summary>
    class Tile : GameObject
    {
        // Fields
        private readonly bool collidable;

        // Properties
        /// <summary>
        /// Gets whether or not the Tile has collisions.
        /// </summary>
        public bool Collidable { get { return collidable; } }

        // Constructors
        /// <summary>
        /// Constructs a Tile GameObject.
        /// </summary>
        /// <param name="position">The Rectangle representing the Tile's position and size.</param>
        /// <param name="texture">The Texture2D representing the Tile's texture.</param>
        /// <param name="collidable">Whether or not the Tile has collisions.</param>
        public Tile(Rectangle position, Texture2D texture, bool collidable) : base(position, texture)
        {
            // Initialize Fields
            this.collidable = collidable;
        }
    }
}
