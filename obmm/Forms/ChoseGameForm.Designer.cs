namespace OblivionModManager.Forms
{
    partial class ChoseGameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChoseGameForm));
            this.lblPick = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgGames = new System.Windows.Forms.DataGridView();
            this.btnAddGamePath = new System.Windows.Forms.Button();
            this.dgvGameNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvGamePathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgGames)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPick
            // 
            this.lblPick.AutoSize = true;
            this.lblPick.Location = new System.Drawing.Point(12, 9);
            this.lblPick.Name = "lblPick";
            this.lblPick.Size = new System.Drawing.Size(110, 13);
            this.lblPick.TabIndex = 1;
            this.lblPick.Text = "Pick the game to mod";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(377, 235);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(74, 32);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(465, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // dgGames
            // 
            this.dgGames.AllowUserToAddRows = false;
            this.dgGames.AllowUserToDeleteRows = false;
            this.dgGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgGames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvGameNameColumn,
            this.dgvGamePathColumn});
            this.dgGames.Location = new System.Drawing.Point(15, 34);
            this.dgGames.Name = "dgGames";
            this.dgGames.ReadOnly = true;
            this.dgGames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgGames.Size = new System.Drawing.Size(519, 185);
            this.dgGames.TabIndex = 4;
            this.dgGames.SelectionChanged += new System.EventHandler(this.DgGames_SelectionChanged);
            // 
            // btnAddGamePath
            // 
            this.btnAddGamePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddGamePath.Enabled = false;
            this.btnAddGamePath.Location = new System.Drawing.Point(18, 235);
            this.btnAddGamePath.Name = "btnAddGamePath";
            this.btnAddGamePath.Size = new System.Drawing.Size(117, 32);
            this.btnAddGamePath.TabIndex = 5;
            this.btnAddGamePath.Text = "Set game path";
            this.btnAddGamePath.UseVisualStyleBackColor = true;
            this.btnAddGamePath.Click += new System.EventHandler(this.btnAddGamePath_Click);
            // 
            // dgvGameNameColumn
            // 
            this.dgvGameNameColumn.DataPropertyName = "Name";
            this.dgvGameNameColumn.HeaderText = "Name";
            this.dgvGameNameColumn.Name = "dgvGameNameColumn";
            this.dgvGameNameColumn.ReadOnly = true;
            // 
            // dgvGamePathColumn
            // 
            this.dgvGamePathColumn.DataPropertyName = "GamePath";
            this.dgvGamePathColumn.HeaderText = "Path";
            this.dgvGamePathColumn.Name = "dgvGamePathColumn";
            this.dgvGamePathColumn.ReadOnly = true;
            this.dgvGamePathColumn.Width = 400;
            // 
            // ChoseGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 279);
            this.Controls.Add(this.btnAddGamePath);
            this.Controls.Add(this.dgGames);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblPick);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChoseGameForm";
            this.Text = "Pick game to mod";
            ((System.ComponentModel.ISupportInitialize)(this.dgGames)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPick;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgGames;
        private System.Windows.Forms.Button btnAddGamePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvGameNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvGamePathColumn;
    }
}