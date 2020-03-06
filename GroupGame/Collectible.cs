using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace for the Game.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Abstract class for Collectible items.
    /// </summary>
    abstract class Collectible : GameObject, ICollidable
    {
        // Fields
        private bool circleBox;
        private bool pickedUp;

        // Properties 
        /// <summary>
        /// Gets whether the Collectible is a Circle or a Box.
        /// </summary>
        public bool CircleBox { get { return circleBox; } }

        /// <summary>
        /// Gets and sets whether the Collectible has been Picked Up or not.
        /// </summary>
        public bool PickedUp { get { return pickedUp; } set { pickedUp = value; } }

        // Constructors
        /// <summary>
        /// Constructs a Collectible GameObject that is Collidable.
        /// </summary>
        /// <param name="position">The Collectible's position.</param>
        /// <param name="sprite">The Collectible's sprite.</param>
        /// <param name="circular">Whether or not the Collectible's shape is circular or rectangular.</param>
        /// <param name="equipped">Where or not the Collectible is equipped.</param>
        public Collectible(Rectangle position, Texture2D sprite, bool circular, bool equipped) : base(position, sprite)
        {
            // Initialize fields
            circleBox = circular;
            pickedUp = equipped;
        }

        // Methods
        /// <summary>
        /// Draw method for items in the inventory.
        /// </summary>
        /// <param name="sb">A SpriteBatch object.</param>
        /// <param name="displayPos">The position to draw the inventory box.</param>
        public virtual void Draw(SpriteBatch sb, Rectangle displayPos)
        {
            // Draw the collectible
            sb.Draw(sprite, displayPos, Color.White);
        }
    }
}
