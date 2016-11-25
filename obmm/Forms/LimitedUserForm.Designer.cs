namespace OblivionModManager.Forms {
    partial class LimitedUserForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lvEspList = new System.Windows.Forms.ListView();
            this.bSaves = new System.Windows.Forms.Button();
            this.bBSAs = new System.Windows.Forms.Button();
            this.bConflicts = new System.Windows.Forms.Button();
            this.bLaunch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvEspList
            // 
            this.lvEspList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEspList.CheckBoxes = true;
            this.lvEspList.Location = new System.Drawing.Point(12, 12);
            this.lvEspList.Name = "lvEspList";
            this.lvEspList.Size = new System.Drawing.Size(418, 220);
            this.lvEspList.TabIndex = 0;
            this.lvEspList.UseCompatibleStateImageBehavior = false;
            this.lvEspList.View = System.Windows.Forms.View.List;
            this.lvEspList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvEspList_ItemCheck);
            // 
            // bSaves
            // 
            this.bSaves.Location = new System.Drawing.Point(12, 238);
            this.bSaves.Name = "bSaves";
            this.bSaves.Size = new System.Drawing.Size(100, 23);
            this.bSaves.TabIndex = 1;
            this.bSaves.Text = "Save manager";
            this.bSaves.UseVisualStyleBackColor = true;
            this.bSaves.Click += new System.EventHandler(this.bSaves_Click);
            // 
            // bBSAs
            // 
            this.bBSAs.Location = new System.Drawing.Point(118, 238);
            this.bBSAs.Name = "bBSAs";
            this.bBSAs.Size = new System.Drawing.Size(100, 23);
            this.bBSAs.TabIndex = 2;
            this.bBSAs.Text = "BSA browser";
            this.bBSAs.UseVisualStyleBackColor = true;
            this.bBSAs.Click += new System.EventHandler(this.bBSAs_Click);
            // 
            // bConflicts
            // 
            this.bConflicts.Location = new System.Drawing.Point(224, 238);
            this.bConflicts.Name = "bConflicts";
            this.bConflicts.Size = new System.Drawing.Size(100, 23);
            this.bConflicts.TabIndex = 3;
            this.bConflicts.Text = "Conflict report";
            this.bConflicts.UseVisualStyleBackColor = true;
            this.bConflicts.Click += new System.EventHandler(this.bConflicts_Click);
            // 
            // bLaunch
            // 
            this.bLaunch.Location = new System.Drawing.Point(330, 238);
            this.bLaunch.Name = "bLaunch";
            this.bLaunch.Size = new System.Drawing.Size(100, 23);
            this.bLaunch.TabIndex = 4;
            this.bLaunch.Text = "Launch Oblivion";
            this.bLaunch.UseVisualStyleBackColor = true;
            this.bLaunch.Click += new System.EventHandler(this.bLaunch_Click);
            // 
            // LimitedUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 273);
            this.Controls.Add(this.bLaunch);
            this.Controls.Add(this.bConflicts);
            this.Controls.Add(this.bBSAs);
            this.Controls.Add(this.bSaves);
            this.Controls.Add(this.lvEspList);
            this.MinimumSize = new System.Drawing.Size(450, 300);
            this.Name = "LimitedUserForm";
            this.Text = "Oblivion Mod Manager (Limited user mode)";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvEspList;
        private System.Windows.Forms.Button bSaves;
        private System.Windows.Forms.Button bBSAs;
        private System.Windows.Forms.Button bConflicts;
        private System.Windows.Forms.Button bLaunch;
    }
}