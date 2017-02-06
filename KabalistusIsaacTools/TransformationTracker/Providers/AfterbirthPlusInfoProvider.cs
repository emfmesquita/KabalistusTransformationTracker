using System;
using System.Collections.Generic;
using System.Text;
using KabalistusCommons.Isaac;
using KabalistusIsaacTools.TransformationTracker.Model;
using static KabalistusIsaacTools.TransformationTracker.Model.AfterbirthPlusTransformations;
using static KabalistusCommons.Utils.MemoryReader;

namespace KabalistusIsaacTools.TransformationTracker.Providers {
    public class AfterbirthPlusInfoProvider : AfterbirthBaseInfoProvider {

        private const int PillsOffset = 33024;
        private const int PubertyId = 9;
        private int _pubertyPill;
        private readonly IIsaacReader _reader = new AfterbirthPlusIsaacReader();

        public override Dictionary<string, Transformation> GetAllTransformations() {
            return AllTransformations;
        }

        protected override void UpdateTransformation(Transformation transformation) {
            if (SuperBum.Equals(transformation)) {
                UpdateSuperBumTransformation(transformation);
            } else if (Adult.Equals(transformation)) {
                UpdateAdultTransformation(transformation);
            } else {
                base.UpdateTransformation(transformation);
            }
        }

        protected override IIsaacReader GetReader() {
            return _reader;
        }

        private void UpdateAdultTransformation(Transformation adultTransformation) {
            var pool = _reader.GetPillPool();
            var lastIndex = _reader.IndexOfLastPillTaken();
            if (lastIndex != 0) {
                Console.WriteLine(pool[lastIndex-1]);
            }

            var counter = GetPlayerInfo(Adult.MemoryOffset);
            counter = counter > 3 ? 3 : counter;

            UpdatePubertyPill(counter);

            adultTransformation.ShowTransformationImage(counter == 3);
            adultTransformation.Count = counter.ToString();

            for (var i = 0; i < 3; i++) {
                var pill = adultTransformation.Items[i] as TransformationPill;
                if (pill == null) return;
                pill.Touched = i + 1 <= counter;
                pill.PillId = _pubertyPill;
            }
        }

        private void UpdatePubertyPill(int adultCount) {
            if (adultCount <= 0 || adultCount > 3) {
                _pubertyPill = 0;
                return;
            }

            if (_pubertyPill != 0) {
                return;
            }

            for (var i = 1; i <= 13; i++) {
                var pillId = GetPlayerManagerInfo(PillsOffset + 4 * i, 4);
                if (pillId != PubertyId) continue;
                _pubertyPill = i;
                return;
            }
        }
    }
}
