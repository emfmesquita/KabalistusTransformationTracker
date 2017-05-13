using System;
using KabalistusCommons.Utils;
using static KabalistusCommons.Utils.MemoryReader;

namespace KabalistusCommons.Isaac {
    public class AfterbirthPlusIsaacReader : AfterbirthBaseIsaacReader {
        private const int ItemBlacklistOffset = 31836;
        private const int HasItemOffset = 10084;
        private const int CursesOffset = 12;
        private const int TouchedItensListInitOffset = 30428;
        private const int TimeCounterOffset = 2178748;
        private const int GamePausedOffset = 1245636;
        
        public override bool HasItem(Item item) {
            var hasItemPointer = GetPlayerInfo(HasItemOffset);
            if (hasItemPointer == 0) {
                return false;
            }

            var hasItem = ReadInt(hasItemPointer + 4 * item.Id, 4);
            return hasItem > 0;
        }

        public override bool IsItemBlacklisted(Item item) {
            var blockListPointer = GetPlayerManagerInfo(ItemBlacklistOffset, 4);
            var blockByte = ReadInt(blockListPointer + item.Id / 8, 1);
            var itemBlockBit = MemoryReaderUtils.Pow(2, item.Id % 8);
            return (blockByte & itemBlockBit) == itemBlockBit;
        }

        public override int GetFloorCurses() {
            return GetPlayerManagerInfo(CursesOffset, 1); ;
        }

        public override int GetTimeCounter() {
            return GetPlayerManagerInfo(TimeCounterOffset, 4) / 30;
        }

        public override bool IsGamePaused() {
            return GetPlayerManagerInfo(GamePausedOffset, 4) > 0;
        }

        protected override int GetTouchedItensListInitOffset() {
            return TouchedItensListInitOffset;
        }
    }
}
