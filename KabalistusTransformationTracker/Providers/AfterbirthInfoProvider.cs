using System.Collections.Generic;
using System.Linq;
using KabalistusTransformationTracker.Trans;
using static KabalistusTransformationTracker.Trans.AfterbirthTransformations;
using static KabalistusTransformationTracker.Utils.MemoryReader;

namespace KabalistusTransformationTracker.Providers {
    public class AfterbirthInfoProvider : BaseInfoProvider {
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

        protected override TransformationInfo GetTransformationInfo(Transformation transformation) {
            return SuperBum.Equals(transformation) ? GetSuperBumInfo() : base.GetTransformationInfo(transformation);
        }

        private TransformationInfo GetSuperBumInfo() {
            var itemsGot = new List<string>();
            var counter = SuperBum.Items.Sum(item => {
                if (!HasItem(item)) return 0;
                itemsGot.Add(item.Name);
                return 1;
            });
            return new TransformationInfo(counter, itemsGot, ItemsBlacklisted(SuperBum.Items));
        }

        private static bool HasItem(Item item) {
            var offset = HasItemOffset + 4 * item.Id;
            return GetPlayerInfo(offset) > 0;
        }

        protected override void UpdateTouchedItems() {
            var touchedItemsListInit = GetPlayerManagerInfo(TouchedItensListInitOffset, 4);
            var touchedItemsListEnd = GetPlayerManagerInfo(TouchedItensListEndOffset, 4);

            var touchedItemsListSize = (touchedItemsListEnd - touchedItemsListInit) / 24;

            if (TouchedItems.Count == touchedItemsListSize) return;

            if (TouchedItems.Count > touchedItemsListSize) {
                TouchedItems.Clear();
            }

            for (var i = TouchedItems.Count; i < touchedItemsListSize; i++) {
                var addressToRead = touchedItemsListInit + 4 + 24 * i;
                var touchedItemId = ReadInt(addressToRead, 4);
                TouchedItems.Add(touchedItemId);
            }
        }
    }
}
