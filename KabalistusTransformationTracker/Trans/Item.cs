namespace KabalistusTransformationTracker.Trans {
    public class Item {
        public Item(string name, string i18N, int id, int x = 0, int y = 0, float scale = 1.0F, int blockReduction = 10) {
            Name = name;
            I18N = i18N;
            Id = id;
            X = x;
            Y = y;
            Scale = scale;
            BlockReduction = blockReduction;
        }

        public string Name { get; }
        public string I18N { get; }
        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public float Scale { get; }
        public int BlockReduction { get; }
    }
}
