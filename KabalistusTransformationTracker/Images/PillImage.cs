using System.Drawing;
using KabalistusTransformationTracker.Providers;

namespace KabalistusTransformationTracker.Images {
    public class PillImage : ItemImage {
        private string _currentPillName;

        public PillImage(string name, int x = 0, int y = 0, float scale = 1, int blockReduction = 10) : base(name, x, y, scale, blockReduction) {
            _currentPillName = BaseInfoProvider.UnknowPubertyPill;
        }

        public override Image Image {
            get {
                var pillName = TransformationInfoProvider.GetPubertyPill();
                if (_currentPillName.Equals(pillName)) {
                    return base.Image;
                }

                _currentPillName = pillName;
                InnerImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(pillName);
                UpdateImages();
                return base.Image;
            }
        }
    }
}
