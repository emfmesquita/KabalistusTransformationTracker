using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KabalistusCommons.Utils;

namespace KabalistusCommons.View {
    public class FormUtils {
        public static string BuiltTitle(string projetTitle, object mainForm) {
            var sb = new StringBuilder();
            sb.Append(projetTitle).Append(" v");
            var assembly = mainForm.GetType().Assembly;
            var version = assembly.GetName().Version;
            sb.Append(version.Major).Append(".").Append(version.Minor).Append(".").Append(version.Build);
            return sb.ToString();
        }

        public static void SetStatusAsync(Status status, ToolStripStatusLabel statusLabel, Form mainForm) {
            new Task(() => {
                mainForm.Invoke((MethodInvoker)(() => {
                    statusLabel.Text = status.Message;
                }));
            }).Start();
        }
    }
}
