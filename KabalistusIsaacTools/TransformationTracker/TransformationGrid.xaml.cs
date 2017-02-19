using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.TransformationTracker.Model;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.TransformationTracker {
    /// <summary>
    /// Interaction logic for TransformationGrid.xaml
    /// </summary>
    public partial class TransformationGrid : UserControl {
        private readonly Transformation _model;
        private readonly Timer _tooltipTimer = new Timer(100);

        public TransformationGrid(Transformation model) {
            InitializeComponent();
            _model = model;

            var transformationImage = new GeneralImage(_model.TransformationImageModel);
            CreationModeClick(transformationImage, _model.TransformationImageModel);
            ImageGrid.Children.Add(transformationImage);

            model.Items.ForEach(item => {
                var itemImage = new TransformationItemControl(item);
                CreationModeClick(itemImage, item);
                ImageGrid.Children.Add(itemImage);
            });

            _tooltipTimer.AutoReset = false;
            _tooltipTimer.Elapsed += (source, e) => {
                Dispatcher.Invoke(UpdateItemToolTip);
            };

            CreateBindings();
        }

        private void CreateBindings() {
            ImageGrid.SetBinding(ToolTipProperty, new Binding("ItemTooltip") {
                Source = _model,
                Mode = BindingMode.OneWay
            });

            Label.SetBinding(ContentProperty, new Binding("TransformationLabel") {
                Source = _model,
                Mode = BindingMode.OneWay
            });

            RootGrid.SetBinding(VisibilityProperty, new Binding("Visibility") {
                Source = _model,
                Mode = BindingMode.OneWay
            });

            Ring.SetBinding(VisibilityProperty, new Binding("RingVisibility") {
                Source = _model,
                Mode = BindingMode.OneWay
            });
        }

        private static void CreationModeClick(IInputElement image, GeneralImageModel model) {
            if (!CreationMode.On) {
                return;
            }
            image.MouseLeftButtonDown += (sender, args) => {
                CreationMode.CurrentImage = model;
            };
        }

        private void OnMouseMove(object sender, MouseEventArgs e) {
            _tooltipTimer.Stop();
            _tooltipTimer.Start();
        }

        private void UpdateItemToolTip() {
            var mousePosition = Mouse.GetPosition(ImageGrid);
            var itemsOver = new List<TransformationItem>();
            foreach (var child in ImageGrid.Children) {
                var itemImage = child as TransformationItemControl;
                if (itemImage == null) continue;
                if (!_model.ShowTransformationImage && IsOverItem(mousePosition, itemImage.Model)) {
                    itemsOver.Add(itemImage.Model);
                } else {
                    itemImage.Model.ShowTooltip = false;
                }
            }

            if (!itemsOver.Any()) return;

            var closest = ClosestItem(mousePosition, itemsOver);
            itemsOver.ForEach(item => {
                if (!item.Equals(closest)) item.ShowTooltip = false;
            });
            closest.ShowTooltip = true;
        }

        private static bool IsOverItem(Point mouse, TransformationItem item) {
            var withinWidth = mouse.X >= item.AbsoluteX && (mouse.X - item.AbsoluteX) <= item.Width;
            var withinHeight = mouse.Y >= item.AbsoluteY && (mouse.Y - item.AbsoluteY) <= item.Height;
            return withinWidth && withinHeight;
        }

        private static TransformationItem ClosestItem(Point mouse, List<TransformationItem> itemsOver) {
            if (!itemsOver.Any()) return null;

            if (itemsOver.Count == 1) {
                return itemsOver[0];
            }

            var closestItem = (TransformationItem)null;
            var currentDistance = double.MaxValue;
            itemsOver.ForEach(item => {
                var distance = GetDistanceBetweenPoints(mouse, item.Center);
                if (!(distance < currentDistance)) return;
                closestItem = item;
                currentDistance = distance;
            });
            return closestItem;
        }
        private static double GetDistanceBetweenPoints(Point p, Point q) {
            var a = p.X - q.X;
            var b = p.Y - q.Y;
            return Math.Sqrt(a * a + b * b);
        }
    }
}
