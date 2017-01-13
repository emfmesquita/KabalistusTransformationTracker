using System.Drawing;
using System.Windows.Forms;

namespace KabalistusCommons.View {
    public sealed class VerticalCenteredControl : AbstractCenteredControl {

        private readonly Control _baseControl;

        public VerticalCenteredControl(Control baseControl) {
            _baseControl = baseControl;

            var panel = new Panel { Dock = DockStyle.Fill };
            panel.Controls.Add(baseControl);

            ColumnCount = 1;
            Dock = DockStyle.Fill;
            Location = new Point(0, 0);
            RowCount = 3;
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Controls.Add(panel, 0, 1);
        }

        public override Control GetBaseControl() {
            return _baseControl;
        }
    }
}
