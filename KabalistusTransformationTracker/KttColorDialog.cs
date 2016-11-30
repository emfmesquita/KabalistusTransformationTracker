using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KabalistusTransformationTracker {
    public sealed class KttColorDialog : ColorDialog {
        private List<ApiWindow> _editWindows;
        private readonly Stopwatch _changePreviewColorSw = new Stopwatch();

        public KttColorDialog() {
            FullOpen = true;
            _changePreviewColorSw.Start();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetAncestor(IntPtr hWnd, int gaFlags);

        public delegate void PreviewColorChanged(Color color);

        public PreviewColorChanged PreviewColorChangedListener { get; set; }

        protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam) {
            if (msg != 307) return base.HookProc(hWnd, msg, wparam, lparam);

            if (_editWindows == null) {
                var mainWindow = GetAncestor(hWnd, 2);
                if (!mainWindow.Equals(IntPtr.Zero)) {
                    _editWindows = new List<ApiWindow>((new WindowsEnumerator()).GetChildWindows(mainWindow, "Edit"));
                }
            } else if (_editWindows != null && _editWindows.Count == 6) {
                var strRed = WindowsEnumerator.WindowText(_editWindows[3].HWnd);
                var strGreen = WindowsEnumerator.WindowText(_editWindows[4].HWnd);
                var strBlue = WindowsEnumerator.WindowText(_editWindows[5].HWnd);

                int red;
                if (!int.TryParse(strRed, out red)) return base.HookProc(hWnd, msg, wparam, lparam);
                int green;
                if (!int.TryParse(strGreen, out green)) return base.HookProc(hWnd, msg, wparam, lparam);
                int blue;
                if (int.TryParse(strBlue, out blue)) {
                    PreviewColorChangedEvent(red, green, blue);
                }
            }
            return base.HookProc(hWnd, msg, wparam, lparam);
        }

        private void PreviewColorChangedEvent(int red, int green, int blue) {
            if (_changePreviewColorSw.ElapsedMilliseconds < 50 || PreviewColorChangedListener == null) {
                return;
            }
            _changePreviewColorSw.Restart();
            //_editWindows = null;
            PreviewColorChangedListener(Color.FromArgb(red, green, blue));
        }

        public class ApiWindow {
            public IntPtr HWnd { get; set; }
            public string ClassName { get; set; }
            public string MainWindowTitle { get; set; }
        }


        public class WindowsEnumerator {

            private delegate int EnumCallBackDelegate(IntPtr hwnd, int lParam);

            [DllImport("user32.dll")]
            private static extern int EnumWindows(EnumCallBackDelegate lpEnumFunc, int lParam);

            [DllImport("user32.dll")]
            private static extern int EnumChildWindows(IntPtr hWndParent, EnumCallBackDelegate lpEnumFunc, int lParam);

            [DllImport("user32.dll")]
            private static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

            [DllImport("user32.dll")]
            private static extern int IsWindowVisible(IntPtr hwnd);

            [DllImport("user32.dll")]
            private static extern int GetParent(IntPtr hwnd);

            [DllImport("user32.dll")]
            private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

            [DllImport("user32.dll")]
            private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, StringBuilder lParam);

            private readonly List<ApiWindow> _listChildren = new List<ApiWindow>();
            private readonly List<ApiWindow> _listTopLevel = new List<ApiWindow>();

            private string _topLevelClass = "";
            private string _childClass = "";

            public ApiWindow[] GetTopLevelWindows() {
                EnumCallBackDelegate enumWindowProc = delegate (IntPtr inthwnd, int lParam) {
                    if (GetParent(inthwnd) != 0 || IsWindowVisible(inthwnd) != 0) return 1;
                    var window = GetWindowIdentification(inthwnd);
                    if (_topLevelClass.Length == 0 || _topLevelClass.ToLower().Equals(window.ClassName.ToLower())) {
                        _listTopLevel.Add(window);
                    }
                    return 1;
                };

                EnumWindows(enumWindowProc, 0);
                return _listTopLevel.ToArray();
            }

            public ApiWindow[] GetTopLevelWindows(string className) {
                _topLevelClass = className;
                return GetTopLevelWindows();
            }

            public ApiWindow[] GetChildWindows(IntPtr hwnd) {
                _listChildren.Clear();

                EnumCallBackDelegate enumChildWindowProc = delegate (IntPtr inthwnd, int lParam) {
                    var window = GetWindowIdentification(inthwnd);
                    if (_childClass.Length == 0 || _childClass.ToLower().Equals(window.ClassName.ToLower())) {
                        _listChildren.Add(window);
                    }
                    return 1;
                };
                EnumChildWindows(hwnd, enumChildWindowProc, 0);
                return _listChildren.ToArray();
            }

            public ApiWindow[] GetChildWindows(IntPtr hwnd, string childClass) {
                _childClass = childClass;
                return GetChildWindows(hwnd);
            }

            private static ApiWindow GetWindowIdentification(IntPtr hwnd) {
                var classBuilder = new StringBuilder(64);
                GetClassName(hwnd, classBuilder, 64);

                var window = new ApiWindow {
                    ClassName = classBuilder.ToString(),
                    MainWindowTitle = WindowText(hwnd),
                    HWnd = hwnd
                };
                return window;
            }

            public static string WindowText(IntPtr hwnd) {
                var sb = new StringBuilder();
                var length = SendMessage(hwnd, 14, 0, 0);
                if (length <= 0) return sb.ToString();
                sb = new StringBuilder(length + 1);
                SendMessage(hwnd, 13, sb.Capacity, sb);
                return sb.ToString();
            }
        }
    }
}
