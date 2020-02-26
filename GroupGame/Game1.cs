using Microsoft.Xna.Framework;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        // Methods
        /// <summary>
        /// Draws menu/GUI to the window based on the current game state.
        /// </summary>
        public void DrawGUI()
        {
            switch(gameState)
            {
                case GameState.MainMenu:

                    break;

                case GameState.Game:

                    break;

                case GameState.Pause:

                    break;

                case GameState.Shop:

                    break;

                case GameState.Stats:

                    break;

                case GameState.Gameover:

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
