using System.Drawing;
using KabalistusTransformationTracker.Providers;
using KabalistusTransformationTracker.Utils;

namespace KabalistusTransformationTracker.Images {
    public class ItemImage : BaseImage {
        private Image _untouchedImage;

        private const float UntouchedImageBrightness = 0.3F;
        private const float UntouchedImageContrast = 0.8F;

        public ItemImage(string name, int x = 0, int y = 0, float scale = 1F, int blockReduction = 10) : base(name, x, y, scale) {
            ItemTouched = false;
            BlockReduction = blockReduction;
            _untouchedImage = BuildUntouchedImage();

            CalcBlockStats();
        }

        public override Image Image => ItemTouched || ShowBlackListed(this) ? InnerImage : _untouchedImage;

        public bool ItemTouched { get; set; }
        public bool ItemBlacklisted { get; set; }

        public int BlockReduction;
        public int BlockSideLength { get; private set; }
        public int BlockX { get; private set; }
        public int BlockY { get; private set; }

        public void CalcBlockStats() {
            var scaledWidth = ScaledWidth;
            var scaledHeight = ScaledHeight;
            var widthIsLower = scaledWidth <= scaledHeight;

            if (widthIsLower) {
                BlockSideLength = scaledWidth - BlockReduction;
                BlockX = X + BlockReduction / 2;
                BlockY = Y + (scaledHeight / 2) - (BlockSideLength / 2);
            } else {
                BlockSideLength = scaledHeight - BlockReduction;
                BlockX = X + (scaledWidth / 2) - (BlockSideLength / 2);
                BlockY = Y + BlockReduction / 2;
            }
        }

        public void UpdateImagesCreationMode() {
            _untouchedImage = BuildUntouchedImage();
        }

        public static bool ShowBlackListed(ItemImage image) {
            return (image.ItemBlacklisted && !TransformationInfoProvider.IsInBlindFloor() &&
                    MainForm.ShowBlacklistedItems) || CreationMode.BlockModeOn;
        }

        public override string ToString() {
            return base.ToString() + "   BR: " + BlockReduction;
        }

        private Image BuildUntouchedImage() {
            return ImageHelper.AdjustBrightnessContrast(InnerImage, UntouchedImageBrightness, UntouchedImageContrast);
        }
    }
}
