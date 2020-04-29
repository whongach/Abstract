// Generated Namespace References
using System;

// Namespace References
using Microsoft.Xna.Framework;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Class to manage events.
    /// </summary>
    class EventManager
    {
        // Methods
        /// <summary>
        /// Checks if two Rectangles collide.
        /// </summary>
        /// <param name="rectangle1">A Rectangle object.</param>
        /// <param name="rectangle2">A different Rectangle object.</param>
        /// <returns>True if the Rectangles collide, false otherwise.</returns>
        public bool CollisionCheck(Rectangle rectangle1, Rectangle rectangle2)
        {
            // Return whether or not the two Rectangles collide
            return rectangle1.Intersects(rectangle2);
        }


        /// <summary>
        /// Damages the Player if they collide with an Enemy.
        /// </summary>
        /// <param name="player">The Player.</param>
        /// <param name="enemy">An Enemy to check the Player against.</param>
        public void Collision(Player player, Enemy enemy)
        {
            // If the Player and the Enemy collide
            if (CollisionCheck(player.Position, enemy.Position))
            {
                // If the Player is not in debug mode
                if (!player.Debug)
                {
                    // Deal damage to the Player
                    player.Health -= enemy.BodyDamage;
                }

                // Call the Collision between the Player and Tile to produce a knock back
                Collision((Character)player, new Tile(new Rectangle(enemy.Position.X - 15, enemy.Position.Y - 15, enemy.Position.Width + 30, enemy.Position.Height + 30), enemy.Texture, true));
            }

            // If the Enemy has a Weapon
            if (enemy.Weapon != null)
            {
                // Call the Collision between the Player and the Enemy's Weapon
                Collision((Character)player, enemy.Weapon);
            }
        }

        /// <summary>
        /// Adds a Weapon to the Player's inventory if it's empty and checks attacks against Characters.
        /// </summary>
        /// <param name="character">A Character.</param>
        /// <param name="weapon">A Weapon to check the Character against.</param>
        public void Collision(Character character, Weapon weapon)
        {
            // If the Character and the Weapon collide
            if (CollisionCheck(character.Position, weapon.Position))
            {
                // If the Character is a Player
                if (character is Player)
                {
                    // If the Weapon is not collected and the Player doesn't have a Weapon in their offHand
                    if (!weapon.Collected && ((Player)character).OffHand == null)
                    {
                        // Equip the Weapon to the Player's offHand
                        ((Player)character).OffHand = weapon;

                        // Set the Weapon's collection status to true
                        weapon.Collected = true;
                    }
                }
            }

            // If the Weapon is a MeleeWeapon
            if (weapon is MeleeWeapon)
            {
                // Call the Collision between the Character and the MeleeWeapon
                Collision(character, (MeleeWeapon)weapon);
            }

            // If the Weapon is a RangedWeapon
            if (weapon is RangedWeapon)
            {
                // Call the Collision between the Character and the RangedWeapon
                Collision(character, (RangedWeapon)weapon);
            }
        }

        /// <summary>
        /// Adds an Item to the Player's inventory if it's empty.
        /// </summary>
        /// <param name="player">The Player.</param>
        /// <param name="item">An Item to check the Player against.</param>
        public void Collision(Player player, Item item)
        {
            // If the Player and the Item collide
            if (CollisionCheck(player.Position, item.Position))
            {
                // If the Player doesn't have an Item and the Item is not collected
                if (player.CurrentItem == null && !item.Collected)
                {
                    // Add the Item to the Player's inventory
                    player.CurrentItem = item;

                    // Set the Item state to collected
                    item.Collected = true;
                }
            }
        }

        /// <summary>
        /// Damages the Character if they collide with a MeleeWeapon.
        /// </summary>
        /// <param name="character">A Character.</param>
        /// <param name="meleeWeapon">A MeleeWeapon to check the Character against.</param>
        public void Collision(Character character, MeleeWeapon meleeWeapon)
        {
            // If the Character and the MeleeWeapon collide
            if (CollisionCheck(character.Position, meleeWeapon.Position))
            {
                // If the MeleeWeapon is in the attack state
                if (meleeWeapon.Attacking)
                {
                    // Reduce the Character's health
                    character.Health -= meleeWeapon.Damage;

                    // Stop the attack
                    meleeWeapon.Attacking = false;
                }
            }
        }

        /// <summary>
        /// Damages the Character if they collide with a RangedWeapon's Projectile.
        /// </summary>
        /// <param name="character">A Character.</param>
        /// <param name="rangedWeapon">A RangedWeapon to check the Character against.</param>
        public void Collision(Character character, RangedWeapon rangedWeapon)
        {
            // Loop through the RangedWeapon's Projectiles
            for (int i = 0; i < rangedWeapon.Projectiles.Count; i++)
            {
                // Call the Collision between the Character and the Projectile
                Collision(character, rangedWeapon.Projectiles[i]);
            }
        }

        /// <summary>
        /// Damages the Character if they collide with a Projectile and destroys the Projectile.
        /// </summary>
        /// <param name="character">A Character.</param>
        /// <param name="projectile">A Projectile to check the Character against.</param>
        public void Collision(Character character, Projectile projectile)
        {
            // If the Character and the Projectile collide
            if (CollisionCheck(character.Position, projectile.Position))
            {
                // Deal damage to the Character
                character.Health -= projectile.Damage;

                // Destroy the Projectile
                projectile.Destroy();
            }
        }

        /// <summary>
        /// Destroys RangedWeapon's Projectiles if they collide with a wall.
        /// </summary>
        /// <param name="tile">A wall.</param>
        /// <param name="weapon">The Weapon to check the wall against.</param>
        public void Collision(Tile tile, Weapon weapon)
        {
            // If the Weapon is a RangedWeapon
            if (weapon is RangedWeapon)
            {
                // Loop through the RangedWeapon's Projectiles
                for (int i = 0; i < ((RangedWeapon)weapon).Projectiles.Count; i++)
                {
                    // Call the Collision between the Tile and the Projectile
                    Collision(tile, ((RangedWeapon)weapon).Projectiles[i]);
                }
            }
        }

        /// <summary>
        /// Destroys the Projectile if it collides with the wall.
        /// </summary>
        /// <param name="tile">A wall.</param>
        /// <param name="projectile">The Projectile to check the wall against.</param>
        public void Collision(Tile tile, Projectile projectile)
        {
            // If the Tile and the Projectile collide
            if (CollisionCheck(tile.Position, projectile.Position))
            {
                // Destroy the Projectile
                projectile.Destroy();
            }
        }

        /// <summary>
        /// Keeps Characters from going through wall Tiles.
        /// </summary>
        /// <param name="character">A Character.</param>
        /// <param name="tile">The wall Tile to check the Character against.</param>
        public void Collision(Character character, Tile tile)
        {
            // If the Character and the Tile collide
            if (CollisionCheck(character.Position, tile.Position))
            {
                // Temporary Fields
                int xOverlap;
                int yOverlap;
                Rectangle adjustedLocation;

                // Initialize Fields
                adjustedLocation = character.Position;

                // Find the x overlap between the Character and Tile
                if (character.Position.X >= tile.Position.X && character.Position.X <= tile.Position.X + tile.Position.Width)
                {
                    xOverlap = Math.Min(character.Position.Width, tile.Position.X + tile.Position.Width - character.Position.X);
                }
                else if (character.Position.X + character.Position.Width >= tile.Position.X)
                {
                    xOverlap = Math.Min(tile.Position.Width, character.Position.X + character.Position.Width - tile.Position.X);
                }
                else
                {
                    return;
                }

                // Find the y overlap between the Character and Tile
                if (character.Position.Y >= tile.Position.Y && character.Position.Y <= tile.Position.Y + tile.Position.Height)
                {
                    yOverlap = Math.Min(character.Position.Height, tile.Position.Y + tile.Position.Height - character.Position.Y);
                }
                else if (character.Position.Y + character.Position.Height >= tile.Position.Y)
                {
                    yOverlap = Math.Min(tile.Position.Height, character.Position.Y + character.Position.Height - tile.Position.Y);
                }
                else
                {
                    return;
                }

                // If the Character overlaps less in x direction
                if (xOverlap < yOverlap)
                {
                    // If the Character was to the left of the Tile
                    if (character.Position.X < tile.Position.X)
                    {
                        // Adjust the location to the left
                        adjustedLocation.X -= xOverlap;
                    }
                    else
                    {
                        // Adjust the location to the right
                        adjustedLocation.X += xOverlap;
                    }
                }
                else if (xOverlap > yOverlap)
                {
                    // If the Character was above of the Tile
                    if (character.Position.Y < tile.Position.Y)
                    {
                        // Adjust the location upward
                        adjustedLocation.Y -= yOverlap;
                    }
                    else
                    {
                        // Adjust the location downward
                        adjustedLocation.Y += yOverlap;
                    }
                }

                // Adjust the Character's location
                character.Position = adjustedLocation;
            }
        }

        /// <summary>
        /// Checks a GameObject to a Player depending on the type of GameObject.
        /// </summary>
        /// <param name="player">The Player.</param>
        /// <param name="gameObject">The GameObject to check the Player against.</param>
        public void Collision(Player player, GameObject gameObject)
        {
            // If the GameObject is an Enemy
            if (gameObject is Enemy)
            {
                // Call the Collision between a Player and an Enemy
                Collision(player, (Enemy)gameObject);
            }

            // If the GameObject is a Weapon
            if (gameObject is Weapon)
            {
                // Call the collision between a Player and a Weapon
                Collision((Character)player, (Weapon)gameObject);
            }

            // If the GameObject is an Item
            if (gameObject is Item)
            {
                // Call the collision between a Player and an Item
                Collision(player, (Item)gameObject);
            }

            return;
        }
    }
}
