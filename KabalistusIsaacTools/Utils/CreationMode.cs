using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.TransformationTracker.Model;

namespace KabalistusIsaacTools.Utils {
    public class CreationMode {
        public static readonly bool On = false;
        public static readonly bool BlockModeOn = false;

        public static float ContrastBuff;
        public static float BrigtnessBuff;

        public static List<Transformation> Transformations = new List<Transformation>();
        public static GeneralImageModel CurrentImage;

        private static bool _showTransformation;

        public static void KeyPressed(Key key) {
            if (!On) {
                return;
            }

            if (key == Key.Space) {
                _showTransformation = !_showTransformation;
                Transformations.ForEach(transformation => transformation.ShowTransformationImage = _showTransformation);
                return;
            }
            if (key == Key.P) {
                Print();
            }

            if (CurrentImage == null) {
                return;
            }

            if (key == Key.W) {
                CurrentImage.Y--;
            } else if (key == Key.A) {
                CurrentImage.X--;
            } else if (key == Key.S) {
                CurrentImage.Y++;
            } else if (key == Key.D) {
                CurrentImage.X++;
            } else if (key == Key.R) {
                CurrentImage.Scale += 0.05F;
            } else if (key == Key.F) {
                CurrentImage.Scale -= 0.05F;
            }

            var transformationItem = CurrentImage as TransformationItem;
            if (transformationItem == null) {
                return;
            }

            if (key == Key.T) {
                transformationItem.BlockScale += 0.05F;
            } else if (key == Key.G) {
                transformationItem.BlockScale -= 0.05F;
            }
        }

        private static void Print() {
            Transformations.ForEach(model => {
                Console.WriteLine(model.TransformationImageModel.Tooltip);
                Console.WriteLine(model.TransformationImageModel);
                model.Items.ForEach(Console.WriteLine);
                Console.WriteLine("\n");
            });
        }
    }
}
