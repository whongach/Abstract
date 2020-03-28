namespace ResourceManager
{
    partial class Form1
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
            this.addMap = new System.Windows.Forms.Button();
            this.addEnemy = new System.Windows.Forms.Button();
            this.addWeapon = new System.Windows.Forms.Button();
            this.resetData = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.viewItems = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addMap
            // 
            this.addMap.Location = new System.Drawing.Point(12, 12);
            this.addMap.Name = "addMap";
            this.addMap.Size = new System.Drawing.Size(123, 88);
            this.addMap.TabIndex = 0;
            this.addMap.Text = "Add Map";
            this.addMap.UseVisualStyleBackColor = true;
            this.addMap.Click += new System.EventHandler(this.addMap_Click);
            // 
            // addEnemy
            // 
            this.addEnemy.Location = new System.Drawing.Point(146, 12);
            this.addEnemy.Name = "addEnemy";
            this.addEnemy.Size = new System.Drawing.Size(123, 88);
            this.addEnemy.TabIndex = 1;
            this.addEnemy.Text = "Add Enemy";
            this.addEnemy.UseVisualStyleBackColor = true;
            this.addEnemy.Click += new System.EventHandler(this.addEnemy_Click);
            // 
            // addWeapon
            // 
            this.addWeapon.Location = new System.Drawing.Point(279, 12);
            this.addWeapon.Name = "addWeapon";
            this.addWeapon.Size = new System.Drawing.Size(123, 88);
            this.addWeapon.TabIndex = 2;
            this.addWeapon.Text = "Add Weapon";
            this.addWeapon.UseVisualStyleBackColor = true;
            this.addWeapon.Click += new System.EventHandler(this.addWeapon_Click);
            // 
            // resetData
            // 
            this.resetData.Location = new System.Drawing.Point(12, 107);
            this.resetData.Name = "resetData";
            this.resetData.Size = new System.Drawing.Size(123, 23);
            this.resetData.TabIndex = 3;
            this.resetData.Text = "Reset";
            this.resetData.UseVisualStyleBackColor = true;
            this.resetData.Click += new System.EventHandler(this.resetData_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(146, 106);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(123, 23);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = "Delete Item";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // viewItems
            // 
            this.viewItems.Location = new System.Drawing.Point(279, 106);
            this.viewItems.Name = "viewItems";
            this.viewItems.Size = new System.Drawing.Size(123, 23);
            this.viewItems.TabIndex = 5;
            this.viewItems.Text = "View Items";
            this.viewItems.UseVisualStyleBackColor = true;
            this.viewItems.Click += new System.EventHandler(this.viewItems_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 142);
            this.Controls.Add(this.viewItems);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.resetData);
            this.Controls.Add(this.addWeapon);
            this.Controls.Add(this.addEnemy);
            this.Controls.Add(this.addMap);
            this.Name = "Form1";
            this.Text = "ResourceManager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addMap;
        private System.Windows.Forms.Button addEnemy;
        private System.Windows.Forms.Button addWeapon;
        private System.Windows.Forms.Button resetData;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button viewItems;
    }
}

