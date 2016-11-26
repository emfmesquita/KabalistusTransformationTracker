using System.Collections.Generic;

namespace KabalistusTransformationTracker.Trans {
    public class Transformations {

        public static readonly Transformation Guppy = new Transformation("guppy", "Guppy", 19480, 6, -2, 0.85F) {
            Items = new List<Item>() {
                new Item("deadcat", 81, 26, 32, 0.55F),
                new Item("guppyspaw", 133, 3, 52, 0.7F),
                new Item("guppystail", 134, 39, 64, 0.6F),
                new Item("guppyshead", 145, 32, -2, 0.65F),
                new Item("guppyshairball", 187, -2, 22, 0.5F),
                new Item("guppyscollar", 212, 65, 36, 0.6F)
            }
        };

        public static readonly Transformation Beelzebub = new Transformation("beelzebub", "Beelzebub", 19484, 7, -5, 0.9F) {
            Items = new List<Item>() {
                new Item("skatole", 9, 72, 62, 0.4F),
                new Item("haloofflies", 10, 6, 62, 0.4F),
                new Item("distantadmiration", 57, 24, 78, 0.3F),
                new Item("foreveralone", 128, 26, 62, 0.35F),
                new Item("infestation", 148, 53, 73, 0.4F),
                new Item("mulligan", 151, 76, 43, 0.35F),
                new Item("hivemind", 248, -2, 40, 0.4F),
                new Item("smartfly", 264, 50, 54, 0.35F),
                new Item("bbf", 272, 24, 40, 0.4F),
                new Item("bestbud", 274, 33, 18, 0.35F),
                new Item("bigfan", 279, 72, 22, 0.4F),
                new Item("bbonlyfriend", 320, 48, 32, 0.4F),
                new Item("friendzone", 364, 41, -1, 0.3F),
                new Item("lostfly", 365, 3, 15, 0.3F),
                new Item("obsessedfan", 426, 11, 27, 0.35F),
                new Item("papafly", 430, 16, 2, 0.35F),
                new Item("jarofflies", 434, 61, 6, 0.4F)
            }
        };

        public static readonly Transformation FunGuy = new Transformation("funguy", "Fun Guy", 19488, 1, -1, 0.75F) {
            Items = new List<Item>() {
                new Item("oneup", 11, -4, 31, 0.65F),
                new Item("magicmush", 12, 48, 0, 0.7F),
                new Item("minimush", 71, 51, 67, 0.5F),
                new Item("oodmushthin", 120, 70, 30, 0.75F),
                new Item("oodmushthick", 121, 31, 36, 0.5F),
                new Item("bluecap", 342, 17, 54, 0.75F),
                new Item("godsflesh", 398, 9, -3, 0.7F)
            }
        };

        public static readonly Transformation Seraphim = new Transformation("seraphim", "Seraphim", 19492, -6, 6, 0.8F) {
            Items = new List<Item>() {
                new Item("bible", 33, 30, 69, 0.5F),
                new Item("rosary", 72, 23, 0, 0.5F),
                new Item("halo", 101, 33, 24, 0.4F),
                new Item("guardianangel", 112, -2, 20, 0.55F),
                new Item("mitre", 173, 57, 58, 0.55F),
                new Item("holygrail", 184, 72, 31, 0.55F),
                new Item("deaddove", 185, 32, 45, 0.6F),
                new Item("holymantle", 313, 0, 50, 0.65F),
                new Item("swornprotector", 363, 49, 2, 0.6F)
            }
        };

        public static readonly Transformation Bob = new Transformation("bob", "Bob", 19496, 4, -1, 0.95F) {
            Items = new List<Item>() {
                new Item("bobshead", 42, 4, 4, 0.75F),
                new Item("bobscurse", 140, 39, 46, 0.95F),
                new Item("ipecac", 149, 6, 45, 0.8F),
                new Item("bobsbrain", 273, 46, 1, 0.85F)
            }
        };

