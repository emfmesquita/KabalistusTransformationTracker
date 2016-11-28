using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KabalistusTransformationTracker.Utils;

namespace KabalistusTransformationTracker.Images {
    public class ItemCluster {

        private int _currentIndex;
        private static readonly Bitmap Ring = Properties.Resources.ring;
        private static readonly Bitmap Block = Properties.Resources.block;

        private readonly Stopwatch _tooltipIntervalSw = new Stopwatch();

        public ItemCluster(PictureBox baseBox, BaseImage transformationImage, List<ItemImage> images) {
            BaseBox = baseBox;
            BaseBox.BackColor = Color.Transparent;
            TransformationImage = transformationImage;
            Images = images;

            AddMouseMovedHandler(this);

            BaseBox.Paint += (sender, args) => {
                if (CreationMode.On) {
                    args.Graphics.DrawImage(Ring, -1, -1, 100, 100);
                }

                if (ShowTransformation()) {
                    DrawImage(args, TransformationImage);
                    return;
                }
                foreach (var image in Images) {
                    DrawImage(args, image);
                    if (ItemImage.ShowBlackListed(image)) {
                        DrawBlock(args, image);
                    }
                }
            };

            _tooltipIntervalSw.Start();

            if (!CreationMode.On) {
                return;
            }

            BaseBox.BorderStyle = BorderStyle.FixedSingle;

            BaseBox.Click += (sender, args) => {
                CreationMode.CurrentCluster = this;
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

        public void UpdateImagesCreationMode() {
            Images.ForEach(image => image.UpdateImagesCreationMode());
        }

        private bool ShowTransformation() {
            return Transformed && MainForm.ShowTransformationImage;
        }

        private static void AddMouseMovedHandler(ItemCluster cluster) {
            cluster.BaseBox.MouseMove += (sender, args) => {
                if (cluster._tooltipIntervalSw.ElapsedMilliseconds < 100) {
                    return;
                }
                cluster._tooltipIntervalSw.Restart();

                if (cluster.ShowTransformation()) {
                    DeactivateTooltips(cluster.Images);
                    ActivateTooltip(cluster.TransformationImage);
                    return;
                }

                DeactivateTooltip(cluster.TransformationImage);

                var imagesOver = new List<ItemImage>();
                var imagesNotOver = new List<ItemImage>();
                var mouse = new Point(args.X, args.Y);

                cluster.Images.ForEach(image => {
                    if (ImageHelper.IsOverImage(mouse, image)) {
                        imagesOver.Add(image);
                    } else {
                        imagesNotOver.Add(image);
                    }
                });

                if (!imagesOver.Any()) {
                    DeactivateTooltips(cluster.Images);
                } else if (imagesOver.Count() == 1) {
                    DeactivateTooltips(imagesNotOver);
                    ActivateTooltip(imagesOver[0]);
                } else {
                    var closestImage = ImageHelper.ClosestImage(mouse, imagesOver);
                    imagesOver.Remove(closestImage);
                    DeactivateTooltips(imagesNotOver);
                    DeactivateTooltips(imagesOver);
                    ActivateTooltip(closestImage);
                }
            };
        }

        private static void DeactivateTooltips<T>(List<T> images) where T : BaseImage {
            images.ForEach(DeactivateTooltip);
        }

        private static void DeactivateTooltip<T>(T image) where T : BaseImage {
            image.Tooltip.Active = false;
        }

        private static void ActivateTooltip<T>(T image) where T : BaseImage {
            image.Tooltip.Active = true;
        }

        private static void DrawImage(PaintEventArgs args, BaseImage image) {
            args.Graphics.DrawImage(image.Image, image.X, image.Y, image.ScaledWidth, image.ScaledHeight);
        }

        private static void DrawBlock(PaintEventArgs args, ItemImage image) {
            args.Graphics.DrawImage(Block, image.BlockX, image.BlockY, image.BlockSideLength, image.BlockSideLength);
        }
    }
}
