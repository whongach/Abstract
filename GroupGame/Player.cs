// Generated Namespace References
using System;

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
    /// Class for Player type Characters.
    /// </summary>
    class Player : Character
    {
        // Fields
        private bool debug;
        private double angle;
        private double damageTaken;
        private double travelledDistance;
        private int keysCollected;
        private int speed;
        private Item currentItem;
        private Weapon offHand;

        // Properties
        /// <summary>
        /// Gets and sets whether or not the Player is in debug mode.
        /// </summary>
        public bool Debug { get { return debug; } set { debug = value; } }

        /// <summary>
        /// Gets the distance travelled by the Player.
        /// </summary>
        public double TravelledDistance { get { return travelledDistance; } set { travelledDistance = value; } }

        /// <summary>
        /// Gets the total damage taken by the Player.
        /// </summary>
        public double DamageTaken { get { return damageTaken; } set { damageTaken = value; } }

        /// <summary>
        /// Gets the total number of keys collected by the Player.
        /// </summary>
        public int KeysCollected { get { return keysCollected; } set { keysCollected = value; } }

        /// <summary>
        /// Gets and sets the speed of the Player.
        /// <summary>
        public int Speed { get { return speed; } set { speed = value; } }

        /// <summary>
        /// Gets and sets the currentItem that the Player has.
        /// </summary>
        public Item CurrentItem { get { return currentItem; } set { currentItem = value; } }

        /// <summary>
        /// Gets and sets the Weapon that the player has in the offHand.
        /// </summary>
        public Weapon OffHand { get { return offHand; } set { offHand = value; } }

        // Constructors
        /// <summary>
        /// Constructs a Player Character.
        /// </summary>
        /// <param name="position">The Rectangle representing the Player's position and size.</param>
        /// <param name="texture">The Texture2D representing the Player's texture.</param>
        /// <param name="health">The Player's health.</param>
        /// <param name="weapon">The Player's weapon.</param>
        public Player(Rectangle position, Texture2D texture, int health, Weapon weapon) : base(position, texture, health, weapon)
        {
            // Initialize Fields
            this.debug = false;
            this.angle = 0;
            this.damageTaken = 0;
            this.travelledDistance = 0;
            this.keysCollected = 0;
            this.speed = 4;
            this.currentItem = null;
            this.offHand = null;
        }

        // Methods
        /// <summary>
        /// Moves the Player's position and tracks the travelled distance.
        /// </summary>
        /// <param name="keyboardState">The current KeyboardState to get user input.</param>
        public void Move(KeyboardState keyboardState)
        {
            // Check if the user is telling the Player to move
            if (keyboardState.IsKeyDown(Keys.W))
            {
                // Move upward
                this.position.Y -= speed;

                // Increment the Player's travelled distance
                travelledDistance += speed;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                // Move to the left
                this.position.X -= speed;

                // Increment the Player's travelled distance
                travelledDistance += speed;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                // Move downward
                this.position.Y += speed;

                // Increment the Player's travelled distance
                travelledDistance += speed;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                // Move to the right
                this.position.X += speed;

                // Increment the Player's travelled distance
                travelledDistance += speed;
            }
        }

        /// <summary>
        /// Updates the Player.
        /// </summary>
        /// <param name="mouseState">The MouseState of the user's mouse.</param>
        /// <param name="previousMouseState">The previous MouseState of the user's mouse.</param>
        /// <param name="keyboardState">The KeyboardState of the user's keyboard.</param>
        /// <param name="previousKeyboardState">The previous KeyboardState of the user's keyboard.</param>
        public void Update(MouseState mouseState, MouseState previousMouseState, KeyboardState keyboardState, KeyboardState previousKeyboardState)
        {
            // Move the Player
            Move(keyboardState);

            // Calculate the angles from the Player to the MouseCursor
            if ((double)mouseState.X - (double)(position.X + position.Width / 2) == 0)
            {
                angle = Math.Atan(((double)mouseState.Y - (double)(position.Y + position.Height / 2)) / ((double)mouseState.X - (double)(position.X + position.Width / 2) - .00000001));
            }
            else
            {
                angle = Math.Atan(((double)mouseState.Y - (double)(position.Y + position.Height / 2)) / ((double)mouseState.X - (double)(position.X + position.Width / 2)));
            }

            // Flips the angle if it's on the right side of the Player
            if (mouseState.X <= position.X + position.Width / 2)
            {
                angle -= Math.PI;
            }

            // If the user switches Weapons by pressing Q
            if (keyboardState.IsKeyDown(Keys.Q) && !previousKeyboardState.IsKeyDown(Keys.Q))
            {
                // Temporary Fields
                Weapon previousWeapon;

                // Initialize Temporary Fields
                previousWeapon = weapon;

                // Change equipped Weapon to the offHand Weapon
                weapon = offHand;

                // Store the previously equipped Weapon in the offHand
                offHand = previousWeapon;
            }

            // If the user clicks
            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                // Attack with the equipped Weapon
                weapon.Attack();
            }

            // If there is an equipped Weapon
            if (weapon != null)
            {
                // Update the Weapon
                weapon.Update(position, angle);
            }

            // If there is an offHand Weapon
            if (offHand != null)
            {
                // Update the Weapon
                offHand.Update(position, angle);
            }
        }

        /// <summary>
        /// Draw the Player and the associated Weapons.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the Player and Weapon.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the Player
            base.Draw(spriteBatch);

            // If the Player has a Weapon
            if (weapon != null)
            {
                // Draw the Weapon
                weapon.Draw(spriteBatch);
            }

            // If the Player's OffHand is a RangedWeapon
            if (OffHand is RangedWeapon)
            {
                // Draw the Projectiles
                ((RangedWeapon)OffHand).DrawProjectiles(spriteBatch);
            }
        }
    }
}
