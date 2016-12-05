using System.Collections.Generic;
using System.Linq;
using KabalistusTransformationTracker.Trans;
using static KabalistusTransformationTracker.Trans.Transformations;

namespace KabalistusTransformationTracker.Utils {
    public class TransformationInfoProvider {

        private const int ItemBlacklistOffset = 37152;
        private const int HasItemOffset = 7588;
        private const int TouchedItensListInitOffset = 35636;
        private const int TouchedItensListEndOffset = TouchedItensListInitOffset + 4;
        private const int FloorTypeOffset = 12;

        private static readonly List<int> TouchedItems = new List<int>();
        private static bool _isInBlindFloor;

        public static Dictionary<string, TransformationInfo> GetTransformationsInfo() {
            UpdateTouchedItems();
            UpdateIsInBlindFloor();

            var transformationsInfo = new Dictionary<string, TransformationInfo>();
            AllTransformations.ToList().ForEach(pair => {
                transformationsInfo.Add(pair.Key, GetTransformationInfo(pair.Value));
            });
            return transformationsInfo;
        }

        public static bool IsInBlindFloor() {
            return _isInBlindFloor;
        }

        private static TransformationInfo GetTransformationInfo(Transformation transformation) {
            if (SuperBum.Equals(transformation)) {
                return GetSuperBumInfo();
            }
            var counter = MemoryReader.GetPlayerInfo(transformation.MemoryOffset);
            return new TransformationInfo(counter, ItemsTouched(transformation.Items), ItemsBlacklisted(transformation.Items));
        }

        private static TransformationInfo GetSuperBumInfo() {
            var itemsGot = new List<string>();
            var counter = SuperBum.Items.Sum(item => {
                if (!HasItem(item)) return 0;
                itemsGot.Add(item.Name);
                return 1;
            });
            return new TransformationInfo(counter, itemsGot, ItemsBlacklisted(SuperBum.Items));
        }

        private static List<string> ItemsBlacklisted(IEnumerable<Item> allItens) {
            return allItens.Where(item => !IsItemTouched(item) && IsItemBlacklisted(item)).Select(item => item.Name).ToList();
        }

        private static List<string> ItemsTouched(IEnumerable<Item> allItens) {
            return allItens.Where(IsItemTouched).Select(item => item.Name).ToList();
        }

        private static bool IsItemBlacklisted(Item item) {
            var offset = ItemBlacklistOffset + item.Id;
            return MemoryReader.GetPlayerManagerInfo(offset, 1) > 0;
        }

        private static bool IsItemTouched(Item item) {
            return TouchedItems.Contains(item.Id);
        }

        private static void UpdateTouchedItems() {
            var touchedItemsListInit = MemoryReader.GetPlayerManagerInfo(TouchedItensListInitOffset, 4);
            var touchedItemsListEnd = MemoryReader.GetPlayerManagerInfo(TouchedItensListEndOffset, 4);

            var touchedItemsListSize = (touchedItemsListEnd - touchedItemsListInit) / 24;

            if (TouchedItems.Count == touchedItemsListSize) return;

            if (TouchedItems.Count > touchedItemsListSize) {
                TouchedItems.Clear();
            }

            for (var i = TouchedItems.Count; i < touchedItemsListSize; i++) {
                var addressToRead = touchedItemsListInit + 4 + 24 * i;
                var touchedItemId = MemoryReader.ReadInt(addressToRead, 4);
                TouchedItems.Add(touchedItemId);
            }
        }

        private static bool HasItem(Item item) {
            var offset = HasItemOffset + 4 * item.Id;
            return MemoryReader.GetPlayerInfo(offset) > 0;
        }

        private static void UpdateIsInBlindFloor() {
            var floorType = MemoryReader.GetPlayerManagerInfo(FloorTypeOffset, 1);
            _isInBlindFloor = floorType == 64;
        }
    }
}
