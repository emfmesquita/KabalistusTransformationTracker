using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KabalistusTransformationTracker.Trans;
using KabalistusTransformationTracker.Utils;

namespace KabalistusTransformationTracker.Images {
    public class ItemCluster {

        private int _currentIndex;
        private static readonly Image Ring = Properties.Resources.ring;
        private static readonly Image Block = Properties.Resources.block;
        private static Image _coloredBlock;

        private readonly Stopwatch _tooltipIntervalSw = new Stopwatch();

        public ItemCluster(Transformation trans) {
            Label = new Label {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                Location = new Point(4, 106),
                MinimumSize = new Size(120, 0),
                Name = trans.Name + "Label",
                Size = new Size(120, 20),
                TabIndex = 0,
                Text = trans.I18N + ": 0",
                TextAlign = ContentAlignment.MiddleCenter
            };

            BaseBox = new PictureBox {
                BackColor = Color.Transparent,
                Location = new Point(14, 3),
                Name = trans.Name + "PBox",
                Size = new Size(100, 100),
                TabIndex = 14,
                TabStop = false
            };

            TransformationImage = new BaseImage(trans.Name, trans.X, trans.Y, trans.Scale);
            InitTooltip(TransformationImage, BaseBox, trans.I18N);

            Images = trans.Items.Select(item => {
                var itemImage = AfterbirthPlusTransformations.Adult.Equals(trans) ? new PillImage(item.Name, item.X, item.Y, item.Scale, item.BlockReduction) : new ItemImage(item.Name, item.X, item.Y, item.Scale, item.BlockReduction);
                InitTooltip(itemImage, BaseBox, item.I18N);
                return itemImage;
            }).ToList();

            Panel = new Panel {
                Location = new Point(544, 8),
                Name = trans.Name + "Panel",
                Size = new Size(128, 143)
            };
            Panel.Controls.Add(BaseBox);
            Panel.Controls.Add(Label);

            Menu = new ToolStripMenuItem {
                Checked = true,
                CheckOnClick = true,
                CheckState = CheckState.Checked,
                Name = trans.Name + "ToolStripMenuItem",
                Size = new Size(132, 22),
                Text = trans.I18N
            };

            Menu.Click += (sender, e) => {
                TransformationViewHelper.ShowHideTransformation(trans, Menu.Checked);
            };

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
        public Label Label { get; }
        public Panel Panel { get; }
        public ToolStripMenuItem Menu { get; }

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
            Images.ForEach(image => image.UpdateImages());
        }

        public static void UpdateBlockImage(Color color) {
            _coloredBlock = ImageHelper.WhiteToColor(Block, color);
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
            args.Graphics.DrawImage(_coloredBlock, image.BlockX, image.BlockY, image.BlockSideLength, image.BlockSideLength);
        }

        private static void InitTooltip(BaseImage image, Control parentControl, string text) {
            image.Tooltip.SetToolTip(parentControl, text);
            image.Tooltip.Active = false;
            image.Tooltip.AutomaticDelay = 300;
        }
    }
}
