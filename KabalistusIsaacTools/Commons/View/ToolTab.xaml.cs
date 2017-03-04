﻿using System.Windows;
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

        public ToolTab() {
            InitializeComponent();
            ToExternalButton.Click += ToExtraWindow;
            //RenderOptions.SetBitmapScalingMode(Icon, BitmapScalingMode.NearestNeighbor);
        }

        public ToolTab(ToolTabModel model, UIElement tabContent, TabControl container, TabSettings settings) : this() {
            Model = model;
            TabContent = tabContent;
            Container = container;
            CreateBindings();
            MainGrid.Children.Add(tabContent);
            Settings = settings;
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
            KabalistusToolsSerializer.Save();
            ToExtraWindow();
        }

        public void ToExtraWindow() {
            Container.Items.Remove(this);
            MainGrid.Children.Remove(TabContent);

            var extraWindowModel = new ExtraWindowModel(Model.IconResource, Model.Label);
            Settings.WindowSettings = Settings.WindowSettings ?? new WindowSettings();
            var extraWindow = new ExtraWindow(extraWindowModel, Settings.WindowSettings, () => {
                if (MainWindow.IsShuttingDown) {
                    return;
                }
                MainGrid.Children.Add(TabContent);
                Container.Items.Add(this);
                Focus();
                Settings.IsWindowed = false;
                KabalistusToolsSerializer.Save();
            });
            extraWindow.Show(TabContent);
            extraWindow.Focus();
        }
    }
}
