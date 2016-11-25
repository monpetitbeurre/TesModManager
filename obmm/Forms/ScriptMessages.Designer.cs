namespace OblivionModManager.Forms
{
    partial class ScriptMessages
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
            this.tbMessages = new System.Windows.Forms.TabControl();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.rtfOutput = new System.Windows.Forms.RichTextBox();
            this.tabError = new System.Windows.Forms.TabPage();
            this.rtfErrors = new System.Windows.Forms.RichTextBox();
            this.tbMessages.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tabError.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbMessages
            // 
            this.tbMessages.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tbMessages.Controls.Add(this.tabOutput);
            this.tbMessages.Controls.Add(this.tabError);
            this.tbMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMessages.Location = new System.Drawing.Point(0, 0);
            this.tbMessages.Name = "tbMessages";
            this.tbMessages.SelectedIndex = 0;
            this.tbMessages.Size = new System.Drawing.Size(735, 203);
            this.tbMessages.TabIndex = 0;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.rtfOutput);
            this.tabOutput.Location = new System.Drawing.Point(4, 4);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(727, 177);
            this.tabOutput.TabIndex = 0;
            this.tabOutput.Text = "Output";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // rtfOutput
            // 
            this.rtfOutput.BackColor = System.Drawing.SystemColors.Window;
            this.rtfOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfOutput.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtfOutput.Location = new System.Drawing.Point(3, 3);
            this.rtfOutput.Name = "rtfOutput";
            this.rtfOutput.ReadOnly = true;
            this.rtfOutput.Size = new System.Drawing.Size(721, 171);
            this.rtfOutput.TabIndex = 0;
            this.rtfOutput.Text = "";
            // 
            // tabError
            // 
            this.tabError.Controls.Add(this.rtfErrors);
            this.tabError.Location = new System.Drawing.Point(4, 4);
            this.tabError.Name = "tabError";
            this.tabError.Padding = new System.Windows.Forms.Padding(3);
            this.tabError.Size = new System.Drawing.Size(727, 177);
            this.tabError.TabIndex = 1;
            this.tabError.Text = "Errors";
            this.tabError.UseVisualStyleBackColor = true;
            // 
            // rtfErrors
            // 
            this.rtfErrors.BackColor = System.Drawing.SystemColors.Window;
            this.rtfErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfErrors.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtfErrors.Location = new System.Drawing.Point(3, 3);
            this.rtfErrors.Name = "rtfErrors";
            this.rtfErrors.ReadOnly = true;
            this.rtfErrors.Size = new System.Drawing.Size(721, 171);
            this.rtfErrors.TabIndex = 1;
            this.rtfErrors.Text = "";
            // 
            // ScriptMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 203);
            this.Controls.Add(this.tbMessages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScriptMessages";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Script Messages";
            this.tbMessages.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.tabError.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbMessages;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.TabPage tabError;
        public System.Windows.Forms.RichTextBox rtfOutput;
        public System.Windows.Forms.RichTextBox rtfErrors;
    }
}