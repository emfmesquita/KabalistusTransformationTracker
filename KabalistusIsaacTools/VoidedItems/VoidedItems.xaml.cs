using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.VoidedItems {
    /// <summary>
    /// Interaction logic for VoidedItems.xaml
    /// </summary>
    public partial class VoidedItems : UserControl {
        public VoidedItems() {
            InitializeComponent();
        }

        public void Update(Status status, IIsaacReader reader) {
            if (MainWindow.IsShuttingDown) return;
            Dispatcher.Invoke(() => {
                if (!status.Ready) {
                    MainPanel.Children.Clear();
                    return;
                }

                var abpREader = reader as AfterbirthPlusIsaacReader;
                if (abpREader == null) {
                    MainPanel.Children.Clear();
                    return;
                }

                var voidedItems = abpREader.GetVoidedItems();

                if (MainPanel.Children.Count > voidedItems.Count) {
                    MainPanel.Children.Clear();
                }

                for (var i = MainPanel.Children.Count; i < voidedItems.Count; i++) {
                    var voidedItem = voidedItems[i];
                    GeneralImageModel imageModel;
                    if (voidedItem == null) {
                        imageModel = new GeneralImageModel(null, "Unknown", 0, 0, 2, Visibility.Visible, Cursors.Arrow, 64, 64);
                    } else {
                        imageModel = new GeneralImageModel(ResourcesUtil.ItemResource(voidedItem), voidedItem.I18N, 0, 0, 2, Visibility.Visible, Cursors.Arrow, 64, 64);
                    }
                    MainPanel.Children.Add(new GeneralImage(imageModel));
                }
            });
        }
    }
}
