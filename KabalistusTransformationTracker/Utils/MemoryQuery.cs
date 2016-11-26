namespace KabalistusTransformationTracker.Utils {
    public class MemoryQuery {
        public byte[] Search { get; set; }

        public int[] SearchInt {
            set { Search = ArrayUtils.ToByteArray(value); }
        }

        public char[] SearchChar {
            set { Search = ArrayUtils.ToByteArray(value); }
        }

        public string SearchPattern { get; set; }
    }
}
