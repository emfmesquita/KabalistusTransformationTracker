using System.Windows.Media.Imaging;
using KabalistusCommons.Model;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.PillPool.Model {
    public class PillRowModel : BaseModel {
        private string _pillImageResource;
        private BitmapImage _pillImage;
        private string _label;

        public BitmapImage PillImage {
            get {
                return _pillImage;
            }
            set {
                if (Equals(value, _pillImage)) return;
                _pillImage = value;
                NotifyPropertyChanged();
            }
        }

        public string PillImageResource {
            get {
                return _pillImageResource;
            }
            set {
                if (value == _pillImageResource) return;
                _pillImageResource = value;
                PillImage = string.IsNullOrEmpty(_pillImageResource) ? null : ImageUtils.GetImage(_pillImageResource);
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
