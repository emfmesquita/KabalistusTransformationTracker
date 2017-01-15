using System.Xml.Serialization;
using KabalistusCommons.Isaac;

namespace IsaacSoundFun.Player {
    public class SoundFunEntity {
        [XmlElement(ElementName = "item")]
        public Item Item { get; set; }

        [XmlElement(ElementName = "file")]
        public string SoundFile { get; set; }
    }
}
