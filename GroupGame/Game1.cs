using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// The namespace for the Game.
/// </summary>
namespace GroupGame
{
    // Enumerations
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
        private GameState previousGameState;
        private GameState gameState;
        private Player player;
        private int tileSize;
        private Point mapOrigin;

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
        Texture2D bowTest;
        Texture2D arrowTest;
        Texture2D swordTest;
        Texture2D spellTest;
        Texture2D wandTest;
        Texture2D keyTest;
        Texture2D playerTest;
        Texture2D wallTest;
        Texture2D floorTest;

        //AttackTesting Fields
        Projectile basicArrow;
        Projectile basicSpell;
        RangedWeapon basicBow;
        RangedWeapon enemyWand;
        MeleeWeapon basicSword;
        MeleeWeapon basicSpear;

        //Enemy Movement Test Fields
        Enemy enemyTest;
        List<Enemy> enemies;

        //ResourceManager Fields
        List<Map> maps;
        Map currentMap;
        Tile topBarrier;
        Tile bottomBarrier;
        List<Weapon> resourceWeapons;
        List<Enemy> resourceEnemies;

        

        //Item test fields
        Item key;

        //Random field
        Random rng;

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
            enemies = new List<Enemy>();
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
            bowTest = Content.Load<Texture2D>("bow");
            arrowTest = Content.Load<Texture2D>("arrow");
            swordTest = Content.Load<Texture2D>("sword");
            spellTest = Content.Load<Texture2D>("spell");
            wandTest = Content.Load<Texture2D>("wand");
            keyTest = Content.Load<Texture2D>("key");
            playerTest = Content.Load<Texture2D>("playerTest");
            floorTest = Content.Load<Texture2D>("floorTest");
            wallTest = Content.Load<Texture2D>("wallTest");

            //initializes random variables
            rng = new Random();

            //creates the mousecursor
            cursor = new MouseCursor(new Rectangle(0, 0, 50, 50), cursorTest);

            //calls method to load resources
            LoadResources();

            // Set to true if testing [DEBUG MODE]
            player.Debug = false;
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
            GraphicsDevice.Clear(Color.TransparentBlack);
            spriteBatch.Begin();

            FiniteStateMachineDraw();
            DrawGUI(spriteBatch);
            cursor.Draw(spriteBatch);

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
                    sb.DrawString(stats, $"HP: {player.Health}/300", new Vector2(2, 2), Color.Black);
                    sb.DrawString(stats, "Score:", new Vector2(Window.ClientBounds.Width - 290, 2), Color.Black);
                    sb.DrawString(stats, "Currency:", new Vector2(Window.ClientBounds.Width - 290, 22), Color.Black);
                    sb.DrawString(stats, "Keys:", new Vector2(Window.ClientBounds.Width - 290, 42), Color.Black);

                    //draws icons for GUI
                    if (player.CurrentItem != null)
                        player.CurrentItem.Draw(sb, new Rectangle(5, Window.ClientBounds.Height-145, 40, 40));
                    if (player.Weapon != null)
                        player.Weapon.Draw(sb, new Rectangle(5, Window.ClientBounds.Height-95, 90, 90));
                    if (player.OffHand != null)
                        player.OffHand.Draw(sb, new Rectangle(55, Window.ClientBounds.Height - 145, 40, 40));

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
                    sb.DrawString(heading, "STATS", new Vector2(graphics.PreferredBackBufferWidth/2 - 50, 50), Color.White);
                    sb.DrawString(buttons, "Back", new Vector2(graphics.PreferredBackBufferWidth/2 - 33, 640), Color.Black);

