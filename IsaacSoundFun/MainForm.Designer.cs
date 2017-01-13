namespace IsaacFun {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.pauseResumeButton = new System.Windows.Forms.Button();
            this.addSoundButton = new System.Windows.Forms.Button();
            this.progressLabel = new System.Windows.Forms.Label();
            this.progressPanel = new System.Windows.Forms.Panel();
            this.nowPlayingPanel = new System.Windows.Forms.Panel();
            this.nowPlayingLabel = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            this.progressPanel.SuspendLayout();
            this.nowPlayingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 339);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(584, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(226, 17);
            this.statusLabel.Text = "Isaac proccess not found. Still searching...";
            // 
            // menuStrip
            // 
            this.menuStrip.AutoSize = false;
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(6, 2, 0, 4);
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(584, 44);
            this.menuStrip.TabIndex = 1;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 44);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(584, 295);
            this.mainPanel.TabIndex = 6;
            // 
            // pauseResumeButton
            // 
            this.pauseResumeButton.Enabled = false;
            this.pauseResumeButton.Image = global::IsaacFun.Properties.Resources.play24;
            this.pauseResumeButton.Location = new System.Drawing.Point(57, 4);
            this.pauseResumeButton.Name = "pauseResumeButton";
            this.pauseResumeButton.Size = new System.Drawing.Size(41, 34);
            this.pauseResumeButton.TabIndex = 4;
            this.pauseResumeButton.UseVisualStyleBackColor = true;
            this.pauseResumeButton.Click += new System.EventHandler(this.PauseResumeClick);
            // 
            // addSoundButton
            // 
            this.addSoundButton.Image = global::IsaacFun.Properties.Resources.new24;
            this.addSoundButton.Location = new System.Drawing.Point(10, 4);
            this.addSoundButton.Name = "addSoundButton";
            this.addSoundButton.Size = new System.Drawing.Size(41, 34);
            this.addSoundButton.TabIndex = 3;
            this.addSoundButton.UseVisualStyleBackColor = true;
            this.addSoundButton.Click += new System.EventHandler(this.AddMenuButtonClick);
            // 
            // progressLabel
            // 
            this.progressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressLabel.Location = new System.Drawing.Point(0, 0);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(77, 32);
            this.progressLabel.TabIndex = 7;
            this.progressLabel.Text = "00:00 / 00:00";
            this.progressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressPanel
            // 
            this.progressPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.progressPanel.Controls.Add(this.progressLabel);
            this.progressPanel.Location = new System.Drawing.Point(104, 4);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(79, 34);
            this.progressPanel.TabIndex = 8;
            // 
            // nowPlayingPanel
            // 
            this.nowPlayingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nowPlayingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nowPlayingPanel.Controls.Add(this.nowPlayingLabel);
            this.nowPlayingPanel.Location = new System.Drawing.Point(189, 4);
            this.nowPlayingPanel.Name = "nowPlayingPanel";
            this.nowPlayingPanel.Size = new System.Drawing.Size(383, 33);
            this.nowPlayingPanel.TabIndex = 9;
            // 
            // nowPlayingLabel
            // 
            this.nowPlayingLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nowPlayingLabel.Location = new System.Drawing.Point(0, 0);
            this.nowPlayingLabel.Name = "nowPlayingLabel";
            this.nowPlayingLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.nowPlayingLabel.Size = new System.Drawing.Size(381, 31);
            this.nowPlayingLabel.TabIndex = 0;
            this.nowPlayingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.nowPlayingPanel);
            this.Controls.Add(this.progressPanel);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.pauseResumeButton);
            this.Controls.Add(this.addSoundButton);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.Text = "Isaac Sound Fun";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.progressPanel.ResumeLayout(false);
            this.nowPlayingPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button addSoundButton;
        private System.Windows.Forms.Button pauseResumeButton;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Panel progressPanel;
        private System.Windows.Forms.Panel nowPlayingPanel;
        private System.Windows.Forms.Label nowPlayingLabel;
    }
}

