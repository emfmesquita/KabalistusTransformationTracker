namespace KabalistusIsaacTools.Serializer {
    public class TabSettings {
        public bool? IsWindowed { get; set; } = false;
        public WindowSettings WindowSettings { get; set; } = new WindowSettings();
    }
}
