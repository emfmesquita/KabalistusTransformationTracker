using System.Windows.Media;

namespace KabalistusIsaacTools.Serializer {
    public class GeneralSettings {
        public Color BackgroundColor { get; set; } = Colors.White;
        public Color TextColor { get; set; } = Colors.Black;
        public string TabWithFocus { get; set; }
        public WindowSettings MainWindow { get; set; } = new WindowSettings();
        public TabSettings TransformationTracker { get; set; } = new TabSettings();
        public TabSettings Pills { get; set; } = new TabSettings();
        public TabSettings SmeltedTrinkets { get; set; } = new TabSettings();
        public TabSettings VoidedItems { get; set; } = new TabSettings();
        public TabSettings SoundFun { get; set; } = new TabSettings();
    }
}
