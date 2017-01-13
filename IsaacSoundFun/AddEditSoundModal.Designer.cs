namespace IsaacFun {
    partial class AddEditSoundModal {
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
            this.saveButton = new System.Windows.Forms.Button();
            this.itemComboBox = new System.Windows.Forms.ComboBox();
            this.itemLabel = new System.Windows.Forms.Label();
            this.soundLabel = new System.Windows.Forms.Label();
            this.soundFileBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.soundFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(413, 76);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(59, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveSound);
            // 
            // itemComboBox
            // 
            this.itemComboBox.FormattingEnabled = true;
            this.itemComboBox.Location = new System.Drawing.Point(57, 10);
            this.itemComboBox.Name = "itemComboBox";
            this.itemComboBox.Size = new System.Drawing.Size(415, 21);
            this.itemComboBox.TabIndex = 2;
            this.itemComboBox.SelectedIndexChanged += new System.EventHandler(this.ItemSelected);
            this.itemComboBox.TextUpdate += new System.EventHandler(this.ItemSelected);
            // 
            // itemLabel
            // 
            this.itemLabel.AutoSize = true;
            this.itemLabel.Location = new System.Drawing.Point(24, 13);
            this.itemLabel.Name = "itemLabel";
            this.itemLabel.Size = new System.Drawing.Size(27, 13);
            this.itemLabel.TabIndex = 3;
            this.itemLabel.Text = "Item";
            // 
            // soundLabel
            // 
            this.soundLabel.AutoSize = true;
            this.soundLabel.Location = new System.Drawing.Point(13, 51);
            this.soundLabel.Name = "soundLabel";
            this.soundLabel.Size = new System.Drawing.Size(38, 13);
            this.soundLabel.TabIndex = 4;
            this.soundLabel.Text = "Sound";
            // 
            // soundFileBox
            // 
            this.soundFileBox.Location = new System.Drawing.Point(57, 48);
            this.soundFileBox.Name = "soundFileBox";
            this.soundFileBox.ReadOnly = true;
            this.soundFileBox.Size = new System.Drawing.Size(350, 20);
            this.soundFileBox.TabIndex = 5;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(413, 45);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(59, 23);
            this.browseButton.TabIndex = 6;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButtonClick);
            // 
            // soundFileDialog
            // 
            this.soundFileDialog.Filter = "Supported Files (*.wav;*.mp3)|*.wav;*.mp3";
            // 
            // AddEditSoundModal
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 111);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.soundFileBox);
            this.Controls.Add(this.soundLabel);
            this.Controls.Add(this.itemLabel);
            this.Controls.Add(this.itemComboBox);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddEditSoundModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Sound";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox itemComboBox;
        private System.Windows.Forms.Label itemLabel;
        private System.Windows.Forms.Label soundLabel;
        private System.Windows.Forms.TextBox soundFileBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.OpenFileDialog soundFileDialog;
    }
}