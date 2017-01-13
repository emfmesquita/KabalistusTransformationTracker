using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IsaacFun.Player;
using IsaacFun.View;
using KabalistusCommons.Isaac;
using KabalistusCommons.View;

namespace IsaacFun {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            FormUtils.BuiltTitle("Isaac Sound Fun", this);
            ViewUtils.AddTooltip(addSoundButton, "New Sound");
            ViewUtils.AddTooltip(pauseResumeButton, "Play/Pause");
            LoadSave();
            StartPooling();
        }

        public void AddSound(string soundFile, Item item, bool save = true) {
            var row = CreateRowPanel(soundFile, item, save);
            mainPanel.Controls.Add(row);
        }

        private void AddMenuButtonClick(object sender, EventArgs e) {
            var addSoundModal = new AddEditSoundModal(this);
            addSoundModal.ShowDialog(this);
        }

        private void LoadSave() {
            var save = SoundFunSerializer.Load();
            if (save == null || !save.Entities.Any()) return;

            var rows = new List<Control>();
            var entities = save.Entities.ToList();
            entities.Reverse();
            entities.ForEach(entity => {
                rows.Add(CreateRowPanel(entity.SoundFile, entity.Item, false));
            });

            mainPanel.Controls.AddRange(rows.ToArray());
        }

        private static RowPanel CreateRowPanel(string soundFile, Item item, bool save = true) {
            var entity = new SoundFunEntity {
                SoundFile = soundFile,
                Item = item
            };

            var rowPanel = new RowPanel(entity) { Dock = DockStyle.Top };

            SoundFunPlayer.Entities.Add(entity.Item.Id, entity);

            if (save) {
                SoundFunSerializer.Save();
            }

            return rowPanel;
        }

        private void PauseResumeClick(object sender, EventArgs e) {
            SoundFunPlayer.PausePlay();
        }

        private void StartPooling() {
            var viewPooling = new System.Timers.Timer(100);

            var isPlaying = false;
            viewPooling.Elapsed += (source, e) => {
                Invoke((MethodInvoker)(() => {
                    var loadedSound = SoundFunPlayer.LoadedSound();
                    pauseResumeButton.Enabled = !string.IsNullOrEmpty(loadedSound);
                    if (!pauseResumeButton.Enabled) return;

                    nowPlayingLabel.Text = loadedSound;
                    progressLabel.Text = SoundFunPlayer.GetProgess();

                    if (isPlaying == SoundFunPlayer.IsPlaying()) return;
                    isPlaying = SoundFunPlayer.IsPlaying();
                    var icon = isPlaying ? "pause24" : "play24";
                    pauseResumeButton.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(icon);
                }));
            };
            viewPooling.AutoReset = true;
            viewPooling.Enabled = true;


            var filePooling = new System.Timers.Timer(2000);
            filePooling.Elapsed += (source, e) => {
                Invoke((MethodInvoker)(() => {
                    foreach (Control control in mainPanel.Controls) {
                        var row = control as RowPanel;
                        row?.UpdateSoundLabel();
                    }
                }));
            };
            filePooling.AutoReset = true;
            filePooling.Enabled = true;
        }
    }
}
