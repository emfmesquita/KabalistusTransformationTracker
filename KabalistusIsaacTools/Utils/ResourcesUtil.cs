namespace KabalistusIsaacTools.Utils {
    public class ResourcesUtil {
        public static string ItemResource(int id) {
            return $"KabalistusIsaacTools.Images.Items.c{id}.png";
        }

        public static string PillResource(int pillNumber) {
            return $"KabalistusIsaacTools.Images.Pills.pill{pillNumber}.png";
        }

        public static string TrinketResource(int id) {
            return $"KabalistusIsaacTools.Images.Trinkets.t{id}.png";
        }
    }
}
