using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusCommons.View;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.TransformationTracker.Model;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly StatusBarModel _statusBarModel = new StatusBarModel();

        public MainWindow() {
            //AfterbirthPlusTransformations.AllTransformations.ToList().ForEach(pair => {
            //    var trans = pair.Value;
            //    Console.WriteLine(trans.I18N);
            //    trans.Items.ForEach(item => {
            //        var scale = item.Scale.ToString("N", new CultureInfo("en-US"));
            //        var str = $"                new TransformationItem(\"{item.Tooltip}\", {item.Id}, {item.X - 32}, {item.Y - 32}, {scale}F),";
            //        Console.WriteLine(str);
            //    });
            //});

            InitializeComponent();

            Title = FormUtils.BuiltTitle("Kabalistus Isaac Tools", this);

            CreateBindings();

            var reader = new AfterbirthPlusIsaacReader();
            MemoryReader.Init((status) => {
                _statusBarModel.Status = status.Message;
                SmeltedTrinkets.UpdateTrinkets(reader.GetSmeltedTrinkets());
                SoundFun.Update(status, reader);
                TransformationTracker.Update(status);
            }, 1000);
        }

        private void CreateBindings() {
            // status Bar
            StatusLabel.SetBinding(ContentProperty, new Binding("Status") {
                Source = _statusBarModel,
                Mode = BindingMode.OneWay
            });
        }

        private void OnKeyDown(object sender, KeyEventArgs e) {
            CreationMode.KeyPressed(e.Key);
        }

        private void ExternalTransformationTracker(object sender, RoutedEventArgs e) {
            Tabs.Items.Remove(TransformationTrackerTab);
            TransformationTrackerTabGrid.Children.Remove(TransformationTracker);

            var transformationTrackerExtraWindowModel = new ExtraWindowModel("KabalistusIsaacTools.Images.Items.c145.png", "Transformation Tracker");
            var transformationTrackerExtraWindow = new ExtraWindow(transformationTrackerExtraWindowModel, () => {
                TransformationTrackerTabGrid.Children.Add(TransformationTracker);
                Tabs.Items.Insert(0, TransformationTrackerTab);
            });
            transformationTrackerExtraWindow.Show(TransformationTracker);
            TransformationTrackerTab.Focus();
        }
    }
}
