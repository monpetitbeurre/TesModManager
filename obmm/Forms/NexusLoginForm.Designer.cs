namespace OblivionModManager.Forms
{
    partial class frmNexusLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNexusLogin));
            this.lblNexusUsername = new System.Windows.Forms.Label();
            this.lblNexusPassword = new System.Windows.Forms.Label();
            this.txtNexusUsername = new System.Windows.Forms.TextBox();
            this.txtNexusPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cbRememberCredentials = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblNexusUsername
            // 
            this.lblNexusUsername.AutoSize = true;
            this.lblNexusUsername.Location = new System.Drawing.Point(12, 91);
            this.lblNexusUsername.Name = "lblNexusUsername";
            this.lblNexusUsername.Size = new System.Drawing.Size(89, 13);
            this.lblNexusUsername.TabIndex = 0;
            this.lblNexusUsername.Text = "Nexus username:";
            // 
            // lblNexusPassword
            // 
            this.lblNexusPassword.AutoSize = true;
            this.lblNexusPassword.Location = new System.Drawing.Point(12, 121);
            this.lblNexusPassword.Name = "lblNexusPassword";
            this.lblNexusPassword.Size = new System.Drawing.Size(88, 13);
            this.lblNexusPassword.TabIndex = 1;
            this.lblNexusPassword.Text = "Nexus password:";
            // 
            // txtNexusUsername
            // 
            this.txtNexusUsername.Location = new System.Drawing.Point(107, 87);
            this.txtNexusUsername.Name = "txtNexusUsername";
            this.txtNexusUsername.Size = new System.Drawing.Size(265, 20);
            this.txtNexusUsername.TabIndex = 1;
            // 
            // txtNexusPassword
            // 
            this.txtNexusPassword.Location = new System.Drawing.Point(107, 117);
            this.txtNexusPassword.Name = "txtNexusPassword";
            this.txtNexusPassword.Size = new System.Drawing.Size(265, 20);
            this.txtNexusPassword.TabIndex = 2;
            this.txtNexusPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(232, 204);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(67, 27);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(305, 204);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 27);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(14, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(358, 66);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "A login to Nexus is required in order to be able to download a file directly. Alt" +
    "ernately, you can save the file from the nexus site to disk and import it.";
            // 
            // cbRememberCredentials
            // 
            this.cbRememberCredentials.AutoSize = true;
            this.cbRememberCredentials.Location = new System.Drawing.Point(18, 151);
            this.cbRememberCredentials.Name = "cbRememberCredentials";
            this.cbRememberCredentials.Size = new System.Drawing.Size(322, 17);
            this.cbRememberCredentials.TabIndex = 3;
            this.cbRememberCredentials.Text = "Remember my credentials and automatically log me in next time";
            this.cbRememberCredentials.UseVisualStyleBackColor = true;
            this.cbRememberCredentials.CheckedChanged += new System.EventHandler(this.cbRememberCredentials_CheckedChanged);
            // 
            // frmNexusLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 243);
            this.Controls.Add(this.cbRememberCredentials);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtNexusPassword);
            this.Controls.Add(this.txtNexusUsername);
            this.Controls.Add(this.lblNexusPassword);
            this.Controls.Add(this.lblNexusUsername);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNexusLogin";
            this.Text = "Nexus login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNexusUsername;
        private System.Windows.Forms.Label lblNexusPassword;
        private System.Windows.Forms.TextBox txtNexusUsername;
        private System.Windows.Forms.TextBox txtNexusPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox cbRememberCredentials;
    }
}