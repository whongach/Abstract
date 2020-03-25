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
            LevelEdit editor = new LevelEdit();
            editor.ShowDialog();
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
