namespace KabalistusIsaacTools.Serializer {
    public class TabSettings {
        public bool IsWindowed { get; set; }

        public WindowSettings WindowSettings { get; set; }

        public static TabSettings Default() {
            return new TabSettings() {
                IsWindowed = false,
                WindowSettings = new WindowSettings()
            };
        }
    }
}
