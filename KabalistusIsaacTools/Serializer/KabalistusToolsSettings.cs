using System.Xml.Serialization;

namespace KabalistusIsaacTools.Serializer {

    public class KabalistusToolsSettings {
        public GeneralSettings GeneralSettings { get; set; } = new GeneralSettings();
        public TransformationTrackerSettings TransformationTrackerSettings { get; set; } = new TransformationTrackerSettings();
        public SoundFunSettings SoundFunSettings { get; set; } = new SoundFunSettings();
    }
}
