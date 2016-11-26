using System.Drawing;
using System.Windows.Forms;
using KabalistusTransformationTracker.Images;
using KabalistusTransformationTracker.Trans;
using static KabalistusTransformationTracker.Trans.Transformations;

namespace KabalistusTransformationTracker {
    public partial class MainForm : Form {
        private static readonly TransformationViewHelper TransViewHelper = new TransformationViewHelper();

        public static readonly bool CreationMode = false;
        public static ItemCluster CurrentCluster;

        public static bool ShowTransformationImage;

        public MainForm() {
            InitializeComponent();

            TransViewHelper.Add(Guppy, new TransformationViewInfo(Guppy, guppyPBox, guppyLabel, guppyPanel, guppyToolStripMenuItem));
            TransViewHelper.Add(Beelzebub, new TransformationViewInfo(Beelzebub, beelzebubPBox, beelzebubLabel, beelzebubPanel, beelzebubToolStripMenuItem));
            TransViewHelper.Add(FunGuy, new TransformationViewInfo(FunGuy, funGuyPBox, funGuyLabel, funGuyPanel, funGuyToolStripMenuItem));
            TransViewHelper.Add(Seraphim, new TransformationViewInfo(Seraphim, seraphimPBox, seraphimLabel, seraphimPanel, seraphimToolStripMenuItem));
            TransViewHelper.Add(Bob, new TransformationViewInfo(Bob, bobPBox, bobLabel, bobPanel, bobToolStripMenuItem));
            TransViewHelper.Add(Spun, new TransformationViewInfo(Spun, spunPBox, spunLabel, spunPanel, spunToolStripMenuItem));
            TransViewHelper.Add(Mom, new TransformationViewInfo(Mom, momPBox, momLabel, momPanel, momToolStripMenuItem));
            TransViewHelper.Add(Conjoined, new TransformationViewInfo(Conjoined, conjoinedPBox, conjoinedLabel, conjoinedPanel, conjoinedToolStripMenuItem));
            TransViewHelper.Add(Leviathan, new TransformationViewInfo(Leviathan, leviathanPBox, leviathanLabel, leviathanPanel, leviathanToolStripMenuItem));
            TransViewHelper.Add(OhCrap, new TransformationViewInfo(OhCrap, ohCrapPBox, ohCrapLabel, ohCrapPanel, ohCrapToolStripMenuItem));
            TransViewHelper.Add(SuperBum, new TransformationViewInfo(SuperBum, superBumPBox, superBumLabel, superBumPanel, superBumToolStripMenuItem));

            SetInitialValuesFromConfig();
        }

        private void SetInitialValuesFromConfig() {
            BackColor = Properties.Settings.Default.BackgroundColor;

            ShowTransformationImage = Properties.Settings.Default.ShowTransformationImages;
            showHideTransformationsToolStripMenuItem.Checked = ShowTransformationImage;

            TransViewHelper.SetInitialValuesFromConfig();

            Resize -= this.MainForm_Resize;
            Width = Properties.Settings.Default.AppWidth;
            Resize += this.MainForm_Resize;
            Height = Properties.Settings.Default.AppHeight;
        }

        public void UpdateTransformationsView() {
            if (CreationMode) {
                return;
            }
            Invoke((MethodInvoker)(() => {
                TransViewHelper.UpdateTransformationsInfo(ShowTransformationImage);
            }));
        }

        private void changeTextColorToolStripMenuItem_Click(object sender, System.EventArgs e) {
            if (textColor.ShowDialog() != DialogResult.OK) return;
            TransViewHelper.SetTextColor(textColor.Color);
            Properties.Settings.Default.TextColor = textColor.Color;
            Properties.Settings.Default.Save();
        }

        private void changeBackgroundColorToolStripMenuItem_Click(object sender, System.EventArgs e) {
            if (backgroundColor.ShowDialog() != DialogResult.OK) return;
            BackColor = backgroundColor.Color;
            Properties.Settings.Default.BackgroundColor = BackColor;
            Properties.Settings.Default.Save();
        }

        private void showHideTransformationsToolStripMenuItem_Click(object sender, System.EventArgs e) {
            ShowTransformationImage = ((ToolStripMenuItem)sender).Checked;
            Properties.Settings.Default.ShowTransformationImages = ShowTransformationImage;
            Properties.Settings.Default.Save();
            Refresh();
        }

        private void guppyToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(Guppy, ((ToolStripMenuItem)sender).Checked);
        }

        private void beelzebubToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(Beelzebub, ((ToolStripMenuItem)sender).Checked);
        }

        private void funGuyToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(FunGuy, ((ToolStripMenuItem)sender).Checked);
        }

        private void seraphimToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(Seraphim, ((ToolStripMenuItem)sender).Checked);
        }

        private void bobToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(Bob, ((ToolStripMenuItem)sender).Checked);
        }

        private void spunToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(Spun, ((ToolStripMenuItem)sender).Checked);
        }

        private void momToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(Mom, ((ToolStripMenuItem)sender).Checked);
        }

        private void conjoinedToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(Conjoined, ((ToolStripMenuItem)sender).Checked);
        }

        private void leviathanToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(Leviathan, ((ToolStripMenuItem)sender).Checked);
        }

        private void ohCrapToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(OhCrap, ((ToolStripMenuItem)sender).Checked);
        }

        private void superBumToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TransViewHelper.ShowHideTransformation(SuperBum, ((ToolStripMenuItem)sender).Checked);
        }

        private void MainForm_Resize(object sender, System.EventArgs e) {
            Properties.Settings.Default.AppHeight = Height;
            Properties.Settings.Default.AppWidth = Width;
            Properties.Settings.Default.Save();
        }

        private void resetToDefaultToolStripMenuItem_Click(object sender, System.EventArgs e) {
            var confirmResult = MessageBox.Show("Are you sure to reset the application to default settings?",
                                     "Reset to Default Confirmation",
                                     MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes) return;
            Properties.Settings.Default.Reset();
            SetInitialValuesFromConfig();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e) {
            if (CurrentCluster == null || !CreationMode) {
                return;
            }

            if (e.KeyChar == 'r') {
                CurrentCluster.PreviousImage();
                return;
            }

            if (e.KeyChar == 'f') {
                CurrentCluster.NextImage();
                return;
            }

            if (e.KeyChar == ' ') {
                CurrentCluster.Transformed = !CurrentCluster.Transformed;
                CurrentCluster.BaseBox.Refresh();
                return;
            }

            if (e.KeyChar == 'w') {
                CurrentCluster.CurrentImage.Y--;
            } else if (e.KeyChar == 'a') {
                CurrentCluster.CurrentImage.X--;
            } else if (e.KeyChar == 's') {
                CurrentCluster.CurrentImage.Y++;
            } else if (e.KeyChar == 'd') {
                CurrentCluster.CurrentImage.X++;
            } else if (e.KeyChar == 't') {
                CurrentCluster.CurrentImage.Scale += 0.05F;
            } else if (e.KeyChar == 'g') {
                CurrentCluster.CurrentImage.Scale -= 0.05F;
            }
            CurrentCluster.BaseBox.Refresh();
        }
    }
}
