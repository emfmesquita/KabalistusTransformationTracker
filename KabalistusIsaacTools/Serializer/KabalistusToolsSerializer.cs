using System.IO;
using System.Xml.Serialization;

namespace KabalistusIsaacTools.Serializer {
    public class KabalistusToolsSerializer {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(KabalistusToolsSettings));
        private const string SaveFileName = "Settings.xml";

        public static KabalistusToolsSettings Settings;

        static KabalistusToolsSerializer() {
            Load();
        }

        public static void Save() {
            if (!File.Exists(SaveFileName)) {
                var fs = File.Create(SaveFileName);
                fs.Close();
            }

            var writer = new StreamWriter(SaveFileName);
            Serializer.Serialize(writer, Settings);
            writer.Close();
        }

        public static void Load() {
            if (!File.Exists(SaveFileName)) {
                Settings = DefaultSettings();
                return;
            }

            var reader = new StreamReader(SaveFileName);
            Settings = Serializer.Deserialize(reader) as KabalistusToolsSettings;
            reader.Close();
        }


        private static KabalistusToolsSettings DefaultSettings() {
            return new KabalistusToolsSettings {
                GeneralSettings = new GeneralSettings() {
                    MainWindow = new WindowSettings() {
                        Width = 800,
                        Height = 600
                    }
                }
            };
        }

        private static TabSettings DefaultTabSettings() {
            return new TabSettings() { IsWindowed = false, WindowSettings = new WindowSettings() };
        }
    }
}
