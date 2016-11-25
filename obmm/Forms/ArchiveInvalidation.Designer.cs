namespace OblivionModManager.Forms {
    partial class ArchiveInvalidation {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchiveInvalidation));
            this.cbiMeshes = new System.Windows.Forms.CheckBox();
            this.cbiTextures = new System.Windows.Forms.CheckBox();
            this.cbiMenus = new System.Windows.Forms.CheckBox();
            this.cbiSounds = new System.Windows.Forms.CheckBox();
            this.cbiMusic = new System.Windows.Forms.CheckBox();
            this.cbiMisc = new System.Windows.Forms.CheckBox();
            this.cbiFonts = new System.Windows.Forms.CheckBox();
            this.cbAutoUpdate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bUpdateNow = new System.Windows.Forms.Button();
            this.cbIgnoreNormal = new System.Windows.Forms.CheckBox();
            this.cbBSAOnly = new System.Windows.Forms.CheckBox();
            this.cbMatchExtensions = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbiBase = new System.Windows.Forms.CheckBox();
            this.cbiTrees = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.rbUniversal = new System.Windows.Forms.RadioButton();
            this.rbEditBSA = new System.Windows.Forms.RadioButton();
            this.bRemoveBSAEdits = new System.Windows.Forms.Button();
            this.cbEditAllBSA = new System.Windows.Forms.CheckBox();
            this.cbHashAI = new System.Windows.Forms.CheckBox();
            this.cbHashWarn = new System.Windows.Forms.CheckBox();
            this.bResetTimestamps = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.rbRedirection = new System.Windows.Forms.RadioButton();
            this.cbPackFaceTextures = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbiMeshes
            // 
            this.cbiMeshes.AutoSize = true;
            this.cbiMeshes.Location = new System.Drawing.Point(15, 137);
            this.cbiMeshes.Name = "cbiMeshes";
            this.cbiMeshes.Size = new System.Drawing.Size(63, 17);
            this.cbiMeshes.TabIndex = 10;
            this.cbiMeshes.Tag = "2";
            this.cbiMeshes.Text = "Meshes";
            this.cbiMeshes.UseVisualStyleBackColor = true;
            this.cbiMeshes.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbiTextures
            // 
            this.cbiTextures.AutoSize = true;
            this.cbiTextures.Checked = true;
            this.cbiTextures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbiTextures.Location = new System.Drawing.Point(15, 114);
            this.cbiTextures.Name = "cbiTextures";
            this.cbiTextures.Size = new System.Drawing.Size(67, 17);
            this.cbiTextures.TabIndex = 7;
            this.cbiTextures.Tag = "1";
            this.cbiTextures.Text = "Textures";
            this.cbiTextures.UseVisualStyleBackColor = true;
            this.cbiTextures.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbiMenus
            // 
            this.cbiMenus.AutoSize = true;
            this.cbiMenus.Location = new System.Drawing.Point(108, 160);
            this.cbiMenus.Name = "cbiMenus";
            this.cbiMenus.Size = new System.Drawing.Size(58, 17);
            this.cbiMenus.TabIndex = 14;
            this.cbiMenus.Tag = "64";
            this.cbiMenus.Text = "Menus";
            this.cbiMenus.UseVisualStyleBackColor = true;
            this.cbiMenus.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbiSounds
            // 
            this.cbiSounds.AutoSize = true;
            this.cbiSounds.Location = new System.Drawing.Point(15, 160);
            this.cbiSounds.Name = "cbiSounds";
            this.cbiSounds.Size = new System.Drawing.Size(62, 17);
            this.cbiSounds.TabIndex = 13;
            this.cbiSounds.Tag = "4";
            this.cbiSounds.Text = "Sounds";
            this.cbiSounds.UseVisualStyleBackColor = true;
            this.cbiSounds.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbiMusic
            // 
            this.cbiMusic.AutoSize = true;
            this.cbiMusic.Location = new System.Drawing.Point(108, 114);
            this.cbiMusic.Name = "cbiMusic";
            this.cbiMusic.Size = new System.Drawing.Size(54, 17);
            this.cbiMusic.TabIndex = 8;
            this.cbiMusic.Tag = "8";
            this.cbiMusic.Text = "Music";
            this.cbiMusic.UseVisualStyleBackColor = true;
            this.cbiMusic.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbiMisc
            // 
            this.cbiMisc.AutoSize = true;
            this.cbiMisc.Location = new System.Drawing.Point(201, 137);
            this.cbiMisc.Name = "cbiMisc";
            this.cbiMisc.Size = new System.Drawing.Size(48, 17);
            this.cbiMisc.TabIndex = 12;
            this.cbiMisc.Tag = "128";
            this.cbiMisc.Text = "Misc";
            this.cbiMisc.UseVisualStyleBackColor = true;
            this.cbiMisc.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbiFonts
            // 
            this.cbiFonts.AutoSize = true;
            this.cbiFonts.Location = new System.Drawing.Point(108, 137);
            this.cbiFonts.Name = "cbiFonts";
            this.cbiFonts.Size = new System.Drawing.Size(52, 17);
            this.cbiFonts.TabIndex = 11;
            this.cbiFonts.Tag = "32";
            this.cbiFonts.Text = "Fonts";
            this.cbiFonts.UseVisualStyleBackColor = true;
            this.cbiFonts.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbAutoUpdate
            // 
            this.cbAutoUpdate.AutoSize = true;
            this.cbAutoUpdate.Location = new System.Drawing.Point(15, 378);
            this.cbAutoUpdate.Name = "cbAutoUpdate";
            this.cbAutoUpdate.Size = new System.Drawing.Size(115, 17);
            this.cbAutoUpdate.TabIndex = 23;
            this.cbAutoUpdate.Text = "Autoupdate on exit";
            this.cbAutoUpdate.UseVisualStyleBackColor = true;
            this.cbAutoUpdate.CheckedChanged += new System.EventHandler(this.cbAutoUpdate_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "File types to include";
            // 
            // bUpdateNow
            // 
            this.bUpdateNow.Location = new System.Drawing.Point(164, 374);
            this.bUpdateNow.Name = "bUpdateNow";
            this.bUpdateNow.Size = new System.Drawing.Size(130, 23);
            this.bUpdateNow.TabIndex = 24;
            this.bUpdateNow.Text = "Update now";
            this.bUpdateNow.UseVisualStyleBackColor = true;
            this.bUpdateNow.Click += new System.EventHandler(this.bUpdateNow_Click);
            // 
            // cbIgnoreNormal
            // 
            this.cbIgnoreNormal.AutoSize = true;
            this.cbIgnoreNormal.Location = new System.Drawing.Point(15, 206);
            this.cbIgnoreNormal.Name = "cbIgnoreNormal";
            this.cbIgnoreNormal.Size = new System.Drawing.Size(118, 17);
            this.cbIgnoreNormal.TabIndex = 17;
            this.cbIgnoreNormal.Tag = "4096";
            this.cbIgnoreNormal.Text = "Ignore normal maps";
            this.cbIgnoreNormal.UseVisualStyleBackColor = true;
            this.cbIgnoreNormal.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbBSAOnly
            // 
            this.cbBSAOnly.AutoSize = true;
            this.cbBSAOnly.Checked = true;
            this.cbBSAOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBSAOnly.Location = new System.Drawing.Point(15, 229);
            this.cbBSAOnly.Name = "cbBSAOnly";
            this.cbBSAOnly.Size = new System.Drawing.Size(279, 17);
            this.cbBSAOnly.TabIndex = 18;
            this.cbBSAOnly.Tag = "8192";
            this.cbBSAOnly.Text = "Only include files which already exist in a BSA archive";
            this.cbBSAOnly.UseVisualStyleBackColor = true;
            this.cbBSAOnly.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbMatchExtensions
            // 
            this.cbMatchExtensions.AutoSize = true;
            this.cbMatchExtensions.Checked = true;
            this.cbMatchExtensions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMatchExtensions.Location = new System.Drawing.Point(15, 252);
            this.cbMatchExtensions.Name = "cbMatchExtensions";
            this.cbMatchExtensions.Size = new System.Drawing.Size(226, 17);
            this.cbMatchExtensions.TabIndex = 19;
            this.cbMatchExtensions.Tag = "16384";
            this.cbMatchExtensions.Text = "Only include files with matching extensions";
            this.cbMatchExtensions.UseVisualStyleBackColor = true;
            this.cbMatchExtensions.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Other options";
            // 
            // cbiBase
            // 
            this.cbiBase.AutoSize = true;
            this.cbiBase.Location = new System.Drawing.Point(201, 160);
            this.cbiBase.Name = "cbiBase";
            this.cbiBase.Size = new System.Drawing.Size(50, 17);
            this.cbiBase.TabIndex = 15;
            this.cbiBase.Tag = "256";
            this.cbiBase.Text = "Base";
            this.cbiBase.UseVisualStyleBackColor = true;
            this.cbiBase.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbiTrees
            // 
            this.cbiTrees.AutoSize = true;
            this.cbiTrees.Location = new System.Drawing.Point(201, 114);
            this.cbiTrees.Name = "cbiTrees";
            this.cbiTrees.Size = new System.Drawing.Size(99, 17);
            this.cbiTrees.TabIndex = 9;
            this.cbiTrees.Tag = "16";
            this.cbiTrees.Text = "Trees and LOD";
            this.cbiTrees.UseVisualStyleBackColor = true;
            this.cbiTrees.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mode (Note: none of this is needed for Skyrim)";
            // 
            // rbStandard
            // 
            this.rbStandard.AutoSize = true;
            this.rbStandard.Location = new System.Drawing.Point(15, 25);
            this.rbStandard.Name = "rbStandard";
            this.rbStandard.Size = new System.Drawing.Size(102, 17);
            this.rbStandard.TabIndex = 1;
            this.rbStandard.Text = "BSA invalidation";
            this.rbStandard.UseVisualStyleBackColor = true;
            this.rbStandard.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // rbUniversal
            // 
            this.rbUniversal.AutoSize = true;
            this.rbUniversal.Location = new System.Drawing.Point(151, 25);
            this.rbUniversal.Name = "rbUniversal";
            this.rbUniversal.Size = new System.Drawing.Size(69, 17);
            this.rbUniversal.TabIndex = 2;
            this.rbUniversal.Tag = "";
            this.rbUniversal.Text = "Universal";
            this.rbUniversal.UseVisualStyleBackColor = true;
            this.rbUniversal.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // rbEditBSA
            // 
            this.rbEditBSA.AutoSize = true;
            this.rbEditBSA.Location = new System.Drawing.Point(15, 49);
            this.rbEditBSA.Name = "rbEditBSA";
            this.rbEditBSA.Size = new System.Drawing.Size(92, 17);
            this.rbEditBSA.TabIndex = 3;
            this.rbEditBSA.Tag = "";
            this.rbEditBSA.Text = "BSA alteration";
            this.rbEditBSA.UseVisualStyleBackColor = true;
            this.rbEditBSA.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // bRemoveBSAEdits
            // 
            this.bRemoveBSAEdits.Enabled = false;
            this.bRemoveBSAEdits.Location = new System.Drawing.Point(151, 72);
            this.bRemoveBSAEdits.Name = "bRemoveBSAEdits";
            this.bRemoveBSAEdits.Size = new System.Drawing.Size(130, 23);
            this.bRemoveBSAEdits.TabIndex = 5;
            this.bRemoveBSAEdits.Text = "Remove BSA edits";
            this.bRemoveBSAEdits.UseVisualStyleBackColor = true;
            this.bRemoveBSAEdits.Click += new System.EventHandler(this.bRemoveBSAEdits_Click);
            // 
            // cbEditAllBSA
            // 
            this.cbEditAllBSA.AutoSize = true;
            this.cbEditAllBSA.Checked = true;
            this.cbEditAllBSA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEditAllBSA.Location = new System.Drawing.Point(15, 275);
            this.cbEditAllBSA.Name = "cbEditAllBSA";
            this.cbEditAllBSA.Size = new System.Drawing.Size(204, 17);
            this.cbEditAllBSA.TabIndex = 20;
            this.cbEditAllBSA.Tag = "131072";
            this.cbEditAllBSA.Text = "Edit BSA entries regardless of file type";
            this.cbEditAllBSA.UseVisualStyleBackColor = true;
            this.cbEditAllBSA.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbHashAI
            // 
            this.cbHashAI.AutoSize = true;
            this.cbHashAI.Checked = true;
            this.cbHashAI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHashAI.Location = new System.Drawing.Point(15, 298);
            this.cbHashAI.Name = "cbHashAI";
            this.cbHashAI.Size = new System.Drawing.Size(276, 17);
            this.cbHashAI.TabIndex = 21;
            this.cbHashAI.Tag = "262144";
            this.cbHashAI.Text = "Generate archiveinvalidation entries on hash collision";
            this.cbHashAI.UseVisualStyleBackColor = true;
            this.cbHashAI.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // cbHashWarn
            // 
            this.cbHashWarn.AutoSize = true;
            this.cbHashWarn.Location = new System.Drawing.Point(15, 321);
            this.cbHashWarn.Name = "cbHashWarn";
            this.cbHashWarn.Size = new System.Drawing.Size(181, 17);
            this.cbHashWarn.TabIndex = 22;
            this.cbHashWarn.Tag = "524288";
            this.cbHashWarn.Text = "Display warning on hash collision";
            this.cbHashWarn.UseVisualStyleBackColor = true;
            this.cbHashWarn.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // bResetTimestamps
            // 
            this.bResetTimestamps.Location = new System.Drawing.Point(15, 72);
            this.bResetTimestamps.Name = "bResetTimestamps";
            this.bResetTimestamps.Size = new System.Drawing.Size(130, 23);
            this.bResetTimestamps.TabIndex = 4;
            this.bResetTimestamps.Text = "Reset BSA timestamps";
            this.bResetTimestamps.UseVisualStyleBackColor = true;
            this.bResetTimestamps.Click += new System.EventHandler(this.bResetTimestamps_Click);
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(151, 190);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(130, 23);
            this.bReset.TabIndex = 25;
            this.bReset.Text = "Reset to defaults";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // rbRedirection
            // 
            this.rbRedirection.AutoSize = true;
            this.rbRedirection.Location = new System.Drawing.Point(151, 48);
            this.rbRedirection.Name = "rbRedirection";
            this.rbRedirection.Size = new System.Drawing.Size(98, 17);
            this.rbRedirection.TabIndex = 26;
            this.rbRedirection.Tag = "";
            this.rbRedirection.Text = "BSA redirection";
            this.rbRedirection.UseVisualStyleBackColor = true;
            this.rbRedirection.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // cbPackFaceTextures
            // 
            this.cbPackFaceTextures.AutoSize = true;
            this.cbPackFaceTextures.Location = new System.Drawing.Point(15, 344);
            this.cbPackFaceTextures.Name = "cbPackFaceTextures";
            this.cbPackFaceTextures.Size = new System.Drawing.Size(241, 17);
            this.cbPackFaceTextures.TabIndex = 27;
            this.cbPackFaceTextures.Tag = "2097152";
            this.cbPackFaceTextures.Text = "Pack face textures into redirection target BSA";
            this.cbPackFaceTextures.UseVisualStyleBackColor = true;
            this.cbPackFaceTextures.CheckedChanged += new System.EventHandler(this.FlagBoxChanged);
            // 
            // ArchiveInvalidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 406);
            this.Controls.Add(this.cbPackFaceTextures);
            this.Controls.Add(this.rbRedirection);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bResetTimestamps);
            this.Controls.Add(this.cbHashWarn);
            this.Controls.Add(this.cbHashAI);
            this.Controls.Add(this.cbEditAllBSA);
            this.Controls.Add(this.bRemoveBSAEdits);
            this.Controls.Add(this.rbEditBSA);
            this.Controls.Add(this.rbUniversal);
            this.Controls.Add(this.rbStandard);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbiTrees);
            this.Controls.Add(this.cbiBase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbMatchExtensions);
            this.Controls.Add(this.cbBSAOnly);
            this.Controls.Add(this.cbIgnoreNormal);
            this.Controls.Add(this.bUpdateNow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbAutoUpdate);
            this.Controls.Add(this.cbiFonts);
            this.Controls.Add(this.cbiMisc);
            this.Controls.Add(this.cbiMusic);
            this.Controls.Add(this.cbiSounds);
            this.Controls.Add(this.cbiMenus);
            this.Controls.Add(this.cbiTextures);
            this.Controls.Add(this.cbiMeshes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(315, 434);
            this.Name = "ArchiveInvalidation";
            this.Text = "Archive Invalidation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbiMeshes;
        private System.Windows.Forms.CheckBox cbiTextures;
        private System.Windows.Forms.CheckBox cbiMenus;
        private System.Windows.Forms.CheckBox cbiSounds;
        private System.Windows.Forms.CheckBox cbiMusic;
        private System.Windows.Forms.CheckBox cbiMisc;
        private System.Windows.Forms.CheckBox cbiFonts;
        private System.Windows.Forms.CheckBox cbAutoUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bUpdateNow;
        private System.Windows.Forms.CheckBox cbIgnoreNormal;
        private System.Windows.Forms.CheckBox cbBSAOnly;
        private System.Windows.Forms.CheckBox cbMatchExtensions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbiBase;
        private System.Windows.Forms.CheckBox cbiTrees;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbStandard;
        private System.Windows.Forms.RadioButton rbUniversal;
        private System.Windows.Forms.RadioButton rbEditBSA;
        private System.Windows.Forms.Button bRemoveBSAEdits;
        private System.Windows.Forms.CheckBox cbEditAllBSA;
        private System.Windows.Forms.CheckBox cbHashAI;
        private System.Windows.Forms.CheckBox cbHashWarn;
        private System.Windows.Forms.Button bResetTimestamps;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.RadioButton rbRedirection;
        private System.Windows.Forms.CheckBox cbPackFaceTextures;
    }
}