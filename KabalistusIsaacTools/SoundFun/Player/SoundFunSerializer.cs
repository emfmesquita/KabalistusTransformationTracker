using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace KabalistusIsaacTools.SoundFun.Player {
    public class SoundFunSerializer {
        private const string SaveFileName = "IsaacSoundFunSave.xml";
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(SoundFunSave));

        public static void Save() {
            var entities = SoundFunPlayer.Entities.Values.ToList();
            entities.Sort((entA, entB) => string.Compare(entA.Item.I18N, entB.Item.I18N, StringComparison.Ordinal));
            var save = new SoundFunSave { Entities = entities.ToArray() };

            if (!File.Exists(SaveFileName)) {
                var fs = File.Create(SaveFileName);
                fs.Close();
            }

            var writer = new StreamWriter(SaveFileName);
            Serializer.Serialize(writer, save);
            writer.Close();
        }

        public static SoundFunSave Load() {
            if (!File.Exists(SaveFileName)) {
                return null;
            }

            var reader = new StreamReader(SaveFileName);
            var save = Serializer.Deserialize(reader) as SoundFunSave;
            reader.Close();
            return save;
        }

        [XmlRoot("soundfun")]
        public class SoundFunSave {
            [XmlArray("entities")]
            [XmlArrayItem("entity")]
            public SoundFunEntity[] Entities { get; set; }
        }
    }
}
