﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GroupGame
{
    /// <summary>
    /// Enumeration for various game states.
    /// </summary>
    public enum GameState { MainMenu, Game, Pause, Shop, Stats, Gameover }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // MonoGame Generated Fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Fields
        private KeyboardState previousKeyboardState;
        private KeyboardState keyboardState;
        private GameState gameState;

        //Graphic Fields
        SpriteFont title;
        SpriteFont buttons;
        SpriteFont stats;
        SpriteFont heading;

        // MonoGame Generated Constructors
        /// <summary>
        /// Constructs the Game class.
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Adjusts console window
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        // MonoGame Generated Methods
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initialize Fields
            gameState = GameState.MainMenu;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loads SpriteFonts
            title = Content.Load<SpriteFont>("Title");
            buttons = Content.Load<SpriteFont>("Buttons");
            stats = Content.Load<SpriteFont>("Stats");
            heading = Content.Load<SpriteFont>("Heading");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Get KeyboardState
            keyboardState = Keyboard.GetState();

            // Finite State Machine
            FiniteStateMachine();

            // Set previous KeyboardState to current state
            previousKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            DrawGUI(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        // Methods
        /// <summary>
        /// Draws menu/GUI to the window based on the current game state.
        /// </summary>
        public void DrawGUI(SpriteBatch sb)
        {
            switch(gameState)
            {
                //Draws text for Main Menu GUI
                case GameState.MainMenu:
                    sb.DrawString(title, "GAME TITLE", new Vector2(graphics.PreferredBackBufferWidth/2 - 220, 70), Color.Black);
                    sb.DrawString(buttons, "Start", new Vector2(graphics.PreferredBackBufferWidth/2 - 30, 300), Color.Black);
                    sb.DrawString(buttons, "Shop", new Vector2(graphics.PreferredBackBufferWidth/2 - 31, 380), Color.Black);
                    sb.DrawString(buttons, "Stats", new Vector2(graphics.PreferredBackBufferWidth/2 - 31, 460), Color.Black);
                    break;

                //Draws text for in-game GUI
                case GameState.Game:
                    sb.DrawString(stats, "HP: 300/300", new Vector2(2, 2), Color.Black);
                    sb.DrawString(stats, "Score:", new Vector2(1100, 2), Color.Black);
                    sb.DrawString(stats, "Currency:", new Vector2(1100, 22), Color.Black);
                    sb.DrawString(stats, "Keys:", new Vector2(1100, 42), Color.Black);
                    break;

                //Draws text for pause menu GUI
                case GameState.Pause:
                    sb.DrawString(heading, "PAUSED", new Vector2(graphics.PreferredBackBufferWidth/2 - 65, 200), Color.Black);
                    sb.DrawString(buttons, "Resume", new Vector2(graphics.PreferredBackBufferWidth/2 - 50, 300), Color.Black);
                    sb.DrawString(buttons, "Shop", new Vector2(graphics.PreferredBackBufferWidth/2 - 35, 380), Color.Black);
                    sb.DrawString(buttons, "Quit", new Vector2(graphics.PreferredBackBufferWidth/2 - 30, 460), Color.Black);
                    break;

                //Draws text for shop GUI
                case GameState.Shop:
                    sb.DrawString(heading, "SHOP", new Vector2(graphics.PreferredBackBufferWidth/2 - 45, 50), Color.Black);
                    sb.DrawString(buttons, "Back", new Vector2(graphics.PreferredBackBufferWidth/2 - 33, 640), Color.Black);
                    break;

                //Draws text for stats GUI
                case GameState.Stats:
                    sb.DrawString(heading, "STATS", new Vector2(graphics.PreferredBackBufferWidth/2 - 50, 50), Color.Black);
                    sb.DrawString(buttons, "Back", new Vector2(graphics.PreferredBackBufferWidth/2 - 33, 640), Color.Black);
                    break;

                //Draws text for game over screen
                case GameState.Gameover:
                    sb.DrawString(title, "GAME OVER", new Vector2(graphics.PreferredBackBufferWidth/2 - 240, 200), Color.Black);
                    sb.DrawString(heading, "You died lol", new Vector2(680, 300), Color.Black);
                    sb.DrawString(buttons, "Menu", new Vector2(graphics.PreferredBackBufferWidth/2 - 100, 430), Color.Black);
                    sb.DrawString(buttons, "Quit", new Vector2(graphics.PreferredBackBufferWidth/2, 430), Color.Black);
                    break;
            }
        }

        /// <summary>
        /// Handles game transitions and game states.
        /// </summary>
        private void FiniteStateMachine()
        {
            switch(gameState)
            {
                case GameState.MainMenu:
                    // If the user presses "Enter" in the menu, start the game
                    // ** Temporary until menu button locations are available for mouse clicks
                    if (SingleKeyPress(Keys.Enter))
                        gameState = GameState.Game;

                    // If the user presses "S" in the menu, show the player's statistics
                    // ** Temporary until menu button locations are available for mouse clicks
                    if (SingleKeyPress(Keys.S))
                        gameState = GameState.Stats;

                    break;

                case GameState.Game:
                    // If the user presses "Escape" in the game, pause it
                    if (SingleKeyPress(Keys.Escape))
                        gameState = GameState.Pause;

                    // Handle Here:
                    // Player Movement
                    // Enemy Movement
                    // Collisions
                    // Loading Next Level
                    // Game Over Scenarios

                    break;

                case GameState.Pause:
                    // If the user presses "Escape" in the pause menu, return to the game
                    if (SingleKeyPress(Keys.Escape))
                        gameState = GameState.Game;
                    

                    // If the user presses "S" in the pause menu, go to the shop
                    // ** Temporary until pause button locations are available for mouse clicks
                    if (SingleKeyPress(Keys.S))
                        gameState = GameState.Shop;
                    

                    // If the user presses "Q" in the pause menu, go to the game over screen
                    // ** Temporary until pause button locations are available for mouse clicks
                    if (SingleKeyPress(Keys.Q))
                        gameState = GameState.Gameover;

                    break;

                case GameState.Shop:
                    // If the user presses "Escape" in the shop menu, return to the pause menu
                    // ** Temporary until shop button locations are available for mouse clicks
                    if (SingleKeyPress(Keys.Escape))
                        gameState = GameState.Pause;
                    
                    // Handle Here:
                    // Player Updates

                    break;

                case GameState.Stats:
                    // If the user presses "Escape" in the stats menu, return to the menu
                    // ** Temporary until stats button locations are available for mouse clicks
                    if (SingleKeyPress(Keys.Escape))
                        gameState = GameState.MainMenu;

                    break;

                case GameState.Gameover:
                    // If the user presses "Enter" in the gameover scenario, return to the menu
                    // ** Temporary until gameover button locations are available for mouse clicks
                    if (SingleKeyPress(Keys.Enter))
                        gameState = GameState.MainMenu;

                    break;
            }
        }

        /// <summary>
        /// Checks if the specified key is down in current state, but not in previous state.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if this is the first frame that the key was pressed and false otherwise.</returns>
        private bool SingleKeyPress(Keys key)
        {
            // Check is key was recently released, if it was, return false (not looking for release)
            if (keyboardState.IsKeyDown(key) == false && previousKeyboardState.IsKeyDown(key) == true)
                return false;

            return keyboardState.IsKeyDown(key) != previousKeyboardState.IsKeyDown(key);
        }
    }
}
