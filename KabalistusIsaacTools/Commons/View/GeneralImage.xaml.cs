using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace KabalistusIsaacTools.Commons.View {
    /// <summary>
    /// Interaction logic for GeneralImage.xaml
    /// </summary>
    public partial class GeneralImage : UserControl {
        public GeneralImage(GeneralImageModel model, BitmapScalingMode scaling = BitmapScalingMode.NearestNeighbor, MouseButtonEventHandler clickHandler = null) {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(Image, scaling);
            Model = model;
            if (clickHandler != null) {
                Image.MouseLeftButtonDown += clickHandler;
            }
            CreateBindings();
        }
        public void SetImageSize(int height, int width) {
            Model.Height = height;
            Model.Width = width;
        }

        public GeneralImageModel Model { get; }

        private void CreateBindings() {
            Image.SetBinding(Image.SourceProperty, new Binding("Image") {
                Source = Model,
                Mode = BindingMode.OneWay
            });
            Image.SetBinding(MarginProperty, new Binding("FormattedMargin") {
                Source = Model,
                Mode = BindingMode.OneWay
            });

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
            ImageGrid.SetBinding(ToolTipProperty, new Binding("Tooltip") {
                Source = Model,
                Mode = BindingMode.OneWay
            });
            ImageGrid.SetBinding(CursorProperty, new Binding("Cursor") {
                Source = Model,
                Mode = BindingMode.OneWay
            });
        }
    }
}
