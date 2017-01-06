using System.Collections.Generic;
using System.Linq;
using KabalistusTransformationTracker.Trans;
using static KabalistusTransformationTracker.Utils.MemoryReader;

namespace KabalistusTransformationTracker.Providers {
    public abstract class AfterbirthBaseInfoProvider : BaseInfoProvider {

        protected abstract Transformation GetSuperBumTransformation();

        protected abstract bool HasItem(Item item);

        protected abstract int GetTouchedItensListInitOffset();
        protected abstract int GetTouchedItensListEndOffset();

        protected virtual TransformationInfo GetSuperBumInfo() {
            var itemsGot = new List<string>();
            var superBumTransformation = GetSuperBumTransformation();
            var counter = superBumTransformation.Items.Sum(item => {
                if (!HasItem(item)) return 0;
                itemsGot.Add(item.Name);
                return 1;
            });
            var transformed = counter == 3;
            return new TransformationInfo(counter.ToString(), transformed, itemsGot, ItemsBlacklisted(superBumTransformation.Items));
        }

        protected override void UpdateTouchedItems() {
            var touchedItemsListInit = GetPlayerManagerInfo(GetTouchedItensListInitOffset(), 4);
            var touchedItemsListEnd = GetPlayerManagerInfo(GetTouchedItensListEndOffset(), 4);

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
