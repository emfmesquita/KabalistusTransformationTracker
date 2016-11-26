using System.Drawing;

namespace KabalistusTransformationTracker.Images {
    public class ItemImage: BaseImage {
        private readonly Bitmap _imageB;

        public ItemImage(string name, int x = 0, int y = 0, float scale = 1F) : base(name, x, y, scale) {
            HasItem = false;
            _imageB = ImageHelper.AdjustBrightnessContrast(InnerImage, 0.3F, 0.8F);
        }
        public override Bitmap Image => HasItem ? InnerImage : _imageB;
        public bool HasItem { get; set; }
    }
}
