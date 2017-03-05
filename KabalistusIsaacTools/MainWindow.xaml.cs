using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusCommons.View;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.Serializer;
using KabalistusIsaacTools.Utils;
using Xceed.Wpf.Toolkit;
using static KabalistusIsaacTools.Utils.ResourcesUtil;

namespace KabalistusIsaacTools {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : StatefulWindow {
        public static bool IsShuttingDown;

        private readonly StatusBarModel _statusBarModel = new StatusBarModel();
        private readonly SettingsModel _settingsModel = new SettingsModel();

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
            CreateTab("tt", UnmoddedItemResource(145), "Transformation Tracker", TransformationTrackerElement, generalSettings.TransformationTracker);

            PillPoolElement = new PillPool.PillPool();
            CreateTab("pill", PillResource(1), "Pills", PillPoolElement, generalSettings.Pills);

            VoidedItemsElement = new VoidedItems.VoidedItems();
            CreateTab("void", UnmoddedItemResource(477), "Voided Items", VoidedItemsElement, generalSettings.VoidedItems);

            SmeltedTrinketsElement = new SmeltedTrinkets.SmeltedTrinkets();
            CreateTab("smelt", UnmoddedItemResource(479), "Smelted Trinkets", SmeltedTrinketsElement, generalSettings.SmeltedTrinkets);

            SoundFunElement = new SoundFun.SoundFun();
            CreateTab("sound", UnmoddedItemResource(4), "Sound Fun", SoundFunElement, generalSettings.SoundFun);

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

            // settings
            BackgroundColorPicker.SetBinding(ColorPicker.SelectedColorProperty, new Binding("BackgroundColor") {
                Source = _settingsModel,
                Mode = BindingMode.TwoWay
            });
            ForegroundColorPicker.SetBinding(ColorPicker.SelectedColorProperty, new Binding("ForegroundColor") {
                Source = _settingsModel,
                Mode = BindingMode.TwoWay
            });
        }

        private void OnKeyDown(object sender, KeyEventArgs e) {
            CreationMode.KeyPressed(e.Key);
        }

        private void CreateTab(string id, string iconREsource, string label, UIElement content, TabSettings settings) {
            var tabModel = new ToolTabModel(iconREsource, label);
            var tab = new ToolTab(id, tabModel, content, Tabs, settings);
            Tabs.Items.Add(tab);
            if (settings.IsWindowed == true) {
                tab.ToExtraWindow();
            } else {
                var focusedTab = KabalistusToolsSerializer.Settings.GeneralSettings.TabWithFocus;
                if (!string.IsNullOrEmpty(focusedTab) && id.Equals(focusedTab)) {
                    tab.Focus();
                }
            }
        }

        private void MainWindowClosed(object sender, System.EventArgs e) {
            IsShuttingDown = true;
            Application.Current.Shutdown();
        }
    }
}
