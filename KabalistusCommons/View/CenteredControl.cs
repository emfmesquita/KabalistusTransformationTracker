using System.Drawing;
using System.Windows.Forms;

namespace KabalistusCommons.View {
    public sealed class CenteredControl : AbstractCenteredControl {

        private readonly Control _baseControl;

        public CenteredControl(Control baseControl) {
            _baseControl = baseControl;
            var panel = new Panel { Dock = DockStyle.Fill };
            panel.Controls.Add(baseControl);

            ColumnCount = 3;
            Dock = DockStyle.Fill;
            Location = new Point(0, 0);
            RowCount = 3;

            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Controls.Add(panel, 1, 1);
        }

        public override Control GetBaseControl() {
            return _baseControl;
        }
    }
}
