using System;
using System.Collections.Generic;
using KabalistusCommons.Utils;
using static KabalistusCommons.Utils.MemoryReader;

namespace KabalistusCommons.Isaac {
    public class AfterbirthPlusIsaacReader : AfterbirthBaseIsaacReader {
        private readonly List<Item> _voidedItems = new List<Item>();

        private const int ItemBlacklistOffset = 31836;
        private const int HasItemOffset = 7600;
        private const int CursesOffset = 12;
        private const int TouchedItensListInitOffset = 30428;
        private const int TimeCounterOffset = 2178748;
        private const int GamePausedOffset = 1245636;
        private const int SmeltedTrinketsPointerOffset = 7588;
        private const int PillsOffset = 33028;
        //private const int PillCountPointerOffset = 7624;
        private const int PillKnownOffset = 33081;
        private const int LastPillTakenOffset = 7680;
        private const int VoidedItemsInitOffset = 7612;
        private const int SeedOffset = 451880;
        private const int PillCardIdOffset = 7732;
        private const int IsPillOrCardOffset = 7736;

        public override bool HasItem(Item item) {
            var hasItemPointer = GetPlayerInfo(HasItemOffset);
            if (hasItemPointer == 0) {
                return false;
            }
            var hasItem = ReadInt(hasItemPointer + 4 * item.Id, 4);
            return hasItem > 0;
        }

        public List<Item> GetSmeltedTrinkets() {
            var smeltedTrinketsOffset = GetPlayerInfo(SmeltedTrinketsPointerOffset);
            var trinketsCount = ModdedHelper.UnmoddedTrinketsCount + ModdedHelper.ModdedTrinketsCount();
            var smeltedTrinkets = Read(smeltedTrinketsOffset + 1, trinketsCount);

            var currentSmeltedTrinkets = new List<Item>();
            for (var i = 0; i < smeltedTrinkets.Length; i++) {
                if (smeltedTrinkets[i] != 1) continue;

                var trinket = i < ModdedHelper.UnmoddedTrinketsCount ? Trinkets.AllTrinkets[i + 1] : ModdedHelper.GetModdedTrinket(i + 1);
                currentSmeltedTrinkets.Add(trinket);
            }

            currentSmeltedTrinkets.Sort((trinketA, trinketB) => string.CompareOrdinal(trinketA.I18N, trinketB.I18N));
            return currentSmeltedTrinkets;
        }

        public List<Item> GetVoidedItems() {
            var voidedListInit = GetPlayerInfo(VoidedItemsInitOffset);
            if (voidedListInit == 0) {
                _voidedItems.Clear();
                return _voidedItems;
            }

            var voidedListEnd = GetPlayerInfo(VoidedItemsInitOffset + 4);
            var voidedListSize = (voidedListEnd - voidedListInit) / 4;

            if (_voidedItems.Count == voidedListSize) return _voidedItems;
            if (_voidedItems.Count > voidedListSize) {
                _voidedItems.Clear();
            }

            for (var i = _voidedItems.Count; i < voidedListSize; i++) {
                var addressToRead = voidedListInit + 4 * i;
                var voidedItemId = ReadInt(addressToRead, 4);
                _voidedItems.Add(GetItem(voidedItemId));
            }

            return _voidedItems;
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

        public override List<Item> GetPillPool() {
            var pillPool = new List<Item>();
            var playermanagetInstruct = GetPlayerManagetInstruct();
            var numberOfPlayers = GetNumberOfPlayers(playermanagetInstruct);
            if (playermanagetInstruct == 0 || numberOfPlayers == 0) {
                return pillPool;
            }
            var pillPoolArray = Read(playermanagetInstruct + PillsOffset, 13 * 4);
            for (var i = 0; i < 13; i++) {
                var pillId = MemoryReaderUtils.ConvertLittleEndian(pillPoolArray, i * 4, 4);
                pillPool.Add(pillId < ModdedHelper.UnmoddedPillsCount ? Pills.AllPills[pillId] : ModdedHelper.GetModdedPill(pillId));
            }
            return pillPool;
        }

        //public Dictionary<int, int> GetPillCount() {
        //    var pillCount = new Dictionary<int, int>();
        //    var pillCountOffset = GetPlayerInfo(PillCountPointerOffset);
        //    if (pillCountOffset == 0) {
        //        return pillCount;
        //    }
        //    var pillCountArray = Read(pillCountOffset, 47 * 4);
        //    for (var i = 0; i < 47; i++) {
        //        pillCount.Add(i, MemoryReaderUtils.ConvertLittleEndian(pillCountArray, i * 4, 4));
        //    }
        //    return pillCount;
        //}

        public override List<bool> GetPillKnowledge() {
            var pillKnowledge = new List<bool>();
            var playermanagetInstruct = GetPlayerManagetInstruct();
            var numberOfPlayers = GetNumberOfPlayers(playermanagetInstruct);
            if (playermanagetInstruct == 0 || numberOfPlayers == 0) {
                return pillKnowledge;
            }
            var pillKnoledgeArray = Read(playermanagetInstruct + PillKnownOffset, 13);
            for (var i = 0; i < 13; i++) {
                pillKnowledge.Add(pillKnoledgeArray[i] == 1);
            }
            return pillKnowledge;
        }

        public override int IndexOfLastPillTaken() {
            return GetPlayerInfo(LastPillTakenOffset);
        }

        public override int GetSeed() {
            return GetGameManagerInfo(SeedOffset, 4);
        }

        public override int GetPillCardId() {
            return GetPlayerInfo(PillCardIdOffset);
        }

        public override Consumable IsPillOrCard() {
            return GetPlayerInfo(IsPillOrCardOffset, 1) == 0 ? Consumable.Pill : Consumable.Card;
        }

        protected override int GetTouchedItensListInitOffset() {
            return TouchedItensListInitOffset;
        }

        private static Item GetItem(int id) {
            if (id > ModdedHelper.UnmoddedItemsCount) {
                return ModdedHelper.GetModdedItem(id);
            }

            if (Items.RebirthItems.ContainsKey(id)) return Items.RebirthItems[id];
            if (Items.AfterbirthItems.ContainsKey(id)) return Items.AfterbirthItems[id];
            return Items.AfterbirthPlusItems.ContainsKey(id) ? Items.AfterbirthPlusItems[id] : null;
        }
    }
}
