using System.Drawing;
using System.Drawing.Imaging;

namespace KabalistusTransformationTracker.Images {
    public class ImageHelper {
        public static Bitmap AdjustBrightnessContrast(Image image, float contrast, float brightness) {
            // Make the ColorMatrix.
            var cm = new ColorMatrix(new float[][]
                {
                    new float[] {contrast, 0, 0, 0, 0}, // scale red
                    new float[] {0, contrast, 0, 0, 0}, // scale green
                    new float[] {0, 0, contrast, 0, 0}, // scale blue
                    new float[] {0, 0, 0, 1.0f, 0}, // don't scale alpha
                    new float[] { brightness, brightness, brightness, 0, 1}
                });
            var attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            // Draw the image onto the new bitmap while applying
            // the new ColorMatrix.
            Point[] points = {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(0, image.Height),
            };
            var rect = new Rectangle(0, 0, image.Width, image.Height);

            // Make the result bitmap.
            var bm = new Bitmap(image.Width, image.Height);
            using (var gr = Graphics.FromImage(bm)) {
                gr.DrawImage(image, points, rect,
                    GraphicsUnit.Pixel, attributes);
            }

            // Return the result.
            return bm;
        }
    }
}
