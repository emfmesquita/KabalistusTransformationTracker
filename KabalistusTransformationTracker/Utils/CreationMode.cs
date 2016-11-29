using System;
using KabalistusTransformationTracker.Images;

namespace KabalistusTransformationTracker.Utils {
    public class CreationMode {
        public static readonly bool On = true;
        public static readonly bool BlockModeOn = true;
        public static float ContrastBuff;
        public static float BrigtnessBuff;
        public static ItemCluster CurrentCluster;

        public static void KeyPressed(char keyChar) {
            if (CurrentCluster == null || !On) {
                return;
            }

            if (keyChar == 'r') {
                CurrentCluster.PreviousImage();
                return;
            }

            if (keyChar == 'f') {
                CurrentCluster.NextImage();
                return;
            }

            if (keyChar == ' ') {
                CurrentCluster.Transformed = !CurrentCluster.Transformed;
                CurrentCluster.BaseBox.Refresh();
                return;
            }

            if (keyChar == 'w') {
                CurrentCluster.CurrentImage.Y--;
            } else if (keyChar == 'a') {
                CurrentCluster.CurrentImage.X--;
            } else if (keyChar == 's') {
                CurrentCluster.CurrentImage.Y++;
            } else if (keyChar == 'd') {
                CurrentCluster.CurrentImage.X++;
            } else if (keyChar == 't') {
                CurrentCluster.CurrentImage.Scale += 0.05F;
            } else if (keyChar == 'g') {
                CurrentCluster.CurrentImage.Scale -= 0.05F;
            } else if (keyChar == 'y') {
                ChangeBlockReduction(-1);
            } else if (keyChar == 'h') {
                ChangeBlockReduction(1);
            } else if (keyChar == 'u') {
                ContrastBuff += 0.1F;
                BuffUpdated();
            } else if (keyChar == 'j') {
                ContrastBuff -= 0.1F;
                BuffUpdated();
            } else if (keyChar == 'i') {
                BrigtnessBuff += 0.1F;
                BuffUpdated();
            } else if (keyChar == 'k') {
                BrigtnessBuff -= 0.1F;
                BuffUpdated();
            }
            CurrentCluster.BaseBox.Refresh();
        }

        private static void ChangeBlockReduction(int value) {
            var itemImage = CurrentCluster.CurrentImage as ItemImage;
            if (itemImage == null) return;
            itemImage.BlockReduction += value;
            itemImage.CalcBlockStats();
        }

        private static void BuffUpdated() {
            Console.WriteLine("contrast: " + ContrastBuff);
            Console.WriteLine("brightness: " + BrigtnessBuff);
            CurrentCluster.UpdateImagesCreationMode();
        }
    }
}
