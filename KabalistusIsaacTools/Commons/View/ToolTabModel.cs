using System.Windows.Media.Imaging;
using KabalistusCommons.Model;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.Commons.View {
    public class ToolTabModel : BaseModel {
        private BitmapImage _icon;
        private string _iconResource;
        private string _label;

        public ToolTabModel(string iconResource, string label) {
            IconResource = iconResource;
            Label = label;
        }

        public BitmapImage Icon {
            get {
                return _icon;
            }
            set {
                if (Equals(value, _icon)) return;
                _icon = value;
                NotifyPropertyChanged();
            }
        }

        public string IconResource {
            get {
                return _iconResource;
            }
            set {
                if (value == _iconResource) return;
                _iconResource = value;
                if (string.IsNullOrEmpty(_iconResource)) return;
                Icon = ImageUtils.GetImage(_iconResource);
            }
        }

        public string Label {
            get {
                return _label;
            }
            set {
                if (value == _label) return;
                _label = value;
                NotifyPropertyChanged();
            }
        }
    }
}
