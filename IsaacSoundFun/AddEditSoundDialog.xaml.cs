using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using IsaacSoundFun.Model;
using IsaacSoundFun.Player;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using Microsoft.Win32;

namespace IsaacSoundFun {

    /// <summary>
    /// Interaction logic for AddEditSoundDialog.xaml
    /// </summary>
    public partial class AddEditSoundDialog : Window {
        private readonly SoundDialogModel _model = new SoundDialogModel();
        private readonly Control _callbackControl;
        private readonly bool _edit;
        private readonly int _oldItemId;

        private static List<Item> _allSortedItems;

        public AddEditSoundDialog(Control callbackControl, int selectedItemId = -1) {
            _callbackControl = callbackControl;
            InitializeComponent();
            Title = "Add New Sound";
            CreateBindings();
            if (_allSortedItems == null) {
                _allSortedItems = BuildAllSortedItems();
            }

            var existingItems = ExistingSoundsItemIds(selectedItemId);
            var validItems = _allSortedItems.Where(item => !existingItems.Contains(item.Id)).ToList();
            _model.SetItems(validItems);
        }

        public AddEditSoundDialog(Control callbackControl, string soundFile, int selectedItemId) : this(callbackControl, selectedItemId) {
            Title = "Edit Sound";
            _edit = true;
            _oldItemId = selectedItemId;

            _model.File = FileUtils.GetFullPath(soundFile);
            _model.SaveButtonEnabled = true;

            foreach (var item in itemComboBox.Items) {
                var itemObject = item as Item;
                if (itemObject == null || selectedItemId != itemObject.Id) continue;
                _model.Item = itemObject;
            }
        }

        private void BrowseFileClick(object sender, RoutedEventArgs e) {
            var fileDialog = new OpenFileDialog { Filter = "Supported Files (*.wav;*.mp3)|*.wav;*.mp3" };

            if (fileDialog.ShowDialog() != true) return;
            var file = fileDialog.FileName;
            _model.File = file;

            if (_model.Item != null) {
                _model.SaveButtonEnabled = true;
            }
        }

        private void SelectedItemChanged(object sender, PropertyChangedEventArgs e) {
            if (!"Item".Equals(e.PropertyName)) return;

            var selected = _model.Item;
            if (selected == null) {
                _model.SaveButtonEnabled = false;
                return;
            }
            if (!string.IsNullOrEmpty(_model.File)) {
                _model.SaveButtonEnabled = true;
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e) {
            var relativePath = FileUtils.GetRelativePath(_model.File);
            if (!_edit) {
                var mainWindow = _callbackControl as MainWindow;
                if (mainWindow == null) {
                    return;
                }
                mainWindow.CreateSoundRow(relativePath, _model.Item);
            } else {
                var soundRow = _callbackControl as SoundRow;
                if (soundRow == null) {
                    return;
                }
                soundRow.EditSound(relativePath, itemComboBox.SelectedItem as Item, _oldItemId);
            }
            Close();
        }

        private void CreateBindings() {
            fileTextBox.SetBinding(TextBox.TextProperty, new Binding("File") {
                Source = _model,
                Mode = BindingMode.OneWay
            });

            itemComboBox.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items") {
                Source = _model,
                Mode = BindingMode.OneWay
            });
            itemComboBox.SetBinding(Selector.SelectedItemProperty, new Binding("Item") {
                Source = _model,
                Mode = BindingMode.TwoWay
            });
            _model.ItemChanged += SelectedItemChanged;

            saveButton.SetBinding(IsEnabledProperty, new Binding("SaveButtonEnabled") {
                Source = _model,
                Mode = BindingMode.OneWay
            });
        }

        private static List<Item> BuildAllSortedItems() {
            var sortedItems = new List<Item>();
            Items.RebirthItems.ForEach(sortedItems.Add);
            Items.AfterbirthItems.ForEach(sortedItems.Add);
            Items.AfterbirthPlusItems.ForEach(sortedItems.Add);
            Items.AntibirthItems.ForEach(item => {
                var editedItem = new Item {
                    Id = item.Id,
                    I18N = item.I18N + " (Antibirth)"
                };
                sortedItems.Add(editedItem);
            });
            sortedItems.Sort((itemA, itemB) => string.Compare(itemA.I18N, itemB.I18N, StringComparison.Ordinal));
            return sortedItems;
        }

        private static List<int> ExistingSoundsItemIds(int editId = -1) {
            var existingSoundsItemIds = SoundFunPlayer.Entities.Keys.ToList();
            existingSoundsItemIds.Remove(editId);
            return existingSoundsItemIds;
        }
    }
}
