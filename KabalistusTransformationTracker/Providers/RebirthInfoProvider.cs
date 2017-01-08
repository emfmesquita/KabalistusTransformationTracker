using System.Collections.Generic;
using KabalistusCommons.Isaac;
using KabalistusTransformationTracker.Trans;
using static KabalistusTransformationTracker.Trans.RebirthTransformations;
using static KabalistusCommons.Utils.MemoryReader;

namespace KabalistusTransformationTracker.Providers {
    public class RebirthInfoProvider : BaseInfoProvider {
        private readonly IIsaacReader _reader = new RebirthIsaacReader();

        public override Dictionary<string, Transformation> GetAllTransformations() {
            return AllTransformations;
        }

        protected override IIsaacReader GetReader() {
            return _reader;
        }

        protected override TransformationInfo GetTransformationInfo(Transformation transformation) {
            if (!ShowP2) {
                return base.GetTransformationInfo(transformation);
            }
            var p1Counter = GetPlayerInfo(transformation.MemoryOffset);
            var p2Counter = GetPlayer2Info(transformation.MemoryOffset);

            var coopMode = MainForm.CoopTransformationMode;
            var transformed = (!coopMode && (p1Counter >= 3 || p2Counter >= 3)) || (coopMode && (p1Counter >= 3 && p2Counter >= 3));
            var counter = p1Counter + "/" + p2Counter;
            return new TransformationInfo(counter, transformed, ItemsTouched(transformation.Items), ItemsBlacklisted(transformation.Items));
        }
    }
}
