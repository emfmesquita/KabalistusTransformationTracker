using System.Globalization;
using static KabalistusIsaacTools.Utils.ResourcesUtil;

namespace KabalistusIsaacTools.TransformationTracker.Model {
    public class TransformationPill : TransformationItem {

        private int _pillId;

        public TransformationPill(string i18N, int pillId, int x = 0, int y = 0, float scale = 1) : base(i18N, PillResource(pillId), x, y, scale) {
            PillId = pillId;
        }

        public int PillId {
            get {
                return _pillId;
            }

            set {
                if (value == _pillId) return;
                _pillId = value;
                Resource = PillResource(_pillId);
            }
        }

        public override string ToString() {
            var scale = Scale.ToString("N", new CultureInfo("en-US"));
            return $"                new TransformationPill(\"{Tooltip}\", {PillId}, {X}, {Y}, {scale}F),";
        }
    }
}
