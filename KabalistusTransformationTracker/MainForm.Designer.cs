using System.Windows.Forms;

namespace KabalistusTransformationTracker {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeTextColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBackgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showHideTransformationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coopTransformationImageModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTransformationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clangeBlacklisteIconColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showBlacklistedItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.resetToDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel.Size = new System.Drawing.Size(834, 337);
            this.flowLayoutPanel.TabIndex = 23;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(834, 24);
            this.menu.TabIndex = 24;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeTextColorToolStripMenuItem,
            this.changeBackgroundColorToolStripMenuItem,
            this.toolStripSeparator2,
            this.showHideTransformationsToolStripMenuItem,
            this.coopTransformationImageModeToolStripMenuItem,
            this.showTransformationsToolStripMenuItem,
            this.toolStripSeparator1,
            this.clangeBlacklisteIconColorToolStripMenuItem,
            this.showBlacklistedItemsToolStripMenuItem,
            this.toolStripSeparator6,
            this.resetToDefaultToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.fileToolStripMenuItem.Text = "&Settings";
            // 
            // changeTextColorToolStripMenuItem
            // 
            this.changeTextColorToolStripMenuItem.Name = "changeTextColorToolStripMenuItem";
            this.changeTextColorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.changeTextColorToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.changeTextColorToolStripMenuItem.Text = "Change Text Color";
            this.changeTextColorToolStripMenuItem.Click += new System.EventHandler(this.changeTextColorToolStripMenuItem_Click);
            // 
            // changeBackgroundColorToolStripMenuItem
            // 
            this.changeBackgroundColorToolStripMenuItem.Name = "changeBackgroundColorToolStripMenuItem";
            this.changeBackgroundColorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.changeBackgroundColorToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.changeBackgroundColorToolStripMenuItem.Text = "Change Background Color";
            this.changeBackgroundColorToolStripMenuItem.Click += new System.EventHandler(this.changeBackgroundColorToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(294, 6);
            // 
            // showHideTransformationsToolStripMenuItem
            // 
            this.showHideTransformationsToolStripMenuItem.Checked = true;
            this.showHideTransformationsToolStripMenuItem.CheckOnClick = true;
            this.showHideTransformationsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showHideTransformationsToolStripMenuItem.Name = "showHideTransformationsToolStripMenuItem";
            this.showHideTransformationsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.showHideTransformationsToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.showHideTransformationsToolStripMenuItem.Text = "Show Transformation Image";
            this.showHideTransformationsToolStripMenuItem.ToolTipText = "Controls if the image of the transformation should be displayed when you have the" +
    " transformation. Or only the items continue to be displayed.";
            this.showHideTransformationsToolStripMenuItem.Click += new System.EventHandler(this.showHideTransformationsToolStripMenuItem_Click);
            // 
            // coopTransformationImageModeToolStripMenuItem
            // 
            this.coopTransformationImageModeToolStripMenuItem.CheckOnClick = true;
            this.coopTransformationImageModeToolStripMenuItem.Name = "coopTransformationImageModeToolStripMenuItem";
            this.coopTransformationImageModeToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.coopTransformationImageModeToolStripMenuItem.Text = "Coop Transformation Image Mode";
            this.coopTransformationImageModeToolStripMenuItem.ToolTipText = resources.GetString("coopTransformationImageModeToolStripMenuItem.ToolTipText");
            this.coopTransformationImageModeToolStripMenuItem.Click += new System.EventHandler(this.coopTransformationImageModeToolStripMenuItem_Click);
            // 
            // showTransformationsToolStripMenuItem
            // 
            this.showTransformationsToolStripMenuItem.Name = "showTransformationsToolStripMenuItem";
            this.showTransformationsToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.showTransformationsToolStripMenuItem.Text = "Show Transformations";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(294, 6);
            // 
            // clangeBlacklisteIconColorToolStripMenuItem
            // 
            this.clangeBlacklisteIconColorToolStripMenuItem.Name = "clangeBlacklisteIconColorToolStripMenuItem";
            this.clangeBlacklisteIconColorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.clangeBlacklisteIconColorToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.clangeBlacklisteIconColorToolStripMenuItem.Text = "Clange Blacklisted Items Icon Color";
            this.clangeBlacklisteIconColorToolStripMenuItem.Click += new System.EventHandler(this.clangeBlacklisteIconColorToolStripMenuItem_Click);
            // 
            // showBlacklistedItemsToolStripMenuItem
            // 
            this.showBlacklistedItemsToolStripMenuItem.Checked = true;
            this.showBlacklistedItemsToolStripMenuItem.CheckOnClick = true;
            this.showBlacklistedItemsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showBlacklistedItemsToolStripMenuItem.Name = "showBlacklistedItemsToolStripMenuItem";
            this.showBlacklistedItemsToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.showBlacklistedItemsToolStripMenuItem.Text = "Show Blacklisted Items Icon";
            this.showBlacklistedItemsToolStripMenuItem.ToolTipText = "Controls the indication if an item has no change to be generated again. So it wil" +
    "l not appear again on the current run.";
            this.showBlacklistedItemsToolStripMenuItem.Click += new System.EventHandler(this.showBlacklistedItemsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(294, 6);
            // 
            // resetToDefaultToolStripMenuItem
            // 
            this.resetToDefaultToolStripMenuItem.Name = "resetToDefaultToolStripMenuItem";
            this.resetToDefaultToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
            this.resetToDefaultToolStripMenuItem.Text = "Reset to Default";
            this.resetToDefaultToolStripMenuItem.Click += new System.EventHandler(this.resetToDefaultToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 339);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(834, 22);
            this.statusStrip.TabIndex = 25;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(146, 17);
            this.statusLabel.Text = "Searching Isaac proccess...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(834, 361);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "MainForm";
            this.Text = "Kabalistus Transformation Tracker";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private FlowLayoutPanel flowLayoutPanel;
        private MenuStrip menu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem changeTextColorToolStripMenuItem;
        private ToolStripMenuItem changeBackgroundColorToolStripMenuItem;
        private ToolStripMenuItem showHideTransformationsToolStripMenuItem;
        private ToolStripMenuItem showTransformationsToolStripMenuItem;
        private ToolStripMenuItem resetToDefaultToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem showBlacklistedItemsToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripMenuItem clangeBlacklisteIconColorToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem coopTransformationImageModeToolStripMenuItem;
        public ToolStripStatusLabel statusLabel;
    }
}

