using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using IsaacSoundFun.Model;
using IsaacSoundFun.Player;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;

namespace IsaacSoundFun {
    /// <summary>
    /// Interaction logic for SoundRow.xaml
    /// </summary>
    public partial class SoundRow : UserControl {
        private readonly SoundRowModel _model = new SoundRowModel();
        private readonly MainWindow _mainWindow;

        public SoundRow(string file, Item item, MainWindow mainWindow) {
            InitializeComponent();
            CreateBindings();
            _model.Item = item;
            _model.File = file;
            _mainWindow = mainWindow;
            UpdateSoundLabelColor();
        }

        public void EditSound(string soundFile, Item item, int oldItemId) {
            var entity = SoundFunPlayer.Entities[oldItemId];
            _model.File = soundFile;
            entity.SoundFile = soundFile;
            if (item.Id != oldItemId) {
                _model.Item = item;
                entity.Item = item;
                SoundFunPlayer.Entities.Add(item.Id, entity);
                SoundFunPlayer.Entities.Remove(oldItemId);
            }
            SoundFunSerializer.Save();
        }

        public void UpdateSoundLabelColor() {
            if (FileUtils.Exists(_model.File)) {
                _model.FileFound();
            } else {
                _model.FileNotFound();
            }
        }

        private void PlayClick(object sender, RoutedEventArgs e) {
            SoundFunPlayer.PlaySound(_model.File);
        }

        private void EditClick(object sender, RoutedEventArgs e) {
            new AddEditSoundDialog(this, _model.File, _model.Item.Id).ShowDialog();
        }

        private void DeleteClick(object sender, RoutedEventArgs e) {
            var confirmResult = MessageBox.Show("Are you sure to delete this sound entry?", "Delete Sound Entry", MessageBoxButton.YesNo);
            if (confirmResult != MessageBoxResult.Yes) return;
            _mainWindow.RemoveRow(this, _model.Item.Id);
        }

        private void CreateBindings() {
            // menu bar
            itemLabel.SetBinding(ContentProperty, new Binding("Item") {
                Source = _model,
                Mode = BindingMode.OneWay
            });
            fileLabel.SetBinding(ContentProperty, new Binding("File") {
                Source = _model,
                Mode = BindingMode.OneWay
            });
            fileLabel.SetBinding(ForegroundProperty, new Binding("FileLabelForeground") {
                Source = _model,
                Mode = BindingMode.OneWay
            });
        }
    }
}
