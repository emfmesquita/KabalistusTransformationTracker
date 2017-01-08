using System;
using KabalistusCommons.Utils;
using static KabalistusCommons.Utils.MemoryReader;

namespace KabalistusCommons.Isaac {
    public class AfterbirthPlusIsaacReader : AfterbirthBaseIsaacReader {
        private const int ItemBlacklistOffset = 32048;
        private const int HasItemOffset = 7600;
        private const int CursesOffset = 12;
        private const int TouchedItensListInitOffset = 30640;
        
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

        protected override int GetTouchedItensListInitOffset() {
            return TouchedItensListInitOffset;
        }
    }
}
