using System.Windows.Media.Imaging;
using KabalistusCommons.Model;

namespace KabalistusIsaacTools.Commons.View {
    public class CenteredImageModel : BaseModel {
        private BitmapImage _image;

        public CenteredImageModel(BitmapImage image) {
            _image = image;
        }

        public BitmapImage Image {
            get {
                return _image;
            }
            set {
                if (Equals(value, _image)) return;
                _image = value;
                NotifyPropertyChanged();
            }
        }
    }
}
