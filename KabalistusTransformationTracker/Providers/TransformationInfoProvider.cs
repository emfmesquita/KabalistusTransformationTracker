using System.Collections.Generic;
using KabalistusTransformationTracker.Trans;
using KabalistusTransformationTracker.Utils;

namespace KabalistusTransformationTracker.Providers {
    public class TransformationInfoProvider {

        private static readonly BaseInfoProvider RebirthProvider = new RebirthInfoProvider();
        private static readonly BaseInfoProvider AfterbirthProvider = new AfterbirthInfoProvider();
        private static readonly BaseInfoProvider AfterbirthPlusProvider = new AfterbirthPlusInfoProvider();

        public static Dictionary<string, TransformationInfo> GetTransformationsInfo() {
            return GetProvider().GetTransformationsInfo();
        }

        public static Dictionary<string, Transformation> GetAllTransformations() {
            return GetProvider().GetAllTransformations();
        }

        public static bool IsInBlindFloor() {
            return GetProvider().IsInBlindFloor();
        }

        public static string GetPubertyPill() {
            var provider = GetProvider();
            return provider == null ? BaseInfoProvider.UnknowPubertyPill : provider.GetPubertyPill();
        }

        private static BaseInfoProvider GetProvider() {
            var version = MemoryReader.GetVersion();
            BaseInfoProvider provider = null;
            switch (version) {
                case IsaacVersion.Rebirth:
                case IsaacVersion.Antibirth:
                    provider = RebirthProvider;
                    break;
                case IsaacVersion.Afterbirth:
                    provider = AfterbirthProvider;
                    break;
                case IsaacVersion.AfterbirthPlus:
                    provider = AfterbirthPlusProvider;
                    break;
                case null:
                default:
                    break;
            }
            return provider;
        }
    }
}
