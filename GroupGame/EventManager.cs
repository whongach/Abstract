﻿using Microsoft.Xna.Framework;
using System;

/// <summary>
/// The namespace for the Game.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Class to manage events.
    /// </summary>
    class EventManager
    {
        /// <summary>
        /// Checks if two shapes collide.
        /// </summary>
        /// <param name="hit1">Object one rectangle.</param>
        /// <param name="hit2">Object two rectangle.</param>
        /// <returns>True if the shapes collide, false otherwise.</returns>
        public bool CollisionCheck(Rectangle hit1, Rectangle hit2)
        {
            return hit1.Intersects(hit2);
        }
        

        /// <summary>
        /// Deals body damage to the player if it collides with an Enemy and checks the Player against the Enemy's Weapon
        /// </summary>
        /// <param name="obj1">The Player.</param>
        /// <param name="obj2">The Enemy to be checked.</param>
        public void Collision(Player obj1, Enemy obj2)
        {
            if(CollisionCheck(obj1.Position, obj2.Position))
            {
                // Check if the Player is in debug mode
                if (!obj1.Debug)
                    obj1.Health -= obj2.BodyDamage;
                Collision((Character)obj1, (Tile)new Tile(new Rectangle(obj2.Position.X - 15, obj2.Position.Y - 15, obj2.Position.Width + 30, obj2.Position.Height + 30), obj2.Sprite, true));
            }

            if (obj2.Weapon != null)
                Collision((Character)obj1, obj2.Weapon);
        }

        /// <summary>
        /// allows the user to pick up a weapon if their inventory is empty and checks all attacks against any character
        /// </summary>
        /// <param name="obj1">the character to be checked for attacks or pick up the weapon</param>
        /// <param name="obj2">the weapon in question</param>
        public void Collision(Character obj1, Weapon obj2)
        {
            if (CollisionCheck(obj1.Position, obj2.Position))
            {
                if(obj1 is Player)
                {
                    if (!obj2.PickedUp && ((Player)obj1).OffHand == null)
                    {
                        ((Player)obj1).OffHand = obj2;
                        obj2.PickedUp = true;
                    }
                }
                else
                {
                }
                
            }


            if (obj2 is MeleeWeapon)
                Collision(obj1, (MeleeWeapon)obj2);

            if (obj2 is RangedWeapon)
                Collision(obj1, (RangedWeapon)obj2);
        }

        /// <summary>
        /// picks up the item if the player's inventory is empty
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        public void Collision(Player obj1, Item obj2)
        {
            if (CollisionCheck(obj1.Position, obj2.Position))
            {
                if (obj1.CurrentItem == null && !obj2.PickedUp)
                {
                    obj1.CurrentItem = obj2;
                    obj2.PickedUp = true;
                }

            }
        }

        /// <summary>
        /// deals damage to the character if the meleeweapon is attacking and stops the attack
        /// </summary>
        /// <param name="obj1">the character to be checked</param>
        /// <param name="obj2">the melee weapon to be checked</param>
        public void Collision(Character obj1, MeleeWeapon obj2)
        {
            if (CollisionCheck(obj1.Position, obj2.Position))
            {
                if (obj2.Attacking)
                {
                    obj1.Health -= obj2.Damage;
                    obj2.Attacking = false;
                }
            }
        }

        /// <summary>
        /// calls the collision for each of the projectiles from this weapon against the character
        /// </summary>
        /// <param name="obj1">the character to be attacked</param>
        /// <param name="obj2">the weapon to be checked</param>
        public void Collision(Character obj1, RangedWeapon obj2)
        {
            for (int i = 0; i < (obj2).Projectiles.Count; i++)
            {
                Collision((Character)obj1, (obj2).Projectiles[i]);
            }
        }

        /// <summary>
        /// deals damage and destroys the projectile 
        /// </summary>
        /// <param name="obj1">the character to be checked</param>
        /// <param name="obj2">the projectile to be checked</param>
        public void Collision(Character obj1, Projectile obj2)
        {
            if (CollisionCheck(obj1.Position, obj2.Position))
            {
                obj1.Health -= obj2.Damage;
                obj2.Destroy();
            }
        }

        /// <summary>
        /// calls the collision for a ranged weapon against the walls if the weapon is ranged
        /// </summary>
        /// <param name="obj1">the wall to be checked</param>
        /// <param name="obj2">the weapon to be checked</param>
        public void Collision(Tile obj1, Weapon obj2)
        {
            if (obj2 is RangedWeapon)
            {
                for (int i = 0; i < ((RangedWeapon)obj2).Projectiles.Count; i++)
                {
                    Collision(obj1, ((RangedWeapon)obj2).Projectiles[i]);
                }
            }
        }

        /// <summary>
        /// destroys the projectile 
        /// </summary>
        /// <param name="obj1">the wall to be checked</param>
        /// <param name="obj2">the projectile to be checked</param>
        public void Collision(Tile obj1, Projectile obj2)
        {
            if (CollisionCheck(obj1.Position, obj2.Position))
            {
                obj2.Destroy();
            }
        }

        /// <summary>
        /// pushes the character in question out of the wall
        /// </summary>
        /// <param name="obj1">character to be checked</param>
        /// <param name="obj2">wall to be checked</param>
        public void Collision(Character obj1, Tile obj2)
        {
            if (CollisionCheck(obj1.Position, obj2.Position))
            {
                //finds the overlap between the two shapes
                int xOverlap = 0;
                int yOverlap = 0;
                int xPos = 0;
                int yPos = 0;
                if (obj1.Position.X >= obj2.Position.X && obj1.Position.X <= obj2.Position.X + obj2.Position.Width)
                {
                    xOverlap = Math.Min(obj1.Position.Width, obj2.Position.X + obj2.Position.Width - obj1.Position.X);
                    xPos = obj1.Position.X;
                }
                else if (obj1.Position.X + obj1.Position.Width >= obj2.Position.X)
                {
                    xOverlap = Math.Min(obj2.Position.Width, obj1.Position.X + obj1.Position.Width - obj2.Position.X);
                    xPos = obj2.Position.Y;
                }
                else
                    return;
                if (obj1.Position.Y >= obj2.Position.Y && obj1.Position.Y <= obj2.Position.Y + obj2.Position.Height)
                {
                    yOverlap = Math.Min(obj1.Position.Height, obj2.Position.Y + obj2.Position.Height - obj1.Position.Y);
                    yPos = obj1.Position.Y;
                }
                else if (obj1.Position.Y + obj1.Position.Height >= obj2.Position.Y)
                {
                    yOverlap = Math.Min(obj2.Position.Height, obj1.Position.Y + obj1.Position.Height - obj2.Position.Y);
                    yPos = obj2.Position.Y;
                }
                else
                    return;

                //adjusts new location and sets the characters location to it
                Rectangle newLocation = obj1.Position;
                if (xOverlap < yOverlap)
                {
                    if (obj1.Position.X < obj2.Position.X)
                        newLocation.X -= xOverlap;
                    else
                        newLocation.X += xOverlap;
                }
                else if(xOverlap > yOverlap)
                {
                    if (obj1.Position.Y < obj2.Position.Y)
                        newLocation.Y -= yOverlap;
                    else
                        newLocation.Y += yOverlap;
                }
                
                obj1.Position = newLocation;
            }
        }

        /// <summary>
        /// if a game object is inputted, calls the appropriate collision and returns
        /// </summary>
        /// <param name="player"></param>
        /// <param name="gameObject"></param>
        public void Collision(Player player, GameObject gameObject)
        {
            if (gameObject is Enemy)
                Collision(player, (Enemy)gameObject);
            if (gameObject is Weapon)
                Collision((Character)player, (Weapon)gameObject);
            if (gameObject is Item)
                Collision(player, (Item)gameObject);
            return;
        }
    }
}
