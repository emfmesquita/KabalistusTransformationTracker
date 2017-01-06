using System.Collections.Generic;
using System.Linq;
using KabalistusTransformationTracker.Trans;
using KabalistusTransformationTracker.Utils;

namespace KabalistusTransformationTracker.Providers {
    public abstract class BaseInfoProvider {

        public const string UnknowPubertyPill = "pill0";

        protected bool BlindFloor;
        protected bool ShowP2;
        protected readonly List<int> TouchedItems = new List<int>();

        public virtual Dictionary<string, TransformationInfo> GetTransformationsInfo() {
            UpdateTouchedItems();
            UpdateIsInBlindFloor();
            UpdateShowP2();

            var transformationsInfo = new Dictionary<string, TransformationInfo>();
            GetAllTransformations().ToList().ForEach(pair => {
                transformationsInfo.Add(pair.Key, GetTransformationInfo(pair.Value));
            });
            return transformationsInfo;
        }

        public virtual bool IsInBlindFloor() {
            return BlindFloor;
        }

        public abstract Dictionary<string, Transformation> GetAllTransformations();

        public virtual string GetPubertyPill() {
            return "pill0";
        }

        protected abstract int GetFloorTypeOffset();

        protected abstract int GetBlacklistedOffset();

        protected abstract void UpdateTouchedItems();

        protected virtual void UpdateIsInBlindFloor() {
            var offset = GetFloorTypeOffset();
            var floorType = MemoryReader.GetPlayerManagerInfo(offset, 1);
            BlindFloor = (floorType & 64) == 64;
        }

        protected virtual TransformationInfo GetTransformationInfo(Transformation transformation) {
            var counter = MemoryReader.GetPlayerInfo(transformation.MemoryOffset);
            var transformed = counter >= 3;
            return new TransformationInfo(counter.ToString(), transformed, ItemsTouched(transformation.Items), ItemsBlacklisted(transformation.Items));
        }

        protected virtual List<string> ItemsTouched(IEnumerable<Item> allItens) {
            return allItens.Where(IsItemTouched).Select(item => item.Name).ToList();
        }

        protected virtual List<string> ItemsBlacklisted(IEnumerable<Item> allItens) {
            return allItens.Where(item => !IsItemTouched(item) && IsItemBlacklisted(item)).Select(item => item.Name).ToList();
        }

        protected virtual bool IsItemTouched(Item item) {
            return TouchedItems.Contains(item.Id);
        }

        protected virtual bool IsItemBlacklisted(Item item) {
            var offset = GetBlacklistedOffset() + item.Id;
            return MemoryReader.GetPlayerManagerInfo(offset, 1) > 0;
        }

        protected virtual void UpdateShowP2() {
            if (IsaacVersion.Antibirth != MemoryReader.GetVersion()) {
                ShowP2 = false;
                return;
            }
            ShowP2 = MemoryReader.GetNumberOfPlayers() >= 2;
        }
    }
}
