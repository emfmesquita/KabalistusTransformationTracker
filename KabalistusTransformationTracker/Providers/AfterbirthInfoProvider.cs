using System.Collections.Generic;
using KabalistusTransformationTracker.Trans;
using static KabalistusTransformationTracker.Trans.AfterbirthTransformations;
using static KabalistusTransformationTracker.Utils.MemoryReader;

namespace KabalistusTransformationTracker.Providers {
    public class AfterbirthInfoProvider : AfterbirthBaseInfoProvider {
        private const int ItemBlacklistOffset = 37152;
        private const int HasItemOffset = 7588;
        private const int FloorTypeOffset = 12;

        private const int TouchedItensListInitOffset = 35636;
        private const int TouchedItensListEndOffset = TouchedItensListInitOffset + 4;

        public override Dictionary<string, Transformation> GetAllTransformations() {
            return AllTransformations;
        }

        protected override int GetFloorTypeOffset() {
            return FloorTypeOffset;
        }

        protected override int GetBlacklistedOffset() {
            return ItemBlacklistOffset;
        }

        protected override Transformation GetSuperBumTransformation() {
            return SuperBum;
        }
        protected override int GetTouchedItensListEndOffset() {
            return TouchedItensListInitOffset;
        }

        protected override int GetTouchedItensListInitOffset() {
            return TouchedItensListEndOffset;
        }

        protected override TransformationInfo GetTransformationInfo(Transformation transformation) {
            return SuperBum.Equals(transformation) ? GetSuperBumInfo() : base.GetTransformationInfo(transformation);
        }

        protected override bool HasItem(Item item) {
            var offset = HasItemOffset + 4 * item.Id;
            return GetPlayerInfo(offset) > 0;
        }
    }
}
