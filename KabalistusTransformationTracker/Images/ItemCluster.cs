using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KabalistusTransformationTracker.Images {
    public class ItemCluster {
        private int _currentIndex;
        private static readonly Bitmap Ring = Properties.Resources.ring;

        public ItemCluster(PictureBox baseBox, BaseImage transformationImage, List<ItemImage> images) {
            BaseBox = baseBox;
            BaseBox.BackColor = Color.Transparent;
            TransformationImage = transformationImage;
            Images = images;

            BaseBox.Paint += (sender, args) => {
                if (MainForm.CreationMode) {
                    args.Graphics.DrawImage(Ring, -1, -1, 100, 100);
                }

                if (Transformed && MainForm.ShowTransformationImage) {
                    DrawImage(args, TransformationImage);
                    return;
                }
                foreach (var image in Images) {
                    DrawImage(args, image);
                }
            };

            if (!MainForm.CreationMode) {
                return;
            }

            BaseBox.BorderStyle = BorderStyle.FixedSingle;

            BaseBox.Click += (sender, args) => {
                MainForm.CurrentCluster = this;
                Console.WriteLine(TransformationImage);
                foreach (var image in Images) {
                    Console.WriteLine(image);
                }
                Console.WriteLine(CurrentImage.Name);
            };
        }

        public PictureBox BaseBox { get; }
        public BaseImage TransformationImage { get; }
        public List<ItemImage> Images { get; }
        public BaseImage CurrentImage => Transformed ? TransformationImage : Images[_currentIndex];
        public bool Transformed { get; set; }

        public void NextImage() {
            _currentIndex++;
            if (_currentIndex >= Images.Count) {
                _currentIndex = 0;
            }
            Console.WriteLine(CurrentImage.Name);
        }

        public void PreviousImage() {
            _currentIndex--;
            if (_currentIndex < 0) {
                _currentIndex = Images.Count - 1;
            }
            Console.WriteLine(CurrentImage.Name);
        }

        private static void DrawImage(PaintEventArgs args, BaseImage image) {
            var width = (int)(image.Image.Width * image.Scale);
            var height = (int)(image.Image.Height * image.Scale);
            args.Graphics.DrawImage(image.Image, image.X, image.Y, width, height);
        }
    }
}
