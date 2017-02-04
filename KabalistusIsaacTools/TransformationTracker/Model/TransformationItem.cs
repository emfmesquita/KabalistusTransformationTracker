using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.TransformationTracker.Model {
    public class TransformationItem : GeneralImageModel {

        private const float UnlitImageBrightness = 0.3F;
        private const float UnlitImageContrast = 0.8F;
        private const string BlockImageResource = "KabalistusIsaacTools.Images.block.png";

        private bool _touched;
        private bool _blocked;
        private Color _blockColor;
        private float _blockScale = 1;

        private static readonly Dictionary<string, BitmapImage> UnlitImageCache = new Dictionary<string, BitmapImage>();

        public TransformationItem(string i18N, string resource, int x = 0, int y = 0, float scale = 1) : base(resource, i18N, x, y) {
            ItemImageModel = new GeneralImageModel(resource);
            Blocked = CreationMode.BlockModeOn;
            Touched = CreationMode.On;
            Scale = scale;
        }

        public TransformationItem(string i18N, int id, int x = 0, int y = 0, float scale = 1, float blockScale = 1) : this(i18N, ToResource(id), x, y, scale) {
            BlockImageModel = new GeneralImageModel(BlockImageResource) {
                Visibility = Visibility.Hidden
            };
            Id = id;
            BlockScale = blockScale;
            BlockColor = Color.FromArgb(143, 25, 139);
        }

        public GeneralImageModel ItemImageModel { get; }
        public GeneralImageModel BlockImageModel { get; }

        public int Id { get; }

        public bool Touched {
            get {
                return _touched;
            }
            set {
                _touched = value;
                var image = ImageUtils.GetImage(Resource);
                if (image != null) {
                    ItemImageModel.Image = _touched ? image : UnlitImage(Resource);
                }
            }
        }

        public bool Blocked {
            get {
                return _blocked;
            }
            set {
                _blocked = value;
                if (BlockImageModel == null) return;
                BlockImageModel.Visibility = _blocked ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public override float Scale {
            get {
                return base.Scale;
            }

            set {
                base.Scale = value;
                ItemImageModel.Scale = value;
                if (BlockImageModel == null) return;
                BlockImageModel.Scale = value * _blockScale;
            }
        }

        public float BlockScale {
            get {
                return _blockScale;
            }

            set {
                _blockScale = value;
                if (BlockImageModel == null) return;
                BlockImageModel.Scale = Scale * _blockScale;
            }
        }

        public Color BlockColor {
            get {
                return _blockColor;
            }
            set {
                _blockColor = value;
                if (BlockImageModel == null) return;
                BlockImageModel.Image = BlockImage(_blockColor);
            }
        }

        private static string ToResource(int id) {
            return $"KabalistusIsaacTools.Images.Items.c{id}.png";
        }

        private static BitmapImage UnlitImage(string resource) {
            if (UnlitImageCache.ContainsKey(resource)) return UnlitImageCache[resource];
            var bitmapImage = ImageUtils.GetImage(resource);
            if (bitmapImage == null) {
                return null;
            }
            var image = ImageUtils.ToBitmap(bitmapImage);
            image = ImageUtils.AdjustBrightnessContrast(image, UnlitImageBrightness, UnlitImageContrast);
            UnlitImageCache.Add(resource, ImageUtils.ToBitmapImage(new Bitmap(image)));
            return UnlitImageCache[resource];
        }

        private static BitmapImage BlockImage(Color color) {
            var bitmapImage = ImageUtils.GetImage(BlockImageResource);
            var image = ImageUtils.ToBitmap(bitmapImage);
            image = ImageUtils.WhiteToColor(image, color);
            return ImageUtils.ToBitmapImage(new Bitmap(image));
        }

        public override string ToString() {
            var scale = Scale.ToString("N", new CultureInfo("en-US"));
            var blockScale = BlockScale.ToString("N", new CultureInfo("en-US"));
            return $"                new TransformationItem(\"{Tooltip}\", {Id}, {X}, {Y}, {scale}F, {blockScale}F),";
        }
    }
}
