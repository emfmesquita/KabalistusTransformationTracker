using System.Drawing;
using System.Windows.Forms;
using KabalistusTransformationTracker.Providers;

namespace KabalistusTransformationTracker.Images {
    public class BaseImage {
        protected Bitmap InnerImage;

        public BaseImage(string name, int x = 0, int y = 0, float scale = 1F) {
            Name = name;
            X = x;
            Y = y;
            Scale = scale;

            var fileName = name.StartsWith("pill") ? BaseInfoProvider.UnknowPubertyPill : name;
            InnerImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(fileName);

            Tooltip = new ToolTip();

            var centerX = X + ScaledWidth/2;
            var centerY = Y + ScaledHeight/2;
            Center = new Point(centerX, centerY);
        }

        public int X { get; set; }
        public int Y { get; set; }
        public float Scale { get; set; }
        public int ScaledWidth => (int)(InnerImage.Width * Scale);
        public int ScaledHeight => (int)(InnerImage.Height * Scale);
        public string Name { get; }
        public ToolTip Tooltip { get; }
        public virtual Image Image => InnerImage;
        public Point Center { get; }

        public override string ToString() {
            return $"{Name} - X: {X}   Y: {Y}   S: {Scale}";
        }
    }
}
