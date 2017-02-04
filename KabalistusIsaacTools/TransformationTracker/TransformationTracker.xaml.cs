using System.Linq;
using System.Windows.Controls;
using KabalistusCommons.Utils;
using KabalistusIsaacTools.TransformationTracker.Providers;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.TransformationTracker {
    /// <summary>
    /// Interaction logic for TransformationTracker.xaml
    /// </summary>
    public partial class TransformationTracker : UserControl {
        private bool _started;

        public TransformationTracker() {
            InitializeComponent();
        }

        public void Update(Status status) {
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
    }
}
