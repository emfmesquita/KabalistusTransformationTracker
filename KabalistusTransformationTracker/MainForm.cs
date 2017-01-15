using System;
using System.Linq;
using System.Windows.Forms;
using KabalistusCommons.Utils;
using KabalistusCommons.View;
using KabalistusTransformationTracker.Images;
using KabalistusTransformationTracker.Providers;
using KabalistusTransformationTracker.Trans;
using KabalistusTransformationTracker.Utils;

namespace KabalistusTransformationTracker {
    public partial class MainForm : Form {

        public static bool ShowTransformationImage;
        public static bool ShowBlacklistedItems;
        public static bool CoopTransformationMode;

        private IsaacVersion? _currentVersion;

        public MainForm() {
            InitializeComponent();
            Text = FormUtils.BuiltTitle("Kabalistus Transformation Tracker", this);

            statusLabel.BackColor = statusStrip.BackColor;

            SetInitialValuesFromConfig();
        }

        public sealed override string Text {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public void UpdateTransformationsView() {
            Invoke((MethodInvoker)(() => {
                var newVersion = MemoryReader.GetVersion();
                if (newVersion != _currentVersion) {
                    _currentVersion = newVersion;
                    InitTransformations();
                }
                if (_currentVersion != null && !CreationMode.On) {
                    TransformationViewHelper.UpdateTransformationsInfo(ShowTransformationImage);
                }
            }));
        }

        private void InitTransformations() {
            flowLayoutPanel.Controls.Clear();
            showTransformationsToolStripMenuItem.DropDownItems.Clear();
            TransformationViewHelper.InitTransformations();
            if (_currentVersion == null) {
                return;
            }

            TransformationInfoProvider.GetAllTransformations().ToList().ForEach(pair => {
                var transformation = pair.Value;
                var cluster = new ItemCluster(transformation);

                flowLayoutPanel.Controls.Add(cluster.Panel);
                showTransformationsToolStripMenuItem.DropDownItems.Add(cluster.Menu);

                TransformationViewHelper.Add(transformation, cluster);
            });

            TransformationViewHelper.SetInitialValuesFromConfig();

            ItemCluster.UpdateBlockImage(Properties.Settings.Default.BlacklistedItemsIconColor);
        }

        private void SetInitialValuesFromConfig() {
            BackColor = Properties.Settings.Default.BackgroundColor;

            ShowTransformationImage = Properties.Settings.Default.ShowTransformationImages;
            showHideTransformationsToolStripMenuItem.Checked = ShowTransformationImage;

            ShowBlacklistedItems = Properties.Settings.Default.ShowBlacklistedItems;
            showBlacklistedItemsToolStripMenuItem.Checked = ShowBlacklistedItems;

            Resize -= this.MainForm_Resize;
            Width = Properties.Settings.Default.AppWidth;
            Resize += this.MainForm_Resize;
            Height = Properties.Settings.Default.AppHeight;
        }

        private void changeTextColorToolStripMenuItem_Click(object sender, System.EventArgs e) {
            var colorDialog = new KttColorDialog() {
                Color = Properties.Settings.Default.TextColor,
                PreviewColorChangedListener = color => {
                    Invoke((MethodInvoker)(() => {
                        TransformationViewHelper.SetTextColor(color);
                    }));
                }
            };

            if (colorDialog.ShowDialog() == DialogResult.OK) {
                Properties.Settings.Default.TextColor = colorDialog.Color;
                Properties.Settings.Default.Save();
            }
            TransformationViewHelper.SetTextColor(colorDialog.Color);
        }

        private void changeBackgroundColorToolStripMenuItem_Click(object sender, System.EventArgs e) {
            var colorDialog = new KttColorDialog() {
                Color = Properties.Settings.Default.BackgroundColor,
                PreviewColorChangedListener = color => {
                    Invoke((MethodInvoker)(() => {
                        BackColor = color;
                    }));
                }
            };

            if (colorDialog.ShowDialog() == DialogResult.OK) {
                Properties.Settings.Default.BackgroundColor = colorDialog.Color;
                Properties.Settings.Default.Save();
            }
            BackColor = colorDialog.Color;
        }

        private void clangeBlacklisteIconColorToolStripMenuItem_Click(object sender, EventArgs e) {
            var colorDialog = new KttColorDialog() {
                Color = Properties.Settings.Default.BlacklistedItemsIconColor,
                PreviewColorChangedListener = color => {
                    Invoke((MethodInvoker)(() => {
                        ItemCluster.UpdateBlockImage(color);
                        Refresh();
                    }));
                }
            };

            if (colorDialog.ShowDialog() == DialogResult.OK) {
                Properties.Settings.Default.BlacklistedItemsIconColor = colorDialog.Color;
                Properties.Settings.Default.Save();
            }
            ItemCluster.UpdateBlockImage(colorDialog.Color);
            Refresh();
        }

        private void showHideTransformationsToolStripMenuItem_Click(object sender, System.EventArgs e) {
            ShowTransformationImage = ((ToolStripMenuItem)sender).Checked;
            Properties.Settings.Default.ShowTransformationImages = ShowTransformationImage;
            Properties.Settings.Default.Save();
            Refresh();
        }

        private void coopTransformationImageModeToolStripMenuItem_Click(object sender, EventArgs e) {
            CoopTransformationMode = ((ToolStripMenuItem)sender).Checked;
            Properties.Settings.Default.CoopTransformationMode = CoopTransformationMode;
            Properties.Settings.Default.Save();
        }

        private void showBlacklistedItemsToolStripMenuItem_Click(object sender, EventArgs e) {
            ShowBlacklistedItems = ((ToolStripMenuItem)sender).Checked;
            Properties.Settings.Default.ShowBlacklistedItems = ShowBlacklistedItems;
            Properties.Settings.Default.Save();
            Refresh();
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
            Refresh();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e) {
            CreationMode.KeyPressed(e.KeyChar);
        }
    }
}
