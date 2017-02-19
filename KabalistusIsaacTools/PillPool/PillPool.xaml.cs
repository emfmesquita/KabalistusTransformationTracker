using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusIsaacTools.PillPool.Model;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.PillPool {
    /// <summary>
    /// Interaction logic for PillPool.xaml
    /// </summary>
    public partial class PillPool : UserControl {

        public PillPool() {
            InitializeComponent();
            Model = new PillPollModel();
            CreateBindings();
        }

        public PillPollModel Model { get; }

        public void Update(Status status, IIsaacReader reader) {
            Dispatcher.Invoke(() => {
                if (!status.Ready) {
                    Reset();
                    return;
                }

                var pool = reader.GetPillPool();
                if (!pool.Any()) {
                    Reset();
                    return;
                }

                var pillKnowledge = reader.GetPillKnowledge();
                var toGoodPills = reader.HasItem(new Item(75)) || reader.HasItem(new Item(303));
                if (PillPanel.Children.Count != pool.Count) {
                    InitPills(pool, pillKnowledge, toGoodPills);
                } else {
                    UpdatePills(pool, pillKnowledge, toGoodPills);
                }

                var lastPillIndex = reader.IndexOfLastPillTaken();
                if (lastPillIndex == 0 || pool.Count < lastPillIndex) {
                    Model.LastPillVisibility = Visibility.Hidden;
                } else {
                    var lastPill = pool[lastPillIndex - 1];
                    SetPill(LastPill.Model, lastPill, lastPillIndex, true, toGoodPills);
                    Model.LastPillVisibility = Visibility.Visible;
                }
            });
        }

        private void CreateBindings() {
            LastPillPanel.SetBinding(VisibilityProperty, new Binding("LastPillVisibility") {
                Source = Model,
                Mode = BindingMode.OneWay
            });
        }

        private void Reset() {
            PillPanel.Children.Clear();
            Model.LastPillVisibility = Visibility.Hidden;
        }

        private void InitPills(IReadOnlyList<Item> poll, IReadOnlyList<bool> knowledge, bool toGoodPills) {
            PillPanel.Children.Clear();
            for (var i = 0; i < poll.Count; i++) {
                var pillRow = new PillRow();
                PillPanel.Children.Add(pillRow);
                var pill = poll[i];
                SetPill(pillRow.Model, pill, i + 1, knowledge[i], toGoodPills);
            }
        }

        private void UpdatePills(IReadOnlyList<Item> poll, IReadOnlyList<bool> knowledge, bool toGoodPills) {
            for (var i = 0; i < poll.Count; i++) {
                var pillRow = PillPanel.Children[i] as PillRow;
                if (pillRow == null) continue;
                var pill = poll[i];
                SetPill(pillRow.Model, pill, i + 1, knowledge[i], toGoodPills);
            }
        }

        private static void SetPill(PillRowModel pillModel, Item pill, int pillPoolIndex, bool known, bool toGoodPills) {
            var label = pill.I18N;
            if (pill.Id == 1) {
                label = toGoodPills ? "Balls of Steel" : "Bad Trip/Full Health";
            } else if (pill.Id == 5) {
                label = toGoodPills ? "Full Health" : "Bad Trip/Full Health";
            } else if (pill.Id == 6 || pill.Id == 7) {
                label = toGoodPills ? "Health Up" : "Health Down/Health Up";
            } else if (pill.Id == 11 && toGoodPills) {
                label = "Range Up";
            } else if (pill.Id == 13 && toGoodPills) {
                label = "Speed Up";
            } else if (pill.Id == 15 && toGoodPills) {
                label = "Tears Up";
            } else if (pill.Id == 17 && toGoodPills) {
                label = "Luck Up";
            } else if (pill.Id == 22 && toGoodPills) {
                label = "Pheromones";
            } else if (pill.Id == 25 && toGoodPills) {
                label = "I can see forever!";
            } else if (pill.Id == 27 && toGoodPills) {
                label = "Power Pill!";
            } else if (pill.Id == 29 && toGoodPills) {
                label = "Percs!";
            } else if (pill.Id == 31 && toGoodPills) {
                label = "Telepills";
            } else if (pill.Id == 37 && toGoodPills) {
                label = "I can see forever!";
            } else if (pill.Id == 39 && toGoodPills) {
                label = "Something's wrong...";
            }
            pillModel.Label = known ? label : "-";
            pillModel.PillImageResource = ResourcesUtil.PillResource(pillPoolIndex);
        }
    }
}
