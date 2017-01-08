using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KabalistusCommons.Isaac;

namespace IsaacFun {
    public partial class AddSoundModal : Form {

        public static List<Item> SortedItems = new List<Item>();

        public AddSoundModal() {
            InitializeComponent();

            SortedItems.Clear();
            Items.RebirthItems.ForEach(item => SortedItems.Add(item));
            Items.AfterbirthItems.ForEach(item => SortedItems.Add(item));
            Items.AfterbirthPlusItems.ForEach(item => SortedItems.Add(item));

            SortedItems.Sort((itemA, itemB) => string.Compare(itemA.I18N, itemB.I18N, StringComparison.Ordinal));

            SortedItems.ForEach(item => {
                itemComboBox.Items.Add(item);
            });
        }

        private void BrowseButtonClick(object sender, EventArgs e) {
            if (soundFileDialog.ShowDialog(this) != DialogResult.OK) {
                return;
            }
            soundFileBox.Text = soundFileDialog.FileName;
            if (itemComboBox.SelectedItem != null) {
                addButton.Enabled = true;
            }
        }

        private void ItemSelected(object sender, EventArgs e) {
            if (itemComboBox.SelectedItem == null) {
                addButton.Enabled = false;
                return;
            }
            if (!string.IsNullOrEmpty(soundFileBox.Text)) {
                addButton.Enabled = true;
            }
        }

        private void AddSound(object sender, EventArgs e) {
            var item = itemComboBox.SelectedItem as Item;
            var soundFile = soundFileBox.Text;

            Console.WriteLine();
        }
    }
}
