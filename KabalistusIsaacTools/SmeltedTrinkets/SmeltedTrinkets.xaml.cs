using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.SmeltedTrinkets {
    /// <summary>
    /// Interaction logic for SmeltedTrinkets.xaml
    /// </summary>
    public partial class SmeltedTrinkets : UserControl {
        private readonly List<Item> _addedTrinkets = new List<Item>();

        private static readonly Dictionary<string, string> WikiDictionary = new Dictionary<string, string>(){
            { "Cancer", "Cancer_(Trinket)"},
            { "Tick", "The_Tick"},
            { "???'s Soul", "%3F%3F%3F%27s_Soul"},
            { "Rib of Greed", "Rib_Of_Greed"},
            { "Locust of Wrath", "Locust_Of_Wrath"},
            { "Locust of Pestilence", "Locust_Of_Pestilence"},
            { "Locust of Famine", "Locust_Of_Famine"},
            { "Locust of Death", "Locust_Of_Death"},
            { "Locust of Conquest", "Locust_Of_Conquest"}
        };

        public SmeltedTrinkets() {
            InitializeComponent();
        }

        public void Update(Status status, IIsaacReader reader) {
            if (MainWindow.IsShuttingDown) return;
            Dispatcher.Invoke(() => {
                if (!status.Ready) {
                    Clear();
                    return;
                }

                var abpReader = reader as AfterbirthPlusIsaacReader;
                if (abpReader == null) {
                    Clear();
                    return;

                }

                List<Item> trinkets;
                if (CreationMode.On) {
                    trinkets = Trinkets.AllTrinkets.Select(pair => pair.Value).ToList();
                    trinkets.Sort((a, b) => string.CompareOrdinal(a.I18N, b.I18N));
                } else {
                    trinkets = abpReader.GetSmeltedTrinkets();
                }

                if (_addedTrinkets.Count > trinkets.Count) {
                    Clear();
                }

                var toAdd = new Dictionary<int, Item>();
                for (var i = 0; i < trinkets.Count; i++) {
                    var trinket = trinkets[i];
                    if (!_addedTrinkets.Contains(trinket)) {
                        toAdd.Add(i, trinket);
                    }
                }

                toAdd.ToList().ForEach(pair => {
                    var trinket = pair.Value;
                    var resource = ResourcesUtil.TrinketResource(trinket);
                    var imageModel = new GeneralImageModel(resource, trinket.I18N, 0, 0, 2, Visibility.Visible, Cursors.Hand, 64, 64);
                    var trinketImage = new GeneralImage(imageModel, BitmapScalingMode.NearestNeighbor, MouseLeftButtonDownOnTrinketImage);
                    MainPanel.Children.Insert(pair.Key, trinketImage);
                    _addedTrinkets.Add(pair.Value);
                });
            });
        }

        public void ChangeTrinketImageSize(int newSize) {
            foreach (var child in MainPanel.Children) {
                var trinketImage = child as GeneralImage;
                trinketImage?.SetImageSize(newSize, newSize);
            }
        }

        private static void MouseLeftButtonDownOnTrinketImage(object sender, MouseButtonEventArgs e) {
            var image = sender as Image;
            var tooltip = image?.ToolTip as ToolTip;
            var i18N = tooltip?.Content as string;
            if (string.IsNullOrEmpty(i18N)) {
                return;
            }
            if (WikiDictionary.ContainsKey(i18N)) {
                i18N = WikiDictionary[i18N];
            }
            var url = $"https://bindingofisaacrebirth.gamepedia.com/{i18N.Replace(" ", "_")}";
            System.Diagnostics.Process.Start(url);
        }

        private void Clear() {
            _addedTrinkets.Clear();
            MainPanel.Children.Clear();
        }
    }
}
