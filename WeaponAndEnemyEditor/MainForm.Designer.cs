namespace WeaponAndEnemyEditor
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
            this.weaponBuildButton = new System.Windows.Forms.Button();
            this.enemyBuildButton = new System.Windows.Forms.Button();
            this.enemyGroupBox = new System.Windows.Forms.GroupBox();
            this.weaponGroupBox = new System.Windows.Forms.GroupBox();
            this.weaponBox1 = new System.Windows.Forms.TextBox();
            this.weaponLabel1 = new System.Windows.Forms.Label();
            this.weaponLabel2 = new System.Windows.Forms.Label();
            this.weaponBox2 = new System.Windows.Forms.TextBox();
            this.weaponLabel3 = new System.Windows.Forms.Label();
            this.weaponBox3 = new System.Windows.Forms.TextBox();
            this.weaponRadioMelee = new System.Windows.Forms.RadioButton();
            this.weaponRadioRanged = new System.Windows.Forms.RadioButton();
            this.enemyBox3 = new System.Windows.Forms.TextBox();
            this.enemyLabel3 = new System.Windows.Forms.Label();
            this.enemyBox2 = new System.Windows.Forms.TextBox();
            this.enemyLabel2 = new System.Windows.Forms.Label();
            this.enemyLabel1 = new System.Windows.Forms.Label();
            this.enemyBox1 = new System.Windows.Forms.TextBox();
            this.enemyBox5 = new System.Windows.Forms.TextBox();
            this.enemyLabel5 = new System.Windows.Forms.Label();
            this.enemyLabel4 = new System.Windows.Forms.Label();
            this.enemyBox4 = new System.Windows.Forms.TextBox();
            this.enemyBox6 = new System.Windows.Forms.TextBox();
            this.enemyLoadButton = new System.Windows.Forms.Button();
            this.weaponLoadButton = new System.Windows.Forms.Button();
            this.enemyGroupBox.SuspendLayout();
            this.weaponGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // weaponBuildButton
            // 
            this.weaponBuildButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.weaponBuildButton.Location = new System.Drawing.Point(505, 600);
            this.weaponBuildButton.Name = "weaponBuildButton";
            this.weaponBuildButton.Size = new System.Drawing.Size(200, 100);
            this.weaponBuildButton.TabIndex = 0;
            this.weaponBuildButton.Text = "Build Weapon";
            this.weaponBuildButton.UseVisualStyleBackColor = false;
            this.weaponBuildButton.Click += new System.EventHandler(this.weaponBuildButton_Click);
            // 
            // enemyBuildButton
            // 
            this.enemyBuildButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.enemyBuildButton.Location = new System.Drawing.Point(100, 600);
            this.enemyBuildButton.Name = "enemyBuildButton";
            this.enemyBuildButton.Size = new System.Drawing.Size(200, 100);
            this.enemyBuildButton.TabIndex = 1;
            this.enemyBuildButton.Text = "Build Enemy";
            this.enemyBuildButton.UseVisualStyleBackColor = false;
            this.enemyBuildButton.Click += new System.EventHandler(this.enemyBuildButton_Click);
            // 
            // enemyGroupBox
            // 
            this.enemyGroupBox.Controls.Add(this.enemyBox6);
            this.enemyGroupBox.Controls.Add(this.enemyBox5);
            this.enemyGroupBox.Controls.Add(this.enemyLabel5);
            this.enemyGroupBox.Controls.Add(this.enemyLabel4);
            this.enemyGroupBox.Controls.Add(this.enemyBox4);
            this.enemyGroupBox.Controls.Add(this.enemyBox3);
            this.enemyGroupBox.Controls.Add(this.enemyLabel3);
            this.enemyGroupBox.Controls.Add(this.enemyBox2);
            this.enemyGroupBox.Controls.Add(this.enemyLabel2);
            this.enemyGroupBox.Controls.Add(this.enemyLabel1);
            this.enemyGroupBox.Controls.Add(this.enemyBox1);
            this.enemyGroupBox.Location = new System.Drawing.Point(12, 12);
            this.enemyGroupBox.Name = "enemyGroupBox";
            this.enemyGroupBox.Size = new System.Drawing.Size(356, 456);
            this.enemyGroupBox.TabIndex = 2;
            this.enemyGroupBox.TabStop = false;
            this.enemyGroupBox.Text = "Enemy Options:";
            // 
            // weaponGroupBox
            // 
            this.weaponGroupBox.Controls.Add(this.weaponRadioRanged);
            this.weaponGroupBox.Controls.Add(this.weaponRadioMelee);
            this.weaponGroupBox.Controls.Add(this.weaponBox3);
            this.weaponGroupBox.Controls.Add(this.weaponLabel3);
            this.weaponGroupBox.Controls.Add(this.weaponBox2);
            this.weaponGroupBox.Controls.Add(this.weaponLabel2);
            this.weaponGroupBox.Controls.Add(this.weaponLabel1);
            this.weaponGroupBox.Controls.Add(this.weaponBox1);
            this.weaponGroupBox.Location = new System.Drawing.Point(410, 12);
            this.weaponGroupBox.Name = "weaponGroupBox";
            this.weaponGroupBox.Size = new System.Drawing.Size(356, 456);
            this.weaponGroupBox.TabIndex = 3;
            this.weaponGroupBox.TabStop = false;
            this.weaponGroupBox.Text = "Weapon Options:";
            // 
            // weaponBox1
            // 
            this.weaponBox1.Location = new System.Drawing.Point(86, 55);
            this.weaponBox1.Name = "weaponBox1";
            this.weaponBox1.Size = new System.Drawing.Size(255, 26);
            this.weaponBox1.TabIndex = 0;
            // 
            // weaponLabel1
            // 
            this.weaponLabel1.AutoSize = true;
            this.weaponLabel1.Location = new System.Drawing.Point(6, 55);
            this.weaponLabel1.Name = "weaponLabel1";
            this.weaponLabel1.Size = new System.Drawing.Size(55, 20);
            this.weaponLabel1.TabIndex = 1;
            this.weaponLabel1.Text = "Name:";
            // 
            // weaponLabel2
            // 
            this.weaponLabel2.AutoSize = true;
            this.weaponLabel2.Location = new System.Drawing.Point(6, 116);
            this.weaponLabel2.Name = "weaponLabel2";
            this.weaponLabel2.Size = new System.Drawing.Size(74, 20);
            this.weaponLabel2.TabIndex = 2;
            this.weaponLabel2.Text = "Damage:";
            // 
            // weaponBox2
            // 
            this.weaponBox2.Location = new System.Drawing.Point(86, 116);
            this.weaponBox2.Name = "weaponBox2";
            this.weaponBox2.Size = new System.Drawing.Size(255, 26);
            this.weaponBox2.TabIndex = 3;
            // 
            // weaponLabel3
            // 
            this.weaponLabel3.AutoSize = true;
            this.weaponLabel3.Location = new System.Drawing.Point(6, 176);
            this.weaponLabel3.Name = "weaponLabel3";
            this.weaponLabel3.Size = new System.Drawing.Size(78, 20);
            this.weaponLabel3.TabIndex = 4;
            this.weaponLabel3.Text = "Durability:";
            // 
            // weaponBox3
            // 
            this.weaponBox3.Location = new System.Drawing.Point(86, 176);
            this.weaponBox3.Name = "weaponBox3";
            this.weaponBox3.Size = new System.Drawing.Size(255, 26);
            this.weaponBox3.TabIndex = 5;
            // 
            // weaponRadioMelee
            // 
            this.weaponRadioMelee.AutoSize = true;
            this.weaponRadioMelee.Location = new System.Drawing.Point(10, 223);
            this.weaponRadioMelee.Name = "weaponRadioMelee";
            this.weaponRadioMelee.Size = new System.Drawing.Size(164, 24);
            this.weaponRadioMelee.TabIndex = 6;
            this.weaponRadioMelee.TabStop = true;
            this.weaponRadioMelee.Text = "Melee Weapon (0)";
            this.weaponRadioMelee.UseVisualStyleBackColor = true;
            // 
            // weaponRadioRanged
            // 
            this.weaponRadioRanged.AutoSize = true;
            this.weaponRadioRanged.Location = new System.Drawing.Point(10, 254);
            this.weaponRadioRanged.Name = "weaponRadioRanged";
            this.weaponRadioRanged.Size = new System.Drawing.Size(178, 24);
            this.weaponRadioRanged.TabIndex = 7;
            this.weaponRadioRanged.TabStop = true;
            this.weaponRadioRanged.Text = "Ranged Weapon (1)";
            this.weaponRadioRanged.UseVisualStyleBackColor = true;
            // 
            // enemyBox3
            // 
            this.enemyBox3.Location = new System.Drawing.Point(122, 176);
            this.enemyBox3.Name = "enemyBox3";
            this.enemyBox3.Size = new System.Drawing.Size(219, 26);
            this.enemyBox3.TabIndex = 11;
            // 
            // enemyLabel3
            // 
            this.enemyLabel3.AutoSize = true;
            this.enemyLabel3.Location = new System.Drawing.Point(6, 176);
            this.enemyLabel3.Name = "enemyLabel3";
            this.enemyLabel3.Size = new System.Drawing.Size(110, 20);
            this.enemyLabel3.TabIndex = 10;
            this.enemyLabel3.Text = "Attack Speed:";
            // 
            // enemyBox2
            // 
            this.enemyBox2.Location = new System.Drawing.Point(86, 116);
            this.enemyBox2.Name = "enemyBox2";
            this.enemyBox2.Size = new System.Drawing.Size(255, 26);
            this.enemyBox2.TabIndex = 9;
            // 
            // enemyLabel2
            // 
            this.enemyLabel2.AutoSize = true;
            this.enemyLabel2.Location = new System.Drawing.Point(6, 116);
            this.enemyLabel2.Name = "enemyLabel2";
            this.enemyLabel2.Size = new System.Drawing.Size(74, 20);
            this.enemyLabel2.TabIndex = 8;
            this.enemyLabel2.Text = "Damage:";
            // 
            // enemyLabel1
            // 
            this.enemyLabel1.AutoSize = true;
            this.enemyLabel1.Location = new System.Drawing.Point(6, 55);
            this.enemyLabel1.Name = "enemyLabel1";
            this.enemyLabel1.Size = new System.Drawing.Size(60, 20);
            this.enemyLabel1.TabIndex = 7;
            this.enemyLabel1.Text = "Health:";
            // 
            // enemyBox1
            // 
            this.enemyBox1.Location = new System.Drawing.Point(86, 55);
            this.enemyBox1.Name = "enemyBox1";
            this.enemyBox1.Size = new System.Drawing.Size(255, 26);
            this.enemyBox1.TabIndex = 6;
            // 
            // enemyBox5
            // 
            this.enemyBox5.Location = new System.Drawing.Point(121, 288);
            this.enemyBox5.Name = "enemyBox5";
            this.enemyBox5.Size = new System.Drawing.Size(100, 26);
            this.enemyBox5.TabIndex = 15;
            // 
            // enemyLabel5
            // 
            this.enemyLabel5.AutoSize = true;
            this.enemyLabel5.Location = new System.Drawing.Point(6, 288);
            this.enemyLabel5.Name = "enemyLabel5";
            this.enemyLabel5.Size = new System.Drawing.Size(109, 20);
            this.enemyLabel5.TabIndex = 14;
            this.enemyLabel5.Text = "Position (X/Y):";
            // 
            // enemyLabel4
            // 
            this.enemyLabel4.AutoSize = true;
            this.enemyLabel4.Location = new System.Drawing.Point(6, 227);
            this.enemyLabel4.Name = "enemyLabel4";
            this.enemyLabel4.Size = new System.Drawing.Size(60, 20);
            this.enemyLabel4.TabIndex = 13;
            this.enemyLabel4.Text = "Speed:";
            // 
            // enemyBox4
            // 
            this.enemyBox4.Location = new System.Drawing.Point(86, 227);
            this.enemyBox4.Name = "enemyBox4";
            this.enemyBox4.Size = new System.Drawing.Size(255, 26);
            this.enemyBox4.TabIndex = 12;
            // 
            // enemyBox6
            // 
            this.enemyBox6.Location = new System.Drawing.Point(241, 288);
            this.enemyBox6.Name = "enemyBox6";
            this.enemyBox6.Size = new System.Drawing.Size(100, 26);
            this.enemyBox6.TabIndex = 16;
            // 
            // enemyLoadButton
            // 
            this.enemyLoadButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.enemyLoadButton.Location = new System.Drawing.Point(100, 474);
            this.enemyLoadButton.Name = "enemyLoadButton";
            this.enemyLoadButton.Size = new System.Drawing.Size(200, 100);
            this.enemyLoadButton.TabIndex = 4;
            this.enemyLoadButton.Text = "Load Enemy";
            this.enemyLoadButton.UseVisualStyleBackColor = false;
            this.enemyLoadButton.Click += new System.EventHandler(this.enemyLoadButton_Click);
            // 
            // weaponLoadButton
            // 
            this.weaponLoadButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.weaponLoadButton.Location = new System.Drawing.Point(505, 474);
            this.weaponLoadButton.Name = "weaponLoadButton";
            this.weaponLoadButton.Size = new System.Drawing.Size(200, 100);
            this.weaponLoadButton.TabIndex = 5;
            this.weaponLoadButton.Text = "Load Weapon";
            this.weaponLoadButton.UseVisualStyleBackColor = false;
            this.weaponLoadButton.Click += new System.EventHandler(this.weaponLoadButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 744);
            this.Controls.Add(this.weaponLoadButton);
            this.Controls.Add(this.enemyLoadButton);
            this.Controls.Add(this.weaponGroupBox);
            this.Controls.Add(this.enemyGroupBox);
            this.Controls.Add(this.enemyBuildButton);
            this.Controls.Add(this.weaponBuildButton);
            this.Name = "MainForm";
            this.Text = "Enemy & Weapon Editor";
            this.enemyGroupBox.ResumeLayout(false);
            this.enemyGroupBox.PerformLayout();
            this.weaponGroupBox.ResumeLayout(false);
            this.weaponGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button weaponBuildButton;
        private System.Windows.Forms.Button enemyBuildButton;
        private System.Windows.Forms.GroupBox enemyGroupBox;
        private System.Windows.Forms.GroupBox weaponGroupBox;
        private System.Windows.Forms.Label weaponLabel1;
        private System.Windows.Forms.TextBox weaponBox1;
        private System.Windows.Forms.TextBox weaponBox3;
        private System.Windows.Forms.Label weaponLabel3;
        private System.Windows.Forms.TextBox weaponBox2;
        private System.Windows.Forms.Label weaponLabel2;
        private System.Windows.Forms.RadioButton weaponRadioRanged;
        private System.Windows.Forms.RadioButton weaponRadioMelee;
        private System.Windows.Forms.TextBox enemyBox3;
        private System.Windows.Forms.Label enemyLabel3;
        private System.Windows.Forms.TextBox enemyBox2;
        private System.Windows.Forms.Label enemyLabel2;
        private System.Windows.Forms.Label enemyLabel1;
        private System.Windows.Forms.TextBox enemyBox1;
        private System.Windows.Forms.TextBox enemyBox5;
        private System.Windows.Forms.Label enemyLabel5;
        private System.Windows.Forms.Label enemyLabel4;
        private System.Windows.Forms.TextBox enemyBox4;
        private System.Windows.Forms.TextBox enemyBox6;
        private System.Windows.Forms.Button enemyLoadButton;
        private System.Windows.Forms.Button weaponLoadButton;
    }
}

