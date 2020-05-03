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

namespace WeaponAndEnemyEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads a new weapon from a .weapon file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weaponLoadButton_Click(object sender, EventArgs e)
        {
            // WEAPON FILE FORMAT
            // name,damage,size,type (string,int,int,int)

            // Creates a new OpenFileDialog & opens a .weapon file
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open a weapon file";
            openFile.Filter = "Weapon Files|*.weapon";
            DialogResult result = openFile.ShowDialog();

            // If the user presses OK, load the file
            if (result == DialogResult.OK)
            {
                // Create the stream and the writer
                FileStream inStream = null;
                StreamReader input = null;

                try
                {
                    // Initializes both stream and writer
                    inStream = File.OpenRead(openFile.FileName);
                    input = new StreamReader(inStream);

                    // Read in the data and store it in a size 4 array
                    string[] fileContents = new string[4]; // stores the 4 parameters
                    string line = input.ReadLine();
                    fileContents = line.Split(',');

                    // Set weapon values to the file contents
                    weaponBox1.Text = fileContents[0];
                    weaponBox2.Text = fileContents[1];
                    weaponBox3.Text = fileContents[2];

                    int parseResult = -1;
                    if (int.TryParse(fileContents[3], out parseResult) && parseResult == 0)
                    {
                        weaponRadioSword.Checked = true;
                        weaponRadioSpear.Checked = false;
                        weaponRadioWand.Checked = false;
                        weaponRadioBow.Checked = false;
                    }
                    else if (parseResult == 1)
                    {
                        weaponRadioSword.Checked = false;
                        weaponRadioSpear.Checked = true;
                        weaponRadioWand.Checked = false;
                        weaponRadioBow.Checked = false;
                    }
                    else if (parseResult == 2)
                    {
                        weaponRadioSword.Checked = false;
                        weaponRadioSpear.Checked = false;
                        weaponRadioWand.Checked = true;
                        weaponRadioBow.Checked = false;
                    }
                    else
                    {
                        weaponRadioSword.Checked = false;
                        weaponRadioSpear.Checked = false;
                        weaponRadioWand.Checked = false;
                        weaponRadioBow.Checked = true;
                    }

                } // end try
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading data: " + ex.Message);
                }
                finally
                {
                    if (input != null)
                    {
                        input.Close();
                    }
                }
            } // end file loading
        } // end weaponLoadButton_Click()

        /// <summary>
        /// Builds a new weapon with a .weapon extension
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weaponBuildButton_Click(object sender, EventArgs e)
        {
            // WEAPON FILE FORMAT
            // name,damage,durability,type (string,int,int,int)

            // Creates variables to hold text box inputs
            string name;
            int damage;
            int size;
            int weaponType; // 0 sword, 1 spear, 2 wand, 3 bow

            // ERROR CHECKING
            // Check for valid NON-NEGATIVE integer parameters damage / durability
            if (!int.TryParse(weaponBox2.Text, out damage) ||
                !int.TryParse(weaponBox3.Text, out size) ||
                damage < 0 || size < 0)
            {
                MessageBox.Show(
                    "Errors:\n- Please enter valid NON-NEGATIVE integer parameters for the weapon damage and size",
                    "Error building weapon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // ERROR CHECKING
            // Make sure at least one radio box is checked
            if (!weaponRadioSword.Checked && !weaponRadioSpear.Checked && !weaponRadioWand.Checked && !weaponRadioBow.Checked)
            {
                MessageBox.Show(
                    "Errors:\n- Please check at least one weapon type: sword/spear/wand/bow ",
                    "Error building weapon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Parse inputs after the check
            name = weaponBox1.Text;
            damage = int.Parse(weaponBox2.Text);
            size = int.Parse(weaponBox3.Text);
            
            // Set weaponType to the selected radio box
            if(weaponRadioSword.Checked)
            {
                weaponType = 0;
            }
            else if(weaponRadioSpear.Checked)
            {
                weaponType = 1;
            }
            else if(weaponRadioWand.Checked)
            {
                weaponType = 2;
            }
            else
            {
                weaponType = 3;
            }

            // Creates a new SaveFileDialog
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Save a weapon file.";
            saveFile.Filter = "Weapon Files|*.weapon";
            DialogResult result = saveFile.ShowDialog();

            // If the user presses OK, save the file
            if (result == DialogResult.OK)
            {
                // Create the stream and the writer
                FileStream outStream = null;
                StreamWriter output = null;

                try
                {
                    // Initializes both stream and writer
                    outStream = File.OpenWrite(saveFile.FileName);
                    output = new StreamWriter(outStream);

                    // Write the parameters, in order, separated by commas
                    output.WriteLine(name + "," + damage + "," + size + "," + weaponType);

                    // Display a success message box
                    MessageBox.Show(
                        "File saved successfully.",
                        "File saved",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving data: " + ex.Message);
                }
                finally
                {
                    if (output != null)
                    {
                        output.Close();
                    }
                }
            }

        } // end weaponBuildButton_Click()

        /// <summary>
        /// Loads a new enemy from a .enemy file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enemyLoadButton_Click(object sender, EventArgs e)
        {
            // ENEMY FILE FORMAT
            // health,damage,attackSpeed,speed,xSize,ySize,Movement,Weapon (int,int,int,int,int,int,int,int)

            // Creates a new OpenFileDialog & opens a .weapon file
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open a enemy file";
            openFile.Filter = "Enemy Files|*.enemy";
            DialogResult result = openFile.ShowDialog();

            // If the user presses OK, load the file
            if (result == DialogResult.OK)
            {
                // Create the stream and the reader
                FileStream inStream = null;
                StreamReader input = null;

                try
                {
                    // Initializes both stream and reader
                    inStream = File.OpenRead(openFile.FileName);
                    input = new StreamReader(inStream);

                    // Read in the data and store it in a size 8 array
                    string[] fileContents = new string[8]; // stores the 8 parameters
                    string line = input.ReadLine();
                    fileContents = line.Split(',');

                    // Set weapon values to the file contents
                    enemyBox1.Text = fileContents[0];
                    enemyBox2.Text = fileContents[1];
                    enemyBox3.Text = fileContents[2];
                    enemyBox4.Text = fileContents[3];
                    enemyBox5.Text = fileContents[4];
                    enemyBox6.Text = fileContents[5];
                    enemyBox7.Text = fileContents[6];
                    enemyBox8.Text = fileContents[7];

                } // end try
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading data: " + ex.Message);
                }
                finally
                {
                    if (input != null)
                    {
                        input.Close();
                    }
                }
            } // end file loading
        } // end enemyLoadButton_Click()

        /// <summary>
        /// Builds a new enemy with a .enemy extension
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enemyBuildButton_Click(object sender, EventArgs e)
        {
            // ENEMY FILE FORMAT
            // health,damage,attackSpeed,speed,xCoord,yCoord (int,int,int,int,int,int)

            // Creates variables to hold text box inputs
            int health;
            int damage;
            int attackSpeed;
            int speed;
            int xSize;
            int ySize;
            int movementType;
            int weaponType;

            // ERROR CHECKING
            // Check for valid NON-NEGATIVE integer parameters
            if (!int.TryParse(enemyBox1.Text, out health) ||
                !int.TryParse(enemyBox2.Text, out damage) ||
                !int.TryParse(enemyBox3.Text, out attackSpeed) ||
                !int.TryParse(enemyBox4.Text, out speed) ||
                !int.TryParse(enemyBox5.Text, out xSize) ||
                !int.TryParse(enemyBox6.Text, out ySize) ||
                !int.TryParse(enemyBox7.Text, out movementType) ||
                !int.TryParse(enemyBox8.Text, out weaponType) ||
                health < 0 || damage < 0 || attackSpeed < 0 ||
                speed < 0 || xSize < 0 || ySize < 0 || movementType < 0
                || weaponType < 0)
            {
                MessageBox.Show(
                    "Errors:\n- Please enter valid NON-NEGATIVE integer parameters for all enemy parameters",
                    "Error building enemy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Parse inputs after the check
            health = int.Parse(enemyBox1.Text);
            damage = int.Parse(enemyBox2.Text);
            attackSpeed = int.Parse(enemyBox3.Text);
            speed = int.Parse(enemyBox4.Text);
            xSize = int.Parse(enemyBox5.Text);
            ySize = int.Parse(enemyBox6.Text);
            movementType = int.Parse(enemyBox7.Text);
            weaponType = int.Parse(enemyBox8.Text);

            // Creates a new SaveFileDialog
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Save an enemy file.";
            saveFile.Filter = "Enemy Files|*.enemy";
            DialogResult result = saveFile.ShowDialog();

            // If the user presses OK, save the file
            if (result == DialogResult.OK)
            {
                // Create the stream and the writer
                FileStream outStream = null;
                StreamWriter output = null;

                try
                {
                    // Initializes both stream and writer
                    outStream = File.OpenWrite(saveFile.FileName);
                    output = new StreamWriter(outStream);

                    // Write the parameters, in order, separated by commas
                    output.WriteLine(health + "," + damage + "," + attackSpeed + "," + speed
                                    + "," + xSize + "," + ySize + "," + movementType + "," + weaponType);

                    // Display a success message box
                    MessageBox.Show(
                        "File saved successfully.",
                        "File saved",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving data: " + ex.Message);
                }
                finally
                {
                    if (output != null)
                    {
                        output.Close();
                    }
                }
            }
        } // end enemyBuildButton_Click

    }
}
