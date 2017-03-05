using System.Windows.Media;

namespace KabalistusIsaacTools.Serializer {
    public class GeneralSettings {
        public GeneralSettings() {
            BackgroundColor = Colors.White;
            TextColor = Colors.Black;
        }

        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public string TabWithFocus { get; set; }
        public WindowSettings MainWindow { get; set; }
        public TabSettings TransformationTracker { get; set; }
        public TabSettings Pills { get; set; }
        public TabSettings SmeltedTrinkets { get; set; }
        public TabSettings VoidedItems { get; set; }
        public TabSettings SoundFun { get; set; }
    }
}
