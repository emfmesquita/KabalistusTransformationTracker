using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.TransformationTracker.Model;

namespace KabalistusIsaacTools.TransformationTracker {
    /// <summary>
    /// Interaction logic for TransformationItemControl.xaml
    /// </summary>
    public partial class TransformationItemControl : UserControl {
        public TransformationItemControl(TransformationItem model) {
            InitializeComponent();
            Model = model;
            ItemImage = new GeneralImage(Model.ItemImageModel);
            ImageGrid.Children.Add(ItemImage);

            if (Model.BlockImageModel != null) {
                BlockImage = new GeneralImage(Model.BlockImageModel, TransformationTracker.BlockIconImageModel, BitmapScalingMode.Fant);
                ImageGrid.Children.Add(BlockImage);
            }
            CreateBindings();
        }

        public TransformationItem Model { get; }
        private GeneralImage ItemImage { get; }
        private GeneralImage BlockImage { get; }

        private void CreateBindings(){
            ImageGrid.SetBinding(HeightProperty, new Binding("Height") {
                Source = Model,
                Mode = BindingMode.OneWay
            });
            ImageGrid.SetBinding(WidthProperty, new Binding("Width") {
                Source = Model,
                Mode = BindingMode.OneWay
            });
            ImageGrid.SetBinding(VisibilityProperty, new Binding("Visibility") {
                Source = Model,
                Mode = BindingMode.OneWay
            });
            ImageGrid.SetBinding(RenderTransformProperty, new Binding("Translate") {
                Source = Model,
                Mode = BindingMode.OneWay
            });
        }
    }
}
