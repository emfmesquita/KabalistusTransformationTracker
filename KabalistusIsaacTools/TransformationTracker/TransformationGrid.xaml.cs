using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.TransformationTracker.Model;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.TransformationTracker {
    /// <summary>
    /// Interaction logic for TransformationGrid.xaml
    /// </summary>
    public partial class TransformationGrid : UserControl {
        private readonly Transformation _model;

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

            CreateBindings();
        }

        private void CreateBindings() {
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
    }
}
