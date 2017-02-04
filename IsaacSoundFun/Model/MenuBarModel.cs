using System.Windows.Media.Imaging;
using KabalistusCommons.Model;

namespace IsaacSoundFun.Model {
    public class MenuBarModel : BaseModel {
        private readonly BitmapImage _playImage = ViewUtils.ToBitmapImage(Properties.Resources.play24);
        private readonly BitmapImage _pauseImage = ViewUtils.ToBitmapImage(Properties.Resources.pause24);


        private bool _playButtonEnabled;
        private BitmapImage _playButtonImage;
        private string _progress = "";
        private string _nowPlaying = "";

        public MenuBarModel() {
            Progress = "00:00 / 00:00";
            PlayButtonImage = _playImage;
        }

        public bool PlayButtonEnabled {
            get {
                return _playButtonEnabled;
            }

            set {
                if (value == _playButtonEnabled) return;
                _playButtonEnabled = value;
                NotifyPropertyChanged();
            }
        }

        public BitmapImage PlayButtonImage {
            get {
                return _playButtonImage;
            }

            private set {
                if (value.Equals(_playButtonImage)) return;
                _playButtonImage = value;
                NotifyPropertyChanged();
            }
        }

        public void SetToPlayImage() {
            PlayButtonImage = _playImage;
        }

        public void SetToPauseImage() {
            PlayButtonImage = _pauseImage;
        }

        public string Progress {
            get {
                return _progress;
            }

            set {
                if (value == _progress) return;
                _progress = value;
                NotifyPropertyChanged();
            }
        }

        public string NowPlaying {
            get {
                return _nowPlaying;
            }

            set {
                if (value == _nowPlaying) return;
                _nowPlaying = value;
                NotifyPropertyChanged();
            }
        }
    }
}
