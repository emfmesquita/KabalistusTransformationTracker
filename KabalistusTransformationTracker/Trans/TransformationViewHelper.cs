using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using KabalistusTransformationTracker.Utils;

namespace KabalistusTransformationTracker.Trans {
    public class TransformationViewHelper {
        private readonly Dictionary<string, TransformationViewInfo> _transformationViewInfos = new Dictionary<string, TransformationViewInfo>();
        private readonly Dictionary<string, TransformationInfo> _transformationInfos = new Dictionary<string, TransformationInfo>();

        public void Add(Transformation transformation, TransformationViewInfo info) {
            _transformationViewInfos.Add(transformation.Name, info);
        }

        public void SetInitialValuesFromConfig() {
            SetTextColor(Properties.Settings.Default.TextColor);

            _transformationViewInfos.ToList().ForEach(pair => {
                var configValue = (bool)Properties.Settings.Default["show" + pair.Key];
                pair.Value.Menu.Checked = configValue;
                pair.Value.Panel.Visible = configValue;
            });
        }

        public void UpdateTransformationsInfo(bool showTransformationImage) {
            Transformations.AllTransformations.Values.ToList().ForEach(transformation => {
                UpdateTransformationInfo(transformation, showTransformationImage);
            });
        }

        public void SetTextColor(Color color) {
            _transformationViewInfos.Values.ToList().ForEach(transformationViewInfo => {
                transformationViewInfo.Label.ForeColor = color;
            });
        }

        public void ShowHideTransformation(Transformation transformation, bool visible) {
            var info = _transformationViewInfos[transformation.Name];
            info.Panel.Visible = visible;
            Properties.Settings.Default["show" + transformation.Name] = visible;
            Properties.Settings.Default.Save();
        }

        private void UpdateTransformationInfo(Transformation transformation, bool showTransformationImage) {
            var info = TransformationInfoProvider.GetTransformationInfo(transformation);
            var name = transformation.Name;
            if (_transformationInfos.ContainsKey(name) && _transformationInfos[name].Equals(info)) {
                return;
            }

            if (!_transformationInfos.ContainsKey(name)) {
                _transformationInfos.Add(name, info);
            }

            var viewInfo = _transformationViewInfos[name];
            viewInfo.Label.Text = transformation.I18N + ": " + info.TransformationCount;
            viewInfo.Cluster.Transformed = info.TransformationCount >= 3;
            if (viewInfo.Cluster.Transformed && showTransformationImage) {
                viewInfo.Cluster.BaseBox.Refresh();
                return;
            }

            foreach (var itemImage in viewInfo.Cluster.Images) {
                itemImage.HasItem = info.ItemsGot.Contains(itemImage.Name);
            }
            viewInfo.Cluster.BaseBox.Refresh();
        }
    }
}
