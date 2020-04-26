// Namespace References
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Class for MouseCursor type GameObjects.
    /// </summary>
    class MouseCursor : GameObject
    {
        // Constructors
        /// <summary>
        /// Constructs a MouseCursor GameObject.
        /// </summary>
        /// <param name="position">The Rectangle representing the MouseCursor's position and size.</param>
        /// <param name="texture">The Texture2D representing the MouseCursor's texture.</param>
        public MouseCursor(Rectangle position, Texture2D texture) : base(position, texture) { }

        // Methods
        /// <summary>
        /// Updates the MouseCursor's position.
        /// </summary>
        /// <param name="mouseState">The MouseState of the user's mouse.</param>
        public void Update(MouseState mouseState)
        {
            // Updates the MouseCursor's position
            position.X = mouseState.X - position.Width / 2;
            position.Y = mouseState.Y - position.Height / 2;
        }
    }
}
