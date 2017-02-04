using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusIsaacTools.SoundFun.Player;

namespace KabalistusIsaacTools.SoundFun {
    /// <summary>
    /// Interaction logic for SoundFun.xaml
    /// </summary>
    public partial class SoundFun : UserControl {
        private readonly MenuBarModel _menuBarModel = new MenuBarModel();

        private bool _firstCheck = true;

        public SoundFun() {
            InitializeComponent();
            CreateBindings();
            LoadSave();
            StartPooling();
        }

        private void CreateBindings() {
            // menu bar
            PlayButton.SetBinding(IsEnabledProperty, new Binding("PlayButtonEnabled") {
                Source = _menuBarModel,
                Mode = BindingMode.OneWay
            });
            PlayButtonImage.SetBinding(Image.SourceProperty, new Binding("PlayButtonImage") {
                Source = _menuBarModel,
                Mode = BindingMode.OneWay
            });
            ProgressLabel.SetBinding(ContentProperty, new Binding("Progress") {
                Source = _menuBarModel,
                Mode = BindingMode.OneWay
            });
            NowPlayingLabel.SetBinding(ContentProperty, new Binding("NowPlaying") {
                Source = _menuBarModel,
                Mode = BindingMode.OneWay
            });
        }

        private void LoadSave() {
            var save = SoundFunSerializer.Load();
            if (save == null || !save.Entities.Any()) return;

            var entities = save.Entities.ToList();
            entities.ForEach(entity => {
                CreateSoundRow(entity.SoundFile, entity.Item, false);
            });
        }

        public void CreateSoundRow(string soundFile, Item item, bool save = true) {
            var entity = new SoundFunEntity {
                SoundFile = soundFile,
                Item = item
            };

            var row = new SoundRow(soundFile, item, this);
            MainPanel.Children.Add(row);

            SoundFunPlayer.Entities.Add(entity.Item.Id, entity);

            if (save) {
                SoundFunSerializer.Save();
            }
        }

        public void RemoveRow(SoundRow row, int itemId) {
            MainPanel.Children.Remove(row);
            SoundFunPlayer.Entities.Remove(itemId);
            SoundFunSerializer.Save();
        }

        public void Update(Status status, IIsaacReader reader) {
            if (!status.Ready) {
                SoundFunPlayer.ResetTouchedItems();
                _firstCheck = true;
                return;
            }

            var numberOfPlayers = MemoryReader.GetNumberOfPlayers();
            if (numberOfPlayers == 0) {
                SoundFunPlayer.ResetTouchedItems();
                _firstCheck = true;
                return;
            }

            var reseted = reader.GetTimeCounter() < 2;
            // resets on hold "r"
            if (reseted) {
                SoundFunPlayer.ResetTouchedItems();
            }

            if (reader.IsGamePaused()) {
                SoundFunPlayer.Pause(false);
                return;
            }

            // tries to resume
            SoundFunPlayer.Resume(false);

            var startCheck = reseted || _firstCheck;
            _firstCheck = false;

            SoundFunPlayer.CheckPlaySound(reader.GetItemsTouchedList(), startCheck);
        }

        private void NewButtonClick(object sender, RoutedEventArgs e) {
            new AddEditSoundDialog(this).ShowDialog();
        }

        private void PauseResumeClick(object sender, RoutedEventArgs e) {
            SoundFunPlayer.PausePlay();
        }

        private void StartPooling() {
            var viewPooling = new System.Timers.Timer(100);

            var wasPlaying = false;
            viewPooling.Elapsed += (source, e) => {
                var loadedSound = SoundFunPlayer.LoadedSound();
                _menuBarModel.PlayButtonEnabled = !string.IsNullOrEmpty(loadedSound);
                if (!_menuBarModel.PlayButtonEnabled) return;

                _menuBarModel.NowPlaying = loadedSound;
                _menuBarModel.Progress = SoundFunPlayer.GetProgess();

                var isPlaying = SoundFunPlayer.IsPlaying();
                if (wasPlaying == isPlaying) return;
                wasPlaying = isPlaying;
                if (isPlaying) {
                    _menuBarModel.SetToPauseImage();
                } else {
                    _menuBarModel.SetToPlayImage();
                }
            };
            viewPooling.AutoReset = true;
            viewPooling.Enabled = true;


            var filePooling = new System.Timers.Timer(2000);
            filePooling.Elapsed += (source, e) => {
                Dispatcher.Invoke(() => {
                    foreach (Control control in MainPanel.Children) {
                        var row = control as SoundRow;
                        row?.UpdateSoundLabelColor();
                    }
                });

            };
            filePooling.AutoReset = true;
            filePooling.Enabled = true;
        }
    }
}
