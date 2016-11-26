using System.Collections.Generic;
using System.Linq;
using KabalistusTransformationTracker.Trans;
using static KabalistusTransformationTracker.Trans.Transformations;

namespace KabalistusTransformationTracker.Utils {
    public class TransformationInfoProvider {

        private const int ItemTouchOffset = 37152;
        private const int HasItemOffset = 7588;

        public static TransformationInfo GetTransformationInfo(Transformation transformation) {
            if (SuperBum.Equals(transformation)) {
                return GetSuperBumInfo();
            }
            var counter = MemoryReader.GetPlayerInfo(transformation.MemoryOffset);
            return new TransformationInfo(counter, ItensGot(transformation.Items));
        }

        private static TransformationInfo GetSuperBumInfo() {
            var counter = SuperBum.Items.Sum(item => HasItem(item) ? 1 : 0);
            return new TransformationInfo(counter, ItensGot(SuperBum.Items));
        }

        private static List<string> ItensGot(IEnumerable<Item> allItens) {
            return allItens.Where(ItemTouched).Select(item => item.Name).ToList();
        }

        private static bool ItemTouched(Item item) {
            var offset = ItemTouchOffset + item.Id;
            return MemoryReader.GetPlayerManagerInfo(offset) > 0;
        }

        private static bool HasItem(Item item) {
            var offset = HasItemOffset + 4 * item.Id;
            return MemoryReader.GetPlayerInfo(offset) > 0;
        }
    }
}
