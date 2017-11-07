using System.Collections.Generic;
using KabalistusCommons.Isaac;
using KabalistusTransformationTracker.Trans;
using static KabalistusTransformationTracker.Trans.AfterbirthPlusTransformations;
using static KabalistusCommons.Utils.MemoryReader;

namespace KabalistusTransformationTracker.Providers {
    public class AfterbirthPlusInfoProvider : AfterbirthBaseInfoProvider {

        private const int PillsOffset = 32628;
        private const int PubertyId = 9;
        private string _pubertyPill = UnknowPubertyPill;
        private bool _pubertyPillSet;
        private readonly IIsaacReader _reader = new AfterbirthPlusIsaacReader();

        public override Dictionary<string, Transformation> GetAllTransformations() {
            return AllTransformations;
        }

        public override string GetPubertyPill() {
            return _pubertyPill;
        }

        protected override Transformation GetSuperBumTransformation() {
            return SuperBum;
        }

        protected override TransformationInfo GetTransformationInfo(Transformation transformation) {
            if (SuperBum.Equals(transformation)) {
                return GetSuperBumInfo();
            }
            if (Adult.Equals(transformation)) {
                return GetAdultInfo();
            }
            return Stompy.Equals(transformation) ? GetStompyInfo() : base.GetTransformationInfo(transformation);
        }

        protected override IIsaacReader GetReader() {
            return _reader;
        }

        private TransformationInfo GetAdultInfo() {
            var counter = GetPlayerInfo(Adult.MemoryOffset);
            counter = counter > 3 ? 3 : counter;

            UpdatePubertyPill(counter);

            var itemsGot = new List<string>();
            for (var i = 0; i < counter; i++) {
                itemsGot.Add(Adult.Items[i].Name);
            }
            var transformed = counter >= 3;
            return new TransformationInfo(counter.ToString(), transformed, itemsGot, new List<string>());
        }

        private TransformationInfo GetStompyInfo() {
            var counter = GetPlayerInfo(Stompy.MemoryOffset);
            counter = counter > 3 ? 3 : counter;
            var itemsGot = new List<string>();
            for (var i = 0; i < counter; i++) {
                itemsGot.Add(Stompy.Items[i].Name);
            }
            var transformed = counter >= 3;
            return new TransformationInfo(counter.ToString(), transformed, itemsGot, new List<string>());
        }

        private void UpdatePubertyPill(int adultCount) {
            if (adultCount <= 0 || adultCount > 3) {
                _pubertyPill = UnknowPubertyPill;
                _pubertyPillSet = false;
                return;
            }

            if (_pubertyPillSet) {
                return;
            }

            _pubertyPillSet = true;
            for (var i = 1; i <= 13; i++) {
                var pillId = GetPlayerManagerInfo(PillsOffset + 4 * i, 4);
                if (pillId != PubertyId) continue;
                _pubertyPill = "pill" + i;
                return;
            }
        }
    }
}
