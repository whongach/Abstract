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
        /// Gets and sets the Weapon that the Character is using.
        /// </summary>
        public Weapon Weapon { get { return weapon; } set { weapon = value; } }

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
            if(weapon!=null){
                if(weapon is RangedWeapon)
                    this.weapon = new RangedWeapon((RangedWeapon)weapon);
                if(weapon is MeleeWeapon)
                    this.weapon = new MeleeWeapon((MeleeWeapon)weapon);
            }
        }
    }
}
