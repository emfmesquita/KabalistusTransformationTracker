using System;
using System.Collections.Generic;
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
        private const int PillsOffset = 33028;
        private const int PillCountPointerOffset = 7624;
        private const int LastPillTakenOffset = 7680;

        public override bool HasItem(Item item) {
            var hasItemPointer = GetPlayerInfo(HasItemOffset);
            if (hasItemPointer == 0) {
                return false;
            }
            var hasItem = ReadInt(hasItemPointer + 4 * item.Id, 4);
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

        public override List<Pill> GetPillPool() {
            var pillPool = new List<Pill>();
            var playermanagetInstruct = GetPlayerManagetInstruct();
            if (playermanagetInstruct == 0) {
                return pillPool;
            }
            var pillPoolArray = Read(playermanagetInstruct + PillsOffset, 13 * 4);
            for (var i = 0; i < 13; i++) {
                var pillId = MemoryReaderUtils.ConvertLittleEndian(pillPoolArray, i*4, 4);
                pillPool.Add(Pills.AllPills[pillId]);
            }
            return pillPool;
        }

        public override Dictionary<int, int> GetPillCount() {
            var pillCount = new Dictionary<int, int>();
            var pillCountOffset = GetPlayerInfo(PillCountPointerOffset);
            if (pillCountOffset == 0) {
                return pillCount;
            }
            var pillCountArray = Read(pillCountOffset, 47 * 4);
            for (var i = 0; i < 47; i++) {
                pillCount.Add(i, MemoryReaderUtils.ConvertLittleEndian(pillCountArray, i * 4, 4));
            }
            return pillCount;
        }

        public override int IndexOfLastPillTaken() {
            return GetPlayerInfo(LastPillTakenOffset);
        }

        protected override int GetTouchedItensListInitOffset() {
            return TouchedItensListInitOffset;
        }
    }
}
