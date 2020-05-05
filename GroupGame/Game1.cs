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
    public enum GameState { MainMenu, Game, Pause, Stats, Gameover }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // MonoGame Generated Fields
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // C# Fields
        private int randComment;
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
        private List<Weapon> enemyWeapons;
        private List<Weapon> resourceWeapons;
        int levelNumber;

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

            // Load Stats
            LoadStats();

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

            // Save stats
            SaveStats();
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

        /// <summary>
        /// Loads all stats from the stat file.
        /// </summary>
        public void LoadStats()
        {
            // Create the stream and reader
            FileStream inStream = null;
            BinaryReader reader = null;

            try
            {
                // Initialize stream and the reader
                inStream = File.OpenRead("../../../../../Resources/master.stat");
                reader = new BinaryReader(inStream);

                // Read in the three stats from file
                player.TravelledDistance = reader.ReadDouble();
                player.DamageTaken = reader.ReadDouble();
                player.KeysCollected = reader.ReadInt32();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading stats: " + e.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

        }
            
        /// <summary>
        /// Saves all stats to the stat file.
        /// </summary>
        public void SaveStats()
        {
            // Create the stream and writer
            FileStream outStream = null;
            BinaryWriter writer = null;

            try
            {
                // Initialize the stream and the writer
                outStream = File.OpenWrite("../../../../../Resources/master.stat");
                writer = new BinaryWriter(outStream);

                // Write the three stats to the file
                writer.Write(player.TravelledDistance);
                writer.Write(player.DamageTaken);
                writer.Write(player.KeysCollected);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving stats: " + e.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        // Methods
        /// <summary>
        /// Loads all maps, weapons and enemies from the resource file
        /// </summary>
        public void LoadResources()
        {
            // Temporary Fields
            string currentEnemyName;
            int[] weaponFieldsParsed;
            int[] currentEnemyFields;
            string[] currentWeaponFields;
            int[,] tiles;
            BinaryReader reader;
            FileStream resources;

            // Initialize Temporary Fields
            tiles = new int[16, 16];

            // Initialize Class Fields
            tileSize = 68;
            key = new Item(new Rectangle(500, 500, (int)(.75*tileSize), (int)(.75*tileSize)), keyTexture, false, true);
            spear = new MeleeWeapon(new Point((int)(1.2*tileSize), (int)(.6*tileSize)), swordTexture, 8, 20);
            sword = new MeleeWeapon(new Point((int)(.6*tileSize), (int)(.6*tileSize)), swordTexture, 5, 90, 5);
            player = new Player(new Rectangle(0, 0, (int)(.75*tileSize), (int)(.75*tileSize)), playerTexture, 300, sword);
            arrow = new Projectile(new Point((int)(.3*tileSize), (int)(.08*tileSize)), arrowTexture, 20);
            spell = new Projectile(new Point((int)(.3*tileSize), (int)(.3*tileSize)), spellTexture, 12);
            bow = new RangedWeapon(new Point((int)(.6*tileSize), (int)(.6*tileSize)), bowTexture, 2, arrow);
            wand = new RangedWeapon(new Point((int)(.75*tileSize), (int)(.75*tileSize)), wandTexture, 1, spell);
            maps = new List<Map>();
            resourceEnemies = new List<Enemy>();
            resourceWeapons = new List<Weapon>();

            // Creates the enemyWeapons List
            enemyWeapons = new List<Weapon> { sword, spear, wand, bow };

            // Loads Enemy Textures
            List<Texture2D> enemySprites = new List<Texture2D>();
            enemySprites.Add(Content.Load<Texture2D>("enemies/archer"));
            enemySprites.Add(Content.Load<Texture2D>("enemies/berserker"));
            enemySprites.Add(Content.Load<Texture2D>("enemies/bug"));
            enemySprites.Add(Content.Load<Texture2D>("enemies/sentry"));
            enemySprites.Add(Content.Load<Texture2D>("enemies/knight"));
            enemySprites.Add(Content.Load<Texture2D>("enemies/Wizard"));
            enemySprites.Add(Content.Load<Texture2D>("enemies/tank"));
            enemySprites.Add(Content.Load<Texture2D>("enemies/sentry"));
            enemySprites.Add(Content.Load<Texture2D>("enemies/viking"));
            enemySprites.Add(Content.Load<Texture2D>("enemies/wanderer"));

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
                        // Enemy File Format (8 parameters from .enemy file): health, damage, attackSpeed, speed, xSize, ySize, movementType, weaponType
                        //    -  All items are read in as strings but parsed to: (int, int, int, int, int, int, int, int)

                        // Stores name of the Enemy file
                        currentEnemyName = reader.ReadString();

                        // Initialize Enemy Fields
                        currentEnemyFields = new int[8];

                        // Read in parameters
                        for (int i = 0; i < currentEnemyFields.Length; i++)
                        {
                            currentEnemyFields[i] = int.Parse(reader.ReadString());
                        }

                        // Sets the appropriate movement type based on the integer from the file
                        EnemyMovementType movementType = EnemyMovementType.Chase; // default type
                        if (currentEnemyFields[6] == 0)
                            movementType = EnemyMovementType.Chase;
                        else if (currentEnemyFields[6] == 1)
                            movementType = EnemyMovementType.LeftRight;
                        else if (currentEnemyFields[6] == 2)
                            movementType = EnemyMovementType.Random;
                        else if (currentEnemyFields[6] == 3)
                            movementType = EnemyMovementType.Rectangle;
                        else if (currentEnemyFields[6] == 4)
                            movementType = EnemyMovementType.UpDown;

                        // Add the enemy to the list of imported resource enemies
                        resourceEnemies.Add(new Enemy(new Rectangle(0, 0, (int)(currentEnemyFields[4] / 10.0 * tileSize), (int)(currentEnemyFields[5] / 10.0 * tileSize)), // Enemy Size (position should be overridden in NextLevel()
                                                      squareTexture, // Enemy Texture
                                                      currentEnemyFields[0], // Health
                                                      null, // Weapon
                                                      currentEnemyFields[2], // Attack Interval
                                                      currentEnemyFields[1], // Body Damage
                                                      50, // Default Max Travel Width
                                                      50, // Default Max Travel Height
                                                      currentEnemyFields[3], // Travel Speed
                                                      currentEnemyName, // Enemy Name
                                                      movementType, // Enemy Movement Type
                                                      player, // The Player
                                                      random)); // Random Number Generator

                        // Assigns Texture if applicable
                        if (resourceEnemies.Count <= enemySprites.Count)
                        {
                            resourceEnemies[resourceEnemies.Count - 1].Texture = enemySprites[resourceEnemies.Count - 1];
                        }

                        //Adds the weapon to the enemy
                        if(currentEnemyFields[7]<4 && currentEnemyFields[7]>-1)
                        // Adds the Weapon to the Enemy
                        // If the field is a valid Weapon type
                        if(currentEnemyFields[7] < 4 && currentEnemyFields[7] > -1)
                        {
                            // If the Weapon is a MeleeWeapon or RangedWeapon, set appropriately
                            if(enemyWeapons[currentEnemyFields[7]] is MeleeWeapon)
                                resourceEnemies[resourceEnemies.Count-1].Weapon = new MeleeWeapon((MeleeWeapon)enemyWeapons[currentEnemyFields[7]]);
                            else if (enemyWeapons[currentEnemyFields[7]] is RangedWeapon)
                                resourceEnemies[resourceEnemies.Count-1].Weapon = new RangedWeapon((RangedWeapon)enemyWeapons[currentEnemyFields[7]]);
                        }
                        break;

                    //####### TO-DO: Integrate this list of enemies into the game.. some helpful info: ######################################################
                    // - The enemies brought in do not have sizes, textures, set movement patterns, or max travelling dimensions
                    // - Note that the current texture used is a square test texture
                    // - I restate, in the .rsrc file, those ints are stored as strings.  They are strings since they come out of the Enemy external tool.
                    //   They are then parsed to an array of ints here, and act as ints. No need to TryParse, the external tool only allows integers > 0 to be used.

                    case "Weapon":
                        // Weapon File Format (four parameters from .weapon file): name, damage, size, type
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
                        // based on its type: 0 - Sword, 1 - Spear, 2 - Wand, 3 - Bow

                        // The weapon is a sword
                        if (weaponFieldsParsed[2] == 0)
                        {
                            resourceWeapons.Add(new MeleeWeapon(new Point((int)(weaponFieldsParsed[1] * tileSize / 10), (int)(weaponFieldsParsed[1] * tileSize / 10)), swordTexture, weaponFieldsParsed[0], 90, 5));
                        }

                        // The weapon is a spear
                        else if (weaponFieldsParsed[2] == 1)
                        {
                            resourceWeapons.Add(new MeleeWeapon(new Point((int)(weaponFieldsParsed[1] * tileSize / 5), (int)(weaponFieldsParsed[1] * tileSize / 10)), swordTexture, weaponFieldsParsed[0], 20));
                        }

                        // The weapon is a wand
                        else if (weaponFieldsParsed[2] == 2)
                        {
                            resourceWeapons.Add(new RangedWeapon(new Point((int)(weaponFieldsParsed[1] * tileSize / 10), (int)(weaponFieldsParsed[1] * tileSize / 10)), wandTexture, weaponFieldsParsed[0], spell));
                        }

                        // The weapon is a bow
                        else
                        {
                            resourceWeapons.Add(new RangedWeapon(new Point((int)(weaponFieldsParsed[1] * tileSize / 10), (int)(weaponFieldsParsed[1] * tileSize / 10)), bowTexture, weaponFieldsParsed[0], arrow));
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
                // Randomly generate the Position
                empty = new Point(random.Next(16), random.Next(10));
            } while (map.Layout[empty.X, empty.Y].Collidable);

            // Shifts the point to accurately fit the Map and returns it
            return new Point(empty.X * tileSize + mapOrigin.X, empty.Y * tileSize + mapOrigin.Y);
        }

        /// <summary>
        /// Resets the game to it's default states.
        /// </summary>
        private void Reset()
        {
            //Sets Level to 1
            levelNumber = 1;

            // Reset collected statistics
            score = -200;

            // Reset Enemies
            enemies.Clear();
        }

        /// <summary>
        /// Moves Game to the Next Level.
        /// </summary>
        public void NextLevel()
        {
            // Clear the List of Enemies
            enemies.Clear();

            // Clear the List of GameObjects
            gameObjects.Clear();

            // Removes any Items from the Player's inventory
            player.CurrentItem = null;

            // Creates a new Map
            NewMap();
            levelNumber++;

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


            // Adds a Weapon on the ground
            if (random.Next(5) == 0)
            {
                // Picks a random Weapon
                int weaponIndex = random.Next(resourceWeapons.Count);

                // Adds the appropriate Weapon type
                if (resourceWeapons[weaponIndex] is RangedWeapon)
                {
                    gameObjects.Add(new RangedWeapon((RangedWeapon)(resourceWeapons[weaponIndex])));
                }
                if (resourceWeapons[weaponIndex] is MeleeWeapon)
                {
                    gameObjects.Add(new MeleeWeapon((MeleeWeapon)(resourceWeapons[weaponIndex])));
                }


                // Fixes variables in the Weapon

                // Sets the Weapon to be uncollected
                ((Weapon)gameObjects[1]).Collected = false;

                // Sets the Weapon's position
                gameObjects[1].Position = new Rectangle(GetEmptyTile(), new Point(gameObjects[1].Position.Width, gameObjects[1].Position.Height));

                //Scales Weapon stats
                ((Weapon)gameObjects[1]).Damage = (int)(((Weapon)gameObjects[1]).Damage * (2.5 * Math.Sqrt(levelNumber) * Math.Pow(Math.Log(levelNumber+1, 2.0), .8))/10);
            }
            
            

            // Loop a random number of times up to five times
            for (int i = 0; i < random.Next(5) + 1; i++)
            {
                // Add a new Enemy to the List of Enemies
                enemies.Add(new Enemy(resourceEnemies[random.Next(resourceEnemies.Count)]));

                // Set the Enemy's position
                enemies[i].Position = new Rectangle(GetEmptyTile(), new Point(enemies[i].Position.Width, enemies[i].Position.Height));

                // Scales Enemies
                enemies[i].BodyDamage = (int)((enemies[i].BodyDamage) * 2.5 * Math.Sqrt(levelNumber) * Math.Pow(Math.Log(levelNumber + 1, 2.0), .8)/10);
                if(enemies[i].Weapon!=null)
                    enemies[i].Weapon.Damage = (int)((enemies[i].Weapon.Damage) * 2.5 * Math.Sqrt(levelNumber) * Math.Pow(Math.Log(levelNumber + 1, 2.0), .8)/10);
                enemies[i].Health = (int)((enemies[i].Health) * 2.5 * Math.Sqrt(levelNumber) * Math.Pow(Math.Log(levelNumber + 1, 2.0), .8)/10);
                Console.WriteLine(enemies[i].Health);
            }


            // Set a random comment for the next death this level;
            randComment = random.Next(0, 5);
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

                        Reset();
                        NextLevel();
                    }

                    // If the user presses "S" (or presses the button) in the menu, show the statistics
                    if (SingleKeyPress(Keys.S) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 370, 100, 60)))
                    {
                        gameState = GameState.Stats;
                    }

                    // If the user presses "Escape" (or presses the button) in the menu, quit the game
                    if (SingleKeyPress(Keys.Escape) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 45, 450, 100, 60)))
                    {
                        Exit();
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

                    // Give the Player a speed boost if all Enemies are dead
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

                        // Checks if player is trying to swap weapons
                        if(eventManager.CollisionCheck(player.Position, gameObjects[i].Position) && gameObjects[i] is Weapon)
                        {
                            if(SingleKeyPress(Keys.E))
                            {
                                // Temporary variable to swap Weapons
                                Weapon temp = player.Weapon;
                                player.Weapon = (Weapon)gameObjects[i];
                                gameObjects[i] = temp;
                            }
                        }
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

                    // If the Player has the key, and goes through the top of the level
                    if (player.CurrentItem == key && player.Position.Y <= 5)
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

                    // If the user presses "Q" (or presses the button) in the pause menu, go to the game over screen
                    if (SingleKeyPress(Keys.Q) ||
                        SingleMousePress(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 63, 370, 130, 60)))
                    {
                        gameState = GameState.Gameover;
                    }

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
                    sb.DrawString(buttonFont, "Stats", new Vector2(graphics.PreferredBackBufferWidth / 2 - 31, 380), Color.Black);
                    sb.DrawString(buttonFont, "Quit", new Vector2(graphics.PreferredBackBufferWidth / 2 - 31, 460), Color.Black);
                    break;

                case GameState.Game:
                    // Draw the underlying boxes for GUI
                    sb.Draw(squareTexture, new Rectangle(0, 0, 300, 30), Color.Red);
                    sb.Draw(squareTexture, new Rectangle(0, 0, player.Health, 30), Color.LightGreen);
                    sb.Draw(squareTexture, new Rectangle(Window.ClientBounds.Width - 300, 0, 300, 130), Color.Gray);
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
                    sb.DrawString(statFont, $"Current Damage: {player.Weapon.Damage}", new Vector2(Window.ClientBounds.Width - 290, 82), Color.Black);

                    // Loop through GameObjects
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        // Check Collisions between the Player and Ground Weapons
                        if(eventManager.CollisionCheck(player.Position, gameObjects[i].Position) == true && gameObjects[i] is Weapon)
                        {
                            // Displays damage of ground Weapon
                            sb.DrawString(statFont, $"New Damage: {((Weapon)gameObjects[i]).Damage}", new Vector2(Window.ClientBounds.Width - 290, 102), Color.Black);
                        }
                    }

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

                    // Draw text for the pause menu
                    sb.DrawString(headingFont, "PAUSED", new Vector2(graphics.PreferredBackBufferWidth / 2 - 65, 200), Color.Black);
                    sb.DrawString(buttonFont, "Resume", new Vector2(graphics.PreferredBackBufferWidth / 2 - 50, 300), Color.Black);
                    sb.DrawString(buttonFont, "Quit", new Vector2(graphics.PreferredBackBufferWidth / 2 - 35, 380), Color.Black);
                    break;

                case GameState.Stats:
                    // Draw buttons for the stats menu
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 50, 630, 100, 60), Color.Gray);

                    // Draw text for the stats menu
                    sb.DrawString(headingFont, "STATS", new Vector2(graphics.PreferredBackBufferWidth / 2 - 50, 50), Color.White);
                    sb.DrawString(buttonFont, "Back", new Vector2(graphics.PreferredBackBufferWidth / 2 - 33, 640), Color.Black);

                    // Draw text for the tracked stats
                    sb.DrawString(buttonFont, "Distance Travelled: " + player.TravelledDistance, new Vector2(graphics.PreferredBackBufferWidth / 2 - 125, 200), Color.White);
                    sb.DrawString(buttonFont, "Damage Taken: " + player.DamageTaken, new Vector2(graphics.PreferredBackBufferWidth / 2 - 125, 250), Color.White);
                    sb.DrawString(buttonFont, "Keys Collected: " + player.KeysCollected, new Vector2(graphics.PreferredBackBufferWidth / 2 - 125, 300), Color.White);
                    
                    break;

                case GameState.Gameover:
                    // Draw buttons for the gameover screen
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2 - 130, 420, 100, 60), Color.Gray);
                    sb.Draw(squareTexture, new Rectangle(graphics.PreferredBackBufferWidth / 2, 420, 100, 60), Color.Gray);

                    // Draw text for the gameover screen
                    sb.DrawString(titleFont, "GAME OVER", new Vector2(graphics.PreferredBackBufferWidth / 2 - 240, 200), Color.Black);

                    // Draw a funny comment, at random
                    if(randComment == 0)
                        sb.DrawString(buttonFont, "you are dead.", new Vector2(850, 350), Color.Yellow);
                    else if(randComment == 1)
                        sb.DrawString(buttonFont, "well.. that was quick.", new Vector2(810, 350), Color.Yellow);
                    else if (randComment == 2)
                        sb.DrawString(buttonFont, "maybe.. don't do that?", new Vector2(800, 350), Color.Yellow);
                    else if (randComment == 3)
                        sb.DrawString(buttonFont, "oops!", new Vector2(900, 350), Color.Yellow);
                    else if (randComment == 4)
                        sb.DrawString(buttonFont, "not a new highscore, that's for sure.", new Vector2(700, 350), Color.Yellow);


                    sb.DrawString(buttonFont, "Menu", new Vector2(graphics.PreferredBackBufferWidth / 2 - 120, 430), Color.Black);
                    sb.DrawString(buttonFont, "Quit", new Vector2(graphics.PreferredBackBufferWidth / 2 + 20, 430), Color.Black);
                    break;
            }
        }
    }
}
