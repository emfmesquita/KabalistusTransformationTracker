namespace KabalistusCommons.Isaac {
    public class Item {
        public Item(string i18N, int id) {
            I18N = i18N;
            Id = id;
        }

        public string I18N { get; }
        public int Id { get; }

        public override string ToString() {
            return I18N;
        }
    }
}
