using System.Collections.Generic;
using KabalistusTransformationTracker.Trans;
using KabalistusTransformationTracker.Utils;
using static KabalistusTransformationTracker.Trans.AfterbirthPlusTransformations;
using static KabalistusTransformationTracker.Utils.MemoryReader;

namespace KabalistusTransformationTracker.Providers {
    public class AfterbirthPlusInfoProvider : AfterbirthBaseInfoProvider {
        private const int ItemBlacklistOffset = 32048;
        private const int HasItemOffset = 7600;
        private const int FloorTypeOffset = 12;

        private const int TouchedItensListInitOffset = 30640;
        private const int TouchedItensListEndOffset = TouchedItensListInitOffset + 4;

        private const int PillsOffset = 33236;
        private const int PubertyId = 9;
        private string _pubertyPill = UnknowPubertyPill;
        private bool _pubertyPillSet;

        public override Dictionary<string, Transformation> GetAllTransformations() {
            return AllTransformations;
        }

        public override string GetPubertyPill() {
            return _pubertyPill;
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

        protected override int GetTouchedItensListInitOffset() {
            return TouchedItensListInitOffset;
        }

        protected override int GetTouchedItensListEndOffset() {
            return TouchedItensListEndOffset;
        }

        protected override bool HasItem(Item item) {
            var hasItemPointer = GetPlayerInfo(HasItemOffset);
            var hasItem = ReadInt(hasItemPointer + 4 * item.Id, 4);
            return hasItem > 0;
        }

        protected override bool IsItemBlacklisted(Item item) {
            var blockListPointer = GetPlayerManagerInfo(GetBlacklistedOffset(), 4);
            var blockByte = ReadInt(blockListPointer + item.Id / 8, 1);
            var itemBlockBit = MemoryReaderUtils.Pow(2, item.Id % 8);
            return (blockByte & itemBlockBit) == itemBlockBit;
        }

        protected override TransformationInfo GetTransformationInfo(Transformation transformation) {
            if (SuperBum.Equals(transformation)) {
                return GetSuperBumInfo();
            }
            return Adult.Equals(transformation) ? GetAdultInfo() : base.GetTransformationInfo(transformation);
        }

        private TransformationInfo GetAdultInfo() {
            var counter = GetPlayerInfo(Adult.MemoryOffset);
            counter = counter > 3 ? 3 : counter;

            UpdatePubertyPill(counter);

            var itemsGot = new List<string>();
            for (var i = 0; i < counter; i++) {
                itemsGot.Add(Adult.Items[i].Name);
            }
            var transformed = counter >= 3;
            return new TransformationInfo(counter.ToString(), transformed, itemsGot, new List<string>());
        }

        private void UpdatePubertyPill(int adultCount) {
            if (adultCount <= 0 || adultCount > 3) {
                _pubertyPill = UnknowPubertyPill;
                _pubertyPillSet = false;
                return;
            }

            if (_pubertyPillSet) {
                return;
            }

            _pubertyPillSet = true;
            for (var i = 1; i <= 13; i++) {
                var pillId = GetPlayerManagerInfo(PillsOffset + 4 * i, 4);
                if (pillId != PubertyId) continue;
                _pubertyPill = "pill" + i;
                return;
            }
        }
    }
}
