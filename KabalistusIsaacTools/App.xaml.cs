using System.Windows;
using KabalistusIsaacTools.Serializer;

namespace KabalistusIsaacTools {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            KabalistusToolsSerializer.Load();
        }
    }
}
