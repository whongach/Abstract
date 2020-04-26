// Generated Namespace References
using System.Collections.Generic;

// Namespace References
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The namespace containing the game project.
/// </summary>
namespace GroupGame
{
    /// <summary>
    /// Class for the game Map and Map drawing.
    /// </summary>
    class Map
    {
        // Fields
        private readonly Tile[,] layout;
        private readonly List<Tile> walls;

        // Properties
        /// <summary>
        /// Gets the Tile layout of the Map.
        /// </summary>
        public Tile[,] Layout { get { return layout; } }

        /// <summary>
        /// Gets the Walls in the Map.
        /// </summary>
        public List<Tile> Walls { get { return walls; } }

        // Constructors
        /// <summary>
        /// Constructs a Map object.
        /// </summary>
        /// <param name="tileSize">The size of each individual Tile.</param>
        /// <param name="layout">The layout of the Map.</param>
        /// <param name="floorTexture">The Texture2D representing the floor's texture.</param>
        /// <param name="wallTexture">The Texture2D representing the wall's texture.</param>
        public Map(int tileSize, int[,] layout, Texture2D floorTexture, Texture2D wallTexture)
        {
            // Initialize Fields
            this.layout = new Tile[16, 16];
            this.walls = new List<Tile>();

            // Fill the Map
            // Temporary Fields
            Tile currentWall;

            // Iterate through Map size
            for (int i = 0; i < this.layout.GetLength(0); i++)
            {
                for (int j = 0; j < this.layout.GetLength(1); j++)
                {
                    // If the constructed layout at the location is a wall tile
                    if (layout[i, j] == 1)
                    {
                        // Build a new currentWall tile
                        currentWall = new Tile(new Rectangle(i * tileSize,
                                                             j * tileSize,
                                                             tileSize,
                                                             tileSize),
                                                             wallTexture,
                                                             true);

                        // Set the Map layout to include the new wall tile
                        this.layout[i, j] = currentWall;

                        // Add the wall tile to the List of wall tiles
                        walls.Add(currentWall);
                    }
                    else
                    {
                        // If it is not a Wall, build a floor tile
                        this.layout[i, j] = new Tile(new Rectangle(i * tileSize,
                                                     j * tileSize,
                                                     tileSize,
                                                     tileSize),
                                                     floorTexture,
                                                     false);
                    }
                }
            }
        }

        // Methods
        /// <summary>
        /// Draws the Tiles.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used to draw the Tiles.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Iterate through the Tile layout
            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    // Draw the stored Tile
                    layout[i, j].Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Shifts the Tiles in the Map.
        /// </summary>
        /// <param name="origin">The Point that represents the top left corner of the Map.</param>
        /// <param name="scaledSize">The width and height of a scaled Tile.</param>
        public void SetOrigin(Point origin, int scaledSize)
        {
            // Iterate through the Tile layout
            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    // Shift the Tiles in the Map
                    layout[i, j].Position = new Rectangle(origin.X + i * scaledSize, origin.Y + j * scaledSize, scaledSize, scaledSize);
                }
            }
        }
    }
}
