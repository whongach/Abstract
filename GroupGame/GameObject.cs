// Namespace References
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Abstract class for objects in the game.
    /// </summary>
    abstract class GameObject
    {
        // Fields
        protected Rectangle position;
        protected Texture2D texture;

        // Properties
        /// <summary>
        /// Gets or sets the Rectangle position of the GameObject.
        /// </summary>
        public Rectangle Position { get { return position; } set { position = value; } }

        /// <summary>
        /// Gets or sets the Texture2D texture of the GameObject.
        /// </summary>
        public Texture2D Texture { get { return texture; } set { texture = value; } }

        // Constructors
        /// <summary>
        /// Constructs a GameObject.
        /// </summary>
        /// <param name="position">The Rectangle representing the GameObject's position and size.</param>
        /// <param name="texture">The Texture2D representing the GameObject's texture.</param>
        public GameObject(Rectangle position, Texture2D texture)
        {
            // Initialize Fields
            this.position = position;
            this.texture = texture;
        }

        // Methods
        /// <summary>
        /// The method for each GameObject to Update.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// The default method for drawing a GameObject.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the GameObject.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draw the GameObject using it's texture and position
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
