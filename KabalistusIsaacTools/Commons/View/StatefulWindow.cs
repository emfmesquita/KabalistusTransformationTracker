using System;
using System.Windows;
using KabalistusCommons.Utils;
using KabalistusIsaacTools.Serializer;

namespace KabalistusIsaacTools.Commons.View {
    public class StatefulWindow : Window {

        public static bool IsPerformingActivatingCalc = false;

        public WindowSettings Settings { get; set; }

        protected void LoadSettings() {
            if (Settings.Width != null) {
                Width = (double)Settings.Width;
            }
            if (Settings.Height != null) {
                Height = (double)Settings.Height;
            }
            if (Settings.X != null) {
                Left = (double)Settings.X;
            }
            if (Settings.Y != null) {
                Top = (double)Settings.Y;
            }
        }

        protected void WindowSizeChanged(object sender, SizeChangedEventArgs e) {
            _windowsSizeDebouncer.Tick(new StatefulWindowEvent() {
                Settings = Settings,
                Size = e.NewSize
            });
        }

        protected void WindowLocationChanged(object sender, EventArgs e) {
            _windowsLocationDebouncer.Tick(new StatefulWindowEvent() {
                Settings = Settings,
                Location = new Point(Left, Top)
            });
        }

        protected void WindowActivated(object sender, EventArgs e) {
            if (IsPerformingActivatingCalc) {
                return;
            }
            IsPerformingActivatingCalc = true;
            foreach (Window window in Application.Current.Windows) {
                window.Topmost = true;
                window.Topmost = false;
            }
            IsPerformingActivatingCalc = false;
        }

        private readonly Debouncer<StatefulWindowEvent> _windowsLocationDebouncer = new Debouncer<StatefulWindowEvent>(300,
            e => {
                if (e.Settings == null) {
                    return;
                }
                e.Settings.X = (int)e.Location.X;
                e.Settings.Y = (int)e.Location.Y;
                KabalistusToolsSerializer.Save();
            });

        private readonly Debouncer<StatefulWindowEvent> _windowsSizeDebouncer = new Debouncer<StatefulWindowEvent>(300,
            e => {
                if (e.Settings == null) {
                    return;
                }

                e.Settings.Width = (int)e.Size.Width;
                e.Settings.Height = (int)e.Size.Height;
                KabalistusToolsSerializer.Save();
            });

        private class StatefulWindowEvent {
            public WindowSettings Settings { get; set; }
            public Point Location { get; set; }
            public Size Size { get; set; }
        }
    }
}
