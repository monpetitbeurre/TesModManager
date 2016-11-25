namespace OblivionModManager {
    partial class About {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.lblAbout = new System.Windows.Forms.Label();
            this.lnkOBMM = new System.Windows.Forms.LinkLabel();
            this.lnkOBMMEx = new System.Windows.Forms.LinkLabel();
            this.lnkSneakyTomato = new System.Windows.Forms.LinkLabel();
            this.lnkUESP = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lnkTesModManager = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblAbout
            // 
            this.lblAbout.Location = new System.Drawing.Point(12, 9);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(385, 116);
            this.lblAbout.TabIndex = 0;
            this.lblAbout.Text = resources.GetString("lblAbout.Text");
            // 
            // lnkOBMM
            // 
            this.lnkOBMM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkOBMM.AutoSize = true;
            this.lnkOBMM.Location = new System.Drawing.Point(12, 135);
            this.lnkOBMM.Name = "lnkOBMM";
            this.lnkOBMM.Size = new System.Drawing.Size(114, 13);
            this.lnkOBMM.TabIndex = 2;
            this.lnkOBMM.TabStop = true;
            this.lnkOBMM.Text = "Oblivion Mod Manager";
            this.lnkOBMM.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkOBMMLinkClicked);
            // 
            // lnkOBMMEx
            // 
            this.lnkOBMMEx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkOBMMEx.AutoSize = true;
            this.lnkOBMMEx.Location = new System.Drawing.Point(12, 148);
            this.lnkOBMMEx.Name = "lnkOBMMEx";
            this.lnkOBMMEx.Size = new System.Drawing.Size(162, 13);
            this.lnkOBMMEx.TabIndex = 3;
            this.lnkOBMMEx.TabStop = true;
            this.lnkOBMMEx.Text = "Oblivion Mod Manager Extended";
            this.lnkOBMMEx.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkOBMMExLinkClicked);
            // 
            // lnkSneakyTomato
            // 
            this.lnkSneakyTomato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkSneakyTomato.AutoSize = true;
            this.lnkSneakyTomato.Location = new System.Drawing.Point(11, 161);
            this.lnkSneakyTomato.Name = "lnkSneakyTomato";
            this.lnkSneakyTomato.Size = new System.Drawing.Size(156, 13);
            this.lnkSneakyTomato.TabIndex = 4;
            this.lnkSneakyTomato.TabStop = true;
            this.lnkSneakyTomato.Text = "SneakyTomato\'s Icon Package";
            this.lnkSneakyTomato.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkSneakyTomatoLinkClicked);
            // 
            // lnkUESP
            // 
            this.lnkUESP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkUESP.AutoSize = true;
            this.lnkUESP.Location = new System.Drawing.Point(12, 174);
            this.lnkUESP.Name = "lnkUESP";
            this.lnkUESP.Size = new System.Drawing.Size(145, 13);
            this.lnkUESP.TabIndex = 5;
            this.lnkUESP.TabStop = true;
            this.lnkUESP.Text = "Unofficial Elder Scrolls Pages";
            this.lnkUESP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkUESPLinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(310, 156);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // lnkTesModManager
            // 
            this.lnkTesModManager.AutoSize = true;
            this.lnkTesModManager.Location = new System.Drawing.Point(12, 122);
            this.lnkTesModManager.Name = "lnkTesModManager";
            this.lnkTesModManager.Size = new System.Drawing.Size(88, 13);
            this.lnkTesModManager.TabIndex = 7;
            this.lnkTesModManager.TabStop = true;
            this.lnkTesModManager.Text = "TesModManager";
            this.lnkTesModManager.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTesModManager_LinkClicked);
            // 
            // About
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 204);
            this.Controls.Add(this.lnkTesModManager);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lnkUESP);
            this.Controls.Add(this.lnkSneakyTomato);
            this.Controls.Add(this.lnkOBMMEx);
            this.Controls.Add(this.lnkOBMM);
            this.Controls.Add(this.lblAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "About";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.LinkLabel lnkSneakyTomato;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel lnkUESP;
        private System.Windows.Forms.LinkLabel lnkOBMMEx;
        private System.Windows.Forms.LinkLabel lnkOBMM;
        private System.Windows.Forms.Label lblAbout;

        #endregion
        private System.Windows.Forms.LinkLabel lnkTesModManager;

    }
}