using System.Collections.Generic;
using KabalistusTransformationTracker.Trans;
using static KabalistusTransformationTracker.Trans.RebirthTransformations;
using static KabalistusTransformationTracker.Utils.MemoryReader;

namespace KabalistusTransformationTracker.Providers {
    public class RebirthInfoProvider : BaseInfoProvider {
        private const int ItemBlacklistOffset = 29080;
        private const int FloorTypeOffset = 8;
        private const int TouchedTreeRootOffset = 28128;

        public override Dictionary<string, Transformation> GetAllTransformations() {
            return AllTransformations;
        }

        protected override int GetFloorTypeOffset() {
            return FloorTypeOffset;
        }

        protected override int GetBlacklistedOffset() {
            return ItemBlacklistOffset;
        }

        protected override void UpdateTouchedItems() {
            TouchedItems.Clear();
            var rootPointer = GetPlayerManagerInfo(TouchedTreeRootOffset, 4);
            if (rootPointer == 0) {
                return;
            }

            var root = ReadInt(rootPointer + 4, 4);
            ReadNode(root);
        }

        protected override TransformationInfo GetTransformationInfo(Transformation transformation) {
            if (!ShowP2) {
                return base.GetTransformationInfo(transformation);
            }
            var p1Counter = GetPlayerInfo(transformation.MemoryOffset);
            var p2Counter = GetPlayer2Info(transformation.MemoryOffset);
            var transformed = p1Counter >= 3 || p2Counter >= 3;
            var counter = p1Counter + "/" + p2Counter;
            return new TransformationInfo(counter, transformed, ItemsTouched(transformation.Items), ItemsBlacklisted(transformation.Items));
        }

        private void ReadNode(int nodePointer) {
            if (!IsFilled(nodePointer)) {
                return;
            }

            ReadNode(GetLeftNodePointer(nodePointer));
            TouchedItems.Add(GetTreeValue(nodePointer));
            ReadNode(GetRightNodePointer(nodePointer));
        }

        private static bool IsFilled(int nodePointer) {
            return ReadInt(nodePointer + 37, 1) == 0;
        }

        private static int GetLeftNodePointer(int nodePointer) {
            return ReadInt(nodePointer, 4);
        }

        private static int GetRightNodePointer(int nodePointer) {
            return ReadInt(nodePointer + 8, 4);
        }

        private static int GetTreeValue(int nodePointer) {
            return ReadInt(nodePointer + 16, 4);
        }
    }
}
