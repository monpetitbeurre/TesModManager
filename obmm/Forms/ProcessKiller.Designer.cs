namespace OblivionModManager.Forms {
    partial class ProcessKiller {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessKiller));
            this.tbProcesses = new System.Windows.Forms.TextBox();
            this.rbServicesStop = new System.Windows.Forms.RadioButton();
            this.rbServicesKeep = new System.Windows.Forms.RadioButton();
            this.rbProcessesClose = new System.Windows.Forms.RadioButton();
            this.rbProcessesKill = new System.Windows.Forms.RadioButton();
            this.rbProcessesKeep = new System.Windows.Forms.RadioButton();
            this.bLaunch = new System.Windows.Forms.Button();
            this.cbServices = new System.Windows.Forms.CheckBox();
            this.cbAllServices = new System.Windows.Forms.CheckBox();
            this.cbIgnoreDrivers = new System.Windows.Forms.CheckBox();
            this.cbCloseProcesses = new System.Windows.Forms.CheckBox();
            this.cbKillProcesses = new System.Windows.Forms.CheckBox();
            this.cbCloseAllProcesses = new System.Windows.Forms.CheckBox();
            this.cbKillAllProcesses = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbKillOnFail = new System.Windows.Forms.CheckBox();
            this.cbDisplayLog = new System.Windows.Forms.CheckBox();
            this.tbTimeout = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbProcesses
            // 
            this.tbProcesses.AcceptsReturn = true;
            this.tbProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProcesses.Location = new System.Drawing.Point(247, 12);
            this.tbProcesses.Multiline = true;
            this.tbProcesses.Name = "tbProcesses";
            this.tbProcesses.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbProcesses.Size = new System.Drawing.Size(259, 154);
            this.tbProcesses.TabIndex = 0;
            this.tbProcesses.WordWrap = false;
            // 
            // rbServicesStop
            // 
            this.rbServicesStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbServicesStop.AutoSize = true;
            this.rbServicesStop.Location = new System.Drawing.Point(247, 172);
            this.rbServicesStop.Name = "rbServicesStop";
            this.rbServicesStop.Size = new System.Drawing.Size(101, 17);
            this.rbServicesStop.TabIndex = 1;
            this.rbServicesStop.TabStop = true;
            this.rbServicesStop.Text = "Services to stop";
            this.rbServicesStop.UseVisualStyleBackColor = true;
            this.rbServicesStop.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // rbServicesKeep
            // 
            this.rbServicesKeep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbServicesKeep.AutoSize = true;
            this.rbServicesKeep.Location = new System.Drawing.Point(383, 172);
            this.rbServicesKeep.Name = "rbServicesKeep";
            this.rbServicesKeep.Size = new System.Drawing.Size(107, 17);
            this.rbServicesKeep.TabIndex = 2;
            this.rbServicesKeep.TabStop = true;
            this.rbServicesKeep.Text = "Services to leave";
            this.rbServicesKeep.UseVisualStyleBackColor = true;
            this.rbServicesKeep.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // rbProcessesClose
            // 
            this.rbProcessesClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbProcessesClose.AutoSize = true;
            this.rbProcessesClose.Location = new System.Drawing.Point(247, 195);
            this.rbProcessesClose.Name = "rbProcessesClose";
            this.rbProcessesClose.Size = new System.Drawing.Size(114, 17);
            this.rbProcessesClose.TabIndex = 3;
            this.rbProcessesClose.TabStop = true;
            this.rbProcessesClose.Text = "Processes to close";
            this.rbProcessesClose.UseVisualStyleBackColor = true;
            this.rbProcessesClose.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // rbProcessesKill
            // 
            this.rbProcessesKill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbProcessesKill.AutoSize = true;
            this.rbProcessesKill.Location = new System.Drawing.Point(247, 218);
            this.rbProcessesKill.Name = "rbProcessesKill";
            this.rbProcessesKill.Size = new System.Drawing.Size(101, 17);
            this.rbProcessesKill.TabIndex = 4;
            this.rbProcessesKill.TabStop = true;
            this.rbProcessesKill.Text = "Processes to kill";
            this.rbProcessesKill.UseVisualStyleBackColor = true;
            this.rbProcessesKill.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // rbProcessesKeep
            // 
            this.rbProcessesKeep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbProcessesKeep.AutoSize = true;
            this.rbProcessesKeep.Location = new System.Drawing.Point(383, 195);
            this.rbProcessesKeep.Name = "rbProcessesKeep";
            this.rbProcessesKeep.Size = new System.Drawing.Size(115, 17);
            this.rbProcessesKeep.TabIndex = 5;
            this.rbProcessesKeep.TabStop = true;
            this.rbProcessesKeep.Text = "Processes to leave";
            this.rbProcessesKeep.UseVisualStyleBackColor = true;
            this.rbProcessesKeep.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // bLaunch
            // 
            this.bLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bLaunch.Location = new System.Drawing.Point(383, 218);
            this.bLaunch.Name = "bLaunch";
            this.bLaunch.Size = new System.Drawing.Size(123, 23);
            this.bLaunch.TabIndex = 6;
            this.bLaunch.Text = "Launch";
            this.bLaunch.UseVisualStyleBackColor = true;
            this.bLaunch.Click += new System.EventHandler(this.bLaunch_Click);
            // 
            // cbServices
            // 
            this.cbServices.AutoSize = true;
            this.cbServices.Location = new System.Drawing.Point(12, 12);
            this.cbServices.Name = "cbServices";
            this.cbServices.Size = new System.Drawing.Size(117, 17);
            this.cbServices.TabIndex = 7;
            this.cbServices.Tag = "1";
            this.cbServices.Text = "Stop listed services";
            this.cbServices.UseVisualStyleBackColor = true;
            this.cbServices.CheckedChanged += new System.EventHandler(this.CheckboxChanged);
            // 
            // cbAllServices
            // 
            this.cbAllServices.AutoSize = true;
            this.cbAllServices.Location = new System.Drawing.Point(12, 35);
            this.cbAllServices.Name = "cbAllServices";
            this.cbAllServices.Size = new System.Drawing.Size(103, 17);
            this.cbAllServices.TabIndex = 8;
            this.cbAllServices.Tag = "2";
            this.cbAllServices.Text = "Stop all services";
            this.cbAllServices.UseVisualStyleBackColor = true;
            this.cbAllServices.CheckedChanged += new System.EventHandler(this.CheckboxChanged);
            // 
            // cbIgnoreDrivers
            // 
            this.cbIgnoreDrivers.AutoSize = true;
            this.cbIgnoreDrivers.Location = new System.Drawing.Point(12, 58);
            this.cbIgnoreDrivers.Name = "cbIgnoreDrivers";
            this.cbIgnoreDrivers.Size = new System.Drawing.Size(127, 17);
            this.cbIgnoreDrivers.TabIndex = 9;
            this.cbIgnoreDrivers.Tag = "256";
            this.cbIgnoreDrivers.Text = "Ignore driver services";
            this.cbIgnoreDrivers.UseVisualStyleBackColor = true;
            this.cbIgnoreDrivers.CheckedChanged += new System.EventHandler(this.CheckboxChanged);
            // 
            // cbCloseProcesses
            // 
            this.cbCloseProcesses.AutoSize = true;
            this.cbCloseProcesses.Location = new System.Drawing.Point(12, 81);
            this.cbCloseProcesses.Name = "cbCloseProcesses";
            this.cbCloseProcesses.Size = new System.Drawing.Size(130, 17);
            this.cbCloseProcesses.TabIndex = 10;
            this.cbCloseProcesses.Tag = "4";
            this.cbCloseProcesses.Text = "Close listed processes";
            this.cbCloseProcesses.UseVisualStyleBackColor = true;
            this.cbCloseProcesses.CheckedChanged += new System.EventHandler(this.CheckboxChanged);
            // 
            // cbKillProcesses
            // 
            this.cbKillProcesses.AutoSize = true;
            this.cbKillProcesses.Location = new System.Drawing.Point(12, 104);
            this.cbKillProcesses.Name = "cbKillProcesses";
            this.cbKillProcesses.Size = new System.Drawing.Size(117, 17);
            this.cbKillProcesses.TabIndex = 11;
            this.cbKillProcesses.Tag = "16";
            this.cbKillProcesses.Text = "Kill listed processes";
            this.cbKillProcesses.UseVisualStyleBackColor = true;
            this.cbKillProcesses.CheckedChanged += new System.EventHandler(this.CheckboxChanged);
            // 
            // cbCloseAllProcesses
            // 
            this.cbCloseAllProcesses.AutoSize = true;
            this.cbCloseAllProcesses.Location = new System.Drawing.Point(12, 127);
            this.cbCloseAllProcesses.Name = "cbCloseAllProcesses";
            this.cbCloseAllProcesses.Size = new System.Drawing.Size(116, 17);
            this.cbCloseAllProcesses.TabIndex = 12;
            this.cbCloseAllProcesses.Tag = "8";
            this.cbCloseAllProcesses.Text = "Close all processes";
            this.cbCloseAllProcesses.UseVisualStyleBackColor = true;
            this.cbCloseAllProcesses.CheckedChanged += new System.EventHandler(this.CheckboxChanged);
            // 
            // cbKillAllProcesses
            // 
            this.cbKillAllProcesses.AutoSize = true;
            this.cbKillAllProcesses.Location = new System.Drawing.Point(12, 149);
            this.cbKillAllProcesses.Name = "cbKillAllProcesses";
            this.cbKillAllProcesses.Size = new System.Drawing.Size(103, 17);
            this.cbKillAllProcesses.TabIndex = 13;
            this.cbKillAllProcesses.Tag = "32";
            this.cbKillAllProcesses.Text = "Kill all processes";
            this.cbKillAllProcesses.UseVisualStyleBackColor = true;
            this.cbKillAllProcesses.CheckedChanged += new System.EventHandler(this.CheckboxChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Process close timeout";
            // 
            // cbKillOnFail
            // 
            this.cbKillOnFail.AutoSize = true;
            this.cbKillOnFail.Location = new System.Drawing.Point(12, 198);
            this.cbKillOnFail.Name = "cbKillOnFail";
            this.cbKillOnFail.Size = new System.Drawing.Size(136, 17);
            this.cbKillOnFail.TabIndex = 16;
            this.cbKillOnFail.Tag = "64";
            this.cbKillOnFail.Text = "Kill process if close fails";
            this.cbKillOnFail.UseVisualStyleBackColor = true;
            this.cbKillOnFail.CheckedChanged += new System.EventHandler(this.CheckboxChanged);
            // 
            // cbDisplayLog
            // 
            this.cbDisplayLog.AutoSize = true;
            this.cbDisplayLog.Location = new System.Drawing.Point(12, 221);
            this.cbDisplayLog.Name = "cbDisplayLog";
            this.cbDisplayLog.Size = new System.Drawing.Size(133, 17);
            this.cbDisplayLog.TabIndex = 17;
            this.cbDisplayLog.Tag = "128";
            this.cbDisplayLog.Text = "Display log when done";
            this.cbDisplayLog.UseVisualStyleBackColor = true;
            this.cbDisplayLog.CheckedChanged += new System.EventHandler(this.CheckboxChanged);
            // 
            // tbTimeout
            // 
            this.tbTimeout.Location = new System.Drawing.Point(12, 172);
            this.tbTimeout.MaxLength = 5;
            this.tbTimeout.Name = "tbTimeout";
            this.tbTimeout.Size = new System.Drawing.Size(100, 20);
            this.tbTimeout.TabIndex = 18;
            this.tbTimeout.Text = "1000";
            this.tbTimeout.Leave += new System.EventHandler(this.tbTimeout_Leave);
            this.tbTimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTimeout_KeyPress);
            // 
            // ProcessKiller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 253);
            this.Controls.Add(this.tbTimeout);
            this.Controls.Add(this.cbDisplayLog);
            this.Controls.Add(this.cbKillOnFail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbKillAllProcesses);
            this.Controls.Add(this.cbCloseAllProcesses);
            this.Controls.Add(this.cbKillProcesses);
            this.Controls.Add(this.cbCloseProcesses);
            this.Controls.Add(this.cbIgnoreDrivers);
            this.Controls.Add(this.cbAllServices);
            this.Controls.Add(this.cbServices);
            this.Controls.Add(this.bLaunch);
            this.Controls.Add(this.rbProcessesKeep);
            this.Controls.Add(this.rbProcessesKill);
            this.Controls.Add(this.rbProcessesClose);
            this.Controls.Add(this.rbServicesKeep);
            this.Controls.Add(this.rbServicesStop);
            this.Controls.Add(this.tbProcesses);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(526, 280);
            this.Name = "ProcessKiller";
            this.Text = "Background Process Killer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbProcesses;
        private System.Windows.Forms.RadioButton rbServicesStop;
        private System.Windows.Forms.RadioButton rbServicesKeep;
        private System.Windows.Forms.RadioButton rbProcessesClose;
        private System.Windows.Forms.RadioButton rbProcessesKill;
        private System.Windows.Forms.RadioButton rbProcessesKeep;
        private System.Windows.Forms.Button bLaunch;
        private System.Windows.Forms.CheckBox cbServices;
        private System.Windows.Forms.CheckBox cbAllServices;
        private System.Windows.Forms.CheckBox cbIgnoreDrivers;
        private System.Windows.Forms.CheckBox cbCloseProcesses;
        private System.Windows.Forms.CheckBox cbKillProcesses;
        private System.Windows.Forms.CheckBox cbCloseAllProcesses;
        private System.Windows.Forms.CheckBox cbKillAllProcesses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbKillOnFail;
        private System.Windows.Forms.CheckBox cbDisplayLog;
        private System.Windows.Forms.TextBox tbTimeout;
    }
}