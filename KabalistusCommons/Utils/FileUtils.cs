using System;
using System.IO;

namespace KabalistusCommons.Utils {
    public class FileUtils {

        private static readonly string BaseDir = new DirectoryInfo(Environment.CurrentDirectory).FullName;

        public static string GetRelativePath(string fullPath) {
            var file = new FileInfo(fullPath);
            var fullFile = file.FullName;

            return !fullFile.StartsWith(BaseDir) ? fullPath : "~\\" + fullFile.Substring(BaseDir.Length + 1);
        }

        public static string GetFullPath(string path) {
            if (!path.StartsWith("~\\")) {
                return path;
            }

            var sufix = path.Substring(1);
            return BaseDir + sufix;
        }

        public static string GetFileName(string path) {
            var index = path.LastIndexOf("\\", StringComparison.Ordinal);
            return path.Substring(index + 1);
        }

        public static string GetDirectory(string path) {
            var fullPath = GetFullPath(path);
            var lastIndex = fullPath.LastIndexOf("\\", StringComparison.Ordinal);
            return fullPath.Substring(0, lastIndex);
        }

        public static bool Exists(string path) {
            return File.Exists(GetFullPath(path));
        }
    }
}
