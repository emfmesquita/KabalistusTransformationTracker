using System.Collections.Generic;

namespace KabalistusTransformationTracker.Trans {
    public class TransformationInfo {
        public TransformationInfo(string transformationCount, bool transformed, List<string> touchedItems, List<string> blacklistedItems) {
            TransformationCount = transformationCount;
            Transformed = transformed;
            TouchedItems = touchedItems;
            BlacklistedItems = blacklistedItems;
        }

        public string TransformationCount { get; }
        public bool Transformed { get; }
        public List<string> TouchedItems { get; }
        public List<string> BlacklistedItems { get; }

        public override bool Equals(object obj) {
            var other = obj as TransformationInfo;
            if (other == null) {
                return false;
            }

            return string.Equals(TransformationCount, other.TransformationCount) && Transformed == other.Transformed && Equals(TouchedItems, other.TouchedItems) && Equals(BlacklistedItems, other.BlacklistedItems);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = TransformationCount?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ Transformed.GetHashCode();
                hashCode = (hashCode * 397) ^ (TouchedItems?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (BlacklistedItems?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
