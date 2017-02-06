using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusCommons.View;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.Utils;
using static KabalistusIsaacTools.Utils.ResourcesUtil;

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

            TransformationTrackerElement = new TransformationTracker.TransformationTracker();
            CreateTab(ItemResource(145), "Transformation Tracker", TransformationTrackerElement);

            SmeltedTrinketsElement = new SmeltedTrinkets.SmeltedTrinkets();
            CreateTab(ItemResource(479), "Smelted Trinkets", SmeltedTrinketsElement);

            SoundFunElement = new SoundFun.SoundFun();
            CreateTab(ItemResource(4), "Sound Fun", SoundFunElement);

            CreateBindings();

            var reader = new AfterbirthPlusIsaacReader();
            MemoryReader.Init((status) => {
                _statusBarModel.Status = status.Message;
                SmeltedTrinketsElement.UpdateTrinkets(reader.GetSmeltedTrinkets());
                SoundFunElement.Update(status, reader);
                TransformationTrackerElement.Update(status);
            }, 1000);
        }

        public TransformationTracker.TransformationTracker TransformationTrackerElement { get; }
        public SmeltedTrinkets.SmeltedTrinkets SmeltedTrinketsElement { get; }
        public SoundFun.SoundFun SoundFunElement { get; }

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

        private void CreateTab(string iconREsource, string label, UIElement content) {
            var tabModel = new ToolTabModel(iconREsource, label);
            var tab = new ToolTab(tabModel, content, Tabs);
            Tabs.Items.Add(tab);
        }
    }
}
