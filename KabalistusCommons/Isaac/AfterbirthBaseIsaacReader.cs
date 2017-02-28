using System.Collections.Generic;
using static KabalistusCommons.Utils.MemoryReader;

namespace KabalistusCommons.Isaac {
    public abstract class AfterbirthBaseIsaacReader : IIsaacReader{
        protected List<int> TouchedItems = new List<int>();

        public List<int> GetItemsTouchedList() {
            var initOffset = GetTouchedItensListInitOffset();
            var touchedItemsListInit = GetPlayerManagerInfo(initOffset, 4);
            var touchedItemsListEnd = GetPlayerManagerInfo(initOffset + 4, 4);

            var touchedItemsListSize = (touchedItemsListEnd - touchedItemsListInit) / 24;

            if (TouchedItems.Count == touchedItemsListSize) return TouchedItems;

            if (TouchedItems.Count > touchedItemsListSize) {
                TouchedItems.Clear();
            }

            for (var i = TouchedItems.Count; i < touchedItemsListSize; i++) {
                var addressToRead = touchedItemsListInit + 4 + 24 * i;
                var touchedItemId = ReadInt(addressToRead, 4);
                TouchedItems.Add(touchedItemId);
            }

            return TouchedItems;
        }

        public abstract bool HasItem(Item item);
        public abstract bool IsItemBlacklisted(Item item);
        public abstract int GetFloorCurses();
        public abstract int GetTimeCounter();
        public abstract bool IsGamePaused();
        public abstract List<Item> GetPillPool();
        public abstract List<bool> GetPillKnowledge();
        public abstract int IndexOfLastPillTaken();
        public abstract int GetSeed();
        public abstract int GetPillCardId();
        public abstract Consumable IsPillOrCard();

        protected abstract int GetTouchedItensListInitOffset();
    }
}
