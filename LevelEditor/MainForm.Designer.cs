namespace LevelEditor
{
    partial class MainForm
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
            this.createBox = new System.Windows.Forms.GroupBox();
            this.heightLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.widthBox = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.createBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // createBox
            // 
            this.createBox.Controls.Add(this.heightLabel);
            this.createBox.Controls.Add(this.label1);
            this.createBox.Controls.Add(this.heightBox);
            this.createBox.Controls.Add(this.widthBox);
            this.createBox.Location = new System.Drawing.Point(5, 100);
            this.createBox.Name = "createBox";
            this.createBox.Size = new System.Drawing.Size(275, 139);
            this.createBox.TabIndex = 0;
            this.createBox.TabStop = false;
            this.createBox.Text = "Create New Map";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(137, 35);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(76, 13);
            this.heightLabel.TabIndex = 4;
            this.heightLabel.Text = "Height (in tiles)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Width (in tiles)";
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(219, 32);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(46, 20);
            this.heightBox.TabIndex = 2;
            this.heightBox.Text = "12";
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(85, 32);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(46, 20);
            this.widthBox.TabIndex = 1;
            this.widthBox.Text = "12";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(12, 15);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(126, 70);
            this.createButton.TabIndex = 0;
            this.createButton.Text = "New Map";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(145, 15);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(125, 70);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load Map";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 167);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.createBox);
            this.Controls.Add(this.createButton);
            this.Name = "MainForm";
            this.Text = "Map Editor";
            this.createBox.ResumeLayout(false);
            this.createBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox createBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.TextBox heightBox;
        private System.Windows.Forms.TextBox widthBox;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label label1;
    }
}

