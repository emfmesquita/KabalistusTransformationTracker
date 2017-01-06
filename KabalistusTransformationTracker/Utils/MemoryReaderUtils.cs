using System;
using System.Collections.Generic;
using System.Linq;

namespace KabalistusTransformationTracker.Utils {
    public class MemoryReaderUtils {

        public static int ConvertLittleEndian(byte[] array, int start = 0, int size = 0) {
            if (!array.Any()) {
                return 0;
            }

            if (array.Count() < start + size) {
                throw new Exception("Impossible to convert to int.");
            }

            var realSize = size == 0 ? array.Count() - start : size;

            var pos = 0;
            var result = 0;
            for (var i = start; i < start + realSize; i++) {
                var by = array[i];
                result |= ((int)by) << pos;
                pos += 8;
            }
            return result;
        }

        public static int Pow(int bas, int exp) {
            return Enumerable.Repeat(bas, exp).Aggregate(1, (a, b) => a * b);
        }

        public static bool Match(string pattern, byte[] read, byte[] expected, List<byte> queryResult) {
            for (var i = 0; i < read.Length; i++) {
                if (pattern[i] != 'b') {
                    if (pattern[i] == 'v') {
                        queryResult.Add(read[i]);
                    }
                } else {
                    if (read[i] != expected[i]) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
