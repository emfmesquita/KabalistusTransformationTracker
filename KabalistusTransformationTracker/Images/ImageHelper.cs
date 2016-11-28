using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using KabalistusTransformationTracker.Utils;

namespace KabalistusTransformationTracker.Images {
    public class ImageHelper {
        public static Bitmap AdjustBrightnessContrast(Image image, float contrast, float brightness) {
            // Make the ColorMatrix.
            contrast += CreationMode.ContrastBuff;
            brightness += CreationMode.BrigtnessBuff;

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

        public static ItemImage ClosestImage(Point mouse, List<ItemImage> imagesOver) {
            var closestImage = (ItemImage)null;
            var currentDistance = double.MaxValue;
            imagesOver.ForEach(image => {
                if (closestImage == null) {
                    closestImage = image;
                    return;
                }
                var distance = GetDistanceBetweenPoints(mouse, image.Center);
                if (!(distance < currentDistance)) return;
                closestImage = image;
                currentDistance = distance;
            });
            return closestImage;
        }

        public static double GetDistanceBetweenPoints(Point p, Point q) {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            return Math.Sqrt(a * a + b * b);
        }

        public static bool IsOverImage(Point mouse, BaseImage image) {
            var withinWidth = mouse.X >= image.X && (mouse.X - image.X) <= image.ScaledWidth;
            var withinHeight = mouse.Y >= image.Y && (mouse.Y - image.Y) <= image.ScaledHeight;
            return withinWidth && withinHeight;
        }
    }
}
