using System.Collections.Generic;

namespace KabalistusTransformationTracker.Trans {
    public class TransformationInfo {
        public TransformationInfo(int transformationCount, List<string> itemsGot) {
            TransformationCount = transformationCount;
            ItemsGot = itemsGot;
        }

        public int TransformationCount { get; }
        public List<string> ItemsGot { get; }

        public override bool Equals(object obj) {
            var other = obj as TransformationInfo;
            if (other == null) {
                return false;
            }

            return TransformationCount == other.TransformationCount && Equals(ItemsGot, other.ItemsGot);
        }

        public override int GetHashCode() {
            return (TransformationCount * 397) ^ (ItemsGot?.GetHashCode() ?? 0);
        }
    }
}
