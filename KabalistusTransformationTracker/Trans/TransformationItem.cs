using KabalistusCommons.Isaac;

namespace KabalistusTransformationTracker.Trans {
    public class TransformationItem : Item {
        public TransformationItem(string name, string i18N, int id, int x = 0, int y = 0, float scale = 1.0F, int blockReduction = 10) : base(i18N, id) {
            Name = name;
            X = x;
            Y = y;
            Scale = scale;
            BlockReduction = blockReduction;
        }

        public string Name { get; }
        public int X { get; }
        public int Y { get; }
        public float Scale { get; }
        public int BlockReduction { get; }
    }
}
