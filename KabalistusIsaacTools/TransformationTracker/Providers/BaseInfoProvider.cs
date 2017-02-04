using System;
using System.Collections.Generic;
using System.Linq;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusIsaacTools.TransformationTracker.Model;

namespace KabalistusIsaacTools.TransformationTracker.Providers {
    public abstract class BaseInfoProvider {
        protected bool BlindFloor;
        protected bool ShowP2;
        protected List<int> TouchedItems = new List<int>();

        public virtual void UpdateTransformations() {
            TouchedItems = GetReader().GetItemsTouchedList();

            var curses = GetReader().GetFloorCurses();
            BlindFloor = (curses & 64) == 64;

            if (IsaacVersion.Antibirth != MemoryReader.GetVersion()) {
                ShowP2 = false;
            } else {
                ShowP2 = MemoryReader.GetNumberOfPlayers() >= 2;
            }

            GetAllTransformations().ToList().ForEach(pair => {
                UpdateTransformation(pair.Value);
            });
        }

        protected virtual bool IsInBlindFloor() {
            return BlindFloor;
        }

        public abstract Dictionary<string, Transformation> GetAllTransformations();

        protected abstract IIsaacReader GetReader();

        protected virtual void UpdateTransformationItem(TransformationItem item, bool isItemTouched, bool isItemBlacklisted) {
            if (isItemTouched) {
                item.Blocked = false;
                item.Touched = true;
                return;
            }

            if (!IsInBlindFloor() && isItemBlacklisted) {
                item.Touched = true;
                item.Blocked = true;
                return;
            }

            item.Touched = false;
            item.Blocked = false;
        }

        protected virtual void UpdateTransformation(Transformation transformation) {
            var counter = MemoryReader.GetPlayerInfo(transformation.MemoryOffset);
            transformation.Count = counter.ToString();
            transformation.ShowTransformationImage(counter >= 3);
            transformation.Items.ForEach(item => UpdateTransformationItem(item, IsItemTouched(item), IsItemBlacklisted(item)));
        }

        protected virtual bool IsItemTouched(TransformationItem item) {
            return TouchedItems.Contains(item.Id);
        }

        protected virtual bool IsItemBlacklisted(TransformationItem item) {
            return GetReader().IsItemBlacklisted(new Item(item.Tooltip, item.Id));
        }
    }
}
