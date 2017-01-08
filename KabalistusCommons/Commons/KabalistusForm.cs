using System.Threading.Tasks;
using System.Windows.Forms;
using KabalistusCommons.Utils;

namespace KabalistusCommons.Commons {

    public abstract class KabalistusForm : Form {

        protected abstract ToolStripStatusLabel GetStatusLabel();

        public void SetStatusAsync(Status status) {
            new Task(() => {
                Invoke((MethodInvoker)(() => {
                    GetStatusLabel().Text = status.Message;
                }));

            }).Start();
        }
    }
}
