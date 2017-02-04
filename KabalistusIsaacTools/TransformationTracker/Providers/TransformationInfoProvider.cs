using System.Collections.Generic;
using KabalistusCommons.Utils;
using KabalistusIsaacTools.TransformationTracker.Model;

namespace KabalistusIsaacTools.TransformationTracker.Providers {
    public class TransformationInfoProvider {

        //private static readonly BaseInfoProvider RebirthProvider = new RebirthInfoProvider();
        //private static readonly BaseInfoProvider AfterbirthProvider = new AfterbirthInfoProvider();
        private static readonly BaseInfoProvider AfterbirthPlusProvider = new AfterbirthPlusInfoProvider();

        public static void UpdateTransformations() {
            GetProvider().UpdateTransformations();
        }

        public static Dictionary<string, Transformation> GetAllTransformations() {
            return GetProvider().GetAllTransformations();
        }

        private static BaseInfoProvider GetProvider() {
            var version = MemoryReader.GetVersion();
            BaseInfoProvider provider = null;
            switch (version) {
                //case IsaacVersion.Rebirth:
                //case IsaacVersion.Antibirth:
                //    provider = RebirthProvider;
                //    break;
                //case IsaacVersion.Afterbirth:
                //    provider = AfterbirthProvider;
                //    break;
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
