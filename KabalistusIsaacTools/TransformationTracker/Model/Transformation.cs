using System.Collections.Generic;
using System.Windows;
using KabalistusCommons.Model;
using KabalistusIsaacTools.Commons.View;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.TransformationTracker.Model {
    public class Transformation : BaseModel {
        private string _transformationLabel;
        private Visibility _visibility = Visibility.Visible;
        private Visibility _ringVisibility = CreationMode.On ? Visibility.Visible : Visibility.Hidden;
        private string _count;
        private bool _showTransformationImage;
        private string _itemTooltip;

        public Transformation(string name, string i18N, int memoryOffset, int x = 0, int y = 0, float scale = 1.0F) {
            Name = name;
            MemoryOffset = memoryOffset;
            I18N = i18N;
            var transfotmationImageResource = $"KabalistusIsaacTools.Images.Transformations.{name}.png";
            TransformationImageModel = new GeneralImageModel(transfotmationImageResource, i18N, x, y) {
                Scale = scale,
                Visibility = Visibility.Hidden
            };
            TransformationLabel = $"{I18N}: {_count}";
        }

        public List<TransformationItem> Items { get; set; }

        public string Name { get; }
        public int MemoryOffset { get; }
        public GeneralImageModel TransformationImageModel { get; }
        public string I18N { get; }

        public bool ShowTransformationImage {
            get {
                return _showTransformationImage;
            }

            set {
                if (value == _showTransformationImage) return;
                _showTransformationImage = value;
                TransformationImageModel.Visibility = _showTransformationImage ? Visibility.Visible : Visibility.Hidden;
                Items.ForEach(item => {
                    item.Visibility = _showTransformationImage ? Visibility.Hidden : Visibility.Visible;
                });
            }
        }

        public string Count {
            get {
                return _count;
            }

            set {
                if (value == _count) return;
                _count = value;
                TransformationLabel = $"{I18N}: {_count}";
            }
        }

        public string TransformationLabel {
            get {
                return _transformationLabel;
            }

            set {
                if (value == _transformationLabel) return;
                _transformationLabel = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility Visibility {
            get {
                return _visibility;
            }

            set {
                if (value == _visibility) return;
                _visibility = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility RingVisibility {
            get {
                return _ringVisibility;
            }

            set {
                if (value == _ringVisibility) return;
                _ringVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public string ItemTooltip {
            get {
                return _itemTooltip;
            }

            set {
                if (value == _itemTooltip) return;
                _itemTooltip = value;
                NotifyPropertyChanged();
            }
        }
    }
}
