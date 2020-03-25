using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//level editor is a class that allows the user to select and paint with a given color palette
namespace LevelEditor
{
    public partial class LevelEdit : Form
    {
        //fields
        private int tileSize;
        private PictureBox[,] map;
        private int[,] tileTypes;
        private string fileName;
        private BinaryReader reader;
        private Stream file;
        private BinaryWriter writer;
        private Boolean unsaved;

        /// <summary>
        /// Constructor with dimensions
        /// </summary>
        public LevelEdit()
        {
            tileSize = 32;
            InitializeComponent();
            this.unsaved = false;
            //adding buttons to mapBox
            this.mapBox.Size = new System.Drawing.Size(16*tileSize+50, 16*tileSize+50);
            this.ClientSize = new System.Drawing.Size(300, 103 + mapBox.Height);
            if (mapBox.Width > 290)
                this.ClientSize = new System.Drawing.Size(mapBox.Width + 10, mapBox.Height + 103);
            tileTypes = new int[16, 16];
            map = new PictureBox[16, 16];
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                {
                    map[i, j] = new PictureBox();
                    map[i, j].BackColor = Color.Transparent;
                    map[i, j].Size = new Size(tileSize, tileSize);
                    map[i, j].Location = new Point(25 + tileSize * i, 30 + tileSize * j);
                    map[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChangeColor_MouseDown);
                    map[i, j].MouseEnter += new System.EventHandler(this.ChangeColor_MouseEnter);
                    tileTypes[i, j] = 0;
                    mapBox.Controls.Add(map[i, j]);
                }
            SyncIntToImg();

            this.fileName = "";
        }

        /// <summary>
        /// Constructor with file
        /// </summary>
        /// <param name="fileName">the name of the file to read in from</param>
        public LevelEdit(string fileName)
        {
            InitializeComponent();
            this.fileName = fileName;
            file = File.OpenRead(fileName);
            reader = new BinaryReader(file);
            tileSize = 32;
            this.unsaved = false;
            //adding buttons to mapBox
            this.mapBox.Size = new System.Drawing.Size(16 * tileSize + 50, 16 * tileSize + 50);
            this.ClientSize = new System.Drawing.Size(300, 103 + mapBox.Height);
            if (mapBox.Width > 290)
                this.ClientSize = new System.Drawing.Size(mapBox.Width + 10, mapBox.Height + 103);
            tileTypes = new int[16, 16];
            map = new PictureBox[16, 16];
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                {
                    map[i, j] = new PictureBox();
                    map[i, j].Size = new Size(tileSize, tileSize);
                    map[i, j].Location = new Point(25 + tileSize * i, 30 + tileSize * j);
                    map[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChangeColor_MouseDown);
                    map[i, j].MouseEnter += new System.EventHandler(this.ChangeColor_MouseEnter);
                    tileTypes[i, j] = reader.ReadInt32();
                    mapBox.Controls.Add(map[i, j]);
                }
            SyncIntToImg();
            file.Close();
            this.Text = "Map Editor - " + fileName;
            MessageBox.Show("File loaded successfully.","File loaded");
            this.unsaved = false;
        }

        /// <summary>
        /// sets the current color to the color of the tile clicked
        /// </summary>
        /// <param name="sender">the specific colored button</param>
        /// <param name="e">unused event args variable</param>
        private void ColorSelect_Click(object sender, EventArgs e) 
        {
            currentTile.Image = ((Button)sender).Image;
            SyncImgToInt();
        }
        /// <summary>
        /// sets the color of the clicked tile to the current color
        /// </summary>
        /// <param name="sender">the tile that will have its color changed</param>
        /// <param name="e">unused event args variable</param>
        private void ChangeColor_MouseDown(object sender, EventArgs e)
        {
            ((PictureBox)sender).Capture = false;
            if (MouseButtons == MouseButtons.Left)
            {
                ((PictureBox)sender).Image = currentTile.Image;
                SyncImgToInt();
                if (fileName == "")
                    this.Text = "Map Editor *";
                else
                    this.Text = "Map Editor - " + fileName + " *";
                this.unsaved = true;
            }
        }

