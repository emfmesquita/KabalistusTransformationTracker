using System.Collections.Generic;

namespace KabalistusTransformationTracker.Utils {
    public class ArrayUtils {
        public static byte[] ToByteArray(IReadOnlyList<char> baseArray) {
            var result = new byte[baseArray.Count];
            for (var i = 0; i < baseArray.Count; i++) {
                result[i] = (byte)baseArray[i];
            }
            return result;
        }

        public static byte[] ToByteArray(IReadOnlyList<int> baseArray) {
            var result = new byte[baseArray.Count];
            for (var i = 0; i < baseArray.Count; i++) {
                result[i] = (byte)baseArray[i];
            }
            return result;
        }
    }
}
