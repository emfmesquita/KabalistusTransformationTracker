using System.Collections.Generic;
using System.Linq;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusTransformationTracker.Trans;

namespace KabalistusTransformationTracker.Providers {
    public abstract class BaseInfoProvider {

        public const string UnknowPubertyPill = "pill0";

        protected bool BlindFloor;
        protected bool ShowP2;
        protected List<int> TouchedItems = new List<int>();

        public virtual Dictionary<string, TransformationInfo> GetTransformationsInfo() {
            TouchedItems = GetReader().GetItemsTouchedList();

            var curses = GetReader().GetFloorCurses();
            BlindFloor = (curses & 64) == 64;

            if (IsaacVersion.Antibirth != MemoryReader.GetVersion()) {
                ShowP2 = false;
            } else {
                ShowP2 = MemoryReader.GetNumberOfPlayers() >= 2;
            }

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

        protected abstract IIsaacReader GetReader();

        protected virtual TransformationInfo GetTransformationInfo(Transformation transformation) {
            var counter = MemoryReader.GetPlayerInfo(transformation.MemoryOffset);
            var transformed = counter >= 3;
            return new TransformationInfo(counter.ToString(), transformed, ItemsTouched(transformation.Items), ItemsBlacklisted(transformation.Items));
        }

        protected virtual List<string> ItemsTouched(IEnumerable<TransformationItem> allItens) {
            return allItens.Where(IsItemTouched).Select(item => item.Name).ToList();
        }

        protected virtual List<string> ItemsBlacklisted(IEnumerable<TransformationItem> allItens) {
            return allItens.Where(item => !IsItemTouched(item) && GetReader().IsItemBlacklisted(item)).Select(item => item.Name).ToList();
        }

        public bool IsItemTouched(TransformationItem item) {
            return TouchedItems.Contains(item.Id);
        }
    }
}