        public static readonly Transformation Spun = new Transformation("spun", "Spun", 19500, 4, 0, 0.9F) {
            Items = new List<Item>() {
                new Item("virus", 13, 76, 30, 0.8F),
                new Item("roidrage", 14, 48, -2, 0.8F),
                new Item("growthhormones", 70, 18, 1, 0.8F),
                new Item("speedball", 143, -2, 28, 0.8F),
                new Item("experimentaltreatment", 240, 18, 54, 0.8F),
                new Item("synthoil", 345, 46, 54, 0.8F)
            }
        };

        public static readonly Transformation Mom = new Transformation("mom", "Mom", 19504, 8, -1, 0.9F) {
            Items = new List<Item>() {
                new Item("momsunderwear", 29, 41, 59, 0.3F),
                new Item("momsheels", 30, 34, 76, 0.35F),
                new Item("momslipstick", 31, 9, 26, 0.45F),
                new Item("momsbra", 39, 19, 36, 0.45F),
                new Item("momspad", 41, 24, 60, 0.45F),
                new Item("momseye", 55, 27, 13, 0.45F),
                new Item("momsbottleofpills", 102, 73, 22, 0.4F),
                new Item("momscontacts", 110, 46, 15, 0.45F),
                new Item("momsknife", 114, -2, 38, 0.5F),
                new Item("momspurse", 139, 59, 66, 0.35F),
                new Item("momscoinpurse", 195, 70, 46, 0.4F),
                new Item("momskey", 199, 7, 55, 0.45F),
                new Item("momseyshadow", 200, 60, 2, 0.4F),
                new Item("momswig", 217, 32, -7, 0.5F),
                new Item("momsperfume", 228, 49, 37, 0.4F),
                new Item("momspearls", 355, 7, 7, 0.35F)
            }
        };

        public static readonly Transformation Conjoined = new Transformation("conjoined", "Conjoined", 19508, -5, 6, 0.85F) {
            Items = new List<Item>() {
                new Item("brotherbobby", 8, 2, 11, 0.7F),
                new Item("sistermaggy", 67, 61, 7, 0.7F),
                new Item("littlesteven", 100, 29, -5, 0.65F),
                new Item("harlequinbaby", 167, 63, 49, 0.65F),
                new Item("rottenbaby", 268, 34, 60, 0.7F),
                new Item("headlessbaby", 269, 30, 32, 0.45F),
                new Item("mongobaby", 322, 1, 46, 0.7F)
            }
        };

        public static readonly Transformation Leviathan = new Transformation("leviathan", "Leviathan", 19512, 2, 1, 0.7F) {
            Items = new List<Item>() {
                new Item("pentagram", 51, 25, 59, 0.7F),
                new Item("mark", 79, 63, 27, 0.65F),
                new Item("pact", 80, 2, 46, 0.65F),
                new Item("nail", 83, 60, 5, 0.65F),
                new Item("brimstone", 118, -1 , 13, 0.65F),
                new Item("spiritofthenight", 159, 56, 59, 0.6F),
                new Item("abaddon", 230, 29, 31, 0.65F),
                new Item("mawofthevoid", 399, 27, -3, 0.65F)
            }
        };

        public static readonly Transformation OhCrap = new Transformation("ohcrap", "Oh Crap", 19516, 4, -3, 0.85F) {
            Items = new List<Item>() {
                new Item("poop", 36, -2, 9),
                new Item("ecoli", 236, 40, 4),
                new Item("flush", 291, 15, 50, 0.95F)
            }
        };

        public static readonly Transformation SuperBum = new Transformation("superbum", "Super Bum", 0, 16, 1, 0.8F) {
            Items = new List<Item>() {
                new Item("bumfriend", 144, 48, 39, 0.9F),
                new Item("darkbum", 278, 26, -1, 0.95F),
                new Item("keybum", 388, 1, 40, 0.9F)
            }
        };

        public static Dictionary<string, Transformation> AllTransformations = new Dictionary<string, Transformation>() {
            {Guppy.Name, Guppy},
            {Beelzebub.Name, Beelzebub},
            {FunGuy.Name, FunGuy},
            {Seraphim.Name, Seraphim},
            {Bob.Name, Bob},
            {Spun.Name, Spun},
            {Mom.Name, Mom},
            {Conjoined.Name, Conjoined},
            {Leviathan.Name, Leviathan},
            {OhCrap.Name, OhCrap},
            {SuperBum.Name, SuperBum}
        };
    }
}
