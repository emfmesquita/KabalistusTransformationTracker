using System.Collections.Generic;

namespace KabalistusTransformationTracker.Trans {
    public class RebirthTransformations {

        public static readonly Transformation Guppy = new Transformation("guppy", "Guppy", 12212, 6, -2, 0.85F) {
            Items = new List<Item>() {
                new Item("deadcat", "Dead Cat", 81, 26, 32, 0.55F),
                new Item("guppyspaw", "Guppy's Paw", 133, 3, 52, 0.7F, 11),
                new Item("guppystail", "Guppy's Tail", 134, 39, 64, 0.6F, 9),
                new Item("guppyshead", "Guppy's Head", 145, 32, -2, 0.65F, 6),
                new Item("guppyshairball", "Guppy's Hair Ball", 187, -2, 22, 0.5F, 2),
                new Item("guppyscollar", "Guppy's Collar", 212, 65, 36, 0.6F, 8)
            }
        };

        public static readonly Transformation Beelzebub = new Transformation("beelzebub", "Beelzebub", 12216, 7, -5, 0.9F) {
            Items = new List<Item>() {
                new Item("skatole", "Skatole", 9, 74, 46, 0.5F, 5),
                new Item("haloofflies", "Halo Of Flies", 10, 4, 57, 0.45F, 5),
                new Item("distantadmiration", "Distant Admiration", 57, 22, 77, 0.35F, 3),
                new Item("foreveralone", "Forever Alone", 128, 51, 72, 0.4F, 4),
                new Item("infestation", "Infestation", 148, 27, 47, 0.45F, 4),
                new Item("mulligan", "The Mulligan", 151, 49, 49, 0.4F, 6),
                new Item("hivemind", "Hive Mind", 248, -2, 33, 0.45F, 7),
                new Item("smartfly", "Smart Fly", 264, 34, 27, 0.4F, 2),
                new Item("bbf", "BBF", 272, 10, 7, 0.45F, 7),
                new Item("bestbud", "Best Bud", 274, 34, -2, 0.4F, 3),
                new Item("bigfan", "Big Fan", 279, 72, 23, 0.45F, 7),
                new Item("bbonlyfriend", "???'s Only Friend", 320, 56, 4, 0.45F, 6)
            }
        };

        public static Dictionary<string, Transformation> AllTransformations = new Dictionary<string, Transformation>() {
            {Guppy.Name, Guppy},
            {Beelzebub.Name, Beelzebub}
        };
    }
}
