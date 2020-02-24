using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//the main form allows the user to select the size of the new map, as well as load in an old map
namespace LevelEditor
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// loads the components of the main form
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// if the height and width match the requirements, creates a new level editor
        /// </summary>
        /// <param name="sender">the object that sent the function</param>
        /// <param name="e">unused event argument variable</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();
            if(int.TryParse(widthBox.Text, out int width)&&int.TryParse(heightBox.Text, out int height))
            {
                //adds all errors to a list
                if (height < 1)
                    errors.Add("- Height too low (min 1)");
                if (width < 1)
                    errors.Add("- Width too low (min 1)");
                if (height > 28)
                    errors.Add("- Height too high (max 28)");
                if (width > 58)
                    errors.Add("- Width too high (max 58)");
                //if no errors, starts a new editor
                if (errors.Count == 0)
                {
                    LevelEdit editor = new LevelEdit(width, height);
                    editor.ShowDialog();
                }
                //shows a message box with all errors
                else if (errors.Count == 2)
                    MessageBox.Show("Errors:\n" + errors[0] + "\n" + errors[1],"Error loading map");
                else
                    MessageBox.Show("Errors:\n" + errors[0],"Error loading map");
            }
            else
            {
                MessageBox.Show("Please enter numbers in the text boxes","Error loading map");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open Map File";
            open.Filter = "Map Files|*.map";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string name = open.FileName;
                LevelEdit editor = new LevelEdit(name);
                editor.ShowDialog();
            }    
        }
    }
}
