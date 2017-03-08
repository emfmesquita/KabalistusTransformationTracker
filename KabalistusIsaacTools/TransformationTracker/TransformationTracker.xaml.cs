using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using KabalistusCommons.Utils;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.Serializer;
using KabalistusIsaacTools.TransformationTracker.Providers;
using KabalistusIsaacTools.Utils;
using Color = System.Windows.Media.Color;

namespace KabalistusIsaacTools.TransformationTracker {
    /// <summary>
    /// Interaction logic for TransformationTracker.xaml
    /// </summary>
    public partial class TransformationTracker : UserControl {
        public static CenteredImageModel BlockIconImageModel;
        private const string BlockImageResource = "KabalistusIsaacTools.Images.block.png";

        private bool _started;

        public TransformationTracker() {
            InitializeComponent();
            var blockColor = KabalistusToolsSerializer.Settings.TransformationTrackerSettings.BlacklistedIconColor;
            BlockIconImageModel = new CenteredImageModel(BlockImage(blockColor));
        }

        public void Update(Status status) {
            if (MainWindow.IsShuttingDown) return;
            Dispatcher.Invoke(() => {
                if (!status.Ready) {
                    _started = false;
                    MainPanel.Children.Clear();
                    return;
                }

                if (!_started) {
                    _started = true;
                    TransformationInfoProvider.GetAllTransformations().ToList().ForEach(pair => {
                        var transformation = pair.Value;
                        if (CreationMode.On) {
                            CreationMode.Transformations.Add(transformation);
                        }
                        MainPanel.Children.Add(new TransformationGrid(transformation));
                    });
                }

                if (!CreationMode.On) {
                    TransformationInfoProvider.UpdateTransformations();
                }
            });
        }

        public static void UpdateBlockImageColor(Color color) {
            if (BlockIconImageModel != null) {
                BlockIconImageModel.Image = BlockImage(color);
            }
        }

        private static BitmapImage BlockImage(Color color) {
            var bitmapImage = ImageUtils.GetImage(BlockImageResource);
            var image = ImageUtils.ToBitmap(bitmapImage);
            image = ImageUtils.WhiteToColor(image, System.Drawing.Color.FromArgb(color.R, color.G, color.B));
            return ImageUtils.ToBitmapImage(new Bitmap(image));
        }
    }
}
