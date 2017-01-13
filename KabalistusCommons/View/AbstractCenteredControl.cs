using System.Windows.Forms;

namespace KabalistusCommons.View {
    public  abstract class AbstractCenteredControl : TableLayoutPanel {
        public abstract Control GetBaseControl();
    }
}
