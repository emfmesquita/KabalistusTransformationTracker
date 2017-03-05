using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using KabalistusIsaacTools.Serializer;

namespace KabalistusIsaacTools.Commons.View {
    /// <summary>
    /// Interaction logic for ToolTab.xaml
    /// </summary>
    public partial class ToolTab : TabItem {
        public ToolTabModel Model { get; }
        public TabControl Container { get; }
        public UIElement TabContent { get; }
        public TabSettings Settings { get; }
        public string Id { get; }

        public ToolTab() {
            InitializeComponent();
            ToExternalButton.Click += ToExtraWindow;
        }

        public ToolTab(string id, ToolTabModel model, UIElement tabContent, TabControl container, TabSettings settings) : this() {
            Id = id;
            Model = model;
            TabContent = tabContent;
            Container = container;
            CreateBindings();
            MainGrid.Children.Add(tabContent);
            Settings = settings;

            GotFocus += (sender, args) => {
                KabalistusToolsSerializer.Settings.GeneralSettings.TabWithFocus = Id;
                KabalistusToolsSerializer.MarkToSave();
            };
        }

        private void CreateBindings() {
            Icon.SetBinding(Image.SourceProperty, new Binding("Icon") {
                Source = Model,
                Mode = BindingMode.OneWay
            });
            Label.SetBinding(ContentProperty, new Binding("Label") {
                Source = Model,
                Mode = BindingMode.OneWay
            });
        }

        private void ToExtraWindow(object sender, RoutedEventArgs e) {
            Settings.IsWindowed = true;
            KabalistusToolsSerializer.MarkToSave();
            ToExtraWindow();
        }

        public void ToExtraWindow() {
            Container.Items.Remove(this);
            MainGrid.Children.Remove(TabContent);

            var extraWindowModel = new ExtraWindowModel(Model.IconResource, Model.Label);
            var extraWindow = new ExtraWindow(extraWindowModel, Settings.WindowSettings, () => {
                if (MainWindow.IsShuttingDown) {
                    return;
                }
                MainGrid.Children.Add(TabContent);
                Container.Items.Add(this);
                Focus();
                Settings.IsWindowed = false;
                KabalistusToolsSerializer.MarkToSave();
            });
            extraWindow.Show(TabContent);
            extraWindow.Focus();
        }
    }
}
