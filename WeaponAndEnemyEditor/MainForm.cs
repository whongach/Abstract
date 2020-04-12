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
            // name,damage,durability,type (string,int,int,int)

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
                        weaponRadioSpin.Checked = true;
                        weaponRadioStab.Checked = false;
                        weaponRadioRanged.Checked = false;
                    }
                    else if (parseResult == 1)
                    {
                        weaponRadioSpin.Checked = false;
                        weaponRadioStab.Checked = true;
                        weaponRadioRanged.Checked = false;
                    }
                    else
                    {
                        weaponRadioSpin.Checked = false;
                        weaponRadioStab.Checked = false;
                        weaponRadioRanged.Checked = true;
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
            int durability;
            int weaponType; // 0 is a melee-spin, 1 is a melee-stab, 2 is a ranged weapon

            // ERROR CHECKING
            // Check for valid NON-NEGATIVE integer parameters damage / durability
            if (!int.TryParse(weaponBox2.Text, out damage) ||
                !int.TryParse(weaponBox3.Text, out durability) ||
                damage < 0 || durability < 0)
            {
                MessageBox.Show(
                    "Errors:\n- Please enter valid NON-NEGATIVE integer parameters for the weapon damage and durability",
                    "Error building weapon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // ERROR CHECKING
            // Make sure at least one radio box is checked
            if (!weaponRadioSpin.Checked && !weaponRadioStab.Checked && !weaponRadioRanged.Checked)
            {
                MessageBox.Show(
                    "Errors:\n- Please check at least one weapon type: spin/stab/ranged ",
                    "Error building weapon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Parse inputs after the check
            name = weaponBox1.Text;
            damage = int.Parse(weaponBox2.Text);
            durability = int.Parse(weaponBox3.Text);
            
            // Set weaponType to the selected radio box
            if(weaponRadioSpin.Checked)
            {
                // Melee-Spin Weapon
                weaponType = 0;
            }
            else if(weaponRadioStab.Checked)
            {
                // Melee-Stab Weapon
                weaponType = 1;
            }
            else
            {
                // Ranged Weapon
                weaponType = 2;
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
                    output.WriteLine(name + "," + damage + "," + durability + "," + weaponType);

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
            // health,damage,attackSpeed,speed,xCoord,yCoord (int,int,int,int,int,int)

            // Creates a new OpenFileDialog & opens a .weapon file
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open a enemy file";
            openFile.Filter = "Enemy Files|*.enemy";
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

                    // Read in the data and store it in a size 6 array
                    string[] fileContents = new string[6]; // stores the 6 parameters
                    string line = input.ReadLine();
                    fileContents = line.Split(',');

                    // Set weapon values to the file contents
                    enemyBox1.Text = fileContents[0];
                    enemyBox2.Text = fileContents[1];
                    enemyBox3.Text = fileContents[2];
                    enemyBox4.Text = fileContents[3];
                    enemyBox5.Text = fileContents[4];
                    enemyBox6.Text = fileContents[5];

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
            int xCoord;
            int yCoord;

            // ERROR CHECKING
            // Check for valid NON-NEGATIVE integer parameters
            if (!int.TryParse(enemyBox1.Text, out health) ||
                !int.TryParse(enemyBox2.Text, out damage) ||
                !int.TryParse(enemyBox3.Text, out attackSpeed) ||
                !int.TryParse(enemyBox4.Text, out speed) ||
                !int.TryParse(enemyBox5.Text, out xCoord) ||
                !int.TryParse(enemyBox6.Text, out yCoord) ||
                health < 0 || damage < 0 || attackSpeed < 0 ||
                speed < 0 || xCoord < 0 || yCoord < 0)
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
            xCoord = int.Parse(enemyBox5.Text);
            yCoord = int.Parse(enemyBox6.Text);

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
                                    + "," + xCoord + "," + yCoord);

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
