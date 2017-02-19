using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace KabalistusIsaacTools.Utils {
    public class ImageUtils {

        public static readonly Dictionary<string, BitmapImage> ImageCache = new Dictionary<string, BitmapImage>();

        public static BitmapImage ToBitmapImage(Bitmap bitmap) {
            using (var memory = new MemoryStream()) {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        public static Bitmap ToBitmap(BitmapImage bitmapImage) {
            using (var outStream = new MemoryStream()) {
                BitmapEncoder enc = new PngBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage, null, null, null));
                enc.Save(outStream);
                var bitmap = new System.Drawing.Bitmap(outStream);
                return new Bitmap(bitmap);
            }
        }

        public static Image GetImageFromResource(string resource) {
            if (!resource.StartsWith("KabalistusIsaacTools")) return !File.Exists(resource) ? null : Image.FromFile(resource);
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resource);
            return stream == null ? null : Image.FromStream(stream);
        }

        public static BitmapImage GetImage(string resource) {
            if (ImageCache.ContainsKey(resource)) {
                return ImageCache[resource];
            }
            var image = GetImageFromResource(resource);
            if (image == null) {
                return null;
            }
            ImageCache.Add(resource, ToBitmapImage(new Bitmap(image)));
            return ImageCache[resource];
        }

        public static Bitmap AdjustBrightnessContrast(Bitmap image, float contrast, float brightness) {
            // Make the ColorMatrix.
            contrast += CreationMode.ContrastBuff;
            brightness += CreationMode.BrigtnessBuff;

            var cm = new ColorMatrix(new float[][]{
                    new [] {contrast, 0, 0, 0, 0}, // scale red
                    new [] {0, contrast, 0, 0, 0}, // scale green
                    new [] {0, 0, contrast, 0, 0}, // scale blue
                    new [] {0, 0, 0, 1.0f, 0}, // don't scale alpha
                    new [] { brightness, brightness, brightness, 0, 1}
                });
            return ApplyColorMatrix(image, cm);
        }

        public static Bitmap WhiteToColor(Bitmap image, Color color) {
            var rModifier = color.R / 255F;
            var gModifier = color.G / 255F;
            var bModifier = color.B / 255F;

            var cm = new ColorMatrix(new float[][]{
                    new [] { rModifier, 0, 0, 0, 0},
                    new [] {0, gModifier, 0, 0, 0},
                    new [] {0, 0, bModifier, 0, 0},
                    new [] {0, 0, 0, 1F, 0},
                    new [] { 0F, 0, 0, 0, 1}
                });

            return ApplyColorMatrix(image, cm);
        }

        private static Bitmap ApplyColorMatrix(Bitmap image, ColorMatrix cm) {
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
                gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);
            }

            // Return the result.
            return bm;
        }
    }
}
