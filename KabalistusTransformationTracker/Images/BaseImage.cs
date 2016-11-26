using System.Drawing;

namespace KabalistusTransformationTracker.Images {
    public class BaseImage {
        protected readonly Bitmap InnerImage;

        public BaseImage(string name, int x = 0, int y = 0, float scale = 1F) {
            Name = name;
            X = x;
            Y = y;
            Scale = scale;
            InnerImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(name);
        }

        public int X { get; set; }
        public int Y { get; set; }
        public float Scale { get; set; }
        public string Name { get; }
        public virtual Bitmap Image => InnerImage;

        public override string ToString() {
            return $"{Name} - X: {X}   Y: {Y}   S: {Scale}";
        }
    }
}
