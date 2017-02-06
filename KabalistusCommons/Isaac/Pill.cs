namespace KabalistusCommons.Isaac {
    public class Pill {
        public Pill(string i18N, int id) {
            I18N = i18N;
            Id = id;
        }

        public string I18N { get; set; }
        public int Id { get; set; }

        public override string ToString() {
            return I18N;
        }

        protected bool Equals(Trinket other) {
            return string.Equals(I18N, other.I18N) && Id == other.Id;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Trinket)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((I18N?.GetHashCode() ?? 0) * 397) ^ Id;
            }
        }
    }
}
