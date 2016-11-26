using System.Collections.Generic;
using System.Windows.Forms;
using KabalistusTransformationTracker.Images;

namespace KabalistusTransformationTracker.Trans {
    public class TransformationViewInfo {

        public TransformationViewInfo(Transformation transformation, PictureBox pictureBox, Label label, Panel panel, ToolStripMenuItem menu) {
            var transformationImage = new BaseImage(transformation.Name, transformation.X, transformation.Y, transformation.Scale);
            var itemImages = new List<ItemImage>();
            transformation.Items.ForEach(item => {
                itemImages.Add(new ItemImage(item.Name, item.X, item.Y, item.Scale));
            });
            Cluster = new ItemCluster(pictureBox, transformationImage, itemImages);
            Label = label;
            Panel = panel;
            Menu = menu;
        }

        public ItemCluster Cluster { get; }
        public Label Label { get; set; }
        public Panel Panel { get; set; }
        public ToolStripMenuItem Menu { get; set; }
    }
}
