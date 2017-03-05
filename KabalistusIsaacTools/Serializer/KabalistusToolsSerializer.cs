using System.IO;
using System.Timers;
using System.Xml.Serialization;

namespace KabalistusIsaacTools.Serializer {
    public class KabalistusToolsSerializer {
        private const string SaveFileName = "Settings.xml";
        private const double Interval = 100;

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(KabalistusToolsSettings));
        private static readonly Timer Timer = new Timer(Interval) { AutoReset = false };

        public static KabalistusToolsSettings Settings;

        static KabalistusToolsSerializer() {
            Timer.Elapsed += (sender, args) => {
                Save();
            };
        }

        public static void MarkToSave() {
            if (Timer.Enabled) {
                Timer.Stop();
            }
            Timer.Start();
        }

        public static void Load() {
            if (!File.Exists(SaveFileName)) {
                Settings = new KabalistusToolsSettings();
                Save();
                return;
            }

            var reader = new StreamReader(SaveFileName);
            Settings = Serializer.Deserialize(reader) as KabalistusToolsSettings;
            reader.Close();
        }

        private static void Save() {
            if (!File.Exists(SaveFileName)) {
                var fs = File.Create(SaveFileName);
                fs.Close();
            }

            var writer = new StreamWriter(SaveFileName);
            Serializer.Serialize(writer, Settings);
            writer.Close();
        }
    }
}
