using System;
using System.Drawing;
using System.Windows.Forms;
using IsaacFun.Player;
using KabalistusCommons.View;

namespace IsaacFun.View {
    public class ViewUtils {
        public static AbstractCenteredControl CreateLabel(string text, bool addBorder = false, bool onlyVerticalCentered = true) {
            var label = new Label {
                Text = text,
                TextAlign = onlyVerticalCentered ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            AbstractCenteredControl control;
            if (onlyVerticalCentered) {
                control = new VerticalCenteredControl(label);
            } else {
                control = new CenteredControl(label);
            }
            return control;
        }

        public static void AddTooltip(Control control, string tooltip) {
            var tooltipControl = new ToolTip();
            tooltipControl.SetToolTip(control, tooltip);
        }

        public static AbstractCenteredControl CreateActionButton(string tooltip, string icon, SoundFunEntity entity, EventHandler clickHandler) {
            var buton = new SoundFunButton(entity, tooltip, icon);
            buton.Click += clickHandler;
            var control = new VerticalCenteredControl(buton);
            return control;
        }
    }
}
