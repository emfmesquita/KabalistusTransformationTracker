using System.Xml.Serialization;
using KabalistusIsaacTools.SoundFun.Player;

namespace KabalistusIsaacTools.Serializer {

    public class SoundFunSave {
        [XmlArray("entities")]
        [XmlArrayItem("entity")]
        public SoundFunEntity[] Entities { get; set; }
    }
}
