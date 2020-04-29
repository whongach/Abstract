// Generated Namespace References
using System.Collections.Generic;

// Namespace References
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Class for RangedWeapon type Weapons.
    /// </summary>
    class RangedWeapon : Weapon
    {
        // Fields
        private Projectile projectileType;
        private List<Projectile> projectiles;

        // Properties
        /// <summary>
        /// Gets or sets the ProjectileType of the RangedWeapon.
        /// </summary>
        public Projectile ProjectileType { get { return projectileType; } set { projectileType = value; } }

        /// <summary>
        /// Gets or sets the Projectiles emitted from the RangedWeapon.
        /// </summary>
        public List<Projectile> Projectiles { get { return projectiles; } set { projectiles = value; } }

        // Constructors
        /// <summary>
        /// Constructs an uncollected RangedWeapon Weapon on the ground.
        /// </summary>
        /// <param name="position">The Rectangle representing the RangedWeapon's position and size.</param>
        /// <param name="texture">The Texture2D representing the RangedWeapon's texture.</param>
        /// <param name="damage">The damage that the RangedWeapon deals.</param>
        /// <param name="projectileType">The type of Projectile the RangedWeapon emits.</param>
        public RangedWeapon(Rectangle position, Texture2D texture, int damage, Projectile projectileType) : base(position, texture, false, damage)
        {
            // Initialize Fields
            this.projectileType.Damage = damage;
            this.projectileType = projectileType;
            this.projectiles = new List<Projectile>();
        }

        /// <summary>
        /// Constructs a RangedWeapon Weapon in the Player's hand.
        /// </summary>
        /// <param name="size">The Point representing the size of the RangedWeapon.</param>
        /// <param name="texture">The Texture2D representing the RangedWeapon's texture.</param>
        /// <param name="damage">The damage that the RangedWeapon deals.</param>
        /// <param name="projectileType">The type of Projectile the RangedWeapon emits.</param>
        public RangedWeapon(Point size, Texture2D texture, int damage, Projectile projectileType) : base(new Rectangle(Point.Zero, size), texture, true, damage)
        {
            // Initialize Fields
            this.projectileType = projectileType;
            this.projectileType.Damage = damage;
            projectiles = new List<Projectile>();
        }

        /// <summary>
        /// Constructs a copy of a Ranged Weapon.
        /// </summary>
        /// <param name="ranged">The Weapon to copy.</param>
        public RangedWeapon(RangedWeapon ranged) : base(ranged)
        {
            // Initialize Fields
            this.projectileType = ranged.projectileType;
            this.projectileType.Damage = ranged.damage;
            projectiles = new List<Projectile>();
        }

        // Methods
        /// <summary>
        /// Creates a Projectile at the Player's position.
        /// </summary>
        public override void Attack()
        {
            // Calculate Projectile locations
            int projX = position.X + position.Width / 2 - projectileType.Position.Width / 2;
            int projY = position.Y + position.Height / 2 - projectileType.Position.Height / 2;

            // Add new Projectile to emitted Projectiles List
            projectiles.Add(new Projectile(
                new Rectangle(projX, projY, projectileType.Position.Width, projectileType.Position.Height),
                projectileType.Texture,
                angle,
                damage,
                projectileType.Speed));
        }

        /// <summary>
        /// Updates the Projectiles and the position of the RangedWeapon.
        /// </summary>
        /// <param name="position">The Rectangle representing the Player's position and size.</param>
        /// <param name="angle">The angle from the Player to the MouseCursor.</param>
        public override void Update(Rectangle position, double angle)
        {
            // Updates the position of the RangedWeapon
            base.Update(position, angle);

            // Loop through each Projectile
            for (int i = 0; i < projectiles.Count; i++)
            {
                // Update the Projectile's position
                projectiles[i].Update();

                // If the Projectile no longer deals damage
                if (projectiles[i].Damage == 0)
                {
                    // Remove the Projectile from the List and readjust
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Draws the Projectiles.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the Projectiles.</param>
        public void DrawProjectiles(SpriteBatch spriteBatch)
        {
            // Loops through each Projectile
            for (int i = 0; i < projectiles.Count; i++)
            {
                // Draw the Projectile
                projectiles[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Draws the RangedWeapon and the Projectiles.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the RangedWeapon and Projectiles.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the RangedWeapon
            base.Draw(spriteBatch);

            // Draw the Projectiles
            DrawProjectiles(spriteBatch);
        }
    }
}
