namespace OblivionModManager
{
    partial class DownloadsForm
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
            this.components = new System.ComponentModel.Container();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.downloadsList = new System.Windows.Forms.DataGridView();
            this.downloadsRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.dgEnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgURLColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgModNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgProgressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.downloadsList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(57, 24);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(121, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(57, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(62, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(57, 24);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // downloadsList
            // 
            this.downloadsList.AllowUserToAddRows = false;
            this.downloadsList.AllowUserToDeleteRows = false;
            this.downloadsList.AllowUserToResizeRows = false;
            this.downloadsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.downloadsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgEnabledColumn,
            this.dgURLColumn,
            this.dgStatusColumn,
            this.dgModNameColumn,
            this.dgProgressColumn});
            this.downloadsList.Location = new System.Drawing.Point(1, 30);
            this.downloadsList.Name = "downloadsList";
            this.downloadsList.ReadOnly = true;
            this.downloadsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.downloadsList.Size = new System.Drawing.Size(866, 249);
            this.downloadsList.TabIndex = 4;
            // 
            // downloadsRefreshTimer
            // 
            this.downloadsRefreshTimer.Interval = 5000;
            this.downloadsRefreshTimer.Tick += new System.EventHandler(this.downloadsRefreshTimer_Tick);
            // 
            // dgEnabledColumn
            // 
            this.dgEnabledColumn.HeaderText = "";
            this.dgEnabledColumn.Name = "dgEnabledColumn";
            this.dgEnabledColumn.ReadOnly = true;
            this.dgEnabledColumn.Visible = false;
            this.dgEnabledColumn.Width = 20;
            // 
            // dgURLColumn
            // 
            this.dgURLColumn.HeaderText = "URL";
            this.dgURLColumn.Name = "dgURLColumn";
            this.dgURLColumn.ReadOnly = true;
            this.dgURLColumn.Width = 300;
            // 
            // dgStatusColumn
            // 
            this.dgStatusColumn.HeaderText = "Status";
            this.dgStatusColumn.Name = "dgStatusColumn";
            this.dgStatusColumn.ReadOnly = true;
            // 
            // dgModNameColumn
            // 
            this.dgModNameColumn.HeaderText = "Mod Name";
            this.dgModNameColumn.Name = "dgModNameColumn";
            this.dgModNameColumn.ReadOnly = true;
            this.dgModNameColumn.Width = 300;
            // 
            // dgProgressColumn
            // 
            this.dgProgressColumn.HeaderText = "Progress";
            this.dgProgressColumn.Name = "dgProgressColumn";
            this.dgProgressColumn.ReadOnly = true;
            // 
            // DownloadsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 279);
            this.Controls.Add(this.downloadsList);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStop);
            this.Name = "DownloadsForm";
            this.Text = "Downloads";
            ((System.ComponentModel.ISupportInitialize)(this.downloadsList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView downloadsList;
        private System.Windows.Forms.Timer downloadsRefreshTimer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgEnabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgURLColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgModNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgProgressColumn;
    }
}