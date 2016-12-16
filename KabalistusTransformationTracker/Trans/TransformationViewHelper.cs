using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using KabalistusTransformationTracker.Images;
using KabalistusTransformationTracker.Utils;

namespace KabalistusTransformationTracker.Trans {
    public class TransformationViewHelper {
        private static readonly Dictionary<string, ItemCluster> Clusters = new Dictionary<string, ItemCluster>();
        private static readonly Dictionary<string, TransformationInfo> TransformationInfos = new Dictionary<string, TransformationInfo>();

        public static void Add(Transformation transformation, ItemCluster cluster) {
            Clusters.Add(transformation.Name, cluster);
        }

        public static void SetInitialValuesFromConfig() {
            SetTextColor(Properties.Settings.Default.TextColor);

            Clusters.ToList().ForEach(pair => {
                var configValue = (bool)Properties.Settings.Default["show" + pair.Key];
                pair.Value.Menu.Checked = configValue;
                pair.Value.Panel.Visible = configValue;
            });
        }

        public static void UpdateTransformationsInfo(bool showTransformationImage) {
            var updatedTransformationInfo = TransformationInfoProvider.GetTransformationsInfo();
            Transformations.AllTransformations.Values.ToList().ForEach(transformation => {
                UpdateTransformationInfo(transformation, showTransformationImage, updatedTransformationInfo);
            });
        }

        public static void SetTextColor(Color color) {
            Clusters.Values.ToList().ForEach(cluster => {
                cluster.Label.ForeColor = color;
            });
        }

        public static void ShowHideTransformation(Transformation transformation, bool visible) {
            var cluster = Clusters[transformation.Name];
            cluster.Panel.Visible = visible;
            Properties.Settings.Default["show" + transformation.Name] = visible;
            Properties.Settings.Default.Save();
        }

        private static void UpdateTransformationInfo(Transformation transformation, bool showTransformationImage, Dictionary<string, TransformationInfo> updatedTransformationInfo) {
            var info = updatedTransformationInfo[transformation.Name];
            var name = transformation.Name;
            if (TransformationInfos.ContainsKey(name) && TransformationInfos[name].Equals(info)) {
                return;
            }

            if (!TransformationInfos.ContainsKey(name)) {
                TransformationInfos.Add(name, info);
            }

            var cluster = Clusters[name];
            cluster.Label.Text = transformation.I18N + ": " + info.TransformationCount;
            cluster.Transformed = info.TransformationCount >= 3;
            if (cluster.Transformed && showTransformationImage) {
                cluster.BaseBox.Refresh();
                return;
            }

            foreach (var itemImage in cluster.Images) {
                itemImage.ItemTouched = info.TouchedItems.Contains(itemImage.Name);
                itemImage.ItemBlacklisted = info.BlacklistedItems.Contains(itemImage.Name);
            }
            cluster.BaseBox.Refresh();
        }
    }
}
