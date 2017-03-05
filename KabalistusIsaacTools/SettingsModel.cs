using System.Windows;
using System.Windows.Media;
using KabalistusCommons.Model;
using KabalistusCommons.Utils;
using KabalistusIsaacTools.Serializer;

namespace KabalistusIsaacTools {
    public class SettingsModel : BaseModel {
        private Color _backgroundColor;
        private Color _foregroundColor;

        public SettingsModel() {
            BackgroundColor = KabalistusToolsSerializer.Settings.GeneralSettings.BackgroundColor;
            ForegroundColor = KabalistusToolsSerializer.Settings.GeneralSettings.TextColor;
        }

        public Color BackgroundColor {
            get {
                return _backgroundColor;
            }

            set {
                if (value == _backgroundColor) return;
                _backgroundColor = value;
                _backgroundColorDebouncer.Tick(value);
                Application.Current.Resources["BackgroundColor"] = new SolidColorBrush(value);
                NotifyPropertyChanged();
            }
        }

        public Color ForegroundColor {
            get {
                return _foregroundColor;
            }

            set {
                if (value == _foregroundColor) return;
                _foregroundColor = value;
                _foregroundColorDebouncer.Tick(value);
                Application.Current.Resources["ForegroundColor"] = new SolidColorBrush(value);
                NotifyPropertyChanged();
            }
        }

        private readonly Debouncer<Color> _backgroundColorDebouncer = new Debouncer<Color>(300,
            color => {
                KabalistusToolsSerializer.Settings.GeneralSettings.BackgroundColor = color;
                KabalistusToolsSerializer.MarkToSave();
            });

        private readonly Debouncer<Color> _foregroundColorDebouncer = new Debouncer<Color>(300,
            color => {
                KabalistusToolsSerializer.Settings.GeneralSettings.TextColor = color;
                KabalistusToolsSerializer.MarkToSave();
            });
    }
}
