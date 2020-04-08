using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace for the Game.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Parent class for Character type GameObjects.
    /// </summary>
    abstract class Character : GameObject
    {
        // Fields
        protected int health;
        protected Weapon weapon;

        // Properties
        /// <summary>
        /// Gets the Weapon that the Character is using.
        /// </summary>
        public Weapon Weapon { get { return weapon; } }

        /// <summary>
        /// Gets and sets the Character's Health.
        /// </summary>
        public int Health { get { return health; } set { health = value; } }

        // Constructors
        /// <summary>
        /// Constructs a Character GameObject that is Collidable.
        /// </summary>
        /// <param name="health">The Character's Health.</param>
        /// <param name="weapon">The Character's Weapon.</param>
        /// <param name="position">The Character's position.</param>
        /// <param name="sprite">The Character's texture.</param>
        /// <param name="circular">Whether or not the Character's shape is circular or rectangular.</param>
        public Character(int health, Weapon weapon, Rectangle position, Texture2D sprite) : base(position, sprite)
        {
            this.health = health;
            this.weapon = weapon;
        }
    }
}
