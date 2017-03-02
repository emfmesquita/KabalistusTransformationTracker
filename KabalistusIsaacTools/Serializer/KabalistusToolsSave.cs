using System.Xml.Serialization;

namespace KabalistusIsaacTools.Serializer {

    [XmlRoot("tools")]
    public class KabalistusToolsSave {

        [XmlElement("soundfun")]
        public SoundFunSave SoundFunSave { get; set; }
    }
}
