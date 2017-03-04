using System.Drawing;

namespace KabalistusIsaacTools.Serializer {
    public class GeneralSettings {
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public WindowSettings MainWindow { get; set; }
        public TabSettings TransformationTracker { get; set; }
        public TabSettings Pills { get; set; }
        public TabSettings SmeltedTrinkets { get; set; }
        public TabSettings VoidedItems { get; set; }
        public TabSettings SoundFun { get; set; }
    }
}
