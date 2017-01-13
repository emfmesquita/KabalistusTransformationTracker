using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IsaacFun.Player;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;

namespace IsaacFun {
    public partial class AddEditSoundModal : Form {

        public static List<Item> SortedItems = new List<Item>();

        private readonly bool _edit;
        private readonly int _oldItemId;
        private readonly Control _callbackControl;

        public AddEditSoundModal(Control callbackControl, int selectedItemId = -1) {
            InitializeComponent();
            _callbackControl = callbackControl;
            Text = "Add New Sound";

            SortedItems.Clear();
            var existingSoundsItemIds = ExistingSoundsItemIds(selectedItemId);
            Items.RebirthItems.ForEach(item => AddToSortedItems(item, existingSoundsItemIds));
            Items.AfterbirthItems.ForEach(item => AddToSortedItems(item, existingSoundsItemIds));
            Items.AfterbirthPlusItems.ForEach(item => AddToSortedItems(item, existingSoundsItemIds));
            Items.AntibirthItems.ForEach(item => {
                var editedItem = new Item {
                    Id = item.Id,
                    I18N = item.I18N + " (Antibirth)"
                };
                AddToSortedItems(editedItem, existingSoundsItemIds);
            });


            SortedItems.Sort((itemA, itemB) => string.Compare(itemA.I18N, itemB.I18N, StringComparison.Ordinal));

            SortedItems.ForEach(item => {
                itemComboBox.Items.Add(item);
            });
        }

        public AddEditSoundModal(Control callbackControl, string soundFile, int selectedItemId) : this(callbackControl, selectedItemId) {
            Text = "Edit Sound";
            _edit = true;
            _oldItemId = selectedItemId;
            soundFileBox.Text = FileUtils.GetFullPath(soundFile);
            saveButton.Enabled = true;

            foreach (var item in itemComboBox.Items) {
                var itemObject = item as Item;
                if (itemObject == null || selectedItemId != itemObject.Id) continue;
                itemComboBox.SelectedItem = itemObject;
            }
        }

        public sealed override string Text {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void BrowseButtonClick(object sender, EventArgs e) {
            var path = soundFileBox.Text;
            if (!string.IsNullOrEmpty(path)) {
                soundFileDialog.FileName = path;
                soundFileDialog.InitialDirectory = FileUtils.GetDirectory(path);
            }

            if (soundFileDialog.ShowDialog(this) != DialogResult.OK) {
                return;
            }
            soundFileBox.Text = soundFileDialog.FileName;
            if (itemComboBox.SelectedItem != null) {
                saveButton.Enabled = true;
            }
        }

        private void ItemSelected(object sender, EventArgs e) {
            var selected = itemComboBox.SelectedItem as Item;
            if (selected == null || !selected.I18N.Equals(itemComboBox.Text)) {
                saveButton.Enabled = false;
                return;
            }
            if (!string.IsNullOrEmpty(soundFileBox.Text)) {
                saveButton.Enabled = true;
            }
        }

        private void SaveSound(object sender, EventArgs e) {
            var relativePath = FileUtils.GetRelativePath(soundFileBox.Text);

            if (!_edit) {
                var mainForm = _callbackControl as MainForm;
                if (mainForm == null) {
                    return;
                }
                mainForm.AddSound(relativePath, itemComboBox.SelectedItem as Item);
            } else {
                var rowPanel = _callbackControl as RowPanel;
                if (rowPanel == null) {
                    return;
                }
                rowPanel.EditSound(relativePath, itemComboBox.SelectedItem as Item, _oldItemId);
            }
            Dispose();
        }

        private static List<int> ExistingSoundsItemIds(int editId = -1) {
            var existingSoundsItemIds = new List<int>();
            SoundFunPlayer.Entities.ToList().ForEach(pair => {
                if (pair.Key != editId) {
                    existingSoundsItemIds.Add(pair.Key);
                }
            });
            return existingSoundsItemIds;
        }

        private static void AddToSortedItems(Item item, ICollection<int> existingSoundsItemIds) {
            if (!existingSoundsItemIds.Contains(item.Id)) {
                SortedItems.Add(item);
            }
        }
    }
}
