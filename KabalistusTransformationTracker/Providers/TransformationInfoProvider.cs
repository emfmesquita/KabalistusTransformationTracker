using System.Collections.Generic;
using KabalistusTransformationTracker.Trans;
using KabalistusTransformationTracker.Utils;

namespace KabalistusTransformationTracker.Providers {
    public class TransformationInfoProvider {

        private static readonly BaseInfoProvider RebirthProvider = new RebirthInfoProvider();
        private static readonly BaseInfoProvider AfterbirthProvider = new AfterbirthInfoProvider();

        public static Dictionary<string, TransformationInfo> GetTransformationsInfo() {
            return GetProvider().GetTransformationsInfo();
        }

        public static Dictionary<string, Transformation> GetAllTransformations() {
            return GetProvider().GetAllTransformations();
        }

        public static bool IsInBlindFloor() {
            return GetProvider().IsInBlindFloor();
        }

        public static IsaacVersion? GetVersion() {
            var isAfterbirth = MemoryReader.IsAfterbirth();
            if (isAfterbirth == null) {
                return null;
            }
            return (bool)isAfterbirth ? IsaacVersion.Afterbirth : IsaacVersion.Rebirth;
        }

        private static BaseInfoProvider GetProvider() {
            var version = GetVersion();
            if (version == null) {
                return null;
            }
            return version == IsaacVersion.Rebirth ? RebirthProvider : AfterbirthProvider;
        }
    }
}
