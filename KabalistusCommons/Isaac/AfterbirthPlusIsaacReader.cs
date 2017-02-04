using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using KabalistusCommons.Utils;
using static KabalistusCommons.Utils.MemoryReader;

namespace KabalistusCommons.Isaac {
    public class AfterbirthPlusIsaacReader : AfterbirthBaseIsaacReader {
        private const int ItemBlacklistOffset = 31836;
        private const int HasItemOffset = 7600;
        private const int CursesOffset = 12;
        private const int TouchedItensListInitOffset = 30428;
        private const int TimeCounterOffset = 2178748;
        private const int GamePausedOffset = 1245636;
        private const int SmeltedTrinketsPointerOffset = 7588;

        public override bool HasItem(Item item) {
            var hasItemPointer = GetPlayerInfo(HasItemOffset);
            if (hasItemPointer == 0) {
                return false;
            }

            var hasItem = ReadInt(hasItemPointer + 4 * item.Id, 4);

            var smelted = GetSmeltedTrinkets();
            Console.WriteLine(string.Join(", ", smelted));
            return hasItem > 0;
        }

        public override List<Trinket> GetSmeltedTrinkets() {
            var smeltedTrinketsOffset = GetPlayerInfo(SmeltedTrinketsPointerOffset);
            var smeltedTrinkets = Read(smeltedTrinketsOffset + 1, 119);

            var currentSmeltedTrinkets = new List<Trinket>();
            for (var i = 0; i < smeltedTrinkets.Length; i++) {
                if (smeltedTrinkets[i] == 1) {
                    currentSmeltedTrinkets.Add(Trinkets.AllTrinkets[i + 1]);
                }
            }

            currentSmeltedTrinkets.Sort((trinketA, trinketB) => string.CompareOrdinal(trinketA.I18N, trinketB.I18N));
            return currentSmeltedTrinkets;
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
