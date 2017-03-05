using System.Xml.Serialization;
using KabalistusIsaacTools.SoundFun.Player;

namespace KabalistusIsaacTools.Serializer {

    public class SoundFunSettings {
        [XmlArray("entities")]
        [XmlArrayItem("entity")]
        public SoundFunEntity[] Entities { get; set; }
    }
}
