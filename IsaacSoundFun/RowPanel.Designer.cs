namespace IsaacFun {
    partial class RowPanel {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.playButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.labelPanel = new System.Windows.Forms.Panel();
            this.soundLabel = new System.Windows.Forms.Label();
            this.barPanel = new System.Windows.Forms.Panel();
            this.labelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.Image = global::IsaacFun.Properties.Resources.play16;
            this.playButton.Location = new System.Drawing.Point(6, 6);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(32, 32);
            this.playButton.TabIndex = 0;
            this.playButton.UseVisualStyleBackColor = true;
            // 
            // editButton
            // 
            this.editButton.Image = global::IsaacFun.Properties.Resources.edit16;
            this.editButton.Location = new System.Drawing.Point(44, 6);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(32, 32);
            this.editButton.TabIndex = 1;
            this.editButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Image = global::IsaacFun.Properties.Resources.delete16;
            this.deleteButton.Location = new System.Drawing.Point(82, 6);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(32, 32);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // labelPanel
            // 
            this.labelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPanel.Controls.Add(this.soundLabel);
            this.labelPanel.Location = new System.Drawing.Point(120, 8);
            this.labelPanel.Name = "labelPanel";
            this.labelPanel.Size = new System.Drawing.Size(419, 31);
            this.labelPanel.TabIndex = 3;
            // 
            // soundLabel
            // 
            this.soundLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soundLabel.Location = new System.Drawing.Point(0, 0);
            this.soundLabel.Name = "soundLabel";
            this.soundLabel.Size = new System.Drawing.Size(419, 31);
            this.soundLabel.TabIndex = 0;
            this.soundLabel.Text = "Sound Label";
            this.soundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // barPanel
            // 
            this.barPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.barPanel.BackColor = System.Drawing.SystemColors.ControlText;
            this.barPanel.Location = new System.Drawing.Point(6, 45);
            this.barPanel.Name = "barPanel";
            this.barPanel.Size = new System.Drawing.Size(534, 2);
            this.barPanel.TabIndex = 4;
            // 
            // RowPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.barPanel);
            this.Controls.Add(this.labelPanel);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.playButton);
            this.Name = "RowPanel";
            this.Size = new System.Drawing.Size(548, 49);
            this.labelPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel labelPanel;
        private System.Windows.Forms.Label soundLabel;
        private System.Windows.Forms.Panel barPanel;
    }
}
