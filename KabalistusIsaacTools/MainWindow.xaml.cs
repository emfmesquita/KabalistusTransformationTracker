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
            CreateTab(UnmoddedItemResource(145), "Transformation Tracker", TransformationTrackerElement);

            PillPoolElement = new PillPool.PillPool();
            CreateTab(PillResource(1), "Pills", PillPoolElement);

            VoidedItemsElement = new VoidedItems.VoidedItems();
            CreateTab(UnmoddedItemResource(477), "Voided Items", VoidedItemsElement);

            SmeltedTrinketsElement = new SmeltedTrinkets.SmeltedTrinkets();
            CreateTab(UnmoddedItemResource(479), "Smelted Trinkets", SmeltedTrinketsElement);

            SoundFunElement = new SoundFun.SoundFun();
            CreateTab(UnmoddedItemResource(4), "Sound Fun", SoundFunElement);

            CreateBindings();

            var reader = new AfterbirthPlusIsaacReader();
            MemoryReader.Init((status) => {
                _statusBarModel.Status = status.Message;
                ModdedHelper.Update(status, reader);
                SmeltedTrinketsElement.Update(status, reader);
                SoundFunElement.Update(status, reader);
                TransformationTrackerElement.Update(status);
                PillPoolElement.Update(status, reader);
                VoidedItemsElement.Update(status, reader);
            }, 1000);
        }

        public TransformationTracker.TransformationTracker TransformationTrackerElement { get; }
        public SmeltedTrinkets.SmeltedTrinkets SmeltedTrinketsElement { get; }
        public SoundFun.SoundFun SoundFunElement { get; }
        public PillPool.PillPool PillPoolElement { get; }
        public VoidedItems.VoidedItems VoidedItemsElement { get; }

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

        private void MainWindowClosed(object sender, System.EventArgs e) {
            Application.Current.Shutdown();
        }
    }
}
