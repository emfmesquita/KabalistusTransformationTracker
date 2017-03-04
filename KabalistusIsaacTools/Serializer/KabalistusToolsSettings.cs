using System.Xml.Serialization;

namespace KabalistusIsaacTools.Serializer {

    [XmlRoot("KabalistusToolsSettings")]
    public class KabalistusToolsSettings {

        public GeneralSettings GeneralSettings { get; set; }

        [XmlElement("SoundFunSettings")]
        public SoundFunSave SoundFunSave { get; set; }
    }
}
