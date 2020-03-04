﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        private MouseState previousMouseState;
        private MouseState mouseState;
        private GameState gameState;

        //Object Fields
        MouseCursor cursor;
        List<GameObject> gameObjects;
        EventManager eM;

        //Graphic Fields
        SpriteFont title;
        SpriteFont buttons;
        SpriteFont stats;
        SpriteFont heading;
        Texture2D squareTest;
        Texture2D circleTest;
        Texture2D cursorTest;

        //AttackTesting Fields
        Player attackTest;
        Projectile basicArrow;
        RangedWeapon basicBow;
        RangedWeapon enemyBow;

        //Enemy Movement Test Fields
        Enemy enemyTest;

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

            //initializes the gameobjects list and event manager
            gameObjects = new List<GameObject>();
            eM = new EventManager();

            //allows window to resize
            Window.AllowUserResizing = true;

            //Loads SpriteFonts
            title = Content.Load<SpriteFont>("Title");
            buttons = Content.Load<SpriteFont>("Buttons");
            stats = Content.Load<SpriteFont>("Stats");
            heading = Content.Load<SpriteFont>("Heading");

            //Loads Textures
            squareTest = Content.Load<Texture2D>("square");
            circleTest = Content.Load<Texture2D>("circle");
            cursorTest = Content.Load<Texture2D>("mouseCursor");

            //creates a player, weapon and a projectile for attacking purposes
            basicArrow = new Projectile(0, new Rectangle(new Point(-20, -20), new Point(20, 5)), 20, 5, squareTest, false);
            basicBow = new RangedWeapon(basicArrow, new Rectangle(175, 175, 30, 30), squareTest, 5, false, true);
            attackTest = new Player(10, basicBow, new Rectangle(150, 150, 50, 50), circleTest, true);

            //Creates an enemy to test movement
            enemyBow = new RangedWeapon(basicArrow, new Rectangle(175, 175, 30, 30), squareTest, 5, false, true);
            enemyTest = new Enemy(10, enemyBow, new Rectangle(300, 300, 50, 50), circleTest, EnemyType.Chase, 0, 0, attackTest, true);
            gameObjects.Add(enemyTest);

            //creates the mousecursor
            cursor = new MouseCursor(new Rectangle(0, 0, 50, 50), cursorTest);
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
            mouseState = Mouse.GetState();

            //updates cursor
            cursor.Update(mouseState);

            // Finite State Machine
            FiniteStateMachineUpdate();

            // Set previous KeyboardState to current state
            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);
            spriteBatch.Begin();

            DrawGUI(spriteBatch);
            cursor.Draw(spriteBatch);
            FiniteStateMachineDraw();

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws menu/GUI to the window based on the current game state.
        /// </summary>
        public void DrawGUI(SpriteBatch sb)
        {
            switch(gameState)
            {
                //All cases draw for their appropriate gamestate
                case GameState.MainMenu:
                    //Draws buttons for main menu
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 290, 100, 60), Color.Gray);
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 370, 100, 60), Color.Gray);
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 450, 100, 60), Color.Gray);

                    //Draws text for main menu
                    sb.DrawString(title, "GAME TITLE", new Vector2(graphics.PreferredBackBufferWidth/2 - 220, 70), Color.Black);
                    sb.DrawString(buttons, "Start", new Vector2(graphics.PreferredBackBufferWidth/2 - 30, 300), Color.Black);
                    sb.DrawString(buttons, "Shop", new Vector2(graphics.PreferredBackBufferWidth/2 - 31, 380), Color.Black);
                    sb.DrawString(buttons, "Stats", new Vector2(graphics.PreferredBackBufferWidth/2 - 31, 460), Color.Black);
                    break;

                case GameState.Game:
                    //Draws underlying boxes for GUI
                    sb.Draw(squareTest, new Rectangle(0, 0, 300, 30), Color.LightGreen);
                    sb.Draw(squareTest, new Rectangle(Window.ClientBounds.Width-300, 0, 300, 80), Color.Gray);
                    sb.Draw(squareTest, new Rectangle(0, Window.ClientBounds.Height - 100, 100, 100), Color.Gray);
                    sb.Draw(squareTest, new Rectangle(0, Window.ClientBounds.Height - 150, 50, 50), Color.Gray);
                    sb.Draw(squareTest, new Rectangle(50, Window.ClientBounds.Height - 150, 50, 50), Color.Gray);
                    sb.Draw(squareTest, new Rectangle(2, Window.ClientBounds.Height - 148, 46, 46), Color.Black);
                    sb.Draw(squareTest, new Rectangle(52, Window.ClientBounds.Height - 148, 46, 46), Color.Black);
                    sb.Draw(squareTest, new Rectangle(2, Window.ClientBounds.Height - 98, 96, 96), Color.Black);


                    //Draws text for in-game GUI
                    sb.DrawString(stats, "HP: 300/300", new Vector2(2, 2), Color.Black);
                    sb.DrawString(stats, "Score:", new Vector2(Window.ClientBounds.Width - 290, 2), Color.Black);
                    sb.DrawString(stats, "Currency:", new Vector2(Window.ClientBounds.Width - 290, 22), Color.Black);
                    sb.DrawString(stats, "Keys:", new Vector2(Window.ClientBounds.Width - 290, 42), Color.Black);

                    //draws icons for GUI
                    if (attackTest.CurrentItem != null)
                        attackTest.CurrentItem.Draw(sb, new Rectangle(5, Window.ClientBounds.Height-145, 40, 40));
                    if (attackTest.Weapon != null)
                        attackTest.Weapon.Draw(sb, new Rectangle(5, Window.ClientBounds.Height-95, 90, 90));
                    if (attackTest.OffHand != null)
                        attackTest.OffHand.Draw(sb, new Rectangle(55, Window.ClientBounds.Height - 145, 40, 40));

                    break;

                case GameState.Pause:
                    //Draws buttons/boxes for pause menu
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 175, 150, 350, 400), Color.Gray);
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 63, 290, 130, 60), Color.DimGray);
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 63, 370, 130, 60), Color.DimGray);
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 63, 450, 130, 60), Color.DimGray);

                    //Draws text for pause menu
                    sb.DrawString(heading, "PAUSED", new Vector2(graphics.PreferredBackBufferWidth/2 - 65, 200), Color.Black);
                    sb.DrawString(buttons, "Resume", new Vector2(graphics.PreferredBackBufferWidth/2 - 50, 300), Color.Black);
                    sb.DrawString(buttons, "Shop", new Vector2(graphics.PreferredBackBufferWidth/2 - 35, 380), Color.Black);
                    sb.DrawString(buttons, "Quit", new Vector2(graphics.PreferredBackBufferWidth/2 - 30, 460), Color.Black);
                    break;

                case GameState.Shop:
                    //Draws buttons for shop menu
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 50, 630, 100, 60), Color.Gray);

                    //Draws text for shop menu
                    sb.DrawString(heading, "SHOP", new Vector2(graphics.PreferredBackBufferWidth/2 - 45, 50), Color.Black);
                    sb.DrawString(buttons, "Back", new Vector2(graphics.PreferredBackBufferWidth/2 - 33, 640), Color.Black);
                    break;

                case GameState.Stats:
                    //Draws buttons for stats menu
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 50, 630, 100, 60), Color.Gray);

                    //Draws text for stats menu
                    sb.DrawString(heading, "STATS", new Vector2(graphics.PreferredBackBufferWidth/2 - 50, 50), Color.Black);
                    sb.DrawString(buttons, "Back", new Vector2(graphics.PreferredBackBufferWidth/2 - 33, 640), Color.Black);
                    break;

                case GameState.Gameover:
                    //Draws buttons for gameover screen
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 130, 420, 100, 60), Color.Gray);
                    sb.Draw(squareTest, new Rectangle(graphics.PreferredBackBufferWidth / 2, 420, 100, 60), Color.Gray);

                    //Draws text for gameover screen
                    sb.DrawString(title, "GAME OVER", new Vector2(graphics.PreferredBackBufferWidth/2 - 240, 200), Color.Black);
                    sb.DrawString(heading, "You died lol", new Vector2(680, 300), Color.Black);
                    sb.DrawString(buttons, "Menu", new Vector2(graphics.PreferredBackBufferWidth/2 - 120, 430), Color.Black);
                    sb.DrawString(buttons, "Quit", new Vector2(graphics.PreferredBackBufferWidth/2 + 20, 430), Color.Black);
                    break;
            }
        }

        /// <summary>
        /// Handles game transitions and game states.
        /// </summary>
        private void FiniteStateMachineUpdate()
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
                    // All Updates of game objects


                    attackTest.Update(mouseState, previousMouseState, keyboardState, previousKeyboardState);
                    for (int i = 0; i<gameObjects.Count; i++)
                    {
                        gameObjects[i].Update();
                    }

                    //collisions
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        eM.Collision(attackTest, gameObjects[i]);
                        if(gameObjects[i] is Enemy)
                        {
                            if (attackTest.Weapon != null)
                                eM.Collision(((Enemy)gameObjects[i]), attackTest.Weapon);
                            if (attackTest.OffHand != null)
                                eM.Collision(((Enemy)gameObjects[i]), attackTest.OffHand);
                            /*for (int j = 0; j < testMap.Walls.Count; j++)
                                eM.Collision(((Enemy)gameObjects[i]), //insert reference to the walls list in the map here);*/
                        }
                    }

                    /*for (int i = 0; i < testMap.Walls.Count; i++)
                        eM.Collision((Character)attackTest, //insert reference to the walls list in the map here);*/

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
        /// Handles all drawing
        /// </summary>
        private void FiniteStateMachineDraw()
        {
            switch (gameState)
            {
                case GameState.MainMenu:
                    

                    break;

                case GameState.Game:

                    for(int i = 0; i<gameObjects.Count; i++)
                    {
                        attackTest.Draw(spriteBatch);
                        gameObjects[i].Draw(spriteBatch);
                    }
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
        /// Checks if the specified key is down in current state, but not in previous state.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if this is the first frame that the key was pressed and false otherwise.</returns>
        public bool SingleKeyPress(Keys key)
        {
            return keyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
    }
}
