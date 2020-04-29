// Namespace References
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Abstract parent class for Character type GameObjects.
    /// </summary>
    abstract class Character : GameObject
    {
        // Fields
        protected int health;
        protected Weapon weapon;

        // Properties
        /// <summary>
        /// Gets and sets the Character's Health.
        /// </summary>
        public int Health { get { return health; } set { health = value; } }

        /// <summary>
        /// Gets the Weapon that the Character is using.
        /// </summary>
        public Weapon Weapon { get { return weapon; } }

        // Constructors
        /// <summary>
        /// Constructs a Character GameObject.
        /// </summary>
        /// <param name="position">The Rectangle representing the Character's position and size.</param>
        /// <param name="texture">The Texture2D representing the Character's texture.</param>
        /// <param name="health">The Character's health.</param>
        /// <param name="weapon">The Character's weapon.</param>
        public Character(Rectangle position, Texture2D texture, int health, Weapon weapon) : base(position, texture)
        {
            // Initialize Fields
            this.health = health;
            this.weapon = weapon;
        }
    }
}
