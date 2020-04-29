// MonoGame Generated Namespace References
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Namespace References
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    // Enumerations
    /// <summary>
    /// Enumerations for various game states.
    /// </summary>
    public enum GameState { MainMenu, Game, Pause, Shop, Stats, Gameover }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // MonoGame Generated Fields
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // C# Fields
        private int score;
        private int tileSize;
        private Random random;

        // MonoGame Fields
        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;
        private MouseState mouseState;
        private MouseState previousMouseState;
        private Point mapOrigin;
        private SpriteFont buttonFont;
        private SpriteFont headingFont;
        private SpriteFont statFont;
        private SpriteFont titleFont;
        private Texture2D arrowTexture;
        private Texture2D bowTexture;
        private Texture2D circleTexture;
        private Texture2D cursorTexture;
        private Texture2D floorTexture;
        private Texture2D keyTexture;
        private Texture2D playerTexture;
        private Texture2D spellTexture;
        private Texture2D squareTexture;
        private Texture2D swordTexture;
        private Texture2D wallTexture;
        private Texture2D wandTexture;

        // Enumeration Fields
        private GameState gameState;
        private GameState previousGameState;

        // Class Fields
        private EventManager eventManager;
        private Item key;
        private Map map;
        private MeleeWeapon spear;
        private MeleeWeapon sword;
        private MouseCursor mouseCursor;
        private Player player;
        private Projectile arrow;
        private Projectile spell;
        private RangedWeapon bow;
        private RangedWeapon wand;
        private Tile topBarrier;
        private Tile bottomBarrier;
        private List<Enemy> enemies;
        private List<Enemy> resourceEnemies;
        private List<GameObject> gameObjects;
        private List<Map> maps;
        private List<Weapon> resourceWeapons;

        // MonoGame Generated Constructors
        /// <summary>
        /// Constructs the Game1 class.
        /// </summary>
        public Game1()
        {
            // MonoGame Generated Construction
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Adjust Game Window
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
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

            // MonoGame Generated Initialization
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // MonoGame Generated Content Loading
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load SpriteFonts
            buttonFont = Content.Load<SpriteFont>("Buttons");
            headingFont = Content.Load<SpriteFont>("Heading");
            statFont = Content.Load<SpriteFont>("Stats");
            titleFont = Content.Load<SpriteFont>("Title");

            // Load Textures
            arrowTexture = Content.Load<Texture2D>("arrow");
            bowTexture = Content.Load<Texture2D>("bow");
            circleTexture = Content.Load<Texture2D>("circle");
            cursorTexture = Content.Load<Texture2D>("mouseCursor");
            floorTexture = Content.Load<Texture2D>("floor");
            keyTexture = Content.Load<Texture2D>("key");
            playerTexture = Content.Load<Texture2D>("player");
            spellTexture = Content.Load<Texture2D>("spell");
            squareTexture = Content.Load<Texture2D>("square");
            swordTexture = Content.Load<Texture2D>("sword");
            wallTexture = Content.Load<Texture2D>("wall");
            wandTexture = Content.Load<Texture2D>("wand");

            // Initialize C# Fields
            random = new Random();

            // Initilize Class Fields
            mouseCursor = new MouseCursor(new Rectangle(0, 0, 50, 50), cursorTexture);
            enemies = new List<Enemy>();
            eventManager = new EventManager();
            gameObjects = new List<GameObject>();

            // Initialize Window Resizing
            Window.AllowUserResizing = true;

            // Load Resources
            LoadResources();

            // Initialize Player
            player.OffHand = bow;
            player.Debug = false;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // MonoGame Generated Content Unloading
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Initialize User Inputs
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            // Update Based On User Inputs
            mouseCursor.Update(mouseState);

            // Game Transitional Finite State Machine
            FiniteStateMachineUpdate();

            // Initialize Previous User Inputs
            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;

            // MonoGame Generated Update
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // MonoGame Generated Drawing
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // End MonoGame Generated Drawing

            // Start the SpriteBatch
            spriteBatch.Begin();

            // Draw the Game
            FiniteStateMachineDraw();

            // Draw the UI
            DrawGUI(spriteBatch);

            // Draw the cursor
            mouseCursor.Draw(spriteBatch);

            // Stop the SpriteBatch
            spriteBatch.End();

            // MonoGame Generated Drawing
            base.Draw(gameTime);
        }

        // Methods
        /// <summary>
        /// Loads all maps, weapons and enemies from the resource file
        /// </summary>
        public void LoadResources()
        {
            // Temporary Fields
            int tileSize;
            string currentEnemyName;
            int[] weaponFieldsParsed;
            int[] currentEnemyFields;
            string[] currentWeaponFields;
            int[,] tiles;
            BinaryReader reader;
            FileStream resources;

            // Initialize Temporary Fields
            tiles = new int[16, 16];
            tileSize = 68;

            // Initialize Class Fields
            key = new Item(new Rectangle(500, 500, 50, 50), keyTexture, false);
            spear = new MeleeWeapon(new Point(80, 40), swordTexture, 8, 20);
            sword = new MeleeWeapon(new Point(40, 40), swordTexture, 5, 90, 5);
            player = new Player(new Rectangle(0, 0, 50, 50), playerTexture, 300, sword);
            arrow = new Projectile(new Point(20, 5), arrowTexture, 20);
            spell = new Projectile(new Point(20, 20), spellTexture, 12);
            bow = new RangedWeapon(new Point(40, 40), bowTexture, 2, arrow);
            wand = new RangedWeapon(new Point(50, 50), wandTexture, 1, spell);
            maps = new List<Map>();
            resourceEnemies = new List<Enemy>();
            resourceWeapons = new List<Weapon>();

            // Open FileStream and BinaryReader
            resources = File.OpenRead("../../../../../Resources/master.rsrc");
            reader = new BinaryReader(resources);

            // While there is unread data, continue looping
            while (reader.PeekChar() != -1)
            {
                // Read a string that says "Map," "Enemy," or "Weapon"
                switch (reader.ReadString())
                {
                    case "Map":
                        // Skip file name
                        reader.ReadString();

                        // Assign all tiles to list
                        for (int i = 0; i < tiles.GetLength(0); i++)
                        {
                            for (int j = 0; j < tiles.GetLength(1); j++)
                            {
                                tiles[i, j] = reader.ReadInt32();
                            }
                        }

                        // Add the Map to the List
                        maps.Add(new Map(tileSize, tiles, floorTexture, wallTexture));
                        break;

                    case "Enemy":
                        // Enemy File Format (six parameters from .enemy file): health, damage, attackSpeed, speed, xCoord, yCoord
                        //    -  All items are read in as strings but parsed to: (int, int, int, int, int, int)

                        // Stores name of the Enemy file
                        currentEnemyName = reader.ReadString();

                        // Initialize Enemy Fields
                        currentEnemyFields = new int[6];

                        // Read in parameters
                        for (int i = 0; i < currentEnemyFields.Length; i++)
                        {
                            currentEnemyFields[i] = int.Parse(reader.ReadString());
                        }

                        // Add the enemy to the list of imported resource enemies
                        resourceEnemies.Add(new Enemy(new Rectangle(currentEnemyFields[4], currentEnemyFields[5], 10, 10), // Enemy Position
                                                      squareTexture, // Enemy Texture
                                                      currentEnemyFields[0], // Health
                                                      null, // Weapon
                                                      currentEnemyFields[2], // Attack Interval
                                                      currentEnemyFields[1], // Body Damage
                                                      50, // Default Max Travel Width
                                                      50, // Default Max Travel Height
                                                      currentEnemyFields[3], // Travel Speed
                                                      currentEnemyName, // Enemy Name
                                                      EnemyMovementType.Random, // Enemy Movement Type
                                                      player, // The Player
                                                      random)); // Random Number Generator
                        break;

                    //####### TO-DO: Integrate this list of enemies into the game.. some helpful info: ######################################################
                    // - The enemies brought in do not have sizes, textures, set movement patterns, or max travelling dimensions
                    // - Note that the current texture used is a square test texture
                    // - I restate, in the .rsrc file, those ints are stored as strings.  They are strings since they come out of the Enemy external tool.
                    //   They are then parsed to an array of ints here, and act as ints. No need to TryParse, the external tool only allows integers > 0 to be used.

                    case "Weapon":
                        // Weapon File Format (four parameters from .weapon file): name, damage, durability, type
                        //    -  All items are read in as strings, but the ints are parsed: (string, int, int, int)

                        // Skips file name
                        reader.ReadString();

                        // Intialize Fields
                        currentWeaponFields = new string[4]; // holds the original four parameters in string form
                        weaponFieldsParsed = new int[3]; // takes parameters 2 through 4 and converts them to ints

                        // Read in four parameters
                        for (int i = 0; i < 4; i++)
                        {
                            currentWeaponFields[i] = reader.ReadString();
                        }

                        // Parse the last three parameters to integers, and store them in a separate array
                        weaponFieldsParsed[0] = int.Parse(currentWeaponFields[1]);
                        weaponFieldsParsed[1] = int.Parse(currentWeaponFields[2]);
                        weaponFieldsParsed[2] = int.Parse(currentWeaponFields[3]);

                        // Add the weapon to the list of imported resource weapons
                        // based on its type: 0 - MeleeSpin, 1 - MeleeStab, 2 - Ranged

                        // The weapon is a melee-spin
                        if (weaponFieldsParsed[2] == 0)
                        {
                            resourceWeapons.Add(new MeleeWeapon(new Point(40, 40), swordTexture, weaponFieldsParsed[0], 90, 5));
                        }

                        // The weapon is a melee-stab
                        else if (weaponFieldsParsed[2] == 1)
                        {
                            resourceWeapons.Add(new MeleeWeapon(new Point(80, 40), swordTexture, weaponFieldsParsed[0], 20));
                        }

                        // The weapon is ranged
                        else
                        {
                            resourceWeapons.Add(new RangedWeapon(new Point(40, 40), bowTexture, weaponFieldsParsed[0], arrow));
                        }

                        // End weapon case
                        break;

                        //####### TO-DO: Integrate this list of weapons into the game.. some helpful info: ######################################################
                        // - The weapons brought in do not have textures, sizes, projectiles, spin speeds, or spin angles.
                        // - The weapons brought in have durability and name fields that are not utilized by the constructors,
                        //   but are there if you want to use them.
                }
            }

            // Close the binary reader.
            reader.Close();
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
        /// sets the map to a new map and adjusts size of the map to fit the screen
        /// </summary>
        public void NewMap()
        {
            // Get a random Map from the List
            map = maps[random.Next(maps.Count)];

            tileSize = 68;

            // Calculate Map Origin
            mapOrigin = new Point((Window.ClientBounds.Width - tileSize * 16) / 2, 0);

            // Set the Map Origin
            map.SetOrigin(mapOrigin, tileSize);

            // Set the top and bottom barriers
            topBarrier = new Tile(new Rectangle(mapOrigin.X, mapOrigin.Y - tileSize, tileSize * 16, tileSize), wallTexture, true);
            bottomBarrier = new Tile(new Rectangle(mapOrigin.X, mapOrigin.Y + 16 * tileSize, tileSize * 16, tileSize), wallTexture, true);
        }

        /// <summary>
        /// Gets an empty floor space on the Map.
        /// </summary>
        /// <returns>The top left corner of an empty Tile on the Map.</returns>
        public Point GetEmptyTile()
        {
            // Temporary Fields
            Point empty;

            // Loop until the key is placed on a floor tile
            do
            {
                // Randomly generate the position
                empty = new Point(random.Next(16), random.Next(14));
            } while (map.Layout[empty.X, empty.Y].Collidable);

            // Shifts the point to accurately fit the Map and returns it
            return new Point(empty.X * tileSize + mapOrigin.X, empty.Y * tileSize + mapOrigin.Y);
        }

        /// <summary>
        /// Moves Game to the Next Level.
        /// </summary>
        public void NextLevel()
        {
            // TO-DO ## clean this method up
            // Clear the List of GameObjects
            gameObjects.Clear();

            // Removes any Items from the Player's inventory
            player.CurrentItem = null;

            // Creates a new Map
            NewMap();

            // Sets the Player's position
            player.Position = new Rectangle(new Point((int)(mapOrigin.X + 8.5 * tileSize), mapOrigin.Y + 15 * tileSize), new Point(player.Position.Width, player.Position.Height));

            // Adds the key to the List of GameObjects
            gameObjects.Add(key);

            // Sets the key to be uncollected
            key.Collected = false;

            // Sets the key's position
            key.Position = new Rectangle(GetEmptyTile(), new Point(key.Position.Width, key.Position.Height));

            // Updates the Player's score
            score += 200;

            //adds a weapon on the ground

            //picks a random weapon
            int weaponIndex = random.Next(resourceWeapons.Count);

            //adds the appropriate weapon type
            if(resourceWeapons[weaponIndex] is RangedWeapon)
            {
                gameObjects.Add(new RangedWeapon((RangedWeapon)(resourceWeapons[weaponIndex])));
            }
            if(resourceWeapons[weaponIndex] is MeleeWeapon)
            {
                gameObjects.Add(new MeleeWeapon((MeleeWeapon)(resourceWeapons[weaponIndex])));
            }

            //fixes variables in the weapon

            // Sets the weapon to be uncollected
            ((Weapon)gameObjects[1]).Collected = false;

            // Sets the weapons's position
            gameObjects[1].Position = new Rectangle(GetEmptyTile(), new Point(gameObjects[1].Position.Width, gameObjects[1].Position.Height));

            // Loop a random number of times up to five times
            for (int i = 0; i < random.Next(5) + 1; i++)
            {
                // Add a new Enemy to the List of Enemies
                enemies.Add(new Enemy(resourceEnemies[random.Next(resourceEnemies.Count)]));

                // Set the Enemy's position
                enemies[i].Position = new Rectangle(GetEmptyTile(), new Point(enemies[i].Position.Width, enemies[i].Position.Height));

                // TO-DO ## needs additional code to fully implement enemies
            }
        }

        /// <summary>
        /// Handles game transitions and game states.
        /// </summary>
        private void FiniteStateMachineUpdate()
        {
            switch (gameState)
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

                    // Update the Player
                    player.Update(mouseState, previousMouseState, keyboardState, previousKeyboardState);

                    // Give the player a speed boost if all enemies are dead
                    if (enemies.Count != 0)
                        player.Speed = 4;
                    else
                        player.Speed = 6;

                    // Loop through GameObjects
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        // Update GameObjects
                        gameObjects[i].Update();
                    }

                    // Loop through Enemies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        // Update Enemies
                        enemies[i].Update();
                    }


                    // Check Collisions
                    // Loop through GameObjects
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        // Check Collisions between the Player and GameObjects
                        eventManager.Collision(player, gameObjects[i]);
                    }

                    // Loop through Enemies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        // Check Collisions between the Player and the Enemy
                        eventManager.Collision(player, enemies[i]);

                        // If the Player has a Weapon
                        if (player.Weapon != null)
                        {
                            // Check Collisions between the Enemies and the Player's Weapon
                            eventManager.Collision(enemies[i], player.Weapon);
                        }

                        // If the Player has an offHand Weapon
                        if (player.OffHand != null)
                        {
                            // Check Collisions between the Enemies and the Player's offHand Weapon
                            eventManager.Collision(enemies[i], player.OffHand);
                        }

                        // Loop through the Maps walls
                        for (int j = 0; j < map.Walls.Count; j++)
                        {
                            // Check Collisions between the Enemies and the Map's walls
                            eventManager.Collision(enemies[i], map.Walls[j]);
                        }

                        // Check collisions with barriers
                        eventManager.Collision(enemies[i], topBarrier);
                        eventManager.Collision(enemies[i], bottomBarrier);
                    }

                    // Loop through the Map's walls
                    for (int i = 0; i < map.Walls.Count; i++)
                    {
                        // Check Collisions between the Player and the Map's walls
                        eventManager.Collision((Character)player, map.Walls[i]);

                        // If the Player has a Weapon
                        if (player.Weapon != null)
                        {
                            // Check Collisions between the Map's Walls and the Player's Weapon
                            eventManager.Collision(map.Walls[i], player.Weapon);
                        }

                        // If the Player has an offHand Weapon
                        if (player.OffHand != null)
                        {
                            // Check Collisions between the Map's Walls and the Player's offHand Weapon
                            eventManager.Collision(map.Walls[i], player.OffHand);
                        }
                    }

                    // Check Collisions between the Player and the barriers
                    eventManager.Collision((Character)player, topBarrier);
                    eventManager.Collision((Character)player, bottomBarrier);
                    

                    // Loop through the Enemies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        // If the Enemy is dead
                        if (enemies[i].Health <= 0)
                        {
                            // Remove the Enemy from the List of Enemies
                            enemies.RemoveAt(i);
                            score += 100;
                        }
                    }

                    // If there are no Enemies, the Player has the key, and goes through the top of the level
                    if (enemies.Count == 0 && player.CurrentItem == key && player.Position.Y <= 5)
                    {
                        // Go to the next level
                        NextLevel();
                    }

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
        /// Handles Finite State Machine Drawing.
        /// </summary>
        private void FiniteStateMachineDraw()
        {
            switch (gameState)
            {
                case GameState.MainMenu:

                    break;

                case GameState.Game:
                    // Draw the Map
                    map.Draw(spriteBatch);

                    // Loop through the Game Objects
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        // Draw the GameObject
                        gameObjects[i].Draw(spriteBatch);
                    }

                    // Draw the Player
                    player.Draw(spriteBatch);

                    // Draw the Player's Hands
                    player.Weapon.DrawHands(spriteBatch, player.Position, circleTexture, Color.Black);

                    // Loop through the Enemies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        // Draw the enemy
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
        /// Draws the GUI to the window based on the current game state.
        /// </summary>
        public void DrawGUI(SpriteBatch sb)
        {
            switch (gameState)
            {
                case GameState.MainMenu:

                    // Draw buttons for the main menu
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 290, 100, 60), Color.Gray);
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 370, 100, 60), Color.Gray);
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 450, 100, 60), Color.Gray);

                    // Draw text for the main menu
                    sb.DrawString(titleFont, "GAME TITLE", new Vector2(graphics.PreferredBackBufferWidth / 2 - 220, 70), Color.Black);
                    sb.DrawString(buttonFont, "Start", new Vector2(graphics.PreferredBackBufferWidth / 2 - 30, 300), Color.Black);
                    sb.DrawString(buttonFont, "Shop", new Vector2(graphics.PreferredBackBufferWidth / 2 - 31, 380), Color.Black);
                    sb.DrawString(buttonFont, "Stats", new Vector2(graphics.PreferredBackBufferWidth / 2 - 31, 460), Color.Black);
                    break;

                case GameState.Game:
                    // Draw the underlying boxes for GUI
                    sb.Draw(squareTexture, new Rectangle(0, 0, 300, 30), Color.LightGreen);
                    sb.Draw(squareTexture, new Rectangle(Window.ClientBounds.Width - 300, 0, 300, 80), Color.Gray);
                    sb.Draw(squareTexture, new Rectangle(0, Window.ClientBounds.Height - 100, 100, 100), Color.Gray);
                    sb.Draw(squareTexture, new Rectangle(0, Window.ClientBounds.Height - 150, 50, 50), Color.Gray);
                    sb.Draw(squareTexture, new Rectangle(50, Window.ClientBounds.Height - 150, 50, 50), Color.Gray);
                    sb.Draw(squareTexture, new Rectangle(2, Window.ClientBounds.Height - 148, 46, 46), Color.Black);
                    sb.Draw(squareTexture, new Rectangle(52, Window.ClientBounds.Height - 148, 46, 46), Color.Black);
                    sb.Draw(squareTexture, new Rectangle(2, Window.ClientBounds.Height - 98, 96, 96), Color.Black);


                    // Draw the text for in-game GUI
                    sb.DrawString(statFont, $"HP: {player.Health}/300", new Vector2(2, 2), Color.Black);
                    sb.DrawString(statFont, $"Score: {score}", new Vector2(Window.ClientBounds.Width - 290, 2), Color.Black);
                    sb.DrawString(statFont, "Currency:", new Vector2(Window.ClientBounds.Width - 290, 22), Color.Black);
                    sb.DrawString(statFont, "Keys:", new Vector2(Window.ClientBounds.Width - 290, 42), Color.Black);

                    // Draw the icons for GUI
                    if (player.CurrentItem != null)
                        player.CurrentItem.Draw(sb, new Rectangle(5, Window.ClientBounds.Height - 145, 40, 40));
                    if (player.Weapon != null)
                        player.Weapon.Draw(sb, new Rectangle(5, Window.ClientBounds.Height - 95, 90, 90));
                    if (player.OffHand != null)
                        player.OffHand.Draw(sb, new Rectangle(55, Window.ClientBounds.Height - 145, 40, 40));

                    break;

                case GameState.Pause:
                    // Draw buttons the for pause menu
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 175, 150, 350, 400), Color.Gray);
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 63, 290, 130, 60), Color.DimGray);
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 63, 370, 130, 60), Color.DimGray);
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 63, 450, 130, 60), Color.DimGray);

                    // Draw text for the pause menu
                    sb.DrawString(headingFont, "PAUSED", new Vector2(graphics.PreferredBackBufferWidth / 2 - 65, 200), Color.Black);
                    sb.DrawString(buttonFont, "Resume", new Vector2(graphics.PreferredBackBufferWidth / 2 - 50, 300), Color.Black);
                    sb.DrawString(buttonFont, "Shop", new Vector2(graphics.PreferredBackBufferWidth / 2 - 35, 380), Color.Black);
                    sb.DrawString(buttonFont, "Quit", new Vector2(graphics.PreferredBackBufferWidth / 2 - 30, 460), Color.Black);
                    break;

                case GameState.Shop:
                    // Draw buttons for the shop menu
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 50, 630, 100, 60), Color.Gray);

                    // Draw text for the shop menu
                    sb.DrawString(headingFont, "SHOP", new Vector2(graphics.PreferredBackBufferWidth / 2 - 45, 50), Color.Black);
                    sb.DrawString(buttonFont, "Back", new Vector2(graphics.PreferredBackBufferWidth / 2 - 33, 640), Color.Black);
                    break;

                case GameState.Stats:
                    // Draw buttons for the stats menu
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 50, 630, 100, 60), Color.Gray);

                    // Draw text for the stats menu
                    sb.DrawString(headingFont, "STATS", new Vector2(graphics.PreferredBackBufferWidth / 2 - 50, 50), Color.White);
                    sb.DrawString(buttonFont, "Back", new Vector2(graphics.PreferredBackBufferWidth / 2 - 33, 640), Color.Black);

                    // Draw text for the tracked stats
                    sb.DrawString(buttonFont, "Keys Collected: ", new Vector2(graphics.PreferredBackBufferWidth / 2 - 400, 150), Color.White);
                    sb.DrawString(buttonFont, "Monsters Defeated: ", new Vector2(graphics.PreferredBackBufferWidth / 2 - 400, 200), Color.White);
                    sb.DrawString(buttonFont, "Rooms Cleared: ", new Vector2(graphics.PreferredBackBufferWidth / 2 - 400, 250), Color.White);
                    sb.DrawString(buttonFont, "Distance Travelled: " + player.TravelledDistance, new Vector2(graphics.PreferredBackBufferWidth / 2 - 400, 300), Color.White);
                    break;

                case GameState.Gameover:
                    // Draw buttons for the gameover screen
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 130, 420, 100, 60), Color.Gray);
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2, 420, 100, 60), Color.Gray);

                    // Draw text for the gameover screen
                    sb.DrawString(titleFont, "GAME OVER", new Vector2(graphics.PreferredBackBufferWidth / 2 - 240, 200), Color.Black);
                    sb.DrawString(headingFont, "You died lol", new Vector2(680, 300), Color.Black);
                    sb.DrawString(buttonFont, "Menu", new Vector2(graphics.PreferredBackBufferWidth / 2 - 120, 430), Color.Black);
                    sb.DrawString(buttonFont, "Quit", new Vector2(graphics.PreferredBackBufferWidth / 2 + 20, 430), Color.Black);
                    break;
            }
        }
    }
}
