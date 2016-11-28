using System.Collections.Generic;

namespace KabalistusTransformationTracker.Trans {
    public class TransformationInfo {

        public TransformationInfo(int transformationCount, List<string> touchedItems, List<string> blacklistedItems) {
            TransformationCount = transformationCount;
            TouchedItems = touchedItems;
            BlacklistedItems = blacklistedItems;
        }

        public int TransformationCount { get; }
        public List<string> TouchedItems { get; }
        public List<string> BlacklistedItems { get; }

        public override bool Equals(object obj) {
            var other = obj as TransformationInfo;
            if (other == null) {
                return false;
            }

            return TransformationCount == other.TransformationCount && Equals(TouchedItems, other.TouchedItems) && Equals(BlacklistedItems, other.BlacklistedItems);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = TransformationCount;
                hashCode = (hashCode * 397) ^ (TouchedItems?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (BlacklistedItems?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
