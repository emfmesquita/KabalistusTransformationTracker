using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace KabalistusTransformationTracker.Utils {
    public class MemoryReader {

        private const int ProcessWmRead = 0x0010;

        private const string IsaacProccessNotFound = "Isaac proccess not found. Still searching...";
        private const string LoadingAddresses = "Isaac proccess found. Loading memory addresses...";
        private const string Ready = "Ready.";

        private static int _isaacPid;
        private static ProcessModule _module;
        private static int _baseAddr;
        private static int _moduleMemSize;
        private static IntPtr _processHandle;

        private static int _playerManagerInstructPointer;
        private static int _playerManagerPlayerListOffset;
        private static bool? _isAfterbirth;

        public static MemoryQuery VersionQuery = new MemoryQuery() {
            SearchChar = new[] { 'B', 'i', 'n', 'd', 'i', 'n', 'g', ' ', 'o', 'f', ' ', 'I', 's', 'a', 'a', 'c', ':' },
            SearchPattern = "bbbbbbbbbbbbbbbbb"
        };

        public static MemoryQuery PlayerManagerInstructPointerQuery = new MemoryQuery() {
            SearchInt = new[] { 0x33, 0xC0, 0xC7, 0x45, 0xFC, 0xFF, 0xFF, 0xFF, 0xFF, 0xA3, 0x00, 0x00, 0x00, 0x00, 0xE8 },
            SearchPattern = "bbbbbbbbbbvvvvb"
        };

        public static MemoryQuery PlayerManagerPlayerListOffsetQuery = new MemoryQuery() {
            SearchInt = new[] { 0x8B, 0x35, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x86, 0x00, 0x00, 0x00, 0x00, 0x2B, 0x86, 0x00, 0x00, 0x00, 0x00 },
            SearchPattern = "bb????bb????bbvv??"
        };

        public static bool Init(MainForm mainForm) {
            var processArray = Process.GetProcessesByName("isaac-ng");
            if (processArray.Length == 0) {
                mainForm.SetStatus(new Status() {
                    Message = IsaacProccessNotFound,
                    Ready = false
                });

                _isaacPid = 0;
                _isAfterbirth = null;
                return false;
            }

            var process = processArray[0];
            var isaacPid = process.Id;

            if (isaacPid == _isaacPid) {
                return true;
            }

            _isaacPid = isaacPid;

            mainForm.SetStatus(new Status() {
                Message = LoadingAddresses,
                Ready = false
            });

            _module = process.MainModule;
            _baseAddr = _module.BaseAddress.ToInt32();
            _moduleMemSize = _module.ModuleMemorySize;
            _processHandle = OpenProcess(ProcessWmRead, false, isaacPid);

            var versionAddress = Search(VersionQuery, true, 800000).QueryResultAddress;
            var versionChar = ReadInt(versionAddress + 18, 1);
            _isAfterbirth = versionChar == 'A';

            var instructSearchOffset = _isAfterbirth == true ? 1500000 : 1100000;
            _playerManagerInstructPointer = Search(PlayerManagerInstructPointerQuery, false, instructSearchOffset).QueryResult;

            var playerListSearchOffset = _isAfterbirth == true ? 50000 : 120000;
            _playerManagerPlayerListOffset = Search(PlayerManagerPlayerListOffsetQuery, false, playerListSearchOffset).QueryResult;

            mainForm.SetStatus(new Status() {
                Message = Ready,
                Ready = true
            });

            return true;
        }

        public static int GetPlayerInfo(int offset) {
            if (!IsCurrentInfoValid()) {
                return 0;
            }

            var playerManagetInstruct = ReadInt(_playerManagerInstructPointer, 4);
            var playerListPointer = playerManagetInstruct + _playerManagerPlayerListOffset;

            var numberOfPlayers = ReadInt(playerListPointer + 4, 4) - ReadInt(playerListPointer, 4);
            if (numberOfPlayers == 0) {
                return 0;
            }

            var playerPointer = ReadInt(playerListPointer, 4);
            var player = ReadInt(playerPointer, 4);
            return ReadInt(player + offset, 4);
        }

        public static int GetPlayerManagerInfo(int offset, int size) {
            if (!IsCurrentInfoValid()) {
                return 0;
            }

            var playerManagetInstruct = ReadInt(_playerManagerInstructPointer, 4);
            var playerListPointer = playerManagetInstruct + _playerManagerPlayerListOffset;

            var numberOfPlayers = ReadInt(playerListPointer + 4, 4) - ReadInt(playerListPointer, 4);
            return numberOfPlayers == 0 ? 0 : ReadInt(playerManagetInstruct + offset, size);
        }

        public static int ReadInt(int addr, int size) {
            return ConvertLittleEndian(Read(addr, size));
        }

        public static bool? IsAfterbirth() {
            return _isAfterbirth;
        }

        private static bool IsCurrentInfoValid() {
            if (_isaacPid == 0) {
                return false;
            }

            var processArray = Process.GetProcessesByName("isaac-ng");
            if (processArray.Length == 0) {
                return false;
            }

            return _isaacPid == processArray[0].Id;
        }

        private static byte[] Read(int addr, int size) {
            var bytesRead = 0;
            var buffer = new byte[size];
            ReadProcessMemory((int)_processHandle, addr, buffer, buffer.Length, ref bytesRead);
            return buffer;
        }

        private static int ConvertLittleEndian(IEnumerable<byte> array) {
            var pos = 0;
            var result = 0;
            foreach (var by in array) {
                result |= ((int)by) << pos;
                pos += 8;
            }
            return result;
        }

        private static bool Match(string pattern, byte[] read, byte[] expected, List<byte> queryResult) {
            for (var i = 0; i < read.Length; i++) {
                if (pattern[i] != 'b') {
                    if (pattern[i] == 'v') {
                        queryResult.Add(read[i]);
                    }
                } else {
                    if (read[i] != expected[i]) {
                        return false;
                    }
                }
            }
            return true;
        }

        private static MemoryQuery Search(MemoryQuery query, bool fromEndToStart = false, int offsetAddress = 0) {
            query.QueryResult = -1;
            query.QueryResultAddress = -1;

            if (query?.Search == null || query.Search.Length == 0 || string.IsNullOrEmpty(query.SearchPattern)) {
                return query;
            }

            if (query.Search.Length != query.SearchPattern.Length) {
                return query;
            }

            var searchSize = query.Search.Length;
            var pattern = query.SearchPattern;
            for (
                var i = !fromEndToStart ? offsetAddress : _moduleMemSize - offsetAddress;
                (!fromEndToStart && i < _moduleMemSize - searchSize) || (fromEndToStart && i > searchSize);
                i = !fromEndToStart ? i + 1 : i - 1) {

                var addr = _baseAddr + i;
                var read = Read(addr, searchSize);
                var result = new List<byte>();

                if (!Match(pattern, read, query.Search, result)) continue;

                query.QueryResult = ConvertLittleEndian(result.ToArray());
                query.QueryResultAddress = addr;
                return query;
            }

            return query;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
    }
}
