using System;
using System.Drawing;
using System.Windows.Forms;
using IsaacFun.Player;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;

namespace IsaacFun {
    public partial class RowPanel : UserControl {
        private readonly SoundFunEntity _entity;

        public RowPanel(SoundFunEntity entity) {
            _entity = entity;
            InitializeComponent();

            playButton.Click += PlayClick;
            editButton.Click += EditClick;
            deleteButton.Click += DeleteClick;
            UpdateSoundLabel();
        }

        public void EditSound(string soundFile, Item item, int oldItemId) {
            _entity.SoundFile = soundFile;
            if (item.Id != oldItemId) {
                _entity.Item = item;
                SoundFunPlayer.Entities.Add(item.Id, _entity);
                SoundFunPlayer.Entities.Remove(oldItemId);
            }

            UpdateSoundLabel();
            SoundFunSerializer.Save();
        }

        public void UpdateSoundLabel() {
            soundLabel.Text = _entity.Item.I18N + " -> " + _entity.SoundFile;
            soundLabel.ForeColor = FileUtils.Exists(_entity.SoundFile) ? SystemColors.ControlText : Color.Red;
        }

        private void PlayClick(object sender, EventArgs e) {
            SoundFunPlayer.PlaySound(_entity.SoundFile);
        }

        private void EditClick(object sender, EventArgs e) {
            var modal = new AddEditSoundModal(this, _entity.SoundFile, _entity.Item.Id);
            modal.ShowDialog(this);
        }

        private void DeleteClick(object sender, EventArgs e) {
            var confirmResult = MessageBox.Show("Are you sure to delete this sound entry?", "Delete Sound Entry", MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes) return;

            SoundFunPlayer.Entities.Remove(_entity.Item.Id);
            SoundFunSerializer.Save();

            Parent.Controls.Remove(this);
        }
    }
}
