using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KabalistusCommons.Model;
using KabalistusIsaacTools.Utils;

namespace KabalistusIsaacTools.Commons.View {
    public class GeneralImageModel : BaseModel {
        private string _resource;
        private BitmapImage _image;
        private string _tooltip;
        private int _height;
        private int _width;
        private float _scale = 1;
        private int _margin;
        private string _formattedMargin = "0,0,0,0";
        private Cursor _cursor = Cursors.Arrow;
        private Visibility _visibility = Visibility.Visible;
        private Transform _translate;

        private int _x;
        private int _y;

        public GeneralImageModel(string resource, string tooltip = null, int x = 0, int y = 0, int margin = 0, Visibility visibility = Visibility.Visible, Cursor cursor = null, int height = 0, int width = 0) {
            Resource = resource;
            BaseHeight = height == 0 ? Image.PixelHeight : height;
            Height = BaseHeight;
            BaseWidth = width == 0 ? Image.PixelWidth : width;
            Width = BaseWidth;

            Tooltip = tooltip;
            Margin = margin;
            Cursor = cursor ?? Cursors.Arrow;
            Visibility = visibility;

            X = x;
            Y = y;
        }

        public void ApplyBitmapTransform(Action<System.Drawing.Image> bitmapTransform) {
            var image = System.Drawing.Image.FromStream(Image.StreamSource);
            bitmapTransform(image);
            Image = ImageUtils.ToBitmapImage(new Bitmap(image));
        }

        public int BaseHeight { get; }
        public int BaseWidth { get; }

        public virtual float Scale {
            get {
                return _scale;
            }

            set {
                _scale = value;
                ApplyScaleToBaseSize(_scale);
            }
        }

        public BitmapImage Image {
            get {
                return _image;
            }
            set {
                if (value.Equals(_image)) return;
                _image = value;
                NotifyPropertyChanged();
            }
        }
        public string Resource {
            get {
                return _resource;
            }
            set {
                if (value == _resource) return;
                _resource = value;
                if (string.IsNullOrEmpty(_resource)) return;
                Image = ImageUtils.GetImage(_resource);
            }
        }

        public string Tooltip {
            get {
                return _tooltip;
            }

            set {
                if (value == _tooltip) return;
                _tooltip = value;
                NotifyPropertyChanged();
            }
        }

        public int Height {
            get {
                return _height;
            }

            set {
                if (value + _margin == _height) return;
                _height = value + _margin;
                NotifyPropertyChanged();
            }
        }

        public int Width {
            get {
                return _width;
            }

            set {
                if (value + _margin == _width) return;
                _width = value + _margin;
                NotifyPropertyChanged();
            }
        }

        public int Margin {
            get {
                return _margin;
            }

            set {
                if (value == _margin) return;
                _margin = value;
                FormattedMargin = string.Format("{0},{0},{0},{0}", _margin);
            }
        }

        public string FormattedMargin {
            get {
                return _formattedMargin;
            }

            set {
                if (value == _formattedMargin) return;
                _formattedMargin = value;
                NotifyPropertyChanged();
            }
        }

        public Cursor Cursor {
            get {
                return _cursor;
            }

            set {
                if (value == _cursor) return;
                _cursor = value;
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

        public Transform Translate {
            get {
                return _translate;
            }

            set {
                if (Equals(value, _translate)) return;
                _translate = value;
                NotifyPropertyChanged();
            }
        }

        public int X {
            get {
                return _x;
            }

            set {
                _x = value;
                Translate = new TranslateTransform(_x, _y);
            }
        }

        public int Y {
            get {
                return _y;
            }

            set {
                _y = value;
                Translate = new TranslateTransform(_x, _y);
            }
        }

        private void ApplyScaleToBaseSize(float scale) {
            Height = (int)(BaseHeight * scale);
            Width = (int)(BaseWidth * scale);
        }

        public override string ToString() {
            var scale = Scale.ToString("N", new CultureInfo("en-US"));
            return $"{Tooltip} - X: {X}   Y: {Y}   S: {scale}   ----   {X}, {Y}, {scale}F";
        }
    }
}
