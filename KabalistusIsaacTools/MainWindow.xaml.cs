using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusCommons.View;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.Serializer;
using KabalistusIsaacTools.Utils;
using static KabalistusIsaacTools.Utils.ResourcesUtil;

namespace KabalistusIsaacTools {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : StatefulWindow {
        public static bool IsShuttingDown = false;

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

            var generalSettings = KabalistusToolsSerializer.Settings.GeneralSettings;
            Settings = generalSettings.MainWindow;

            InitializeComponent();

            Title = FormUtils.BuiltTitle("Kabalistus Isaac Tools", this);

            LoadSettings();

            TransformationTrackerElement = new TransformationTracker.TransformationTracker();
            generalSettings.TransformationTracker = generalSettings.TransformationTracker ?? TabSettings.Default();
            CreateTab(UnmoddedItemResource(145), "Transformation Tracker", TransformationTrackerElement, generalSettings.TransformationTracker);

            PillPoolElement = new PillPool.PillPool();
            generalSettings.Pills = generalSettings.Pills ?? TabSettings.Default();
            CreateTab(PillResource(1), "Pills", PillPoolElement, generalSettings.Pills);

            VoidedItemsElement = new VoidedItems.VoidedItems();
            generalSettings.VoidedItems = generalSettings.VoidedItems ?? TabSettings.Default();
            CreateTab(UnmoddedItemResource(477), "Voided Items", VoidedItemsElement, generalSettings.VoidedItems);

            SmeltedTrinketsElement = new SmeltedTrinkets.SmeltedTrinkets();
            generalSettings.SmeltedTrinkets = generalSettings.SmeltedTrinkets ?? TabSettings.Default();
            CreateTab(UnmoddedItemResource(479), "Smelted Trinkets", SmeltedTrinketsElement, generalSettings.SmeltedTrinkets);

            SoundFunElement = new SoundFun.SoundFun();
            generalSettings.SoundFun = generalSettings.SoundFun ?? TabSettings.Default();
            CreateTab(UnmoddedItemResource(4), "Sound Fun", SoundFunElement, generalSettings.SoundFun);

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

        private void CreateTab(string iconREsource, string label, UIElement content, TabSettings settings) {
            var tabModel = new ToolTabModel(iconREsource, label);
            var tab = new ToolTab(tabModel, content, Tabs, settings);
            Tabs.Items.Add(tab);
            if (settings.IsWindowed) {
                tab.ToExtraWindow();
            }
        }

        private void MainWindowClosed(object sender, System.EventArgs e) {
            IsShuttingDown = true;
            Application.Current.Shutdown();
        }
    }
}
