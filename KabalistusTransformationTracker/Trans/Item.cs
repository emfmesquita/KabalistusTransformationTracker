namespace KabalistusTransformationTracker.Trans {
    public class Item {
        public Item(string name, int id, int x = 0, int y = 0, float scale = 1.0F) {
            Name = name;
            Id = id;
            X = x;
            Y = y;
            Scale = scale;
        }

        public string Name { get; }
        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public float Scale { get; }
    }
}
