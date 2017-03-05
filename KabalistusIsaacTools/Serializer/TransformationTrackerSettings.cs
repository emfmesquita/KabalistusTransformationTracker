using System.Windows.Media;

namespace KabalistusIsaacTools.Serializer {
    public class TransformationTrackerSettings {
        public bool? ShowTransformationImage { get; set; } = true;
        public bool? CoopTransformationImageMode { get; set; } = false;
        public bool? ShowBlacklistedIcon { get; set; } = true;
        public Color BlacklistedIconColor { get; set; } = Color.FromRgb(143, 25, 139);
    }
}
