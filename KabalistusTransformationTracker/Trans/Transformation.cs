using System.Collections.Generic;

namespace KabalistusTransformationTracker.Trans {
    public class Transformation {
        public Transformation(string name, string i18N, int memoryOffset, int x = 0, int y = 0, float scale = 1.0F) {
            Name = name;
            I18N = i18N;
            MemoryOffset = memoryOffset;
            X = x;
            Y = y;
            Scale = scale;
        }

        public string Name { get; }
        public string I18N { get; }
        public int MemoryOffset { get; }
        public List<Item> Items { get; set; }
        public int X { get; }
        public int Y { get; }
        public float Scale { get; }
    }
}
