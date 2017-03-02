using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace KabalistusIsaacTools.Commons.View {
    /// <summary>
    /// Interaction logic for ToolTab.xaml
    /// </summary>
    public partial class ToolTab : TabItem {
        public ToolTabModel Model { get; }
        public TabControl Container { get; }
        public UIElement TabContent { get; }

        public ToolTab() {
            InitializeComponent();
            ToExternalButton.Click += ToExtraWindow;
            //RenderOptions.SetBitmapScalingMode(Icon, BitmapScalingMode.NearestNeighbor);
        }

        public ToolTab(ToolTabModel model, UIElement tabContent, TabControl container) : this() {
            Model = model;
            TabContent = tabContent;
            Container = container;
            CreateBindings();
            MainGrid.Children.Add(tabContent);
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
            Container.Items.Remove(this);
            MainGrid.Children.Remove(TabContent);

            var extraWindowModel = new ExtraWindowModel(Model.IconResource, Model.Label);
            var extraWindow = new ExtraWindow(extraWindowModel, () => {
                MainGrid.Children.Add(TabContent);
                Container.Items.Add(this);
                Focus();
            });
            extraWindow.Show(TabContent);
            extraWindow.Focus();
        }
    }
}
