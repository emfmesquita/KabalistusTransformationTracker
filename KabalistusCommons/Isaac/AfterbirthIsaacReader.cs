using System.Collections.Generic;
using static KabalistusCommons.Utils.MemoryReader;

namespace KabalistusCommons.Isaac {
    public class AfterbirthIsaacReader : AfterbirthBaseIsaacReader {
        private const int ItemBlacklistOffset = 37152;
        private const int HasItemOffset = 7588;
        private const int CursesOffset = 12;
        private const int TouchedItensListInitOffset = 35636;

        public override bool HasItem(Item item) {
            var offset = HasItemOffset + 4 * item.Id;
            return GetPlayerInfo(offset) > 0;
        }

        public override List<Trinket> GetSmeltedTrinkets() {
            return new List<Trinket>();
        }

        public override bool IsItemBlacklisted(Item item) {
            return GetPlayerManagerInfo(ItemBlacklistOffset + item.Id, 1) > 0;
        }

        public override int GetFloorCurses() {
            return GetPlayerManagerInfo(CursesOffset, 1); ;
        }

        public override int GetTimeCounter() {
            return 5;
        }

        public override bool IsGamePaused() {
            // TODO
            return false;
        }

        public override List<Pill> GetPillPool() {
            // TODO
            return new List<Pill>();
        }

        public override Dictionary<int, int> GetPillCount() {
            // TODO
            return new Dictionary<int, int>();
        }

        public override int IndexOfLastPillTaken() {
            // TODO
            return 0;
        }

        protected override int GetTouchedItensListInitOffset() {
            return TouchedItensListInitOffset;
        }
    }
}
