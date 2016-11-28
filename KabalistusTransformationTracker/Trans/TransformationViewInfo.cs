using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KabalistusTransformationTracker.Images;

namespace KabalistusTransformationTracker.Trans {
    public class TransformationViewInfo {

        public TransformationViewInfo(Transformation transformation, PictureBox pictureBox, Label label, Panel panel, ToolStripMenuItem menu) {
            var transformationImage = new BaseImage(transformation.Name, transformation.X, transformation.Y, transformation.Scale);
            InitTooltip(transformationImage, pictureBox, transformation.I18N);

            var itemImages = transformation.Items.Select(item => {
                var itemImage = new ItemImage(item.Name, item.X, item.Y, item.Scale, item.BlockReduction);
                InitTooltip(itemImage, pictureBox, item.I18N);
                return itemImage;
            }).ToList();

            Cluster = new ItemCluster(pictureBox, transformationImage, itemImages);

            Label = label;
            Panel = panel;
            Menu = menu;
        }

        public ItemCluster Cluster { get; }
        public Label Label { get; set; }
        public Panel Panel { get; set; }
        public ToolStripMenuItem Menu { get; set; }

        private void InitTooltip(BaseImage image, Control parentControl, string text) {
            image.Tooltip.SetToolTip(parentControl, text);
            image.Tooltip.Active = false;
            image.Tooltip.AutomaticDelay = 300;
        }
    }
}
