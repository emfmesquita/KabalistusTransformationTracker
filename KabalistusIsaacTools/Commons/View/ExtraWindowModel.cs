using System.Windows.Media.Imaging;
using KabalistusCommons.Model;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.Commons.View {
    public class ExtraWindowModel : BaseModel {

        private BitmapImage _icon;
        private string _title;
        private string _iconResource;

        public ExtraWindowModel(string iconResource, string title) {
            IconResource = iconResource;
            Title = title;
        }

        public BitmapImage Icon {
            get {
                return _icon;
            }
            set {
                if (value.Equals(_icon)) return;
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

        public string Title {
            get {
                return _title;
            }
            set {
                if (value == _title) return;
                _title = value;
                NotifyPropertyChanged();
            }
        }
    }
}
