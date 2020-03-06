using Microsoft.Xna.Framework;
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
        /// <param name="circle1">Whether or not object one is a circle.</param>
        /// <param name="circle2">Whether or not object two is a circle.</param>
        /// <returns>True if the shapes collide, false otherwise.</returns>
        public bool CollisionCheck(Rectangle hit1, Rectangle hit2, bool circle1, bool circle2)
        {
            // Checks rectangles
            if (!circle1 && !circle2)
                return hit1.Intersects(hit2);

            // Checks circles
            // Calculate circular properties
            int radius1 = Math.Min(hit1.Width / 2, hit1.Height / 2);
            int radius2 = Math.Min(hit2.Width / 2, hit2.Height / 2);
            Vector2 center1 = new Vector2((hit1.X + hit1.Width / 2), (hit1.Y + hit1.Height / 2));
            Vector2 center2 = new Vector2((hit2.X + hit2.Width / 2), (hit2.Y + hit2.Height / 2));
            Vector2 centerDist = new Vector2(Math.Abs((center1.X - center2.X)), Math.Abs((center1.Y - center2.Y)));

            // Compares distance by sum of radii
            if (circle1 && circle2)
                return Math.Sqrt(Math.Pow(centerDist.X, 2) + Math.Pow(centerDist.Y, 2)) <= (radius1 + radius2);

            // Code to check rectangle and circle collision (Blindman67 on StackOverflow)
            if (circle1 && !circle2)
            {
                if (centerDist.X >= radius1 + center2.X || centerDist.Y >= radius1 + center2.Y)
                    return false;
                if (centerDist.X < hit2.Width/2 || centerDist.Y < hit2.Height/2)
                    return true;
                centerDist.X -= center2.X;
                centerDist.Y -= center2.Y;
                if(Math.Sqrt(Math.Pow(centerDist.X, 2) + Math.Pow(centerDist.Y, 2))<=radius1)
                    return true;
                return false;
            }

            // Reverse prior method
            if (!circle1 && circle2)
            {
                if (centerDist.X >= radius2 + center1.X || centerDist.Y >= radius2 + center1.Y)
                    return false;
                if (centerDist.X < hit1.Width/2 || centerDist.Y < hit2.Height/2)
                    return true;
                centerDist.X -= center1.X;
                centerDist.Y -= center1.Y;
                if (Math.Sqrt(Math.Pow(centerDist.X, 2) + Math.Pow(centerDist.Y, 2)) <= radius2)
                    return true;
            }

            // If nothing collides, return false
            return false;
        }
        

        /// <summary>
        /// Deals body damage to the player if it collides with an Enemy and checks the Player against the Enemy's Weapon
        /// </summary>
        /// <param name="obj1">The Player.</param>
        /// <param name="obj2">The Enemy to be checked.</param>
        public void Collision(Player obj1, Enemy obj2)
        {
            if(CollisionCheck(obj1.Position, obj2.Position, obj1.CircleBox, obj2.CircleBox))
            {
                // Check if the Player is in debug mode
                if (!obj1.Debug)
                    obj1.Health -= obj2.BodyDamage;
                Collision((Character)obj1, (Wall)new Wall(new Rectangle(obj2.Position.X - 15, obj2.Position.Y - 15, obj2.Position.Width + 30, obj2.Position.Height + 30), obj2.Sprite));
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
            if (CollisionCheck(obj1.Position, obj2.Position, obj1.CircleBox, obj2.CircleBox))
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
            if (CollisionCheck(obj1.Position, obj2.Position, obj1.CircleBox, obj2.CircleBox))
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
            if (CollisionCheck(obj1.Position, obj2.Position, obj1.CircleBox, obj2.CircleBox))
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
            if (CollisionCheck(obj1.Position, obj2.Position, obj1.CircleBox, obj2.CircleBox))
            {
                obj1.Health -= obj2.Damage;
                obj2.Destroy();
            }
        }

        /// <summary>
        /// pushes the character in question out of the wall
        /// </summary>
        /// <param name="obj1">character to be checked</param>
        /// <param name="obj2">wall to be checked</param>
        public void Collision(Character obj1, Wall obj2)
        {
            if (CollisionCheck(obj1.Position, obj2.Position, obj1.CircleBox, obj2.CircleBox))
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
                if (xOverlap <= yOverlap)
                {
                    if (obj1.Position.X < obj2.Position.X)
                        newLocation.X -= xOverlap;
                    else
                        newLocation.X += xOverlap;
                }
                else
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
