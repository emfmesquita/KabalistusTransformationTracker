﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using KabalistusCommons.Isaac;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.SmeltedTrinkets {
    /// <summary>
    /// Interaction logic for SmeltedTrinkets.xaml
    /// </summary>
    public partial class SmeltedTrinkets : UserControl {
        private readonly List<Trinket> _addedTrinkets = new List<Trinket>();

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

        public void UpdateTrinkets(List<Trinket> trinkets) {
            if (CreationMode.On) {
                trinkets = Trinkets.AllTrinkets.Select(pair => pair.Value).ToList();
                trinkets.Sort((a, b) => string.CompareOrdinal(a.I18N, b.I18N));
            }

            Dispatcher.Invoke(() => {
                if (_addedTrinkets.Count > trinkets.Count) {
                    _addedTrinkets.Clear();
                    MainPanel.Children.Clear();
                }

                var toAdd = new Dictionary<int, Trinket>();
                for (var i = 0; i < trinkets.Count; i++) {
                    var trinket = trinkets[i];
                    if (!_addedTrinkets.Contains(trinket)) {
                        toAdd.Add(i, trinket);
                    }
                }

                toAdd.ToList().ForEach(pair => {
                    var trinket = pair.Value;
                    var resource = $"KabalistusIsaacTools.Images.Trinkets.t{trinket.Id}.png";
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
            var i18N = image?.ToolTip as string;
            if (string.IsNullOrEmpty(i18N)) {
                return;
            }
            if (WikiDictionary.ContainsKey(i18N)) {
                i18N = WikiDictionary[i18N];
            }
            var url = $"https://bindingofisaacrebirth.gamepedia.com/{i18N.Replace(" ", "_")}";
            System.Diagnostics.Process.Start(url);
        }
    }
}