                    // Draws text for tracked stats
                    sb.DrawString(buttons, "Keys Collected: ", new Vector2(graphics.PreferredBackBufferWidth / 2 - 400, 150), Color.White);
                    sb.DrawString(buttons, "Monsters Defeated: ", new Vector2(graphics.PreferredBackBufferWidth / 2 - 400, 200), Color.White);
                    sb.DrawString(buttons, "Rooms Cleared: ", new Vector2(graphics.PreferredBackBufferWidth / 2 - 400, 250), Color.White);
                    sb.DrawString(buttons, "Distance Travelled: " + player.DistTravelled, new Vector2(graphics.PreferredBackBufferWidth / 2 - 400, 300), Color.White);
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
                    // If the user presses "Enter" in the menu (or presses the button), start the game
                    if (SingleKeyPress(Keys.Enter) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 290, 100, 60)))
                    {
                        gameState = GameState.Game;

                        // Disallow window resizing
                        Window.AllowUserResizing = false;


                        NextLevel();
                    }

                    // If the user presses "B" (or presses the button) in the menu, show the shop
                    if (SingleKeyPress(Keys.B) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 370, 100, 60)))
                    {
                        previousGameState = gameState;
                        gameState = GameState.Shop;
                    }

                    // If the user presses "S" (or presses the button) in the menu, show the statistics
                    if (SingleKeyPress(Keys.S) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 450, 100, 60)))
                    {
                        gameState = GameState.Stats;
                    }

                    break;

                case GameState.Game:
                    // If the user presses "Escape" in the game, pause it
                    if (SingleKeyPress(Keys.Escape))
                    {
                        gameState = GameState.Pause;
                    }

                    // If the Player health drops below zero, transition to Gameover GameState
                    if (player.Health <= 0)
                        gameState = GameState.Gameover;

                    // Handle Here:
                    // All Updates of game objects
                    

                    //updates of player, objects, and enemies
                    player.Update(mouseState, previousMouseState, keyboardState, previousKeyboardState);
                    for (int i = 0; i<gameObjects.Count; i++)
                    {
                        gameObjects[i].Update();
                    }
                    for(int i = 0; i<enemies.Count; i++)
                    {
                        enemies[i].Update();
                    }
                    

                    //collisions
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        eM.Collision(player, gameObjects[i]);
                    }
                    for(int i = 0; i<enemies.Count; i++)
                    {
                        eM.Collision(player, enemies[i]);
                        if (player.Weapon != null)
                            eM.Collision(enemies[i], player.Weapon);
                        if (player.OffHand != null)
                            eM.Collision(enemies[i], player.OffHand);
                        for (int j = 0; j < currentMap.Walls.Count; j++)
                            eM.Collision(enemies[i], currentMap.Walls[j]);
                    }
                    for (int i = 0; i < currentMap.Walls.Count; i++)
                    {
                        eM.Collision((Character)player, currentMap.Walls[i]);
                        if (player.Weapon != null)
                            eM.Collision(currentMap.Walls[i], player.Weapon);
                        if (player.OffHand != null)
                            eM.Collision(currentMap.Walls[i], player.OffHand);
                    }
                    eM.Collision((Character)player, topBarrier);
                    eM.Collision((Character)player, bottomBarrier);
                    
                    
                    //removes dead enemies from the list
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].Health <= 0)
                            enemies.RemoveAt(i);
                    }



                    // Loading Next Level
                    if (enemies.Count == 0 && player.CurrentItem == key && player.Position.Y <= 5)
                    {
                        NextLevel();
                    }
                    // Game Over Scenarios

                    break;

                case GameState.Pause:
                    // If the user presses "Escape" (or presses the button) in the pause menu, return to the game
                    if (SingleKeyPress(Keys.Escape) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 63, 290, 130, 60)))
                    {
                        gameState = GameState.Game;
                    }

                    // If the user presses "B" (or presses the button) in the pause menu, go to the shop
                    if (SingleKeyPress(Keys.B) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 63, 370, 130, 60)))
                    {
                        previousGameState = gameState;
                        gameState = GameState.Shop;
                    }

                    // If the user presses "Q" (or presses the button) in the pause menu, go to the game over screen
                    if (SingleKeyPress(Keys.Q) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 63, 450, 130, 60)))
                    {
                        gameState = GameState.Gameover;
                    }

                    break;

                case GameState.Shop:
                    // If the user presses "Escape" (or presses the button) in the shop menu, return to the pause menu
                    if (SingleKeyPress(Keys.Escape) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 50, 630, 100, 60)))
                    {
                        gameState = previousGameState;
                    }

                    // Handle Here:
                    // Player Updates

                    break;

                case GameState.Stats:
                    // If the user presses "Escape" (or presses the button) in the stats menu, return to the menu
                    if (SingleKeyPress(Keys.Escape) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 50, 630, 100, 60)))
                    {
                        gameState = GameState.MainMenu;
                    }

                    break;

                case GameState.Gameover:
                    // If the user presses "Enter" (or presses the button) in the gameover scenario, return to the menu
                    if (SingleKeyPress(Keys.Enter) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 130, 420, 100, 60)))
                    {
                        gameState = GameState.MainMenu;
                    }

                    // If the user presses "Escape" (or presses the button) in the gameover scenario, quit the game
                    if (SingleKeyPress(Keys.Escape) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2, 420, 100, 60)))
                    {
                        Exit();
                    }

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

                    currentMap.Draw(spriteBatch);
                    for (int i = 0; i<gameObjects.Count; i++)
                    {
                        gameObjects[i].Draw(spriteBatch);
                    }
                    player.Draw(spriteBatch);
                    player.Weapon.DrawHands(spriteBatch, player.Position, circleTest, Color.Black);
                    for (int i = 0; i<enemies.Count; i++)
                    {
                        enemies[i].Draw(spriteBatch);
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

        /// <summary>
        /// Checks if the mouse is down in current state, but not in previous state, within a specified rectangle.
        /// </summary>
        /// <param name="rectangle">The area to check if the mouse is within.</param>
        /// <returns>True if this is the first frame that the key was pressed and false otherwise.</returns>
        public bool SingleMousePress(Rectangle rectangle)
        {
            return mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && rectangle.Contains(mouseState.Position);
        }


        /// <summary>
        /// Loads all maps, weapons and enemies from the resource file
        /// </summary>
        public void LoadResources()
        {
            // Test objects
            basicArrow = new Projectile(new Point(20, 5), 20, arrowTest);
            basicBow = new RangedWeapon(2, basicArrow, new Point(40, 40), bowTest);
            basicSword = new MeleeWeapon(5, new Point(40, 40), swordTest, 90, 5);
            basicSpear = new MeleeWeapon(8, new Point(80, 40), swordTest, 20);
            player = new Player(10, basicSword, new Rectangle(150, 150, 50, 50), playerTest);
            player.OffHand = basicBow;
            basicSpell = new Projectile(new Point(20, 20), 12, spellTest);
            enemyWand = new RangedWeapon(1, basicSpell, new Point(50, 50), wandTest);
            key = new Item(new Rectangle(500, 500, 50, 50), keyTest, false);
            
            // Lists to hold resources
            maps = new List<Map>();
            resourceEnemies = new List<Enemy>();
            resourceWeapons = new List<Weapon>();

            // Open stream & reader
            FileStream resources = File.OpenRead("../../../../../Resources/master.rsrc");
            BinaryReader reader = new BinaryReader(resources);

            // Extra map fields
            int[,] tiles = new int[16, 16];
            int tileSize = 60;

            // Extra enemy fields
            string currentEnemyName;
            int[] currentEnemyFields;

            // Extra weapon fields
            string[] currentWeaponFields;
            int[] weaponFieldsParsed;

            // While there is unread data, continue looping
            while (reader.PeekChar() != -1)
            {
                // Read a string that says "Map" "Enemy" or "Weapon"
                switch (reader.ReadString())
                {
                    case "Map":
                        
                        // Skip file name
                        reader.ReadString();

                        // Assign all tiles to list
                        for (int i = 0; i < 16; i++) 
                        {
                            for (int j = 0; j < 16; j++) 
                            {
                                tiles[i, j] = reader.ReadInt32();
                            }
                        }

                        // Add map to list
                        maps.Add(new Map(wallTest, floorTest, tileSize, tiles));
                        break;

                        
                    case "Enemy":
                        // Enemy File Format (6 parameters from .enemy file): health, damage, attackSpeed, speed, xCoord, yCoord
                        //    -  All items are read in as strings but parsed to: (int, int, int, int, int, int)

                        // Stores name of the enemy file
                        currentEnemyName = reader.ReadString();

                        // Read all values of the enemy
                        currentEnemyFields = new int[6];

                        // Read in 6 parameters
                        for (int i = 0; i < 6; i++)
                        {
                            currentEnemyFields[i] = int.Parse(reader.ReadString());
                        }

                        // Add the enemy to the list of imported resource enemies
                        resourceEnemies.Add(new Enemy(currentEnemyFields[0],  // health
                                                      null, // weapon
                                                      new Rectangle(currentEnemyFields[4], currentEnemyFields[5], 10, 10), // adjust width and height later
                                                      squareTest, // enemy texture (square for now)
                                                      EnemyType.Random, // movement pattern (enum)
                                                      50, // max travelling width - needs a default value here, and then is adjusted when enemy is placed in a room
                                                      50, // max travelling height - same as above
                                                      currentEnemyFields[3], // travel speed
                                                      currentEnemyFields[2], // attack speed
                                                      currentEnemyFields[1], // body damage
                                                      player, // player 
                                                      currentEnemyName));
                        // End enemy case
                        break;

                        //####### TO-DO: Integrate this list of enemies into the game.. some helpful info: ######################################################
                        // - The enemies brought in do not have sizes, textures, set movement patterns, or max travelling dimensions
                        // - Note that the current texture used is a square test texture
                        // - I restate, in the .rsrc file, those ints are stored as strings.  They are strings since they come out of the Enemy external tool.
                        //   They are then parsed to an array of ints here, and act as ints. No need to TryParse, the external tool only allows integers > 0 to be used.


                        
                    case "Weapon":
                        // Weapon File Format (4 parameters from .weapon file): name, damage, durability, type
                        //    -  All items are read in as strings, but the ints are parsed: (string, int, int, int)

                        // Skips file name
                        reader.ReadString();

                        // Read all values of the weapon
                        currentWeaponFields = new string[4]; // holds the original 4 parameters in string form
                        weaponFieldsParsed = new int[3]; // takes parameters 2 through 4 and converts them to ints

                        // Read in 4 parameters
                        for (int i = 0; i < 4; i++)
                        {
                            currentWeaponFields[i] = reader.ReadString();
                        }

                        // Parse the last 3 parameters to integers, and store them in a separate array
                        weaponFieldsParsed[0] = int.Parse(currentWeaponFields[1]);
                        weaponFieldsParsed[1] = int.Parse(currentWeaponFields[2]);
                        weaponFieldsParsed[2] = int.Parse(currentWeaponFields[3]);

                        // Add the weapon to the list of imported resource weapons
                        // based on its type: 0 - MeleeSpin, 1 - MeleeStab, 2 - Ranged

                        // The weapon is a melee-spin
                        if (weaponFieldsParsed[2] == 0)
                        {
                            resourceWeapons.Add(new MeleeWeapon(weaponFieldsParsed[0], new Point(40, 40), swordTest, 90, 5));
                        }

                        // The weapon is a melee-stab
                        else if (weaponFieldsParsed[2] == 1)
                        {
                            resourceWeapons.Add(new MeleeWeapon(weaponFieldsParsed[0], new Point(80, 40), swordTest, 20));
                        }

                        // The weapon is ranged
                        else
                        {
                            resourceWeapons.Add(new RangedWeapon(weaponFieldsParsed[0], basicArrow, new Point(40, 40), bowTest));
                        }

                        // End weapon case
                        break;

                        //####### TO-DO: Integrate this list of weapons into the game.. some helpful info: ######################################################
                        // - The weapons brought in do not have textures, sizes, projectiles, spin speeds, or spin angles.
                        // - The weapons brought in have durability and name fields that are not utilized by the constructors,
                        //   but are there if you want to use them.


                } // end switch
            } // end while

            // Close the binary reader.
            reader.Close();

            
        }

        /// <summary>
        /// sets the map to a new map and adjusts size of the map to fit the screen
        /// </summary>
        public void NewMap()
        {

            // TO-DO ## Clean this method up

            currentMap = maps[rng.Next(maps.Count)];
            if (Window.ClientBounds.Height <= Window.ClientBounds.Width)
                tileSize = Window.ClientBounds.Height / 16;
            else
                tileSize = Window.ClientBounds.Width / 16;
            mapOrigin = new Point((Window.ClientBounds.Width - tileSize * 16) / 2, 0);
            currentMap.SetOrigin(mapOrigin, tileSize);
            topBarrier = new Tile(new Rectangle(mapOrigin.X, mapOrigin.Y - tileSize, tileSize * 16, tileSize), wallTest, true);
            bottomBarrier = new Tile(new Rectangle(mapOrigin.X, mapOrigin.Y + 16 * tileSize, tileSize * 16, tileSize), wallTest, true);
        }

        /// <summary>
        /// Moves Game to the Next Level
        /// </summary>
        public void NextLevel()
        {

            // TO-DO ## clean this method up

            gameObjects.Clear();
            player.CurrentItem = null;
            NewMap();
            player.Position = new Rectangle(new Point((int)(mapOrigin.X + 8.5 * tileSize), mapOrigin.Y + 15 * tileSize), new Point(player.Position.Width, player.Position.Height));
            gameObjects.Add(key);
            key.PickedUp = false;
            key.Position = new Rectangle(GetEmptyTile(), new Point(key.Position.Width, key.Position.Height));

            //random amount of enemies between 1 and 5
            for(int i = 0; i<rng.Next(5)+1; i++)
            {
                enemies.Add(new Enemy(resourceEnemies[rng.Next(resourceEnemies.Count)]));
                enemies[i].Position = new Rectangle(GetEmptyTile(), new Point(enemies[i].Position.Width, enemies[i].Position.Height));
                
                // TO-DO ## needs additional code to fully implement enemies
            
            }
        }

        /// <summary>
        /// generates coordinates corresponding to an empty tile on the map 
        /// </summary>
        /// <returns>the top left corner of an empty tile on the map</returns>
        public Point GetEmptyTile()
        {
            //creates point
            Point empty;

            //loops until the coordinates represent a tile that isnt a wall
            do
            {
                empty = new Point(rng.Next(16), rng.Next(14));
            } while (currentMap.Layout[empty.X, empty.Y].IsWall);

            //shifts the point to accurately fit the map
            return new Point(empty.X * tileSize + mapOrigin.X, empty.Y * tileSize + mapOrigin.Y);
        }
    }
}
