using System.Collections.Generic;
using KabalistusCommons.Isaac;
using KabalistusTransformationTracker.Trans;
using static KabalistusTransformationTracker.Trans.AfterbirthTransformations;

namespace KabalistusTransformationTracker.Providers {
    public class AfterbirthInfoProvider : AfterbirthBaseInfoProvider {
        private readonly IIsaacReader _reader = new AfterbirthIsaacReader();

        public override Dictionary<string, Transformation> GetAllTransformations() {
            return AllTransformations;
        }

        protected override Transformation GetSuperBumTransformation() {
            return SuperBum;
        }

        protected override IIsaacReader GetReader() {
            return _reader;
        }

        protected override TransformationInfo GetTransformationInfo(Transformation transformation) {
            return SuperBum.Equals(transformation) ? GetSuperBumInfo() : base.GetTransformationInfo(transformation);
        }
    }
}