        /// <summary>
        /// sets the color of the clicked tile to the current color
        /// </summary>
        /// <param name="sender">the tile that will have its color changed</param>
        /// <param name="e">unused event args variable</param>
        private void ChangeColor_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).Capture = false;
            if (MouseButtons == MouseButtons.Left)
            {
                ((PictureBox)sender).Image = currentTile.Image;
                SyncImgToInt();
                if (fileName == "")
                    this.Text = "Map Editor *";
                else
                    this.Text = "Map Editor - " + fileName + " *";
                this.unsaved = true;
            }
        }

        /// <summary>
        /// Loads in a map from a selected file
        /// </summary>
        /// <param name="sender">the object calling this method</param>
        /// <param name="e">unused event args variable</param>
        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open Map File";
            open.Filter = "Map Files|*.map";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string name = open.FileName;
                this.fileName = name;
                file = File.OpenRead(fileName);
                reader = new BinaryReader(file);
                tileSize = 32;
                this.unsaved = false;
                //adding buttons to mapBox
                this.mapBox.Size = new System.Drawing.Size(16 * tileSize + 50, 16 * tileSize + 50);
                this.ClientSize = new System.Drawing.Size(300, 103 + mapBox.Height);
                if (mapBox.Width > 290)
                    this.ClientSize = new System.Drawing.Size(mapBox.Width + 10, mapBox.Height + 103);
                mapBox.Controls.Clear();
                tileTypes = new int[16, 16];
                map = new PictureBox[16, 16];
                for (int i = 0; i < 16; i++)
                    for (int j = 0; j < 16; j++)
                    {
                        map[i, j] = new PictureBox();
                        map[i, j].Size = new Size(tileSize, tileSize);
                        map[i, j].Location = new Point(25 + tileSize * i, 30 + tileSize * j);
                        map[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChangeColor_MouseDown);
                        map[i, j].MouseEnter += new System.EventHandler(this.ChangeColor_MouseEnter);
                        tileTypes[i, j] = reader.ReadInt32();
                        mapBox.Controls.Add(map[i, j]);
                    }
                SyncIntToImg();
                file.Close();
                this.Text = "Map Editor - " + fileName;
                MessageBox.Show("File loaded successfully.","File loaded");
                this.unsaved = false;
            }    
        }

        /// <summary>
        /// Saves the map in a selected file
        /// </summary>
        /// <param name="sender">the object calling this method</param>
        /// <param name="e">unused event args variable</param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save Map File";
            save.Filter = "Map Files|*.map";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string name = save.FileName;
                this.fileName = name;
                file = File.OpenWrite(fileName);
                writer = new BinaryWriter(file);
                for (int i = 0; i < 16; i++)
                    for (int j = 0; j < 16; j++)
                        writer.Write(tileTypes[i,j]);
                file.Close();
                this.Text = "Map Editor - " + fileName;
                MessageBox.Show("File saved successfully.","File saved");
                this.unsaved = false;
            }
            
        }

        /// <summary>
        /// Checks if the form has unsaved changes, and allows the user to cancel the forms close
        /// </summary>
        /// <param name="sender">the object calling this method</param>
        /// <param name="e"></param>
        private void LevelEdit_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (unsaved)
            {
                DialogResult result = MessageBox.Show("There are unsaved changes. Are you sure you want to quit?", "Unsaved Changes", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    e.Cancel = true;

            }
        }

        /// <summary>
        /// changes all values in the tiletype array to match the images on the tiles
        /// </summary>
        private void SyncImgToInt()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if(map[i,j].Image == floorSelect.Image)
                        tileTypes[i, j] = 0;
                    if(map[i,j].Image == wallSelect.Image)
                        tileTypes[i, j] = 1;
                }
            }
        }

        /// <summary>
        /// changes all images on the tiles to match the tiletype array
        /// </summary>
        private void SyncIntToImg()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (tileTypes[i, j] == 0)
                        map[i, j].Image = floorSelect.Image;
                    if (tileTypes[i, j] == 1)
                        map[i, j].Image = wallSelect.Image;
                }
            }
        }
    }
}
