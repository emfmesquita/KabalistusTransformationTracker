using System.Drawing;
using System.Windows.Forms;
using IsaacFun.Player;

namespace IsaacFun.View {
    public sealed class SoundFunButton : Button {
        public SoundFunButton(SoundFunEntity entity, string title, string icon) {
            Entity = entity;
            var toolTip = new ToolTip();
            toolTip.SetToolTip(this, title);
            Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(icon);
            Dock = DockStyle.Top;
        }

        public SoundFunEntity Entity { get; private set; }
    }
}
