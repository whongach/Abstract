using System.Drawing;
using System.Windows.Forms;

namespace LevelEditor
{
    partial class LevelEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelEdit));
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.tileSelect = new System.Windows.Forms.GroupBox();
            this.wallSelect = new System.Windows.Forms.Button();
            this.floorSelect = new System.Windows.Forms.Button();
            this.mapBox = new System.Windows.Forms.GroupBox();
            this.currentBox = new System.Windows.Forms.GroupBox();
            this.currentTile = new System.Windows.Forms.PictureBox();
            this.tileSelect.SuspendLayout();
            this.currentBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentTile)).BeginInit();
            this.SuspendLayout();
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(237, 54);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(53, 27);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(237, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(53, 27);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tileSelect
            // 
            this.tileSelect.Controls.Add(this.wallSelect);
            this.tileSelect.Controls.Add(this.floorSelect);
            this.tileSelect.Location = new System.Drawing.Point(5, 5);
            this.tileSelect.Name = "tileSelect";
            this.tileSelect.Size = new System.Drawing.Size(125, 76);
            this.tileSelect.TabIndex = 2;
            this.tileSelect.TabStop = false;
            this.tileSelect.Text = "Tile Selector";
            // 
            // wallSelect
            // 
            this.wallSelect.ForeColor = System.Drawing.Color.Black;
            this.wallSelect.Image = global::LevelEditor.Properties.Resources.wallSprite;
            this.wallSelect.Location = new System.Drawing.Point(75, 20);
            this.wallSelect.Name = "wallSelect";
            this.wallSelect.Size = new System.Drawing.Size(40, 40);
            this.wallSelect.TabIndex = 1;
            this.wallSelect.UseVisualStyleBackColor = false;
            this.wallSelect.Click += new System.EventHandler(this.ColorSelect_Click);
            // 
            // floorSelect
            // 
            this.floorSelect.ForeColor = System.Drawing.Color.Black;
            this.floorSelect.Image = ((System.Drawing.Image)(resources.GetObject("floorSelect.Image")));
            this.floorSelect.Location = new System.Drawing.Point(15, 20);
            this.floorSelect.Name = "floorSelect";
            this.floorSelect.Size = new System.Drawing.Size(40, 40);
            this.floorSelect.TabIndex = 0;
            this.floorSelect.UseVisualStyleBackColor = false;
            this.floorSelect.Click += new System.EventHandler(this.ColorSelect_Click);
            // 
            // mapBox
            // 
            this.mapBox.Location = new System.Drawing.Point(5, 97);
            this.mapBox.Name = "mapBox";
            this.mapBox.Size = new System.Drawing.Size(285, 171);
            this.mapBox.TabIndex = 4;
            this.mapBox.TabStop = false;
            this.mapBox.Text = "Map";
            // 
            // currentBox
            // 
            this.currentBox.Controls.Add(this.currentTile);
            this.currentBox.Location = new System.Drawing.Point(136, 5);
            this.currentBox.Name = "currentBox";
            this.currentBox.Size = new System.Drawing.Size(95, 76);
            this.currentBox.TabIndex = 3;
            this.currentBox.TabStop = false;
            this.currentBox.Text = "Current Tile";
            // 
            // currentTile
            // 
            this.currentTile.BackColor = System.Drawing.Color.Transparent;
            this.currentTile.Location = new System.Drawing.Point(18, 16);
            this.currentTile.Name = "currentTile";
            this.currentTile.Size = new System.Drawing.Size(53, 54);
            this.currentTile.TabIndex = 0;
            this.currentTile.TabStop = false;
            // 
            // LevelEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 551);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.currentBox);
            this.Controls.Add(this.tileSelect);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.loadButton);
            this.Name = "LevelEdit";
            this.Text = "Map Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LevelEdit_FormClosing);
            this.tileSelect.ResumeLayout(false);
            this.currentBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentTile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox tileSelect;
        private System.Windows.Forms.Button wallSelect;
        private System.Windows.Forms.Button floorSelect;
        private System.Windows.Forms.GroupBox mapBox;
        private PictureBox currentTile;
        private GroupBox currentBox;
    }
}