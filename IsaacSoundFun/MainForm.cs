using System.Collections.Generic;
using System.Windows.Forms;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;

namespace IsaacFun {
    public partial class MainForm : Form {
        public static Dictionary<int, Item> IndexedItems = new Dictionary<int, Item>(); 

        public MainForm() {
            InitializeComponent();
            FormUtils.BuiltTitle("Isaac Sound Fun", this);

            Items.RebirthItems.ForEach(item => IndexedItems.Add(item.Id, item));
            Items.AfterbirthItems.ForEach(item => IndexedItems.Add(item.Id, item));
            Items.AfterbirthPlusItems.ForEach(item => IndexedItems.Add(item.Id, item));
        }

        private void AddMenuButtonClick(object sender, System.EventArgs e) {
            var addSoundModal = new AddSoundModal();
            addSoundModal.ShowDialog(this);
        }
    }
}
